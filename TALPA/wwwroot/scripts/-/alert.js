function alertMessage(message) {
    console.log(`message: ${message}`)
    var messageBox = $(`
        <div class="alert alert-warning alert-dismissible fade show position-relative mt-4" role = "alert">
            <strong>${message}</strong>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    `)

    $("#alert-box").append(messageBox)

    setTimeout(function () {
        $(messageBox).addClass("alertRemove");
    }, 3000); 

    setTimeout(function () {
        $(messageBox).hide();
        $(messageBox).addClass("alertRemoved");
    }, 4000); 
}