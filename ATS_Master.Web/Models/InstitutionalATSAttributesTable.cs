using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class InstitutionalATSAttributesRow
    {
        public int Id { get; set; }
    }

    public class InstitutionalATSAttributesIndexViewModel
    {
        public Configurator<InstitutionalATSAttributes, InstitutionalATSAttributesRow> Table { get; set; }
    }

    public static class InstitutionalATSAttributesTable
    {
        public static Configurator<InstitutionalATSAttributes, InstitutionalATSAttributesRow> Configure(this Configurator<InstitutionalATSAttributes, InstitutionalATSAttributesRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(x => x.Id);

            configurator.OrderFallback(x => x.Id);

            return configurator;
        }
    }
}