var valid1 = false;
var valid2 = false;
var valid3 = false;
var valid4 = false;

const suggestionInput = document.getElementById('suggestionInput');
const suggestionFeedback = document.getElementById('suggestionFeedback');

const descriptionInput = document.getElementById('descriptionInput');
const descriptionFeedback = document.getElementById('descriptionFeedback');

const categoryInput = document.getElementById('categoryInput');
const categoryFeedback = document.getElementById('categoryFeedback');

const limitationInputInput = document.getElementById('limitationInput');
const limitationInputFeedback = document.getElementById('limitationFeedback');

const form = document.getElementById('form');
const submitButton = document.getElementById('submitButton');

var modalBody = document.querySelector('.modal-content');
var tagMenus = document.querySelectorAll('.tags-menu');

suggestionInput.addEventListener("input", function () {
    const length = this.value.length;
    if (length > 30) {
        suggestionInput.classList.add("is-invalid")
        suggestionFeedback.textContent = `Maximaal 30 tekens, ${length}/30`;
        valid1 = false;
    } else if(length < 5) {
        suggestionInput.classList.add("is-invalid")
        suggestionFeedback.textContent = `Minimaal 5 tekens, ${length}/30`;
        valid1 = false;
    } else {
        suggestionInput.classList.remove("is-invalid")
        suggestionFeedback.textContent = "";
        valid1 = true;
    }
});

descriptionInput.addEventListener("input", function () {
    const length = this.value.length;
    if (length > 150) {
        descriptionInput.classList.add("is-invalid")
        descriptionFeedback.textContent = `Maximaal 150 tekens, ${length}/150`;
        valid2 = false;
    } else if (length < 30) {
        descriptionInput.classList.add("is-invalid")
        descriptionFeedback.textContent = `Minimaal 30 tekens, ${length}/30`;
        valid2 = false;
    }
    else {
        descriptionInput.classList.remove("is-invalid")
        descriptionFeedback.textContent = "";
        valid2 = true;
    }
});

categoryInput.addEventListener("change", function () {
    const length = this.selectedOptions.length;
    if (length < 1) {
        this.previousElementSibling.classList.add("is-invalid")
        categoryFeedback.textContent = `Minimaal 1`;
        valid3 = false;
    } else {
        this.previousElementSibling.classList.remove("is-invalid")
        categoryFeedback.textContent = "";
        valid3 = true;
    }
});

limitationInputInput.addEventListener("change", function () {
    const length = this.selectedOptions.length;
    if (length < 1) {
        this.previousElementSibling.classList.add("is-invalid")
        limitationInputFeedback.textContent = `Minimaal 1`;
        valid4 = false;
    } else {
        this.previousElementSibling.classList.remove("is-invalid")
        limitationInputFeedback.textContent = "";
        valid4 = true;
    }
});

submitButton.addEventListener('click', async function () {
    if (valid1 && valid2 && valid3 && valid4) {
        console.log(suggestionInput.textContent, descriptionInput.textContent)
        var similarSuggestions = await GetSimilarSuggestions(suggestionInput.textContent, descriptionInput.textContent)
        if (similarSuggestions.length > 0) {
            $("#similarSuggestionModal").modal("show")

        } else {
            form.submit();
        }
    } else {
        suggestionInput.dispatchEvent(new Event('input'));
        descriptionInput.dispatchEvent(new Event('input'));
        categoryInput.dispatchEvent(new Event('change'));
        limitationInput.dispatchEvent(new Event('change'));
    }
});

async function GetSimilarSuggestions(name, description) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: 'app/getSimilarSuggestions',
            method: 'POST',
            data: {
                name: name,
                description: description,
            },
            dataType: 'json',
            success: function (response) {
                resolve(response);
            },
            error: function (xhr, status, error) {
                console.log("Something Went Wrong!: GetSimilarSuggestions")
            }
        });
    });
}