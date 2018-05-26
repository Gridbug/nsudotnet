using System.Linq;
using System.Web.Mvc;
using ATS_Master.Data;
using ATS_Master.Data.Entities;
using ATS_Master.Web.Models;
using Reinforced.Lattice;
using Reinforced.Lattice.Adjustments;
using Reinforced.Lattice.Commands;
using Reinforced.Lattice.Configuration;
using Reinforced.Lattice.Editing;
using Reinforced.Lattice.Mvc;
using Reinforced.Lattice.Processing;

namespace ATS_Master.Web.Controllers
{
    public class CityATSAttributesController : Controller
    {
        private readonly ATSContext _context;

        public CityATSAttributesController(ATSContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public CityATSAttributesIndexViewModel GenerateViewModel() => new CityATSAttributesIndexViewModel()
        {
            Table = new Configurator<CityATSAttributes, CityATSAttributesRow>()
                    .Configure()
                    .Url(Url.Action(actionName: "HandleTable"))
        };

        public ActionResult HandleTable()
        {
            var conf = new Configurator<CityATSAttributes, CityATSAttributesRow>()
                .Configure();

            var handler = conf.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditCityATSAttribute);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.CityATSAttributes);
        }

        public TableAdjustment EditCityATSAttribute(LatticeData<CityATSAttributes, CityATSAttributesRow> latticeData, CityATSAttributesRow cityATSAttributesRow)
        {
            CityATSAttributes currentAttribute = null;
            if (cityATSAttributesRow.Id == 0)
            {
                currentAttribute = new CityATSAttributes();
                _context.CityATSAttributes.Add(currentAttribute);
            }
            else
            {
                currentAttribute = _context.CityATSAttributes.FirstOrDefault(x => x.Id == cityATSAttributesRow.Id);
            }

            _context.SaveChanges();

            cityATSAttributesRow.Id = currentAttribute.Id;

            return latticeData.Adjust(x => x
                .Update(cityATSAttributesRow)
//                .Message(LatticeMessage.User("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<CityATSAttributes, CityATSAttributesRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subj = latticeData.CommandSubject();
            var cityATSAttributes = _context.CityATSAttributes.FirstOrDefault(x => x.Id == subj.Id);

            _context.CityATSAttributes.Remove(cityATSAttributes);
            _context.SaveChanges();

            return latticeData.Adjust(x => x
                .Remove(subj)
//                .Message(LatticeMessage.User("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<CityATSAttributes, CityATSAttributesRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedAttributesIds = selectedRows.Select(x => x.Id);

            var ids = string.Join(",", selectedAttributesIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM CityATSAttributes WHERE Id IN ({ids})");

            return latticeData.Adjust(x => x
                .Remove(selectedRows)
//                .Message(LatticeMessage.User("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}