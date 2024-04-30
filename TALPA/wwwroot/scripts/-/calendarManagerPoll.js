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

    $('#datepicker3').datepicker({
        language: "nl",
        multidate: true,
        todayHighlight: true,
        format: 'dd/mm/yyyy',
        multidateSeparator: ', ',
        startDate: getStartDate()
    });

    $('#datepicker3').on('changeDate', function () {
        $('#dateInput3').val(
            $('#datepicker3').datepicker('getFormattedDate')
        ).change();

        var valuesArray = $('#datepicker3').datepicker('getFormattedDate').split(', ');
        $('#availableDates3').empty();
        $.each(valuesArray, function (index, value) {
            var trimmedValue = value.trim();
            $('#availableDates3').append('<option value="' + trimmedValue + '">' + trimmedValue + '</option>');
            $('#availableDates3 option[value="' + trimmedValue + '"]').prop('selected', true);
        });
    });

    $("th.prev").html("<h3><i class='bi bi-caret-left'></i></h3>")
    $("th.next").html("<h3><i class='bi bi-caret-right'></i></h3>")
})

function getStartDate() {
    var newestDateObject = new Date();
    return (newestDateObject.getDate()) + '/' + (newestDateObject.getMonth() + 1) + '/' + newestDateObject.getFullYear();
}