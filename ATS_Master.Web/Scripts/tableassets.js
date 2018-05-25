var ltcShowModal = function () {
    $('#ltcModalHolder').modal('show');
}

var ltcHideModal = function () {
    $('#ltcModalHolder').modal('hide');
}



var ltcCreateDatePicker = function (element) {
    var date = null;
    $(element).css('cursor', 'pointer');
    $(element).css('text-align', 'center');
    if ($(element).val()) date = new Date($(element).val());
    $(element)
        .datepicker({ format: 'dd MM yyyy', weekStart: 1 });
    if (date) $(element).datepicker('setDate', date);
    $(element)
        .on('changeDate', function () {
            Reinforced.Lattice.Master.fireDomEvent('keyup', element);
        });
}

var ltcPutDateToDatepicker = function (element, date) {
    $(element).datepicker('setDate', date);
}

var ltcGetDateFromDatepicker = function (element, date) {
    var dt = $(element).datepicker('getDate');
    if (dt == null || dt == undefined) return null;
    if (!dt.getTime) return null;
    dt.setHours(dt.getHours() - dt.getTimezoneOffset() / 60);
    return dt;
}

var ltcDestroyDatepicker = function (element) {
    $(element).datepicker('remove');
}




var ltcCreateDateTimePicker = function (element) {
    var date = null;
    $(element).css('cursor', 'pointer');
    $(element).css('text-align', 'center');

    if ($(element).val()) {
        date = new Date($(element).val());
    }

    $(element).datetimepicker({
        format: 'dd MM yyyy hh:mm:ss',
        languange: 'en'
    });
    var picker = $(element).data('datetimepicker');

    if (date) picker.setDate(date);

    $(element)
        .on('changeDate', function () {
            Reinforced.Lattice.Master.fireDomEvent('keyup', element);
        });
}

var ltcPutDateToDateTimePicker = function (element, date) {
    $(element).data('datetimepicker').setDate(date);
}

var ltcGetDateFromDateTimePicker = function (element, date) {
    var dt = $(element).data('datetimepicker').getDate();
    if (dt == null || dt == undefined) return null;
    if (!dt.getTime) return null;
    dt.setHours(dt.getHours() - dt.getTimezoneOffset() / 60);
    return dt;
}

var ltcDestroyDateTimePicker = function (element) {
    $(element).data('datetimepicker').remove();
}