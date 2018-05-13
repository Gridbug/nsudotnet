using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS_Master.Data;
using ATS_Master.Data.Entities;
using ATS_Master.Web.Models;
using Reinforced.Lattice.Configuration;
using Reinforced.Lattice.Mvc;

namespace ATS_Master.Web.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ATSContext _context;

        public PersonsController(ATSContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public PersonsIndexViewModel GenerateViewModel()
        {
            return new PersonsIndexViewModel()
            {
                Table = new Configurator<Person, PersonRow>()
                    .Configure()
                    .Url(Url.Action("HandleTable"))
            };
        }

        public ActionResult HandleTable()
        {
            var conf = new Configurator<Person, PersonRow>().Configure();

            var handler = conf.CreateMvcHandler(ControllerContext);

            return handler.Handle(_context.Persons);
        }
    }
}