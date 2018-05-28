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
    public class UserAccountingController : Controller
    {
        private readonly AtsContext _context;

        public UserAccountingController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public UserAccountingIndexViewModel GenerateViewModel() => new UserAccountingIndexViewModel()
        {
            Table = new Configurator<UserAccounting, UserAccountingRow>()
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
            var conf = new Configurator<UserAccounting, UserAccountingRow>()
                .Configure();

            var handler = conf.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditPerson);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.UserAccountings);
        }

        public TableAdjustment EditPerson(LatticeData<UserAccounting, UserAccountingRow> latticeData, UserAccountingRow userBillRow)
        {
           UserAccounting currentUserBill = null;
            if (userBillRow.Id == 0)
            {
                currentUserBill = new UserAccounting();
                _context.UserAccountings.Add(currentUserBill);
            }
            else
            {
                currentUserBill = _context.UserAccountings.FirstOrDefault(x => x.Id == userBillRow.Id);
            }

            currentUserBill.AtsUserId = userBillRow.UserId;
            currentUserBill.Total = userBillRow.Total;
            _context.SaveChanges();

            userBillRow.Id = currentUserBill.Id;

            userBillRow.UserName = _context.AtsUsers
                .Where(user => user.Id == userBillRow.UserId)
                .Select(user => user.Person.Name)
                .FirstOrDefault();

            userBillRow.UserSurname = _context.AtsUsers
                .Where(user => user.Id == userBillRow.UserId)
                .Select(user => user.Person.Surname)
                .FirstOrDefault();

            return latticeData.Adjust(x => x
                .Update(userBillRow)
//                .Message(LatticeMessage.AtsUser("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<UserAccounting, UserAccountingRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subj = latticeData.CommandSubject();
            var userBill = _context.UserAccountings.FirstOrDefault(x => x.Id == subj.Id);

            _context.UserAccountings.Remove(userBill);
            _context.SaveChanges();

            return latticeData.Adjust(x => x
                .Remove(subj)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<UserAccounting, UserAccountingRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedUserBillIds = selectedRows.Select(x => x.Id);

            var ids = string.Join(",", selectedUserBillIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM UserAccountings WHERE Id IN ({ids})");

            return latticeData.Adjust(x => x
                .Remove(selectedRows)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}