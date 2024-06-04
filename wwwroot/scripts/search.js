document.addEventListener('DOMContentLoaded', function () {
    var searchButton = document.getElementById('searchButton');
    searchButton.addEventListener('click', function () {
        var searchInput = document.getElementById('searchInput').value;
        searchInput = searchInput.trim().replace(/\s+/g, ' ');
        updateUrlParameter('search', searchInput);
    });

    document.getElementById("searchInput").addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            var searchInput = document.getElementById('searchInput').value;
            searchInput = searchInput.trim().replace(/\s+/g, ' ');
            updateUrlParameter('search', searchInput);
        }
    });

    function updateUrlParameter(param, value) {
        var url = new URL(window.location.href);
        if (value != "") {
            url.searchParams.set(param, value);
        } else {
            url.searchParams.delete(param);
        }
        window.history.replaceState({}, '', url);
        reloadWithoutScroll()
    }
});