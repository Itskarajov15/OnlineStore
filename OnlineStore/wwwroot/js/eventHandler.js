import { showProducts } from "./showProductsInCart.js";
import { removeProduct } from "./removeItemFromCart";
import { getAllSortingMethods } from "./sortByCriteria";
import { addProduct } from "./addProductToCart";

const eventHandler = function () {
    let removeButton = document.getElementById('removeButton');
    let productsArea = document.getElementById('productsArea');
    let divContainer = document.getElementById('container');
    let sortingByPrice = document.getElementById('sorting');
    let priceRangeInputElement = document.getElementById('priceRange');

    removeButton.addEventListener('click', removeProduct);
    productsArea.addEventListener('click', addProduct);
    sortingByPrice.addEventListener('change', getAllSortingMethods);
    divContainer.addEventListener('click', getAllSortingMethods);
    priceRangeInputElement.addEventListener('click', getAllSortingMethods);
}