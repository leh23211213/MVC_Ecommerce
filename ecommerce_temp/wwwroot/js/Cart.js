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

$(document).ready(function () {
    $(".remove-from-cart").click(function (e) {
        e.preventDefault();
        var cartItemId = $(this).data("cart-item-id");
        var row = $(this).closest('tr');
        $.ajax({
            type: "POST",
            url: "/Cart/Delete",
            data: {
                cartItemId: cartItemId,
                __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                alert(response.message); // Show a success message
                row.remove(); // Remove the row from the table
            },
            error: function (xhr, status, error) {
                var response = JSON.parse(xhr.responseText);
                alert(response.message); // Show an error message
            }
        });
    });
});

function submitDeleteForm(imgElement) {
    var form = imgElement.closest('form');
    form.submit();
}
