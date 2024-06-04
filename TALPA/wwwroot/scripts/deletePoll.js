const suggestions = document.getElementById("suggestions");
const suggestionLabel = document.getElementById("suggestionRemoveName");
const suggestionRemoveButton = document.getElementById("suggestionRemoveButton");
var removeSuggestionModal = new bootstrap.Modal(document.getElementById("removeSuggestionModal"), {});


suggestions.addEventListener("click", function (event) {
    var card = event.target.closest('.card')
    alert(allSuggestions[card.id].name)
    suggestionLabel.textContent = allSuggestions[card.id].name
    suggestionRemoveButton.href = `/removeSuggestion/${card.id}`
    removeSuggestionModal.show();
});