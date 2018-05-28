using Reinforced.Lattice.Configuration;
using Reinforced.Lattice.Plugins.Limit;
using Reinforced.Lattice.Plugins.Paging;

namespace ATS_Master.Web.Models
{
    public static class DefaultTableConfigurator
    {
        public static void DefaultTable(this NongenericConfigurator configurator)
        {
            configurator.Partition(x => x.InitialSkipTake(take: 10).Client());

            configurator.Limit(ui => ui.Values(new string[] { "10", "20", "50", "-", "All" }), "lt");

            configurator.Paging(ui => ui.PagingWithPeriods().UseGotoPage(), "rb");

            configurator.PrettifyTitles();
            configurator.AppendEmptyFilters();

//            configurator.DatePicker(new DatepickerOptions(
//                createDatepicker: "ltcCreateDatePicker",
//                putToDatepicker: "ltcPutDateToDatepicker",
//                getFromDatePicker: "ltcGetDateFromDatepicker",
//                destroyDatepicker: "ltcDestroyDatepicker"));

//            configurator.DatePicker(new DatepickerOptions(
//                createDatepicker: "ltcCreateDateTimePicker",
//                putToDatepicker: "ltcPutDateToDateTimePicker",
//                getFromDatePicker: "ltcGetDateFromDateTimePicker",
//                destroyDatepicker: "ltcDestroyDateTimePicker"));

            configurator.DatePicker(new DatepickerOptions(
                createDatepicker: "ltcCreateDateTimePicker2",
                putToDatepicker: "ltcPutDateToDateTimePicker2",
                getFromDatePicker: "ltcGetDateFromDateTimePicker2",
                destroyDatepicker: "ltcDestroyDateTimePicker2"));
        }
    }
}