using System.Linq;
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
    public class AtsUsersController : Controller
    {
        private readonly AtsContext _context;

        public AtsUsersController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public AtsUsersIndexViewModel GenerateViewModel() => new AtsUsersIndexViewModel()
        {
            Table = new Configurator<AtsUser, AtsUserRow>()
                .Configure()
                .Url(Url.Action("HandleTable")),

            AllPersons = _context.Persons.Select(person => new SelectListItem()
            {
                Text = person.Name + person.Surname,
                Value = person.Id.ToString()
            }).ToArray(),

            AllPhoneNumbers = _context.PhoneNumbers.Select(number => new SelectListItem()
            {
                Text = number.Number,
                Value = number.Id.ToString()
            }).ToArray()
        };

        public ActionResult HandleTable()
        {
            var configurator = new Configurator<AtsUser, AtsUserRow>()
                .Configure();

            var handler = configurator.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditAtsUser);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.AtsUsers);
        }

        public TableAdjustment EditAtsUser(LatticeData<AtsUser, AtsUserRow> latticeData, AtsUserRow atsUserRow)
        {
            AtsUser currentAtsUser = null;
            if (atsUserRow.Id == 0)
            {
                currentAtsUser = new AtsUser();
                _context.AtsUsers.Add(currentAtsUser);
            }
            else
            {
                currentAtsUser = _context.AtsUsers.FirstOrDefault(user => user.Id == atsUserRow.Id);
            }

            currentAtsUser.PhoneNumberId = atsUserRow.PhoneNumberId;
            currentAtsUser.PersonId = atsUserRow.PersonId;

            currentAtsUser.CityBalance = atsUserRow.CityBalance;
            currentAtsUser.IntercityBalance = atsUserRow.IntercityBalance;
            currentAtsUser.UserFee = atsUserRow.UserFee;

            currentAtsUser.IntercityAccess = atsUserRow.IntercityAccess;
            currentAtsUser.PreferentialUser = atsUserRow.PreferentialUser;
            currentAtsUser.PhoneInstalled = atsUserRow.PhoneInstalled;
            _context.SaveChanges();

            atsUserRow.Id = currentAtsUser.Id;

            atsUserRow.PhoneNumber = _context.PhoneNumbers
                .Where(number => number.Id == atsUserRow.PhoneNumberId)
                .Select(number => number.Number)
                .FirstOrDefault();

            atsUserRow.Person = _context.Persons
                .Where(person => person.Id == atsUserRow.PersonId)
                .Select(person => person.Name + person.Surname)
                .FirstOrDefault();

            return latticeData.Adjust(wrapper => wrapper
                .Update(atsUserRow)
//                .Message(LatticeMessage.AtsUser("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<AtsUser, AtsUserRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subject = latticeData.CommandSubject();
            var atsUser = _context.AtsUsers.FirstOrDefault(user => user.Id == subject.Id);

            _context.AtsUsers.Remove(atsUser);
            _context.SaveChanges();

            return latticeData.Adjust(wrapper => wrapper
                .Remove(subject)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<AtsUser, AtsUserRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedAtsUserIds = selectedRows.Select(row => row.Id);

            var ids = string.Join(",", selectedAtsUserIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM AtsUsers WHERE Id IN ({ids})");

            return latticeData.Adjust(wrapper => wrapper
                .Remove(selectedRows)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}