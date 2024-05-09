const productList = document.getElementById('product-lists');
let isDown = false;
let startX;
let startY;
let scrollLeft;
let scrollTop;

productList.addEventListener('mousedown', (e) => {
    isDown = true;
    startX = e.pageX - productList.offsetLeft;
    startY = e.pageY - productList.offsetTop;
    scrollLeft = productList.scrollLeft;
    scrollTop = productList.scrollTop;
    productList.style.cursor = 'grabbing';
});

productList.addEventListener('mouseleave', () => {
    isDown = false;
    productList.style.cursor = 'grab';
});

productList.addEventListener('mouseup', () => { 
    isDown = false;
    productList.style.cursor = 'grab';
});

document.addEventListener('mousemove', (e) => { 
    if (!isDown) return;
    e.preventDefault();
    const x = e.pageX - productList.offsetLeft;
    const y = e.pageY - productList.offsetTop;
    const walkX = (x - startX) * 1;
    const walkY = (y - startY) * 1;
    productList.scrollLeft = scrollLeft - walkX;
    productList.scrollTop = scrollTop - walkY;
});

const scrollLeftButton = document.getElementById(
    'action-button--previous');
const scrollRightButton = document.getElementById(
    'action-button--next');

scrollLeftButton.addEventListener('click', () => { 
    productList.scrollBy({
        top: 0,
        left: -200,
        behavior: 'smooth'
    });
});

scrollRightButton.addEventListener('click', () => {
    productList.scrollBy({
        top: 0, 
        left: 200, 
        behavior: 'smooth'
    });
});

productList.addEventListener('scroll',(e) => {
    const position = productList.scrollLeft;
    if (position === 0)
        scrollLeftButton.disabled = true;
    else
        scrollLeftButton.disabled = false;

    if (Math.round(position) === productList.scrollWidth - productList.clientWidth)
        scrollRightButton.disabled = true;
    else
        scrollLeftButton.disabled = false;
});