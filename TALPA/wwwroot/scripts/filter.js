document.addEventListener('DOMContentLoaded', function () {
    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    checkboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            updateUrlParameter();
            reloadWithoutScroll()
        });
    });

    var accordionButtons = document.querySelectorAll('.accordion-button');
    accordionButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var buttonId = this.id;
            if (!closedMen.includes(buttonId)) {
                closedMen.push(buttonId)
            } else {
                removeFromList(closedMen, buttonId)
            }   
            updateUrlParameter();
        });
    });

    function updateUrlParameter() {
        var checkedCheckboxes = document.querySelectorAll('input[type="checkbox"]:checked');
        var filterValues = [];
        checkedCheckboxes.forEach(function (checkbox) {
            filterValues.push(checkbox.value.replace(" ", "-"));
        });
        var url = new URL(window.location.href);
        if (filterValues.length != 0) {
            url.searchParams.set('filter', filterValues.join(' '));
        } else {
            url.searchParams.delete('filter');
        }
        if (closedMen.length != 0) {
            url.searchParams.set('closed', closedMen.join(' '));
        } else {
            url.searchParams.delete('closed');
        }
        window.history.replaceState({}, '', url);
    }
});