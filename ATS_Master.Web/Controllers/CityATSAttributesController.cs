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
        private readonly AtsContext _context;

        public CityATSAttributesController(AtsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public CityATSAttributesIndexViewModel GenerateViewModel() => new CityATSAttributesIndexViewModel()
        {
            Table = new Configurator<CityAtsAttributes, CityATSAttributesRow>()
                    .Configure()
                    .Url(Url.Action(actionName: "HandleTable"))
        };

        public ActionResult HandleTable()
        {
            var conf = new Configurator<CityAtsAttributes, CityATSAttributesRow>()
                .Configure();

            var handler = conf.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditCityATSAttribute);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.CityAtsAttributes);
        }

        public TableAdjustment EditCityATSAttribute(LatticeData<CityAtsAttributes, CityATSAttributesRow> latticeData, CityATSAttributesRow cityATSAttributesRow)
        {
            CityAtsAttributes currentAttribute = null;
            if (cityATSAttributesRow.Id == 0)
            {
                currentAttribute = new CityAtsAttributes();
                _context.CityAtsAttributes.Add(currentAttribute);
            }
            else
            {
                currentAttribute = _context.CityAtsAttributes.FirstOrDefault(x => x.Id == cityATSAttributesRow.Id);
            }

            _context.SaveChanges();

            cityATSAttributesRow.Id = currentAttribute.Id;

            return latticeData.Adjust(x => x
                .Update(cityATSAttributesRow)
//                .Message(LatticeMessage.User("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<CityAtsAttributes, CityATSAttributesRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subj = latticeData.CommandSubject();
            var cityATSAttributes = _context.CityAtsAttributes.FirstOrDefault(x => x.Id == subj.Id);

            _context.CityAtsAttributes.Remove(cityATSAttributes);
            _context.SaveChanges();

            return latticeData.Adjust(x => x
                .Remove(subj)
//                .Message(LatticeMessage.User("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<CityAtsAttributes, CityATSAttributesRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedAttributesIds = selectedRows.Select(x => x.Id);

            var ids = string.Join(",", selectedAttributesIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM CityAtsAttributes WHERE Id IN ({ids})");

            return latticeData.Adjust(x => x
                .Remove(selectedRows)
//                .Message(LatticeMessage.User("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}