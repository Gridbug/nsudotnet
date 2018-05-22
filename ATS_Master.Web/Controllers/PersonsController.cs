using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ATS_Master.Data;
using ATS_Master.Data.Entities;
using ATS_Master.Web.Models;
using Reinforced.Lattice;
using Reinforced.Lattice.Adjustments;
using Reinforced.Lattice.Commands;
using Reinforced.Lattice.Configuration;
using Reinforced.Lattice.Editing;
using Reinforced.Lattice.Mvc;
using Reinforced.Lattice.Processing;

namespace ATS_Master.Web.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ATSContext _context;

        public PersonsController(ATSContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public PersonsIndexViewModel GenerateViewModel() => new PersonsIndexViewModel()
        {
            AllPersonGenders = EnumHelper.GetSelectList(typeof(PersonGender)).ToArray(),
            Table = new Configurator<Person, PersonRow>()
                    .Configure()
                    .Url(Url.Action(actionName: "HandleTable"))
        };

        public ActionResult HandleTable()
        {
            var conf = new Configurator<Person, PersonRow>()
                .Configure();

            var handler = conf.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditPerson);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);
//            handler.AddCommandHandler("UpdateRow", UpdateRow);

            return handler.Handle(_context.Persons);
        }

//        public TableAdjustment UpdateRow(LatticeData<Person, PersonRow> latticeData)
//        {
//            return latticeData.Adjust(x => x.Build());
//        }

        public TableAdjustment EditPerson(LatticeData<Person, PersonRow> latticeData, PersonRow personRow)
        {
            Person currentPerson = null;
            if (personRow.Id == 0)
            {
                currentPerson = new Person();
                _context.Persons.Add(currentPerson);
            }
            else
            {
                currentPerson = _context.Persons.FirstOrDefault(x => x.Id == personRow.Id);
            }

            currentPerson.Age = personRow.Age;
            currentPerson.Gender = personRow.Gender;
            currentPerson.Name = personRow.Name;
            currentPerson.Surname = personRow.Surname;
            currentPerson.Middlename = personRow.Middlename;
            _context.SaveChanges();

            personRow.Id = currentPerson.Id;

            return latticeData.Adjust(x => x
                    .Update(personRow)
//                .Message(LatticeMessage.User("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<Person, PersonRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subj = latticeData.CommandSubject();
            var person = _context.Persons.FirstOrDefault(x => x.Id == subj.Id);

            _context.Persons.Remove(person);
            _context.SaveChanges();

            return latticeData.Adjust(x => x
                .Remove(subj)
//                .Message(LatticeMessage.User("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<Person, PersonRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedPersonIds = selectedRows.Select(x => x.Id);

            var ids = string.Join(",", selectedPersonIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM People WHERE Id IN ({ids})");

            return latticeData.Adjust(x => x
                .Remove(selectedRows)
//                .Message(LatticeMessage.User("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}