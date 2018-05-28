using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class PhoneHistoryRow
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public int PhoneNumberId { get; set; }

        public int Duration { get; set; }

        public DateTime PhoneDate { get; set; }

        public string Caller { get; set; }

        public string Callee { get; set; }

        public string CallerCity { get; set; }

        public string CalleeCity { get; set; }
    }

    public class PhoneHistoryIndexViewModel
    {
        public Configurator<PhoneHistory, PhoneHistoryRow> Table { get; set; }

        public SelectListItem[] AllPhoneNumbers { get; set; }
    }

    public static class PhoneHistoryTable
    {
        public static Configurator<PhoneHistory, PhoneHistoryRow> Configure(this Configurator<PhoneHistory, PhoneHistoryRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(row => row.Id);

            configurator.OrderFallback(number => number.Id);

            configurator.Column(row => row.Id).DataOnly();
            configurator.Column(row => row.PhoneNumberId).DataOnly();
            configurator.Column(row => row.PhoneNumber).Title("AtsUser");

            configurator.ProjectDataWith(calls => calls.Select(phoneCall => new PhoneHistoryRow()
            {
                Id = phoneCall.Id,
                PhoneNumber = phoneCall.PhoneNumber.Number,
                PhoneDate = phoneCall.PhoneDate,
                PhoneNumberId = phoneCall.PhoneNumberId,
                Duration = phoneCall.Duration,
                Caller = phoneCall.Caller,
                Callee = phoneCall.Callee,
                CallerCity = phoneCall.CallerCity,
                CalleeCity = phoneCall.CalleeCity
            }));

            return configurator;
        }
    }
}