// I want to write a function that takes the proudct id to add to the cart, and the quantity to add and then it makes a POST request to an endpoint
// I will make a post reuqest to /Cart/AddToCart so I need a cart controller with AddToCart action
document.addEventListener('DOMContentLoaded', () => {
    populateCart();
});

function addToCart(productId) {
    fetch('/Cart/AddToCart?productId=' + productId, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        populateCart();
        alert('Item added to cart');
        
    })
    .catch(error => {
        console.error('Error:', error);
    });

}

function removeFromCart(productId) {
    fetch('/Cart/RemoveFromCart?productId=' + productId, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        populateCart();
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
            <li>
                <a onclick="removeFromCart(${item.productId})" class="remove" title="Remove this item"><i
                        class="lni lni-close"></i></a>
                <div class="cart-img-head">
                    <a class="cart-img" href="product-details.html"><img
                            src="/Template/assets/images/header/cart-items/item1.jpg" alt="#"></a>
                </div>
                <div class="content">
                    <h4><a href="product-details.html">
                            ${item.productName}</a></h4>
                    <p class="quantity">${item.quantity}x - <span class="amount">${item.price}</span></p>
                </div>
            `;
            cartItemsContainer.appendChild(li);
        });
    });
}
