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
            var sanPhams = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.LoaiSanPham).Include(s => s.MauSac).Include(s => s.ThuongHieu);
            return View(sanPhams.ToList());
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
            var sanPhams = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.LoaiSanPham).Include(s => s.MauSac).Include(s => s.ThuongHieu);
            return View(sanPhams.ToList());
        }

        public ActionResult PriceSortingHighToLow()
        {
            // Lấy danh sách sản phẩm và sắp xếp theo giá từ cao đến thấp
            var sanPhams = db.SanPhams.OrderByDescending(s => s.product_price).ToList();

            return View("Shop", sanPhams);
        }

        public ActionResult PriceSortingLowToHigh()
        {
            // Lấy danh sách sản phẩm và sắp xếp theo giá từ cao đến thấp
            var sanPhams = db.SanPhams.OrderBy(s => s.product_price).ToList();

            return View("Shop", sanPhams);
        }

        public ActionResult AlphabetSortingAToZ()
        {
            // Lấy danh sách sản phẩm và sắp xếp theo giá từ cao đến thấp
            var sanPhams = db.SanPhams.OrderBy(s => s.product_name).ToList();

            return View("Shop", sanPhams);
        }

        public ActionResult AlphabetSortingZToA()
        {
            // Lấy danh sách sản phẩm và sắp xếp theo giá từ cao đến thấp
            var sanPhams = db.SanPhams.OrderByDescending(s => s.product_name).ToList();


            return View("Shop", sanPhams);
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

        public ActionResult UserOrderDetail(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }

            var chiTietDonHang = db.ChiTietDonHangs.Where(d => d.od_orderno == id).ToList();

            ViewBag.OrderDetail = chiTietDonHang;

            return View(donHang);
        }


    }
}