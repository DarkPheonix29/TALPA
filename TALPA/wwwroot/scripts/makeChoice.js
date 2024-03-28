$(function () {
    $("#makeChoice").on('hide.bs.modal', function(){
        $('#dateInput').val(null).trigger("change")
        $('#datepicker').val("").datepicker("update");
        $('#choiceName').text("-")
        $('#activity').val("-")
    });

    $("#selectOption1").on( "click", function() {
        $('#choiceName').text($("#selectOption1Name").text())
        $('#activity').val($("#selectOption1Name").text())
        $('#makeChoice').modal('show');
    });

    $("#selectOption2").on( "click", function() {
        $('#choiceName').text($("#selectOption2Name").text())
        $('#activity').val($("#selectOption2Name").text())
        $('#makeChoice').modal('show');
    });

    $("#selectOption3").on( "click", function() {
        $('#choiceName').text($("#selectOption3Name").text())
        $('#activity').val($("#selectOption3Name").text())
        $('#makeChoice').modal('show');
    });
})