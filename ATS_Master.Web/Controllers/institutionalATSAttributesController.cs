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
    public class InstitutionalATSAttributesController : Controller
    {
        private readonly ATSContext _context;

        public InstitutionalATSAttributesController(ATSContext context)
        {
            _context = context;
        }

        // GET: InstitutionalATSAttributes
        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public InstitutionalATSAttributesIndexViewModel GenerateViewModel() => new InstitutionalATSAttributesIndexViewModel()
        {
            Table = new Configurator<InstitutionalATSAttributes, InstitutionalATSAttributesRow>()
                    .Configure()
                    .Url(Url.Action(actionName: "HandleTable"))
        };

        public ActionResult HandleTable()
        {
            var conf = new Configurator<InstitutionalATSAttributes, InstitutionalATSAttributesRow>()
                .Configure();

            var handler = conf.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditInstitutionalATSAttributes);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.InstitutionalATSAttributes);
        }

        public TableAdjustment EditInstitutionalATSAttributes(LatticeData<InstitutionalATSAttributes, InstitutionalATSAttributesRow> latticeData, InstitutionalATSAttributesRow institutionalATSAttributesRow)
        {
            InstitutionalATSAttributes currentAttribute = null;
            if (institutionalATSAttributesRow.Id == 0)
            {
                currentAttribute = new InstitutionalATSAttributes();
                _context.InstitutionalATSAttributes.Add(currentAttribute);
            }
            else
            {
                currentAttribute = _context.InstitutionalATSAttributes.FirstOrDefault(x => x.Id == institutionalATSAttributesRow.Id);
            }

            _context.SaveChanges();

            institutionalATSAttributesRow.Id = currentAttribute.Id;

            return latticeData.Adjust(x => x
                .Update(institutionalATSAttributesRow)
//                .Message(LatticeMessage.User("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<InstitutionalATSAttributes, InstitutionalATSAttributesRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subj = latticeData.CommandSubject();
            var institutionalATSAttributes = _context.InstitutionalATSAttributes.FirstOrDefault(x => x.Id == subj.Id);

            _context.InstitutionalATSAttributes.Remove(institutionalATSAttributes);
            _context.SaveChanges();

            return latticeData.Adjust(x => x
                .Remove(subj)
//                .Message(LatticeMessage.User("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<InstitutionalATSAttributes, InstitutionalATSAttributesRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedAttributesIds = selectedRows.Select(x => x.Id);

            var ids = string.Join(",", selectedAttributesIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM InstitutionalATSAttributes WHERE Id IN ({ids})");

            return latticeData.Adjust(x => x
                .Remove(selectedRows)
//                .Message(LatticeMessage.User("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}