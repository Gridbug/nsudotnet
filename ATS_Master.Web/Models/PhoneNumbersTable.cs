using System.Linq;
using System.Web.Mvc;
using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class PhoneNumberRow
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public PhoneType PhoneType { get; set; }

        public string PhoneNumberAddressShort { get; set; }

        public int AddressId { get; set; }

        public bool IsFree { get; set; }
    }

    public class PhoneNumbersIndexViewModel
    {
        public Configurator<PhoneNumber, PhoneNumberRow> Table { get; set; }

        public SelectListItem[] AllAddresses { get; set; }

        public SelectListItem[] AllPhoneTypes { get; set; }
    }

    public static class PhoneNumbersTable
    {
        public static Configurator<PhoneNumber, PhoneNumberRow> Configure(this Configurator<PhoneNumber, PhoneNumberRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(row => row.Id);

            configurator.OrderFallback(number => number.Id);

            configurator.Column(row => row.Id).DataOnly();
            configurator.Column(row => row.AddressId).DataOnly();
            configurator.Column(row => row.PhoneNumberAddressShort).Title("Address");

            configurator.ProjectDataWith(phoneNumbers => phoneNumbers.Select(number => new PhoneNumberRow()
            {
                Id = number.Id,
                Number = number.Number,
                PhoneType = number.PhoneType,
                IsFree = number.IsFree,
                AddressId = number.AddressId,
                PhoneNumberAddressShort = number.Address.Street
            }));

            return configurator;
        }
    }
}