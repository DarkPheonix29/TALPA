function loadMoreColumns() {
    let hiddenColumns = document.querySelectorAll('.d-none');
    setTimeout(function () {
        for (let i = 0; i < Math.min(hiddenColumns.length, 6); i++) {
            hiddenColumns[i].classList.remove('d-none');
        }
    }, 200);

    setTimeout(function () {
        for (let i = 0; i < Math.min(hiddenColumns.length, 6); i++) {
            hiddenColumns[i].classList.remove('loader');
        }
    }, 1000);
}

loadMoreColumns();
window.addEventListener('scroll', function () {
    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
        loadMoreColumns();
    }
});