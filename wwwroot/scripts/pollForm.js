var exampleModal = document.getElementById('pollModal')
exampleModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget
    var suggestionId = button.getAttribute('data-bs-suggestion-id')
    var suggestionName = button.getAttribute('data-bs-suggestion-name')

    var suggestionInput = exampleModal.querySelector('#suggestionInput')
    var suggestionIdInput = exampleModal.querySelector('#suggestionId')

    suggestionInput.value = suggestionName
    suggestionIdInput.value = suggestionId
    
})

var valid = false;
$(function () {
    $.fn.datepicker.dates['nl'] = {
        days: ["Zondag", "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag"],
        daysShort: ["Zo", "Ma", "Di", "Wo", "Do", "Vr", "Za"],
        daysMin: ["Zo", "Ma", "Di", "Wo", "Do", "Vr", "Za"],
        months: ["Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December"],
        monthsShort: ["Jan", "Feb", "Mrt", "Apr", "Mei", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"],
        today: "Vandaag",
        clear: "Wissen",
        format: "dd-mm-yyyy",
        titleFormat: "MM yyyy",
        weekStart: 0
    };

    $('#datepicker').datepicker({
        language: "nl",
        multidate: true,
        todayHighlight: true,
        format: 'dd-mm-yyyy',
        multidateSeparator: ', ',
        datesDisabled: disabledDates(),
        startDate: getStartDate(),
        endDate: getEndDate()
    });

    $('#datepicker').on('changeDate', function () {
        $('#dateInput').val(
            $('#datepicker').datepicker('getFormattedDate')
        ).change();
        resizeTextarea()

        var valuesArray = $('#datepicker').datepicker('getFormattedDate').split(', ');
        if ($('#datepicker').datepicker('getFormattedDate') != "") {
            $("#dateInput").removeClass("is-invalid")
            $("#dateFeedback").text("")
            valid = true
        } else {
            $("#dateInput").addClass("is-invalid")
            $("#dateFeedback").text("Selecteer minimaal 1 datum")
            valid = false
        }
        $('#dateInputHidden').empty();
        $.each(valuesArray, function (index, value) {
            var trimmedValue = value.trim();
            $('#dateInputHidden').append('<option value="' + trimmedValue + '">' + trimmedValue + '</option>');
            $('#dateInputHidden option[value="' + trimmedValue + '"]').prop('selected', true);
        });
    });

    $("#pollModal").on('hide.bs.modal', function () {
        $('#dateInput').val(null).trigger("change")
        $('#datepicker').val("").datepicker("update");
    });

    $("th.prev").html('<h3><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-caret-left" viewBox="0 0 16 16"><path d="M10 12.796V3.204L4.519 8zm-.659.753-5.48-4.796a1 1 0 0 1 0-1.506l5.48-4.796A1 1 0 0 1 11 3.204v9.592a1 1 0 0 1-1.659.753" /></svg></h3>')
    $("th.next").html('<h3><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-caret-right" viewBox="0 0 16 16"><path d="M6 12.796V3.204L11.481 8zm.659.753 5.48-4.796a1 1 0 0 0 0-1.506L6.66 2.451C6.011 1.885 5 2.345 5 3.204v9.592a1 1 0 0 0 1.659.753" /></svg></h3>')

    function disabledDates() {
        var allDates = [];
        var startDate = new Date();

        for (var i = 0; i < getDateRange() * 2; i++) {
            var currentDate = startDate.getDate() + '-' + (startDate.getMonth() + 1) + '-' + startDate.getFullYear();
            if (($.inArray(currentDate, availableDates) === -1)) {
                allDates.push(currentDate);
            }
            startDate.setDate(startDate.getDate() + 1);
        }

        return allDates;
    }

    function getStartDate() {
        var dateObjects = availableDates.map(function (dateString) {
            var parts = dateString.split('-');
            return new Date(parts[2], parts[1] - 1, parts[0]);
        });
        var newestDateObject = new Date(Math.min.apply(null, dateObjects));

        return (newestDateObject.getDate()) + '-' + (newestDateObject.getMonth() + 1) + '-' + newestDateObject.getFullYear();
    }

    function getEndDate() {
        var dateObjects = availableDates.map(function (dateString) {
            var parts = dateString.split('-');
            return new Date(parts[2], parts[1] - 1, parts[0]);
        });
        var newestDateObject = new Date(Math.max.apply(null, dateObjects));

        return (newestDateObject.getDate()) + '-' + (newestDateObject.getMonth() + 1) + '-' + newestDateObject.getFullYear();
    }

    function getDateRange() {
        var dateObjects = availableDates.map(function (dateString) {
            var parts = dateString.split('-');
            return new Date(parts[2], parts[1] - 1, parts[0]);
        });

        var oldestDate = new Date(Math.min.apply(null, dateObjects));
        var newestDate = new Date(Math.max.apply(null, dateObjects));
        var differenceInMs = newestDate.getTime() - oldestDate.getTime();
        return Math.floor(differenceInMs / (1000 * 60 * 60 * 24));
    }

    function resizeTextarea() {
        var textarea = document.getElementById("dateInput");
        textarea.style.height = "auto";

        textarea.style.height = textarea.scrollHeight + 2 + "px";
    }
})

const form = document.getElementById('form');
const submitButton = document.getElementById('submitButton');

submitButton.addEventListener('click', function () {
    if (valid) {
        form.submit();
    } else {
        document.getElementById("datepicker").dispatchEvent(new Event('changeDate'));
    }
});