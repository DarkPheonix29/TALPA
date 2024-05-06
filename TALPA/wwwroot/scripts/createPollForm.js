var valid1 = false;
var valid2 = false;
var valid3 = false;
var valid4 = false;

activities = [];
activityIds = [];

const activity1Input = document.getElementById('activity1');
const activity2Input = document.getElementById('activity2');
const activity3Input = document.getElementById('activity3');
const activityFeedback = document.getElementById('activityFeedback');

const activitiesHiddenInput = document.getElementById('activitiesHiddenInput');

const submitPollButton = document.getElementById('submitPollButton');
const planForm = document.getElementById('planForm');
const planButton = document.getElementById('planButton');

const suggestions = document.getElementById("suggestions");
suggestions.addEventListener("click", function (event) {
    var card = event.target.closest('.card')
    var cardSelect = card.querySelector('.card-select');
    if (cardSelect.classList.contains('hidden')) {
        if (activities.length < 3) {
            activities.push(card.querySelector('.card-title').textContent)
            activityIds.push(card.id)
            cardSelect.classList.remove('hidden');
        }
    } else {
        removeFromList(activities, card.querySelector('.card-title').textContent)
        removeFromList(activityIds, card.id)
        cardSelect.classList.add('hidden');
    }   
    activity1Input.value = ""
    activity2Input.value = ""
    activity3Input.value = ""
    activities.forEach(function (activity, index) {
        var inputId = "activity" + (index + 1);
        var inputElement = document.getElementById(inputId);
        if (inputElement) {
            inputElement.value = activity;
        }
    });

    while (activitiesHiddenInput.options.length > 0) {
        activitiesHiddenInput.remove(0);
    }

    for (var i = 0; i < activityIds.length; i++) {
        var option = document.createElement("option");
        option.value = activityIds[i];
        option.textContent = activityIds[i];
        activitiesHiddenInput.appendChild(option);
        option.selected = true;
    }

    activity1Input.dispatchEvent(new Event('change'));
    activity2Input.dispatchEvent(new Event('change'));
    activity3Input.dispatchEvent(new Event('change'));
});

activity1Input.addEventListener("change", function () {
    const length = this.value.length;
    if (length == 0) {
        activity1Input.classList.add("is-invalid")
        activityFeedback.textContent = "Selecteer 3 activiteiten";
        valid1 = false;
    } else {
        activity1Input.classList.remove("is-invalid")
        valid1 = true;
    }
});

activity2Input.addEventListener("change", function () {
    const length = this.value.length;
    if (length == 0) {
        activity2Input.classList.add("is-invalid")
        activityFeedback.textContent = "Selecteer 3 activiteiten";
        valid2 = false;
    } else {
        activity2Input.classList.remove("is-invalid")
        valid2 = true;
    }
});

activity3Input.addEventListener("change", function () {
    const length = this.value.length;
    if (length == 0) {
        activity3Input.classList.add("is-invalid")
        activityFeedback.textContent = "Selecteer 3 activiteiten";
        valid3 = false;
    } else {
        activity3Input.classList.remove("is-invalid")
        activityFeedback.textContent = "";
        valid3 = true;
    }
});

planButton.addEventListener('click', function () {
    if (valid1 && valid2 && valid3) {
        var myModal = new bootstrap.Modal(document.getElementById('pollModal'));
        myModal.show();
    } else {
        activity1Input.dispatchEvent(new Event('change'));
        activity2Input.dispatchEvent(new Event('change'));
        activity3Input.dispatchEvent(new Event('change'));
    }
});

submitPollButton.addEventListener('click', function () {
    if (valid1 && valid2 && valid3 && valid4) {
        planForm.submit()
    } else {
        activity1Input.dispatchEvent(new Event('change'));
        activity2Input.dispatchEvent(new Event('change'));
        activity3Input.dispatchEvent(new Event('change'));
        document.getElementById("datepicker").dispatchEvent(new Event('changeDate'));
    }
});

function removeFromList(array, stringToRemove) {
    var index = array.indexOf(stringToRemove);
    if (index !== -1) {
        array.splice(index, 1);
    }
}

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
        todayHighlight: true,
        format: 'dd-mm-yyyy',
        startDate: getStartDate()
    });

    $('#datepicker').on('changeDate', function () {
        $('#dateInput').val(
            $('#datepicker').datepicker('getFormattedDate')
        ).change();
        $("#dateHiddenInput").val($('#datepicker').datepicker('getFormattedDate'))
        var valuesArray = $('#datepicker').datepicker('getFormattedDate').split(', ');
        if ($('#datepicker').datepicker('getFormattedDate') != "") {
            $("#dateInput").removeClass("is-invalid")
            $("#dateFeedback").text("")
            $("#dateFeedback2").text("")
            valid4 = true
        } else {
            $("#dateInput").addClass("is-invalid")
            $("#dateFeedback").text("Selecteer datum")
            $("#dateFeedback2").text("Selecteer datum")
            valid4 = false
        }
    });

    $("#pollModal").on('hide.bs.modal', function () {
        $('#dateInput').val(null).trigger("change")
        $('#datepicker').val("").datepicker("update");
    });

    $("th.prev").html('<h3><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-caret-left" viewBox="0 0 16 16"><path d="M10 12.796V3.204L4.519 8zm-.659.753-5.48-4.796a1 1 0 0 1 0-1.506l5.48-4.796A1 1 0 0 1 11 3.204v9.592a1 1 0 0 1-1.659.753" /></svg></h3>')
    $("th.next").html('<h3><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-caret-right" viewBox="0 0 16 16"><path d="M6 12.796V3.204L11.481 8zm.659.753 5.48-4.796a1 1 0 0 0 0-1.506L6.66 2.451C6.011 1.885 5 2.345 5 3.204v9.592a1 1 0 0 0 1.659.753" /></svg></h3>')

    function getStartDate() {
        var newestDateObject = new Date();
        return (newestDateObject.getDate()) + '/' + (newestDateObject.getMonth() + 1) + '/' + newestDateObject.getFullYear();
    }
})

$(document).ready(function () {
    $('#hour-dropdown').on('click', ".dropdown-item", function () {
        $('#hour-dropdown').find(".dropdown-item").removeClass('active');
        $(this).addClass('active');
        $('#hourInput').text($(this).text());
        updateTimeInput()
    });

    $('#min-dropdown').on('click', ".dropdown-item", function () {
        $('#min-dropdown').find(".dropdown-item").removeClass('active');
        $(this).addClass('active');
        $('#minInput').text($(this).text());
        updateTimeInput()
    });

    $('.timeGroup').on('show.bs.dropdown', function () {
        $('.timeGroup').addClass('focused');
    });

    $('.timeGroup').on('hide.bs.dropdown', function () {
        $('.timeGroup').removeClass('focused');
    });

    updateTimeInput()
});

function updateTimeInput() {
    $("#timeInput").val($('#hourInput').text() + ":" + $('#minInput').text())
}