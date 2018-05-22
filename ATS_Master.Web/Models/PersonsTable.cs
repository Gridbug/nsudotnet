using System.Web.Mvc;
using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;
using Reinforced.Lattice.Plugins.Limit;
using Reinforced.Lattice.Plugins.Paging;

namespace ATS_Master.Web.Models
{
    public class PersonRow
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Middlename { get; set; }

        public PersonGender Gender { get; set; }

        public int Age { get; set; }
    }

    public class PersonsIndexViewModel
    {
        public Configurator<Person, PersonRow> Table { get; set; }

        public SelectListItem[] AllPersonGenders { get; set; }
    }

    public static class PersonsTable
    {
        public static Configurator<Person, PersonRow> Configure(this Configurator<Person, PersonRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(x => x.Id);

            configurator.OrderFallback(x => x.Id);

            //configurator.Column(x => x.Id).DataOnly();

            //configurator.Column(x => x.Gender).Title("Gender");

            return configurator;
        }

        public static void DefaultTable(this NongenericConfigurator configurator)
        {
            configurator.Partition(x => x.InitialSkipTake(take: 10).Client());

            configurator.Limit(ui => ui.Values(new string[] { "10", "20", "50", "-", "All" }), "lt");

            configurator.Paging(ui => ui.PagingWithPeriods().UseGotoPage(), "rb");

            configurator.PrettifyTitles();
            configurator.AppendEmptyFilters();
        }
    }
}