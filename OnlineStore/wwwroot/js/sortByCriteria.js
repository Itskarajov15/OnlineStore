const Init = function () {
    let divContainer = document.getElementById('container');
    let sortingByPrice = document.getElementById('sorting');

    sortingByPrice.addEventListener('change', getAllSortingMethods);
    divContainer.addEventListener('click', getAllSortingMethods);
}

const getAllSortingMethods = function () {
    let sortingModel = {};

    let divCheckboxes = document.getElementById('brandCheckboxes');
    let checkboxes = divCheckboxes.querySelectorAll('.checkbox');

    let brandsIds = [];

    checkboxes.forEach(x => {
        if (x.checked) {
            brandsIds.push(x.id);
        }
    });

    sortingModel.brandsIds = brandsIds;
    sortingModel.sortingValue = document.getElementById('sorting').value;

    getSortedProducts(sortingModel);
}

const getSortedProducts = async function (sortingModel) {
    fetch("/Product/All", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(sortingModel)
    })
        .then(response => response.json())
        .then(result => {
            let productsArea = document.getElementById('productsArea');
            productsArea.innerHTML = '';

            if (result.length <= 0) {
                let h3Element = document.createElement('h3');
                h3Element.textContent = 'There are no products that match the chosen criteria';
                productsArea.appendChild(h3Element);
            }
            else {
                result.forEach(createProductCard);
            }
        });
}

const createProductCard = function (product) {
    let colDiv = document.createElement('div');
    colDiv.classList.add('col-lg-4');
    colDiv.classList.add('col-md-6');
    colDiv.classList.add('col-12');

    let productDiv = document.createElement('div');
    productDiv.classList.add('single-product');

    let imageDiv = document.createElement('div');
    imageDiv.classList.add('product-image');

    let imageElement = document.createElement('img');
    imageElement.src = product.imageUrl;
    imageElement.alt = product.title;

    let cartDiv = document.createElement('div');
    cartDiv.classList.add('button');

    let aTag = document.createElement('a');
    aTag.classList.add('btn');
    let link = '/Product/Details/' + product.id;
    aTag.setAttribute('href', link);

    let iElement = document.createElement('i');
    iElement.classList.add('lni');
    iElement.classList.add('lni-cart');

    aTag.innerHTML = iElement + 'Add to Cart';

    cartDiv.appendChild(aTag);

    imageDiv.appendChild(imageElement);
    imageDiv.appendChild(cartDiv);

    productDiv.appendChild(imageDiv);

    let productInfoDiv = document.createElement('div');
    productInfoDiv.classList.add('product-info');

    let spanElement = document.createElement('span');
    spanElement.classList.add('category');
    spanElement.textContent = product.category;

    let h4Element = document.createElement('h4');
    h4Element.classList.add('title');

    let aTagTitle = document.createElement('a');
    aTagTitle.setAttribute('href', link);  

    h4Element.appendChild(aTagTitle);

    let priceDiv = document.createElement('div');
    priceDiv.classList.add('price');

    let priceSpan = document.createElement('span');
    priceSpan.textContent = product.price + 'лв.';

    priceDiv.appendChild(priceSpan);

    productInfoDiv.appendChild(spanElement);
    productInfoDiv.appendChild(h4Element);
    productInfoDiv.appendChild(priceDiv);

    colDiv.appendChild(productDiv);
}

window.onload = Init();