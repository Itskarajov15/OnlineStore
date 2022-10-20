import { showProducts } from './showProductsInCart.js';

const eventHandler = function () {
    let cartDiv = document.getElementById('cartDiv');
    cartDiv.addEventListener('click', removeProduct);
}

const removeProduct = async function (e) {
    if (e.target.tagName == 'A') {
        console.log('clicked');
        fetch("/Cart/Remove", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(e.target.id)
        })
            .then(response => response.json())
            .then(result => {
                showProducts(result);
            });
    }
}

export { removeProduct };
window.onload = eventHandler();