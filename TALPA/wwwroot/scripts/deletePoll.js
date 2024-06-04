const suggestions = document.getElementById("suggestions");
const suggestionLabel = document.getElementById("suggestionRemoveName");
const suggestionRemoveButton = document.getElementById("suggestionRemoveButton");

suggestions.addEventListener("click", function (event) {
    var card = event.target.closest('.card')
    suggestionLabel.textContent = allSuggestions[card.id].name
    suggestionRemoveButton.href = `/removeSuggestion/${card.id}`
    $("#removeSuggestionModal").modal("show");
});