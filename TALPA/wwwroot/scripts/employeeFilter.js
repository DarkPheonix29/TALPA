$(function () {
    $('#employeeSearch').on("keyup", function () {
        filter()
    });
})

function filter() {
    var value = $('#employeeSearch').val().toLowerCase();

    $("tbody tr").each(function () {
        var row = $(this);
        var text = row.text().toLowerCase();

        var match = text.indexOf(value) > -1 || value === "";

        row.toggle(match);
    });
}