$(function () {
    $.fn.datepicker.dates['nl'] = {
        days: ["Zondag", "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag"],
        daysShort: ["Zo", "Ma", "Di", "Wo", "Do", "Vr", "Za"],
        daysMin: ["Zo", "Ma", "Di", "Wo", "Do", "Vr", "Za"],
        months: ["Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December"],
        monthsShort: ["Jan", "Feb", "Mrt", "Apr", "Mei", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"],
        today: "Vandaag",
        clear: "Wissen",
        format: "dd/mm/yyyy",
        titleFormat: "MM yyyy",
        weekStart: 0
    };

    $('#datepicker').datepicker({
        language: "nl",
        multidate: false,
        todayHighlight: true,
        format: 'dd/mm/yyyy',
        multidateSeparator: ', '
    });

    $('#datepicker').on('changeDate', function() {
        $('#dateInput').val(
            $('#datepicker').datepicker('getFormattedDate')
        ).change();
        $("#hiddenDateInput").val($('#dateInput').val());
    });


    $("#changeDeadline").on('hide.bs.modal', function(){
        $('#dateInput').val(null).trigger("change")
        $('#datepicker').val("").datepicker("update");
    });

    $("th.prev").html("<h3><i class='bi bi-caret-left'></i></h3>")
    $("th.next").html("<h3><i class='bi bi-caret-right'></i></h3>")
})