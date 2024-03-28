using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaraLuggage.Controllers.CommandPattern
{
    public class CartReceiver : Controller
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();
        private List<Cart> cartItems =  new List<Cart>();

        public ActionResult AddToCart(string productId)
        {
            SanPham sanPham = db.SanPhams.Find(productId);

            if (sanPham.product_quantity == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Retrieve cart items from session
                List<Cart> cartItems = System.Web.HttpContext.Current.Session["CartItems"] as List<Cart> ?? new List<Cart>();

                // Check if the product is already in the cart
                Cart existingItem = cartItems.FirstOrDefault(item => item.productID == productId);

                if (existingItem != null)
                {
                    // If the product is already in the cart, increment the quantity
                    existingItem.productQuantity += 1;
                }
                else
                {

                    // If the product is not in the cart, add a new cart item
                    Cart cartItem = new Cart(sanPham.product_id, sanPham.product_name, (double)sanPham.product_price, 1, sanPham.product_image);
                    cartItems.Add(cartItem);
                }

                // Save the updated cart items to session
                System.Web.HttpContext.Current.Session["CartItems"] = cartItems;

                // Calculate the total price
                double total = cartItems.Sum(item => item.productPrice * item.productQuantity);

                // Pass the total to the view
                ViewBag.Total = total;

                // Return a view to display the total
                return View("Index", cartItems);
            }
            
        }

        public ActionResult RemoveFromCart(string productId)
        {
            // Retrieve cart items from session
            List<Cart> cartItems = System.Web.HttpContext.Current.Session["CartItems"] as List<Cart>;

            // Find the item to be removed
            Cart itemToRemove = cartItems.FirstOrDefault(item => item.productID == productId);

            if (itemToRemove != null)
            {
                // If the quantity is greater than 1, decrement the quantity
                if (itemToRemove.productQuantity > 1)
                {
                    itemToRemove.productQuantity -= 1;
                }
                else
                {
                    // If the quantity is 1 or less, remove the item from the cart
                    cartItems.Remove(itemToRemove);
                }

                // Save the updated cart items to session
                System.Web.HttpContext.Current.Session["CartItems"] = cartItems;
            }

            // Calculate the total price
            double total = cartItems.Sum(item => item.productPrice * item.productQuantity);

            // Pass the total to the view
            ViewBag.Total = total;

            // Return a view to display the total
            return View("Index", cartItems);
        }

    }
}