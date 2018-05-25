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
        public Configurator<DepartmentalATSAttributes, DepartmentalATSAttributesRow> Table { get; set; }
    }

    public static class DepartmentalATSAttributesTable
    {
        public static Configurator<DepartmentalATSAttributes, DepartmentalATSAttributesRow> Configure(this Configurator<DepartmentalATSAttributes, DepartmentalATSAttributesRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(x => x.Id);

            configurator.OrderFallback(x => x.Id);

            return configurator;
        }
    }
}