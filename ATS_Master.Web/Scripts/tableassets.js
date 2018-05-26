var ltcShowModal = function () {
    $('#ltcModalHolder').modal('show');
}

var ltcHideModal = function () {
    $('#ltcModalHolder').modal('hide');
}



//var ltcCreateDatePicker = function (element) {
//    var date = null;
//    $(element).css('cursor', 'pointer');
//    $(element).css('text-align', 'center');
//    if ($(element).val()) date = new Date($(element).val());
//    $(element)
//        .datepicker({ format: 'dd MM yyyy', weekStart: 1 });
//    if (date) $(element).datepicker('setDate', date);
//    $(element)
//        .on('changeDate', function () {
//            Reinforced.Lattice.Master.fireDomEvent('keyup', element);
//        });
//}
//
//var ltcPutDateToDatepicker = function (element, date) {
//    $(element).datepicker('setDate', date);
//}
//
//var ltcGetDateFromDatepicker = function (element, date) {
//    var dt = $(element).datepicker('getDate');
//    if (dt == null || dt == undefined) return null;
//    if (!dt.getTime) return null;
//    dt.setHours(dt.getHours() - dt.getTimezoneOffset() / 60);
//    return dt;
//}
//
//var ltcDestroyDatepicker = function (element) {
//    $(element).datepicker('remove');
//}




//var ltcCreateDateTimePicker = function (element) {
//    var date = null;
//    $(element).css('cursor', 'pointer');
//    $(element).css('text-align', 'center');
//
//    if ($(element).val()) {
//        date = new Date($(element).val());
//    }
//
//    $(element).datetimepicker({
//        format: 'dd MM yyyy hh:mm:ss',
//        languange: 'en'
//    });
//    var picker = $(element).data('datetimepicker');
//
//    if (date) picker.setDate(date);
//
//    $(element)
//        .on('changeDate', function () {
//            Reinforced.Lattice.Master.fireDomEvent('keyup', element);
//        });
//}
//
//var ltcPutDateToDateTimePicker = function (element, date) {
//    $(element).data('datetimepicker').setDate(date);
//}
//
//var ltcGetDateFromDateTimePicker = function (element, date) {
//    var dt = $(element).data('datetimepicker').getDate();
//    if (dt == null || dt == undefined) return null;
//    if (!dt.getTime) return null;
//    dt.setHours(dt.getHours() - dt.getTimezoneOffset() / 60);
//    return dt;
//}
//
//var ltcDestroyDateTimePicker = function (element) {
//    $(element).data('datetimepicker').remove();
//}

var ltcCreateDateTimePicker2 = function (element) {
    var date = null;
    $(element).css('cursor', 'pointer');
    $(element).css('text-align', 'center');

    if ($(element).val()) {
        date = new Date($(element).val());
    }

//    if ($(element).parents("#editform").length > 0) {
        $(element)
            .datetimepicker({
                format: "HH:mm DD.MM.YY",
                showClear: true,
                showTodayButton: true,
                

            });
//    } else {
//        $(element)
//            .datetimepicker({
//                format: "DD.MM.YY",
//                showClear: true,
//                showTodayButton: true
//            });
//    }

    if (date != undefined)
        ltcPutDateToDatepicker(element, date);

    $(element)
        .on('dp.change',
            function () {
                Reinforced.Lattice.Master.fireDomEvent('keyup', element);
            });
}

var ltcPutDateToDateTimePicker2 = function (element, date) {
    $(element).data("DateTimePicker").date(date);
}

var ltcGetDateFromDateTimePicker2 = function (element, date) {
    var dt = $(element).data("DateTimePicker").date();

    if (dt == null || dt == undefined) {
        return null;
    }

    if (dt._isAMomentObject) {
        var toDate = dt.toDate();
        toDate.setHours(toDate.getHours() - toDate.getTimezoneOffset() / 60);
        return toDate;
    } else {
        return dt;
    }

    // if (!dt.getTime) return null; 
}

var ltcDestroyDateTimePicker2 = function (element) {
    $(element).data("DateTimePicker").destroy();
}