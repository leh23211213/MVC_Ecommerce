let charlife = document.getElementById('quantity');
let increaseWith = 1;
function increase() {
     value = parseInt(charlife.value);
     value += increaseWith;
     charlife.value = value;
}