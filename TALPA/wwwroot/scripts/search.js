document.addEventListener('DOMContentLoaded', function () {
    var searchButton = document.getElementById('searchButton');
    searchButton.addEventListener('click', function () {
        var searchInput = document.getElementById('searchInput').value;
        updateUrlParameter('search', searchInput);
    });

    function updateUrlParameter(param, value) {
        var url = new URL(window.location.href);
        url.searchParams.set(param, value);
        window.location.href = url.toString();
    }
});