$(function () {
    $('#activitySearch, #CategoryFilter, #RestrictionFilter').on("change", function () {
        filter()
    });

    $('#activitySearch').on("keyup", function () {
        filter()
    });
})

function filter() {
    var value1 = $('#activitySearch').val().toLowerCase();
    var value2 = $('#CategoryFilter').val().map(function (option) {
        return option.toLowerCase();
    });
    var value3 = $('#RestrictionFilter').val().map(function (option) {
        return option.toLowerCase();
    });

    $("tbody tr").each(function () {
        var row = $(this);
        var text = row.text().toLowerCase();

        var match1 = text.indexOf(value1) > -1 || value1 === "";
        var match2 = text.indexOf(value2) > -1 || value2 === "";
        var match3 = text.indexOf(value3) > -1 || value3 === "";

        row.toggle(match1 && match2 && match3);
    });
}