document.querySelectorAll('.dropdown-item').forEach(function (item) {
    item.addEventListener('click', function (event) {
        event.preventDefault();
        var value = this.getAttribute('value'); 
        updateUrlParameter('sort', value);
    });
});

function updateUrlParameter(param, value) {
    var url = new URL(window.location.href);
    if (value != "trending") {
        url.searchParams.set(param, value);
    } else {
        url.searchParams.delete(param);
    }
    window.history.replaceState({}, '', url);
    reloadWithoutScroll()
}