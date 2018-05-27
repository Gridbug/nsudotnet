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
    public class ATSController : Controller
    {
        private readonly AtsContext _context;

        public ATSController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public AtsIndexViewModel GenerateViewModel() => new AtsIndexViewModel()
        {
            Table = new Configurator<Ats, AtsRow>()
                .Configure()
                .Url(Url.Action("HandleTable")),

            AllAtsTypes = EnumHelper.GetSelectList(typeof(AtsType)).ToArray(),

            AllCityAtsAttributes = _context.CityAtsAttributes.Select(attributes => new SelectListItem()
            {
                Text = attributes.Id.ToString(),
                Value = attributes.Id.ToString()
            }).ToArray(),

            AllDepartmentalAtsAttributes = _context.DepartmentalAtsAttributes.Select(attributes => new SelectListItem()
            {
            Text = attributes.Id.ToString(),
            Value = attributes.Id.ToString()
            }).ToArray(),

            AllInstitutionalAtsAttributes = _context.InstitutionalAtsAttributes.Select(attributes => new SelectListItem()
            {
                Text = attributes.Id.ToString(),
                Value = attributes.Id.ToString()
            }).ToArray()
        };

        public ActionResult HandleTable()
        {
            var configurator = new Configurator<Ats, AtsRow>()
                .Configure();

            var handler = configurator.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditAts);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.AtsStations);
        }

        public TableAdjustment EditAts(LatticeData<Ats, AtsRow> latticeData, AtsRow atsRow)
        {
            Ats currentATS = null;
            if (atsRow.Id == 0)
            {
                currentATS = new Ats();
                _context.AtsStations.Add(currentATS);
            }
            else
            {
                currentATS = _context.AtsStations.FirstOrDefault(ats => ats.Id == atsRow.Id);
            }

            currentATS.CityAtsAttributesId = atsRow.CityAtsAttributesId;
            currentATS.DepartmentalAtsAttributesId = atsRow.DepartmentalAtsAttributesId;
            currentATS.InstitutionalAtsAttributesId = atsRow.InstitutionalAtsAttributesId;
            currentATS.AtsType = atsRow.AtsType;

            _context.SaveChanges();

            atsRow.Id = currentATS.Id;

            return latticeData.Adjust(wrapper => wrapper
                .Update(atsRow)
//                .Message(LatticeMessage.User("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<Ats, AtsRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subject = latticeData.CommandSubject();
            var ats = _context.AtsStations.FirstOrDefault(ats1 => ats1.Id == subject.Id);

            _context.AtsStations.Remove(ats);
            _context.SaveChanges();

            return latticeData.Adjust(wrapper => wrapper
                .Remove(subject)
//                .Message(LatticeMessage.User("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<Ats, AtsRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedAtsIds = selectedRows.Select(row => row.Id);

            var ids = string.Join(",", selectedAtsIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM Ats WHERE Id IN ({ids})");

            return latticeData.Adjust(wrapper => wrapper
                .Remove(selectedRows)
//                .Message(LatticeMessage.User("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}