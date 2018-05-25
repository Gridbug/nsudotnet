using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class CityATSAttributesRow
    {
        public int Id { get; set; }
    }

    public class CityATSAttributesIndexViewModel
    {
        public Configurator<CityATSAttributes, CityATSAttributesRow> Table { get; set; }
    }

    public static class CityATSAttributesTable
    {
        public static Configurator<CityATSAttributes, CityATSAttributesRow> Configure(this Configurator<CityATSAttributes, CityATSAttributesRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(x => x.Id);

            configurator.OrderFallback(x => x.Id);

            return configurator;
        }
    }
}