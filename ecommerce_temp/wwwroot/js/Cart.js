let charlife = document.getElementById('base-life');
let increaseWith = 1;
function increase() {
     value = parseInt(charlife.value);
     value += increaseWith;
     charlife.value = value;
}