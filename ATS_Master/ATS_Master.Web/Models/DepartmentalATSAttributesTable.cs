using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class DepartmentalATSAttributesRow
    {
        public int Id { get; set; }
    }

    public class DepartmentalATSAttributesIndexViewModel
    {
        public Configurator<DepartmentalAtsAttributes, DepartmentalATSAttributesRow> Table { get; set; }
    }

    public static class DepartmentalATSAttributesTable
    {
        public static Configurator<DepartmentalAtsAttributes, DepartmentalATSAttributesRow> Configure(this Configurator<DepartmentalAtsAttributes, DepartmentalATSAttributesRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(x => x.Id);

            configurator.OrderFallback(x => x.Id);

            return configurator;
        }
    }
}