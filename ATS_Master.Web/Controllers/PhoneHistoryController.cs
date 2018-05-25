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
    public class PhoneHistoryController : Controller
    {

        private readonly ATSContext _context;

        public PhoneHistoryController(ATSContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public PhoneHistoryIndexViewModel GenerateViewModel() => new PhoneHistoryIndexViewModel()
        {
            Table = new Configurator<PhoneHistory, PhoneHistoryRow>()
                .Configure()
                .Url(Url.Action("HandleTable")),

            AllPhoneNumbers = _context.PhoneNumbers.Select(number => new SelectListItem()
            {
                Text = number.Number,
                Value = number.Id.ToString()
            }).ToArray()
        };

        public ActionResult HandleTable()
        {
            var configurator = new Configurator<PhoneHistory, PhoneHistoryRow>()
                .Configure();

            var handler = configurator.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditPhoneCall);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.PhoneHistories);
        }

        public TableAdjustment EditPhoneCall(LatticeData<PhoneHistory, PhoneHistoryRow> latticeData, PhoneHistoryRow phoneCallRow)
        {
            PhoneHistory phoneCall = null;
            if (phoneCallRow.Id == 0)
            {
                phoneCall = new PhoneHistory();
                _context.PhoneHistories.Add(phoneCall);
            }
            else
            {
                phoneCall = _context.PhoneHistories.FirstOrDefault(history => history.Id == phoneCallRow.Id);
            }

            phoneCall.PhoneNumberId = phoneCallRow.PhoneNumberId;
            phoneCall.PhoneDate = phoneCallRow.PhoneDate;
            phoneCall.Duration = phoneCallRow.Duration;
            phoneCall.Caller = phoneCallRow.Caller;
            phoneCall.CallerCity = phoneCallRow.CallerCity;
            phoneCall.Callee = phoneCallRow.Callee;
            phoneCall.CalleeCity = phoneCallRow.CalleeCity;
            _context.SaveChanges();

            phoneCallRow.Id = phoneCall.Id;

            phoneCallRow.PhoneNumber = _context.PhoneNumbers
                .Where(number => number.Id == phoneCallRow.PhoneNumberId).Select(number => number.Number)
                .FirstOrDefault();

            return latticeData.Adjust(wrapper => wrapper
                .Update(phoneCallRow)
//                .Message(LatticeMessage.User("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<PhoneHistory, PhoneHistoryRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subject = latticeData.CommandSubject();
            var phoneCall = _context.PhoneHistories.FirstOrDefault(call => call.Id == subject.Id);

            _context.PhoneHistories.Remove(phoneCall);
            _context.SaveChanges();

            return latticeData.Adjust(wrapper => wrapper
                .Remove(subject)
//                .Message(LatticeMessage.User("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<PhoneHistory, PhoneHistoryRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedPhoneCallIds = selectedRows.Select(row => row.Id);

            var ids = string.Join(",", selectedPhoneCallIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM PhoneHistories WHERE Id IN ({ids})");

            return latticeData.Adjust(wrapper => wrapper
                .Remove(selectedRows)
//                .Message(LatticeMessage.User("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}