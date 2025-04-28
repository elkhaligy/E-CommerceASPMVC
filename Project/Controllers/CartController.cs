using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Project.Contract;
using Project.Models;
using Project.DTO;
using System.Security.Authentication;

namespace Project.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public CartController(ICustomerRepository customerRepository)
        {
            // I only need the customer repository, with its navigational properties and ef tracking I can access everything I need
            _customerRepository = customerRepository;
        }
        public async Task<IActionResult> GetCartItems()
        {
            Customer customer = await _customerRepository.GetByEmailAsync(User.FindFirst(ClaimTypes.Email)!.Value)! ?? throw new Exception("Customer not found");
            CartDTO cartDto = new CartDTO
            {
                CartId = customer.Cart.CartId,
                CartItems = customer.Cart.CartItems.Select(ci => new CartItemDTO 
                {
                    CartItemId = ci.CartItemId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ProductName = ci.Product.Name,
                    ProductImagePath = ci.Product.Images.FirstOrDefault()?.ImagePath ?? null,
                    Price = ci.Product.Price
                }).ToList(),
                CreatedAt = customer.Cart.CreatedAt,
                UpdatedAt = customer.Cart.UpdatedAt
            };
            return Ok(cartDto);
        }
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // First I need to know which customer is requesting
            Customer customer = await _customerRepository.GetByEmailAsync(User.FindFirst(ClaimTypes.Email)!.Value)! ?? throw new Exception("Customer not found");
            // this customer is a proxy class and if I used any virtual navigational property
            // ef will add it to the tracked entities
            // with one repository I can access EVERYTHING I need

            var cart = customer.Cart;
            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customer.Id,
                    CartItems = new List<CartItem>(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };
                customer.Cart = cart;
            }
            // Check if the product already exists in the cart
            var existingItems = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId) ?? null;
            if (existingItems != null)
            {
                existingItems.Quantity += 1;
            }
            else
            {
                CartItem cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                cart.CartItems!.Add(cartItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _customerRepository.SaveChangesAsync();
            return Ok(new { status = "Added"});
        }

        public async Task<IActionResult> RemoveFromCart(int productId, int quantity)
        {
            Customer? customer = await _customerRepository.GetByEmailAsync(User.FindFirst(ClaimTypes.Email)!.Value);
            if (customer == null)
            {
                return NotFound(new {message = "Customer not found"}); // This is impossible
            }

            var cart = customer.Cart;
            if (cart == null)
            {
                return NotFound(new {message = "Cart not found"});
            }

            var cartItem = cart.CartItems!.FirstOrDefault(cartItem => cartItem.ProductId == productId);
            if (cartItem == null)
            {
                return NotFound(new {message = "Item not found in cart"});
            }
            // Remove that reference from CartItems list which is a proxy itself so .Remove method 
            // is a custom implemented one that change the tracking state of that reference
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            else
            {
                cart.CartItems!.Remove(cartItem);
            }
            cart.UpdatedAt = DateTime.UtcNow;
            await _customerRepository.SaveChangesAsync();
            return Ok(new {message = "Removed Item Successfully"});
        }
    }
}