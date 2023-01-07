function MakeUpdateQtyButtonVisible(id, visible) {
    const updateQtyButton = document.querySelector(`button[data-itemId="${id}"]`);
    updateQtyButton.style.display = visible ? "inline-block" : "none";
}

function PurchaseShoppingCartItems(bool visible) {
    const successfulPaymentMessage = document.querySelector("#successfulPaymentMessage");
    successfulPaymentMessage.style.display = visible ? "inline-block" : "none";
}
