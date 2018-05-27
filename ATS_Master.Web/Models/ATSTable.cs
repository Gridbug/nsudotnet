using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class ATSRow
    {
        public int Id { get; set; }

        public ATSType ATSType { get; set; }

        public int CityATSAttributesId { get; set; }

        public int DepartmentalATSAttributesId { get; set; }

        public int InstitutionalATSAttributesId { get; set; }

    }

    public class ATSIndexViewModel
    {
        public Configurator<ATS, ATSRow> Table { get; set; }

        public SelectListItem[] AllATSTypes { get; set; }

        public SelectListItem[] AllCityATSAttributes { get; set; }

        public SelectListItem[] AllDepartmentalATSAttributes { get; set; }

        public SelectListItem[] AllInstitutionalATSAttributes { get; set; }
    }

    public static class ATSTable
    {
        public static Configurator<ATS, ATSRow> Configure(this Configurator<ATS, ATSRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(row => row.Id);

            configurator.OrderFallback(ats => ats.Id);

//            configurator.Column(row => row.Id).DataOnly();

            return configurator;
        }
    }
}