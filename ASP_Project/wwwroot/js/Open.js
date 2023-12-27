const imgcontainer = document.getElementsByClassName('imgcontainer')[0]; // Updated to access first item in the collection
const up = document.getElementsByClassName('up')[0];
const down = document.getElementsByClassName('down')[0];
const heart = document.getElementsByClassName('heart')[0];
const cart = document.getElementsByClassName('cart')[0];
const buy = document.getElementsByClassName('buy')[0];
const bordered = document.getElementsByClassName('bordered')[0];
const crossed = document.getElementsByClassName('crossed')[0];
const attention = document.getElementsByClassName('attention')[0];
const counting = document.getElementsByClassName('count')[0];
const increase = document.getElementsByClassName('increase')[0];
const decrease = document.getElementsByClassName('decrease')[0];

increase.addEventListener('click', () => {
    counting.value = Number(counting.value) + 1;
});

decrease.addEventListener('click', () => {
    if (Number(counting.value) > 1) {
        counting.value = Number(counting.value) - 1;
    }
}
);

const changeHeart = () => {
    if (heart.style.color !== 'rgb(229, 56, 56)') {
        heart.style.color = 'rgb(229, 56, 56)';
        heart.innerHTML = '<i style=color: rgb(229, 56, 56) class="fa-solid fa-heart"></i>';
    } else {
        heart.innerHTML = '<i style=color: grey class="fa-regular fa-heart"></i>';
        heart.style.color = 'grey';
    }
}

const changeCart = () => {
    if (cart.style.backgroundColor !== 'white') {
        cart.style.backgroundColor = 'white';
        cart.style.color = '#e53b3b';
        cart.innerHTML = 'Remove from cart <i style="color: #e53b3b" class="fa-solid fa-cart-shopping"></i>';
    } else {
        cart.innerHTML = 'Add to cart <i  style="color: white" class="fa-solid fa-cart-shopping"></i>';
        cart.style.backgroundColor = '#e53b3b';
        cart.style.color = 'white';
    }
}
const changeBuy = () => {
    if (buy.style.backgroundColor !== 'transparent') {
        buy.style.color = '#0cb225';
        buy.style.border = '1px solid #0cb225;';
        buy.style.borderColor = '#0cb225';
        buy.innerHTML = "Succesfully bought";
    }
}

buy.addEventListener('click', changeBuy);
heart.addEventListener('click', changeHeart);
cart.addEventListener('click', changeCart);

 