using System.Web.Mvc;
using ATS_Master.Data;

namespace ATS_Master.Web.Controllers
{
    public class ATSUsersController : Controller
    {
        private readonly ATSContext _context;

        public ATSUsersController(ATSContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}