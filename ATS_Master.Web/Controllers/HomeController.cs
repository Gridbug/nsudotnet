using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS_Master.Data;

namespace ATS_Master.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AtsContext _context;

        public HomeController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}