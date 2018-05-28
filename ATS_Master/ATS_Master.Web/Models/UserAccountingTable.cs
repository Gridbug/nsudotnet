using System.Linq;
using System.Web.Mvc;
using ATS_Master.Data;
using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class UserAccountingRow
    {
        public int Id { get; set; }

        public string AtsUser { get; set; }

        public int UserId { get; set; }

        public int Total { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }
    }

    public class UserAccountingIndexViewModel
    {
        public Configurator<UserAccounting, UserAccountingRow> Table { get; set; }

        public SelectListItem[] AllAtsUsers { get; set; }
    }

    public static class UserAccountingTable
    {
        public static Configurator<UserAccounting, UserAccountingRow> Configure(this Configurator<UserAccounting, UserAccountingRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(x => x.Id);

            configurator.OrderFallback(x => x.Id);

            //configurator.Column(x => x.Id).DataOnly();
            
            configurator.ProjectDataWith(userAccountings => userAccountings.Select(accounting => new UserAccountingRow()
            {
                Id = accounting.Id,
                Total = accounting.Total,
                UserName = accounting.AtsUser.Person.Name,
                UserSurname = accounting.AtsUser.Person.Surname,
                UserId = accounting.AtsUserId
            }));

            return configurator;
        }
    }
}