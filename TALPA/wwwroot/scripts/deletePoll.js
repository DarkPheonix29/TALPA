const suggestions = document.getElementById("suggestions");

suggestions.addEventListener("click", function (event) {
    var card = event.target.closest('.card')
    alert(card.id)
});