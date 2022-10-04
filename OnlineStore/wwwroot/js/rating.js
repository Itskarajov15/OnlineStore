const Init = function (rating) {
    let ulElement = document.createElement('ul');
    ulElement.classList.add('review');

    let countOfUnfilledStars = 5 - rating;

    while (rating > 0) {
        let liElement = document.createElement('li');
        let iElement = document.createElement('i');
        iElement.classList.add('lni lni-star-filled');

        liElement.appendChild(iElement);
        ulElement.appendChild(liElement);

        rating = rating - 1;
    }

    while (countOfUnfilledStars > 0) {
        let liElement = document.createElement('li');
        let iElement = document.createElement('i');
        iElement.classList.add('lni lni-star');

        liElement.appendChild(iElement);
        ulElement.appendChild(liElement);

        countOfUnfilledStars = countOfUnfilledStars - 1;
    }
}

window.onload = Init();