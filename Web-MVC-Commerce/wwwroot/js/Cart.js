document.addEventListener("DOMContentLoaded", function() {
    const decreaseButton = document.querySelector('.qty-decrease');
    const increaseButton = document.querySelector('.qty-increase');
    const quantityInput = document.querySelector('.qty-input');

    decreaseButton.addEventListener('click', function() {
        let currentValue = parseInt(quantityInput.value);
        if (currentValue > 1) {
            quantityInput.value = currentValue - 1;
        }
    });

    increaseButton.addEventListener('click', function() {
        let currentValue = parseInt(quantityInput.value);
        quantityInput.value = currentValue + 1;
    });
});
