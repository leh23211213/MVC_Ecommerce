function Decre(productId, price) {
    let decreaseWith = 1;
    let newValue = parseInt(document.getElementById(`product-${productId}`).value) - decreaseWith;
    if (newValue >= 1) {
        document.getElementById(`product-${productId}`).value = newValue;
        if (newValue == 1)
            alert("min 1 product");
    }
    let currentValue = document.getElementById(`product-${productId}`).value;
        let total = price * currentValue;
    document.getElementById(`itemTotal-${productId}`).innerHTML = total;
}

function Incre(productId, price) {
    let increaseWith = 1;
    let newValue = parseInt(document.getElementById(`product-${productId}`).value) + increaseWith;
    if (newValue <= 5)
    {
        document.getElementById(`product-${productId}`).value = newValue;
        if (newValue == 5)
            alert("max 5 product ");
    }
    let currentValue = document.getElementById(`product-${productId}`).value;
    let total = price * currentValue;
    document.getElementById(`itemTotal-${productId}`).innerHTML = total;
}


