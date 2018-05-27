using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
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
    public class ATSController : Controller
    {
        private readonly ATSContext _context;

        public ATSController(ATSContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(GenerateViewModel());
        }

        public ATSIndexViewModel GenerateViewModel() => new ATSIndexViewModel()
        {
            Table = new Configurator<ATS, ATSRow>()
                .Configure()
                .Url(Url.Action("HandleTable")),

            AllATSTypes = EnumHelper.GetSelectList(typeof(ATSType)).ToArray(),

            AllCityATSAttributes = _context.CityATSAttributes.Select(attributes => new SelectListItem()
            {
                Text = attributes.Id.ToString(),
                Value = attributes.Id.ToString()
            }).ToArray(),

            AllDepartmentalATSAttributes = _context.DepartmentalATSAttributes.Select(attributes => new SelectListItem()
            {
            Text = attributes.Id.ToString(),
            Value = attributes.Id.ToString()
            }).ToArray(),

            AllInstitutionalATSAttributes = _context.InstitutionalATSAttributes.Select(attributes => new SelectListItem()
            {
                Text = attributes.Id.ToString(),
                Value = attributes.Id.ToString()
            }).ToArray()
        };

        public ActionResult HandleTable()
        {
            var configurator = new Configurator<ATS, ATSRow>()
                .Configure();

            var handler = configurator.CreateMvcHandler(ControllerContext);

            handler.AddEditHandler(EditATS);
            handler.AddCommandHandler("Remove", Remove);
            handler.AddCommandHandler("RemoveSelected", RemoveSelected);

            return handler.Handle(_context.ATSStations);
        }

        public TableAdjustment EditATS(LatticeData<ATS, ATSRow> latticeData, ATSRow atsRow)
        {
            ATS currentATS = null;
            if (atsRow.Id == 0)
            {
                currentATS = new ATS();
                _context.ATSStations.Add(currentATS);
            }
            else
            {
                currentATS = _context.ATSStations.FirstOrDefault(number => number.Id == atsRow.Id);
            }

            currentATS.CityATSAttributesId = atsRow.CityATSAttributesId;
            currentATS.DepartmentalATSAttributesId = atsRow.DepartmentalATSAttributesId;
            currentATS.InstitutionalATSAttributesId = atsRow.InstitutionalATSAttributesId;
            _context.SaveChanges();

            atsRow.Id = currentATS.Id;

            return latticeData.Adjust(wrapper => wrapper
                .Update(atsRow)
//                .Message(LatticeMessage.User("success", "Editing", "Person saved!"))
            );
        }

        public TableAdjustment Remove(LatticeData<ATS, ATSRow> latticeData)
        {
            var confirmationData = latticeData.CommandConfirmation<RemovalConfirmationViewModel>();

            var subject = latticeData.CommandSubject();
            var ats = _context.ATSStations.FirstOrDefault(ats1 => ats1.Id == subject.Id);

            _context.ATSStations.Remove(ats);
            _context.SaveChanges();

            return latticeData.Adjust(wrapper => wrapper
                .Remove(subject)
//                .Message(LatticeMessage.User("success", "Remove", $"Order removed because of {confirmationData.Cause}"))
            );
        }

        public TableAdjustment RemoveSelected(LatticeData<ATS, ATSRow> latticeData)
        {
            var selectedRows = latticeData.Selection().ToArray();
            var selectedATSIds = selectedRows.Select(row => row.Id);

            var ids = string.Join(",", selectedATSIds);

            _context.Database.ExecuteSqlCommand($"DELETE FROM ATS WHERE Id IN ({ids})");

            return latticeData.Adjust(wrapper => wrapper
                .Remove(selectedRows)
//                .Message(LatticeMessage.User("success", "Remove", $"{selectedRows.Length} orders removed!"))
            );
        }
    }
}