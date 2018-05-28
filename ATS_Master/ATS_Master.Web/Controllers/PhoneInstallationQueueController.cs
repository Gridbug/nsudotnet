using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    public class PhoneInstallationQueueController : Controller
    {
        private readonly AtsContext _context;

        public PhoneInstallationQueueController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public PhoneInstallationQueueIndexViewModel GenerateViewModel() => new PhoneInstallationQueueIndexViewModel()
        {
            Table = new Configurator<PhoneInstallationQueue, PhoneInstallationQueueRow>()
                    .Configure()
                    .Url(Url.Action(actionName: "HandleTable")),

            AllAtsUsers = _context.AtsUsers.Select(user => new SelectListItem()
            {
                Text = user.Person.Name + " " + user.Person.Surname + " (#" + user.Id + ")",
                Value = user.Id.ToString()
            }).ToArray()
        };

        public ActionResult HandleTable()
        {
            var conf = new Configurator<PhoneInstallationQueue, PhoneInstallationQueueRow>()
                .Configure();

            var handler = conf.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditQueue);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.PhoneInstallationQueues);
        }

        public TableAdjustment EditQueue(LatticeData<PhoneInstallationQueue, PhoneInstallationQueueRow> latticeData, PhoneInstallationQueueRow queueRow)
        {
            PhoneInstallationQueue currentQueue = null;
            if (queueRow.Id == 0)
            {
                currentQueue = new PhoneInstallationQueue();
                _context.PhoneInstallationQueues.Add(currentQueue);
            }
            else
            {
                currentQueue = _context.PhoneInstallationQueues.FirstOrDefault(x => x.Id == queueRow.Id);
            }

            currentQueue.AtsUserId = queueRow.UserId;
            _context.SaveChanges();

            queueRow.Id = currentQueue.Id;

            queueRow.AtsUser = _context.AtsUsers
                .Join(_context.Persons, user => user.PersonId, person => person.Id, (user, person) => new
                {
                    Id = user.Id,
                    Name = person.Name,
                    Surname = person.Surname
                })
                .Where(user => user.Id == queueRow.UserId)
                .Select(user => user.Name + " " + user.Surname + " (#" + user.Id + ")")
                .FirstOrDefault();

            return latticeData.Adjust(x => x
                .Update(queueRow)
//                .Message(LatticeMessage.AtsUser("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<PhoneInstallationQueue, PhoneInstallationQueueRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subj = latticeData.CommandSubject();
            var queue = _context.PhoneInstallationQueues.FirstOrDefault(x => x.Id == subj.Id);

            _context.PhoneInstallationQueues.Remove(queue);
            _context.SaveChanges();

            return latticeData.Adjust(x => x
                .Remove(subj)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<PhoneInstallationQueue, PhoneInstallationQueueRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedQueuesIds = selectedRows.Select(x => x.Id);

            var ids = string.Join(",", selectedQueuesIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM PhoneInstallationQueues WHERE Id IN ({ids})");

            return latticeData.Adjust(x => x
                .Remove(selectedRows)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}