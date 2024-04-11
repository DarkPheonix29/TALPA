$(function () {
    $('#activityInput').select2( {
        theme: "bootstrap-5",
        width: $( this ).data( 'width' ) ? $( this ).data( 'width' ) : $( this ).hasClass( 'w-100' ) ? '100%' : 'style',
        placeholder: $( this ).data( 'placeholder' ),
        closeOnSelect: false,
    });

    $("#createPollModal").on('hide.bs.modal', function () {
        $('#activityInput').val(null).trigger("change")
    });
})