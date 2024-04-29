var activities = []
var activitiesIds = []
var dates = []

$(function () {
    $("tbody").on("click", "tr", function () {
        var name = $(this).attr("name")
        var id = $(this).attr("actId")
        if (!$(this).hasClass("selected")) {
            if (activities.length <= 2) {
                $(this).addClass("selected")
                activities.push(name)
                activitiesIds.push(id)
            }
        } else {
            $(this).removeClass("selected")
            var filteredList = $.grep(activities, function (item) {
                return item !== name;
            });
            activities = filteredList

            var filteredListIds = $.grep(activitiesIds, function (item) {
                return item !== id;
            });
            activitiesIds = filteredListIds
        }
        $("#activities").text(activities.join(", "))
        $('#activitiesSelect').empty();
        $.each(activitiesIds, function (index, value) {
            var trimmedValue = value.trim();
            console.log(value)
            $('#activitiesSelect').append('<option value="' + trimmedValue + '">' + trimmedValue + '</option>');
            $('#activitiesSelect option[value="' + trimmedValue + '"]').prop('selected', true);
        });
    });

    $("#startPoll").on("click", function () {
        if (activities.length == 3) {
            $('#setPeriod').modal('show');
        } else {
            alertMessage("Kies 3 activiteiten.")
        }
    });

    $("#startPoll2").on("click", function () {
        var datesInput = $("#dateInput3").val()
        dates = $("#dateInput3").val().split(", ")

        if (datesInput != "") {
            createPoll()
        } else {
            alertMessage("Kies beschikbare dagen.")
        }
    });
})

function createPoll() {
    $('#pollForm').submit();
}