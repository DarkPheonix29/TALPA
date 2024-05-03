document.addEventListener('DOMContentLoaded', function () {
    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    checkboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            updateUrlParameter();
        });
    });

    function updateUrlParameter() {
        var checkedCheckboxes = document.querySelectorAll('input[type="checkbox"]:checked');
        var filterValues = [];
        checkedCheckboxes.forEach(function (checkbox) {
            filterValues.push(checkbox.value);
        });
        var url = new URL(window.location.href);
        if (filterValues.length != 0) {
            url.searchParams.set('filter', filterValues.join(' '));
        } else {
            url.searchParams.delete('filter');
        }
        window.history.replaceState({}, '', url);
        reloadWithoutScroll()
    }
});