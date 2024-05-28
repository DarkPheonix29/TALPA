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

const similarSuggestionModalCloseButton = document.getElementById('similarSuggestionModalClose');

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
        inputString = suggestionInput.value + " " + descriptionInput.value;
        var sqlInjections = ["SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "UNION", "WHERE", "AND", "OR", "LIKE", "EXEC", "EXECUTE", "TRUNCATE", "ORDER BY", "GROUP BY", "/*", "*/", "XP_CMDShell"];
        if ($.grep(sqlInjections, function (keyword) { return inputString.toUpperCase().indexOf(keyword) !== -1; }).length > 0) {
            alert("Nice try! Ik ga je kietelen!")
            window.open('https://media4.giphy.com/media/cr9vIO7NsP5cY/200w.gif?cid=6c09b952tvuwg978upn4aqp1f4hc9i9y4qe9wdq9tg1djcuj&ep=v1_gifs_search&rid=200w.gif&ct=g', '_blank');
        } else {
            $("#newSuggestionModal").modal("hide")
            $("#similarSuggestionWaitModal").modal("show")
            var similarSuggestions = await GetSimilarSuggestions(suggestionInput.value, descriptionInput.value)
            if (similarSuggestions.length > 0) {
                $("#similarSuggestionModalList").clear()
                $.each(similarSuggestions, function (index, suggestionId) {
                    var suggestion = allSuggestions[suggestionId]
                    var listElement = `
                    <div class="card h-100 mb-3" id = "@suggestion.Id" >
						<div class="card-body">
							<div class="d-flex align-items-start justify-content-between mb-2">
								<h5 class="card-title mb-0" id="suggestion-title">${suggestion.name}</h5>
							</div>
							<p class="card-text" id="suggestion-description">${suggestion.description}</p>
						</div>
					</div >
               `
                    $("#similarSuggestionModalList").append(listElement);
                });

                $("#similarSuggestionWaitModal").modal("hide")
                $("#similarSuggestionModal").modal("show")

            } else {
                form.submit();
            }
        }
    } else {
        suggestionInput.dispatchEvent(new Event('input'));
        descriptionInput.dispatchEvent(new Event('input'));
        categoryInput.dispatchEvent(new Event('change'));
        limitationInput.dispatchEvent(new Event('change'));
    }
});

similarSuggestionModalCloseButton.addEventListener('click',  function () {
    $("#similarSuggestionModal").modal("hide")
    $("#newSuggestionModal").modal("show")
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