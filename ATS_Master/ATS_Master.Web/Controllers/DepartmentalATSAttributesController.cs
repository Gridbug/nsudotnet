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
    public class DepartmentalATSAttributesController : Controller
    {
        private readonly AtsContext _context;

        public DepartmentalATSAttributesController(AtsContext context)
        {
            _context = context;
        }

        // GET: DepartmentalAtsAttributes
        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public DepartmentalATSAttributesIndexViewModel GenerateViewModel() => new DepartmentalATSAttributesIndexViewModel()
        {
            Table = new Configurator<DepartmentalAtsAttributes, DepartmentalATSAttributesRow>()
                    .Configure()
                    .Url(Url.Action(actionName: "HandleTable"))
        };

        public ActionResult HandleTable()
        {
            var conf = new Configurator<DepartmentalAtsAttributes, DepartmentalATSAttributesRow>()
                .Configure();

            var handler = conf.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditDepartmentalATSAttributes);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.DepartmentalAtsAttributes);
        }

        public TableAdjustment EditDepartmentalATSAttributes(LatticeData<DepartmentalAtsAttributes, DepartmentalATSAttributesRow> latticeData, DepartmentalATSAttributesRow departmentalATSAttributesRow)
        {
            DepartmentalAtsAttributes currentAttribute = null;
            if (departmentalATSAttributesRow.Id == 0)
            {
                currentAttribute = new DepartmentalAtsAttributes();
                _context.DepartmentalAtsAttributes.Add(currentAttribute);
            }
            else
            {
                currentAttribute = _context.DepartmentalAtsAttributes.FirstOrDefault(x => x.Id == departmentalATSAttributesRow.Id);
            }

            _context.SaveChanges();

            departmentalATSAttributesRow.Id = currentAttribute.Id;

            return latticeData.Adjust(x => x
                .Update(departmentalATSAttributesRow)
//                .Message(LatticeMessage.AtsUser("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<DepartmentalAtsAttributes, DepartmentalATSAttributesRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subj = latticeData.CommandSubject();
            var departmentalATSAttributes = _context.DepartmentalAtsAttributes.FirstOrDefault(x => x.Id == subj.Id);

            _context.DepartmentalAtsAttributes.Remove(departmentalATSAttributes);
            _context.SaveChanges();

            return latticeData.Adjust(x => x
                .Remove(subj)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<DepartmentalAtsAttributes, DepartmentalATSAttributesRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedAttributesIds = selectedRows.Select(x => x.Id);

            var ids = string.Join(",", selectedAttributesIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM DepartmentalAtsAttributes WHERE Id IN ({ids})");

            return latticeData.Adjust(x => x
                .Remove(selectedRows)
//                .Message(LatticeMessage.AtsUser("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}