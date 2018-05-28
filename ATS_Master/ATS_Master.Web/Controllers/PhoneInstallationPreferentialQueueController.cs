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
    public class PhoneInstallationPreferentialQueueController : Controller
    {
        private readonly AtsContext _context;

        public PhoneInstallationPreferentialQueueController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public PhoneInstallationPreferentialQueueIndexViewModel GenerateViewModel() => new PhoneInstallationPreferentialQueueIndexViewModel()
        {
            Table = new Configurator<PhoneInstallationPreferentialQueue, PhoneInstallationPreferentialQueueRow>()
                    .Configure()
                    .Url(Url.Action(actionName: "HandleTable")),

            AllAtsUsers = _context.AtsUsers
                .Where(user => user.PreferentialUser == true)
                .Select(user => new SelectListItem()
            {
                Text = user.Person.Name + " " + user.Person.Surname + " (#" + user.Id + ")",
                Value = user.Id.ToString()
            }).ToArray()
        };

        public ActionResult HandleTable()
        {
            var conf = new Configurator<PhoneInstallationPreferentialQueue, PhoneInstallationPreferentialQueueRow>()
                .Configure();

            var handler = conf.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditQueue);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.PhoneInstallationPreferentialQueues);
        }

        public TableAdjustment EditQueue(LatticeData<PhoneInstallationPreferentialQueue, PhoneInstallationPreferentialQueueRow> latticeData, PhoneInstallationPreferentialQueueRow queueRow)
        {
            PhoneInstallationPreferentialQueue currentQueue = null;
            if (queueRow.Id == 0)
            {
                currentQueue = new PhoneInstallationPreferentialQueue();
                _context.PhoneInstallationPreferentialQueues.Add(currentQueue);
            }
            else
            {
                currentQueue = _context.PhoneInstallationPreferentialQueues.FirstOrDefault(x => x.Id == queueRow.Id);
            }

            currentQueue.AtsUserId = queueRow.UserId;
            _context.SaveChanges();

            queueRow.Id = currentQueue.Id;

            queueRow.UserName = _context.AtsUsers
                .Where(user => user.Id == queueRow.UserId)
                .Select(user => user.Person.Name)
                .FirstOrDefault();

            queueRow.UserSurname = _context.AtsUsers
                .Where(user => user.Id == queueRow.UserId)
                .Select(user => user.Person.Surname)
                .FirstOrDefault();

            return latticeData.Adjust(x => x
                .Update(queueRow)
//                .Message(LatticeMessage.AtsUser("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<PhoneInstallationPreferentialQueue, PhoneInstallationPreferentialQueueRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subj = latticeData.CommandSubject();
            var queue = _context.PhoneInstallationPreferentialQueues.FirstOrDefault(x => x.Id == subj.Id);

            _context.PhoneInstallationPreferentialQueues.Remove(queue);
            _context.SaveChanges();

            return latticeData.Adjust(x => x
                .Remove(subj)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<PhoneInstallationPreferentialQueue, PhoneInstallationPreferentialQueueRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedQueuesIds = selectedRows.Select(x => x.Id);

            var ids = string.Join(",", selectedQueuesIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM PhoneInstallationPreferentialQueues WHERE Id IN ({ids})");

            return latticeData.Adjust(x => x
                .Remove(selectedRows)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}