using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class AtsRow
    {
        public int Id { get; set; }

        public AtsType AtsType { get; set; }

        public int CityAtsAttributesId { get; set; }

        public int DepartmentalAtsAttributesId { get; set; }

        public int InstitutionalAtsAttributesId { get; set; }

    }

    public class AtsIndexViewModel
    {
        public Configurator<Ats, AtsRow> Table { get; set; }

        public SelectListItem[] AllAtsTypes { get; set; }

        public SelectListItem[] AllCityAtsAttributes { get; set; }

        public SelectListItem[] AllDepartmentalAtsAttributes { get; set; }

        public SelectListItem[] AllInstitutionalAtsAttributes { get; set; }
    }

    public static class AtsTable
    {
        public static Configurator<Ats, AtsRow> Configure(this Configurator<Ats, AtsRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(row => row.Id);

            configurator.OrderFallback(ats => ats.Id);

//            configurator.Column(row => row.Id).DataOnly();

            return configurator;
        }
    }
}