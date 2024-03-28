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
            var newArrivals = db.SanPhams
                                .OrderByDescending(p => p.product_createAt)
                                .Take(4)
                                .ToList();

            // Lấy các mặt hàng có tổng số lượng bán nhiều nhất
            var topSellingItems = db.ChiTietDonHangs
                                    .GroupBy(d => d.od_product)
                                    .Select(g => new {
                                        od_product = g.Key,
                                        TongSoLuongBan = g.Sum(d => d.od_quantity)
                                    })
                                    .OrderByDescending(g => g.TongSoLuongBan)
                                    .Take(8) // Lấy 8 sản phẩm có tổng số lượng bán nhiều nhất
                                    .ToList();

            // Lấy ra danh sách các sản phẩm trong topSellingItems
            var topSellingProducts = (from item in topSellingItems
                                      join product in db.SanPhams
                                      on item.od_product equals product.product_id
                                      select product).ToList();

            var sanPhams = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.LoaiSanPham).Include(s => s.MauSac).Include(s => s.ThuongHieu).Take(12);

            ViewBag.topSellingProducts = topSellingProducts;
            ViewBag.newArrival = newArrivals;
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

        public ActionResult Shop(int? page)
        {
            int pageSize = 8; // Number of items per page
            int pageNumber = (page ?? 1);

            var sanPhams = db.SanPhams
                            .Include(s => s.ChatLieu)
                            .Include(s => s.LoaiSanPham)
                            .Include(s => s.MauSac)
                            .Include(s => s.ThuongHieu)
                            .OrderByDescending(s => s.product_createAt)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            int totalItems = db.SanPhams.Count();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.PageNumber = pageNumber;

            return View(sanPhams);
        }

        public ActionResult PriceSortingHighToLow(int? page)
        {
            int pageSize = 8; // Number of items per page
            int pageNumber = (page ?? 1);

            // Lấy danh sách sản phẩm và sắp xếp theo giá từ cao đến thấp
            var sanPhams = db.SanPhams
                            .Include(s => s.ChatLieu)
                            .Include(s => s.LoaiSanPham)
                            .Include(s => s.MauSac)
                            .Include(s => s.ThuongHieu)
                            .OrderByDescending(s => s.product_price)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            int totalItems = db.SanPhams.Count();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.PageNumber = pageNumber;

            Session["Sorting"] = "High to Low";

            return View("Shop", sanPhams);
        }

        public ActionResult PriceSortingLowToHigh(int? page)
        {
            int pageSize = 8; // Number of items per page
            int pageNumber = (page ?? 1);

            // Lấy danh sách sản phẩm và sắp xếp theo giá từ cao đến thấp
            var sanPhams = db.SanPhams
                            .Include(s => s.ChatLieu)
                            .Include(s => s.LoaiSanPham)
                            .Include(s => s.MauSac)
                            .Include(s => s.ThuongHieu)
                            .OrderBy(s => s.product_price)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            int totalItems = db.SanPhams.Count();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.PageNumber = pageNumber;

            Session["Sorting"] = "Low to High";

            return View("Shop", sanPhams);
        }

        public ActionResult AlphabetSortingAToZ(int? page)
        {
            int pageSize = 8; // Number of items per page
            int pageNumber = (page ?? 1);

            // Lấy danh sách sản phẩm và sắp xếp theo giá từ cao đến thấp
            var sanPhams = db.SanPhams
                            .Include(s => s.ChatLieu)
                            .Include(s => s.LoaiSanPham)
                            .Include(s => s.MauSac)
                            .Include(s => s.ThuongHieu)
                            .OrderBy(s => s.product_name)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            int totalItems = db.SanPhams.Count();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.PageNumber = pageNumber;

            Session["Sorting"] = "A to Z"; 

            return View("Shop", sanPhams);
        }

        public ActionResult AlphabetSortingZToA(int? page)
        {
            int pageSize = 8; // Number of items per page
            int pageNumber = (page ?? 1);

            // Lấy danh sách sản phẩm và sắp xếp theo giá từ cao đến thấp
            var sanPhams = db.SanPhams
                            .Include(s => s.ChatLieu)
                            .Include(s => s.LoaiSanPham)
                            .Include(s => s.MauSac)
                            .Include(s => s.ThuongHieu)
                            .OrderByDescending(s => s.product_name)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            int totalItems = db.SanPhams.Count();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.PageNumber = pageNumber;

            Session["Sorting"] = "Z to A";

            return View("Shop", sanPhams);
        }

        public ActionResult UserOrder(string cusID)
        {
            if (cusID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KhachHang khachHang = db.KhachHangs.Find(cusID);
            if (khachHang == null)
            {
                return HttpNotFound();
            }

            TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(k => k.account_name == khachHang.customer_account);

            if (taiKhoan == null)
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

        public ActionResult UserProfile(string cusID)
        {
            if (cusID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KhachHang khachHang = db.KhachHangs.Find(cusID);
            if (khachHang == null)
            {
                return HttpNotFound();
            }

            TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(k => k.account_name == khachHang.customer_account);

            if (taiKhoan == null)
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