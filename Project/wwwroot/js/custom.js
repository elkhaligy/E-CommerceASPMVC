// I want to write a function that takes the proudct id to add to the cart, and the quantity to add and then it makes a POST request to an endpoint
// I will make a post reuqest to /Cart/AddToCart so I need a cart controller with AddToCart action
document.addEventListener('DOMContentLoaded', () => {
    populateCart();
});

function showSuccessModal() {
    const modal = document.getElementById('successModal');
    modal.classList.add('show');
    setTimeout(() => {
        modal.classList.remove('show');
    }, 3000);
}

function showRemoveModal() {
    const modal = document.getElementById('removeModal');
    modal.classList.add('show');
    setTimeout(() => {
        modal.classList.remove('show');
    }, 3000);
}

function addToCart(productId) {
    fetch('/Cart/AddToCart?productId=' + productId + '&quantity=' + 1, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        populateCart();
        showSuccessModal();
    })
    .catch(error => {
        console.error('Error:', error);
    });
}

function removeFromCart(productId) {
    fetch('/Cart/RemoveFromCart?productId=' + productId + '&quantity=' + 1, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        const cartItemsContainer = document.getElementById('cart-items');
        const items = cartItemsContainer.querySelectorAll('li');
        items.forEach(li => {
            // Assuming the remove button has the onclick with the productId
            const removeBtn = li.querySelector('.remove');
            if (removeBtn && removeBtn.getAttribute('onclick') === `removeFromCart(${productId})`) {
                // check the quantity of the item
                const quantity = li.querySelector('.quantity').textContent.split('x')[0];
                if (quantity == 1) {
                    li.remove();
                } else {
                    //  <p class="quantity">1x - <span class="amount">$35.00</span></p>
                    li.querySelector('.quantity').innerHTML = quantity - 1 + 'x - ' + `<span class="amount">${li.querySelector('.amount').textContent}</span>`;
                    console.log(quantity);
                }
            }
        });
        const totalItems = document.getElementById('total-items');
        const totalItemsCounter = document.getElementById('total-items-counter');
        const newCount = cartItemsContainer.querySelectorAll('li').length;
        totalItems.innerHTML = newCount + ' Items';
        totalItemsCounter.innerHTML = newCount;
        showRemoveModal();
    })
    .catch(error => {
        console.error('Error:', error);
    });
}

function populateCart() {
    let cartItemsContainer =  document.getElementById('cart-items');
    let totalItems = document.getElementById('total-items');
    let totalItemsCounter = document.getElementById('total-items-counter');
    cartItemsContainer.innerHTML = '';
    fetch('/Cart/GetCartItems')
    .then(response => response.json())
    .then(data => {
        console.log(data);
        totalItems.innerHTML = data.cartItems.length + ' Items';
        totalItemsCounter.innerHTML = data.cartItems.length;
        const cartItems = data.cartItems;
        /*
 <li>
                                                    <a  onclick="removeFromCart($())"class="remove" title="Remove this item"><i
                                                            class="lni lni-close"></i></a>
                                                    <div class="cart-img-head">
                                                        <a class="cart-img" href="product-details.html"><img
                                                                src="~/Template/assets/images/header/cart-items/item1.jpg" alt="#"></a>
                                                    </div>

                                                    <div class="content">
                                                        <h4><a href="product-details.html">
                                                                Apple Watch Series 6</a></h4>
                                                        <p class="quantity">1x - <span class="amount">$99.00</span></p>
                                                    </div>
                                                </li>
        */
        cartItems.forEach(item => {
            const li = document.createElement('li');
            li.innerHTML = `
                <a onclick="removeFromCart(${item.productId})" class="remove" title="Remove this item"><i
                        class="lni lni-close"></i></a>
                <div class="cart-img-head">
                    <a class="cart-img" href="product-details.html"><img
                            src="${item.productImagePath}" alt="#"></a>
                </div>
                <div class="content">
                    <h4><a href="product-details.html">
                            ${item.productName}</a></h4>
                    <p class="quantity">${item.quantity}x - <span class="amount">${item.price}EGP</span></p>
                </div>
            `;
            cartItemsContainer.appendChild(li);
        });
    });
}
