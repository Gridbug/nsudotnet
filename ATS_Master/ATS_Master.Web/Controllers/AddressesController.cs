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
    public class AddressesController : Controller
    {
        private readonly AtsContext _context;

        public AddressesController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public AddressesIndexViewModel GenerateViewModel() => new AddressesIndexViewModel()
        {
            Table = new Configurator<Address, AddressRow>()
                .Configure()
                .Url(Url.Action("HandleTable"))
        };

        public ActionResult HandleTable()
        {
            var configurator = new Configurator<Address, AddressRow>()
                .Configure();

            var handler = configurator.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditAddress);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.Addresses);
        }

        public TableAdjustment EditAddress(LatticeData<Address, AddressRow> latticeData, AddressRow addressRow)
        {
            Address currentAddress = null;
            if (addressRow.Id == 0)
            {
                currentAddress = new Address();
                _context.Addresses.Add(currentAddress);
            }
            else
            {
                currentAddress = _context.Addresses.FirstOrDefault(address => address.Id == addressRow.Id);
            }

            currentAddress.FlatNumber = addressRow.FlatNumber;
            currentAddress.HouseNumber = addressRow.HouseNumber;
            currentAddress.Index = addressRow.Index;
            currentAddress.Locality = addressRow.Locality;
            currentAddress.Street = addressRow.Street;
            _context.SaveChanges();

            addressRow.Id = currentAddress.Id;

            return latticeData.Adjust(wrapper => wrapper
                .Update(addressRow)
//                .Message(LatticeMessage.AtsUser("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<Address, AddressRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subject = latticeData.CommandSubject();
            var address = _context.Addresses.FirstOrDefault(address1 => address1.Id == subject.Id);

            _context.Addresses.Remove(address);
            _context.SaveChanges();

            return latticeData.Adjust(wrapper => wrapper
                .Remove(subject)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<Address, AddressRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedAddressIds = selectedRows.Select(row => row.Id);

            var ids = string.Join(",", selectedAddressIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM Addresses WHERE Id IN ({ids})");
            
            return latticeData.Adjust(wrapper => wrapper
                .Remove(selectedRows)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}