using System.Linq;
using System.Text;
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
    public class PhoneNumbersController : Controller
    {
        private readonly AtsContext _context;

        public PhoneNumbersController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public PhoneNumbersIndexViewModel GenerateViewModel() => new PhoneNumbersIndexViewModel()
        {
            Table = new Configurator<PhoneNumber, PhoneNumberRow>()
                .Configure()
                .Url(Url.Action("HandleTable")),

            AllAddresses = _context.Addresses.Select(address => new SelectListItem()
            {
                Text = address.Street,
                Value = address.Id.ToString()
            }).ToArray(),

            AllAts = _context.AtsStations.Select(ats => new SelectListItem()
            {
                Text = ats.AtsType.ToString() + " ATS " + ats.Id.ToString(),
                Value = ats.Id.ToString()
            }).ToArray(),

            AllPhoneTypes = EnumHelper.GetSelectList(typeof(PhoneType)).ToArray()
        };

        public ActionResult HandleTable()
        {
            var configurator = new Configurator<PhoneNumber, PhoneNumberRow>()
                .Configure();

            var handler = configurator.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditPhoneNumber);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.PhoneNumbers);
        }

        public TableAdjustment EditPhoneNumber(LatticeData<PhoneNumber, PhoneNumberRow> latticeData, PhoneNumberRow phoneNumberRow)
        {
            PhoneNumber currentPhoneNumber = null;
            if (phoneNumberRow.Id == 0)
            {
                currentPhoneNumber = new PhoneNumber();
                _context.PhoneNumbers.Add(currentPhoneNumber);
            }
            else
            {
                currentPhoneNumber = _context.PhoneNumbers.FirstOrDefault(number => number.Id == phoneNumberRow.Id);
            }

            currentPhoneNumber.AddressId = phoneNumberRow.AddressId;
            currentPhoneNumber.AtsId = phoneNumberRow.AtsId;
            currentPhoneNumber.Number = phoneNumberRow.Number;
            currentPhoneNumber.PhoneType = phoneNumberRow.PhoneType;
            currentPhoneNumber.IsFree = phoneNumberRow.IsFree;
            _context.SaveChanges();

            phoneNumberRow.Id = currentPhoneNumber.Id;

            phoneNumberRow.PhoneNumberAddressShort = _context.Addresses
                .Where(address => address.Id == phoneNumberRow.AddressId)
                .Select(address => address.Street)
                .FirstOrDefault();

            phoneNumberRow.AtsNameAndId = _context.AtsStations
                .Where(ats => ats.Id == phoneNumberRow.AtsId)
                .Select(ats => ats.AtsType.ToString() + " ATS " + ats.Id.ToString())
                .FirstOrDefault();

            return latticeData.Adjust(wrapper => wrapper
                .Update(phoneNumberRow)
//                .Message(LatticeMessage.AtsUser("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<PhoneNumber, PhoneNumberRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subject = latticeData.CommandSubject();
            var phoneNumber = _context.PhoneNumbers.FirstOrDefault(number => number.Id == subject.Id);

            _context.PhoneNumbers.Remove(phoneNumber);
            _context.SaveChanges();

            return latticeData.Adjust(wrapper => wrapper
                .Remove(subject)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<PhoneNumber, PhoneNumberRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedPhoneNumbersIds = selectedRows.Select(row => row.Id);

            var ids = string.Join(",", selectedPhoneNumbersIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM PhoneNumbers WHERE Id IN ({ids})");

            return latticeData.Adjust(wrapper => wrapper
                .Remove(selectedRows)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}