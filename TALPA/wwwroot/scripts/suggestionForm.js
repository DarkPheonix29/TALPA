var valid1 = false;
var valid2 = false;

const suggestionInput = document.getElementById('suggestionInput');
const suggestionFeedback = document.getElementById('suggestionFeedback');

const descriptionInput = document.getElementById('descriptionInput');
const descriptionFeedback = document.getElementById('descriptionFeedback');
const descriptionFeedback2 = document.getElementById('descriptionFeedback2');

const form = document.getElementById('form');
const submitButton = document.getElementById('submitButton');

suggestionInput.addEventListener("input", function () {
    const suggestionLength = this.value.length;
    console.log(suggestionLength)
    if (suggestionLength > 30) {
        suggestionInput.classList.add("is-invalid")
        suggestionFeedback.textContent = `Maximaal 30 tekens, ${suggestionLength}/30`;
        valid1 = false;
    } else if(suggestionLength < 5) {
        suggestionInput.classList.add("is-invalid")
        valid1 = false;
    } else {
        suggestionInput.classList.remove("is-invalid")
        suggestionFeedback.textContent = "";
        valid1 = true;
    }
});

descriptionInput.addEventListener("input", function () {
    const suggestionLength = this.value.length;
    console.log(suggestionLength)
    if (suggestionLength > 150) {
        descriptionInput.classList.add("is-invalid")
        descriptionFeedback.textContent = `Maximaal 150 tekens, ${suggestionLength}/150`;
        valid2 = false;
    } else if (suggestionLength < 30) {
        descriptionInput.classList.add("is-invalid")
        valid2 = false;
    }
    else {
        descriptionInput.classList.remove("is-invalid")
        descriptionFeedback.textContent = "";
        valid2 = true;
    }
    descriptionFeedback2.textContent = `Minimaal 30 tekens, ${suggestionLength}/30`;
});

submitButton.addEventListener('click', function () {
    if (valid1 && valid2) {
        console.log("submit")
        form.submit();
    } else {
        suggestionInput.dispatchEvent(new Event('input'));
        descriptionInput.dispatchEvent(new Event('input'));
    }
});