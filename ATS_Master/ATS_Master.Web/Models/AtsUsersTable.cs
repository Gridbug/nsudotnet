using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class AtsUserRow
    {
        public int Id { get; set; }

        public string Person { get; set; }

        public int PersonId { get; set; }

        public string PhoneNumber { get; set; }

        public int PhoneNumberId { get; set; }

        public bool IntercityAccess { get; set; }

        public bool PreferentialUser { get; set; }

        public int UserFee { get; set; }

        public int CityBalance { get; set; }

        public int IntercityBalance { get; set; }

        public bool PhoneInstalled { get; set; }
    }

    public class AtsUsersIndexViewModel
    {
        public Configurator<AtsUser, AtsUserRow> Table { get; set; }

        public SelectListItem[] AllPhoneNumbers { get; set; }

        public SelectListItem[] AllPersons { get; set; }
    }

    public static class AtsUsersTable
    {
        public static Configurator<AtsUser, AtsUserRow> Configure(this Configurator<AtsUser, AtsUserRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(row => row.Id);

            configurator.OrderFallback(number => number.Id);

            configurator.Column(row => row.Id).DataOnly();
            configurator.Column(row => row.PersonId).DataOnly();
            configurator.Column(row => row.PhoneNumberId).DataOnly();

            configurator.ProjectDataWith(atsUsers => atsUsers.Select(user => new AtsUserRow()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber.Number,
                PhoneNumberId = user.PhoneNumberId,
                Person = user.Person.Name + " " + user.Person.Surname,
                PersonId = user.PersonId,
                CityBalance = user.CityBalance,
                IntercityAccess = user.IntercityAccess,
                IntercityBalance = user.IntercityBalance,
                PhoneInstalled = user.PhoneInstalled,
                PreferentialUser = user.PreferentialUser,
                UserFee = user.UserFee
            }));

            return configurator;
        }
    }
}