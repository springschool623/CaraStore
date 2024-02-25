using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraLuggage.Models;
using System.Net;
using System.Data.Entity;


namespace QuanLyShopBanVali.Controllers
{
    public class HomeController : Controller
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        public ActionResult Index()
        {
            // Lấy danh sách sản phẩm và thương hiệu
            var products = db.SanPhams.ToList();
            var brands = db.ThuongHieux.ToList();

            // Tạo một Dictionary để lưu trữ brand_name tương ứng với mỗi brand_id
            Dictionary<int, string> brandNames = new Dictionary<int, string>();

            // Duyệt qua danh sách thương hiệu và thêm vào Dictionary
            foreach (var brand in brands)
            {
                brandNames.Add(brand.brand_id, brand.brand_name);
            }

            // Thêm dữ liệu vào ViewBag
            ViewBag.Products = products;
            ViewBag.BrandNames = brandNames;

            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult UserOrder(string accountName)
        {
            if (accountName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaiKhoan taiKhoan = db.TaiKhoans.Find(accountName);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }

            KhachHang khachHang = db.KhachHangs.FirstOrDefault(k => k.customer_account == taiKhoan.account_name);

            if (khachHang == null)
            {
                return HttpNotFound();
            }

            var donHang = db.DonHangs.Where(d => d.order_customer == khachHang.customer_id).ToList();

            if (donHang == null)
            {
                return HttpNotFound();
            }

            return View(donHang);
           
        }

        public ActionResult UserProfile(string accountName)
        {
            if (accountName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaiKhoan taiKhoan = db.TaiKhoans.Find(accountName);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }

            KhachHang khachHang = db.KhachHangs.FirstOrDefault(k => k.customer_account == taiKhoan.account_name);

            if (khachHang == null)
            {
                return HttpNotFound();
            }

            return View(khachHang);
        }

    }
}