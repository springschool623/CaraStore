using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyShopBanVali.Controllers
{
    public class CartController : Controller
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        // GET: Cart
        public ActionResult Index()
        {
            List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();

            return View(cartItems);
        }

        public ActionResult AddToCart(string productId)
        {
            // Check if the user is logged in (Session["Username"] is not null)
            if (Session["Username"] != null)
            {
                SanPham sanPham = db.SanPhams.Find(productId);

                if (sanPham.product_quantity == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Retrieve cart items from session
                    List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();

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
                    Session["CartItems"] = cartItems;

                    // Calculate the total price
                    double total = cartItems.Sum(item => item.productPrice * item.productQuantity);

                    // Pass the total to the view
                    ViewBag.Total = total;

                    // Return a view to display the total
                    return View("Index", cartItems);
                }
            }
            else
            {
                // Redirect to a login page or show a message indicating that the user needs to log in.
                return RedirectToAction("LoginSection", "LoginRegister"); // Change the action and controller names accordingly.
            }
        }

        public ActionResult RemoveFromCart(string productId)
        {
            // Retrieve cart items from session
            List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();

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
                Session["CartItems"] = cartItems;
            }

            // Calculate the total price
            double total = cartItems.Sum(item => item.productPrice * item.productQuantity);

            // Pass the total to the view
            ViewBag.Total = total;

            // Return a view to display the total
            return View("Index", cartItems);
        }

        public ActionResult TotalCart()
        {
            // Retrieve cart items from session
            List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();

            if (cartItems == null)
            {
                ViewBag.Total = 0;
                return View("Index");
            }

            // Calculate the total price
            double total = cartItems.Sum(item => item.productPrice * item.productQuantity);

            // Pass the total to the view
            ViewBag.Total = total;

            // Return a view to display the total
            return View("Index", cartItems);
        }

        public ActionResult PayForProductView()
        {
            List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();

            if (cartItems == null)
            {
                ViewBag.Total = 0;
                return View("Index");
            }

            // Calculate the total price
            double total = cartItems.Sum(item => item.productPrice * item.productQuantity);

            // Pass the total to the view
            ViewBag.Total = total;

            // Lấy danh sách sản phẩm và thương hiệu
            var payments = db.PhuongThucThanhToans.ToList();

            ViewBag.Payment = payments;

            return View(cartItems);
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PayConfirm([Bind(Include = "order_no,order_code,order_createAt,order_staff,order_status,order_payment,order_totalPrice")] DonHang donHang,
            [Bind(Include = "orderdetail_id,od_product,od_quantity,od_price,od_orderno")] ChiTietDonHang chiTietDonHang)
        {
            List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();

            string accessUser = Session["UserName"] as string;

            if (accessUser == null)
            {
                return RedirectToAction("LoginSection", "LoginRegister");
            }
            KhachHang khachHang = db.KhachHangs.FirstOrDefault(c => c.customer_account == accessUser);
            donHang.order_customer = khachHang.customer_id;
            string newOrderCode;

            do
            {
                newOrderCode = GenerateRandomOrderCode();
            }
            while (db.DonHangs.Any(p => p.order_code == newOrderCode));

            donHang.order_code = newOrderCode;

            donHang.order_createAt = DateTime.Now;
            donHang.order_status = "Chưa xác nhận";


            donHang.order_totalPrice = cartItems.Sum(item => item.productPrice * item.productQuantity);

            db.DonHangs.Add(donHang);
            db.SaveChanges();

            foreach(var cartItem in cartItems)
            {
                SanPham sanPham = db.SanPhams.FirstOrDefault(p => p.product_id == cartItem.productID);

                chiTietDonHang.od_product = cartItem.productID;
                chiTietDonHang.od_quantity = cartItem.productQuantity;
                chiTietDonHang.od_price = sanPham.product_price * cartItem.productQuantity;
                chiTietDonHang.od_orderno = donHang.order_no;

                db.ChiTietDonHangs.Add(chiTietDonHang);
                db.SaveChanges();
            }

            Session["CartItems"] = null;

            return RedirectToAction("OrderSuccess");
        }

        public static string GenerateRandomOrderCode()
        {
            // Tạo một đối tượng Random
            Random random = new Random();

            // Tạo một mảng chứa các ký tự chữ cái và số
            char[] chars = new char[] {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
                'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
                'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7',
                '8', '9'
            };

            // Tạo một chuỗi rỗng
            string code = "";

            // Lặp lại cho đến khi chuỗi có độ dài mong muốn
            for (int i = 0; i < 10; i++)
            {
                // Chọn ngẫu nhiên một ký tự từ mảng
                int index = random.Next(chars.Length);
                char c = chars[index];

                // Thêm ký tự vào chuỗi
                code += c;
            }

            return code;
        }

    }
}