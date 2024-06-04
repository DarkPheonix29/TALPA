function reloadWithoutScroll() {
    var scrollPosition = window.scrollY || window.pageYOffset;

    history.replaceState({ scrollPosition: scrollPosition }, "", window.location);

    window.location.reload();

    window.onload = function () {
        var scrollPosition = history.state ? history.state.scrollPosition : 0;
        window.scrollTo(0, scrollPosition);
    };
}