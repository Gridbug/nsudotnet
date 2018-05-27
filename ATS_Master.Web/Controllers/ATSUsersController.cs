using System.Web.Mvc;
using ATS_Master.Data;

namespace ATS_Master.Web.Controllers
{
    public class ATSUsersController : Controller
    {
        private readonly AtsContext _context;

        public ATSUsersController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}