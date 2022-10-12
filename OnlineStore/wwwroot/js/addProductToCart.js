import { showProducts } from "./showProductsInCart.js";

const EventHandler = function () {
    let productsArea = document.getElementById('productsArea');

    productsArea.addEventListener('click', addProduct);
}

const addProduct = async function (e) {
    if (e.target.tagName == 'A') {
        e.preventDefault();

        fetch("/Cart/Add", {
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

window.onload = EventHandler();