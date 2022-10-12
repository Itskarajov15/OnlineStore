const getProducts = async function () {
    fetch("/Cart/Get", {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => response.json())
        .then(result => {
            showProducts(result);
        });
}

const showProducts = function (result) {
    console.log(result);
    if (result == null) {
        let shoppingList = document.getElementById('cartDiv');
        shoppingList.innerHTML = '';

        let span = document.getElementById('countOfProducts');
        span.textContent = 0;

        let countSpan = document.getElementById('spanCount');
        countSpan.textContent = '0 Items';
    }
    else {
        let span = document.getElementById('countOfProducts');
        span.textContent = result.reduce((n, { quantity }) => n + quantity, 0);

        let countSpan = document.getElementById('spanCount');
        countSpan.textContent = result.length + ' Items';

        let shoppingList = document.getElementById('cartDiv');
        shoppingList.innerHTML = '';

        result.forEach(createShoppingCartList);

        let totalSum = result.reduce((n, { price, quantity }) => n + price * quantity, 0);

        document.getElementById('totalSum').textContent = totalSum.toFixed(2) + 'лв.';
    }
}

const createShoppingCartList = function (product) {
    let shoppingList = document.getElementById('cartDiv');

    let liElement = document.createElement('li');

    liElement.innerHTML = `<a class="remove" id="${product.id}" title="Remove this item">
                                                <i class="lni lni-close"></i>
                                            </a>
                                            <div class="cart-img-head">
                                                <a class="cart-img" href="/Product/Details/${product.id}">
                                                    <img src="${product.imageUrl}" alt="Image">
                                                </a>
                                            </div>

                                            <div class="content">
                                                <h4>
                                                    <a href="/Product/Details/${product.id}">
                                                        ${product.title}
                                                    </a>
                                                </h4>
                                                <p class="quantity">${product.quantity}x - <span class="amount">${product.price}лв.</span></p>
                                            </div>`

    shoppingList.appendChild(liElement);
}

export { showProducts };
window.onload = getProducts();