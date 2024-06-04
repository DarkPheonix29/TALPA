function highlightSubstring(substring) {
    let cards = document.querySelectorAll('#suggestions .card-body');

    cards.forEach(card => {
        let title = card.querySelector('#suggestion-title');
        let description = card.querySelector('#suggestion-description');

        let titleHTML = title.innerHTML;
        let descriptionHTML = description.innerHTML;

        let highlightedTitleHTML = titleHTML.replace(new RegExp(substring, 'gi'), '<span class="highlighted">$&</span>');
        let highlightedDescriptionHTML = descriptionHTML.replace(new RegExp(substring, 'gi'), '<span class="highlighted">$&</span>');

        title.innerHTML = highlightedTitleHTML;
        description.innerHTML = highlightedDescriptionHTML;
    });
}