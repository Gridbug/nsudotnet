using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class AddressRow
    {
        public int Id { get; set; }

        public string Index { get; set; }

        public string Locality { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public int FlatNumber { get; set; }
    }

    public class AddressesIndexViewModel
    {
        public Configurator<Address, AddressRow> Table { get; set; }
    }

    public static class AddressesTable
    {
        public static Configurator<Address, AddressRow> Configure(this Configurator<Address, AddressRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(row => row.Id);

            configurator.OrderFallback(address => address.Id);

            configurator.Column(row => row.Id).DataOnly();

            return configurator;
        }
    }
}