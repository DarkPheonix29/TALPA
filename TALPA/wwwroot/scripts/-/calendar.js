$(function () {
    var availableDates = [
        "27/3/2024",
        "28/3/2024",
        "30/3/2024",
        "6/4/2024",
        "12/4/2024",
        "7/4/2024",
        "18/4/2024",
        "21/4/2024"
    ]

    var chosenAvailableDates = [
        "27/3/2024",
        "28/3/2024",
        "21/4/2024"
    ]

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
        multidate: true,
        todayHighlight: true,
        format: 'dd/mm/yyyy',
        multidateSeparator: ', ',
        datesDisabled: disabledDates(),
        startDate: getStartDate(),
        endDate: getEndDate()
    });

    $('#datepicker').on('changeDate', function() {
        $('#dateInput').val(
            $('#datepicker').datepicker('getFormattedDate')
        ).change();

        console.log("change")
        var valuesArray = $('#datepicker').datepicker('getFormattedDate').split(', ');
        $('#availableDates').empty();
        $.each(valuesArray, function (index, value) {
            var trimmedValue = value.trim();
            $('#availableDates').append('<option value="' + trimmedValue + '">' + trimmedValue + '</option>');
            $('#availableDates option[value="' + trimmedValue + '"]').prop('selected', true);
        });
    });

    $('#datepicker2').datepicker({
        language: "nl",
        multidate: true,
        todayHighlight: true,
        format: 'dd/mm/yyyy',
        multidateSeparator: ', ',
        datesDisabled: disabledDates(),
        startDate: getStartDate(),
        endDate: getEndDate()
    });

    $('#datepicker2').on('changeDate', function() {
        $('#dateInput2').val(
            $('#datepicker2').datepicker('getFormattedDate')
        ).change();

        console.log("change2")
        var valuesArray = $('#datepicker2').datepicker('getFormattedDate').split(', ');
        $('#availableDates2').empty();
        $.each(valuesArray, function (index, value) {
            var trimmedValue = value.trim();
            $('#availableDates2').append('<option value="' + trimmedValue + '">' + trimmedValue + '</option>');
            $('#availableDates2 option[value="' + trimmedValue + '"]').prop('selected', true);
        });
    });

    $("#changeAvailability").on('show.bs.modal', function(){
        LoadAvailability()
    });

    $("#changeAvailability").on('hide.bs.modal', function(){
        $('#dateInput2').val(null).trigger("change")
        $('#datepicker2').val("").datepicker("update");
    });

    $("th.prev").html("<h3><i class='bi bi-caret-left'></i></h3>")
    $("th.next").html("<h3><i class='bi bi-caret-right'></i></h3>")

    function disabledDates() {
        var allDates = [];
        var startDate = new Date();

        for (var i = 0; i < getDateRange()*2; i++) {
            var currentDate = startDate.getDate() + '/' + (startDate.getMonth() + 1) + '/' + startDate.getFullYear();
            if (($.inArray(currentDate, availableDates) === -1)) {
                allDates.push(currentDate);
            }
            startDate.setDate(startDate.getDate() + 1);
        }

        return allDates;
    }

    function getStartDate() {
        var dateObjects = availableDates.map(function(dateString) {
            var parts = dateString.split('/');
            return new Date(parts[2], parts[1] - 1, parts[0]);
        });
        var newestDateObject = new Date(Math.min.apply(null, dateObjects));
        
        return (newestDateObject.getDate()) + '/' + (newestDateObject.getMonth() + 1) + '/' + newestDateObject.getFullYear();
    }

    function getEndDate() {
        var dateObjects = availableDates.map(function(dateString) {
            var parts = dateString.split('/');
            return new Date(parts[2], parts[1] - 1, parts[0]);
        });
        var newestDateObject = new Date(Math.max.apply(null, dateObjects));
        
        return (newestDateObject.getDate()) + '/' + (newestDateObject.getMonth() + 1) + '/' + newestDateObject.getFullYear();
    }

    function getDateRange() {
        var dateObjects = availableDates.map(function(dateString) {
            var parts = dateString.split('/');
            return new Date(parts[2], parts[1] - 1, parts[0]); 
        });
        
        var oldestDate = new Date(Math.min.apply(null, dateObjects));
        var newestDate = new Date(Math.max.apply(null, dateObjects));
        var differenceInMs = newestDate.getTime() - oldestDate.getTime();
        return Math.floor(differenceInMs / (1000 * 60 * 60 * 24));
    }

    function LoadAvailability() {
        $('#datepicker2').datepicker('setDates', chosenAvailableDates);
        $('#dateInput2').val(
            $('#datepicker2').datepicker('getFormattedDate')
        ).change();
        $('#datepicker2').datepicker('updateViewDate', "21/5/2025");
    }
})