using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaraLuggage.Models;

namespace QuanLyShopBanVali.Controllers.AdminController
{
    public class ProductsController : Controller
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        // GET: Products
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.LoaiSanPham).Include(s => s.MauSac).Include(s => s.ThuongHieu);
            return View(sanPhams.ToList());
        }

        public static string GenerateRandomProductID()
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.product_material = new SelectList(db.ChatLieux, "material_id", "material_name");
            ViewBag.product_category = new SelectList(db.LoaiSanPhams, "category_id", "category_name");
            ViewBag.product_color = new SelectList(db.MauSacs, "color_id", "color_name");
            ViewBag.product_brand = new SelectList(db.ThuongHieux, "brand_id", "brand_name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,product_color,product_origin,product_size,product_price,product_image,product_material,product_quantity,product_createAt,product_status,product_category,product_brand")] SanPham sanPham)
        {
            string newProductID;

            do
            {
                newProductID = GenerateRandomProductID();
            }
            while (db.SanPhams.Any(p => p.product_id == newProductID));

            sanPham.product_id = newProductID;

            if (ModelState.IsValid)
            {
                var urlTuongDoi = "/Content/assets/product-img/";

                sanPham.product_image = urlTuongDoi + sanPham.product_image;

                sanPham.product_createAt = DateTime.Now;

                if (sanPham.product_quantity > 0)
                {
                    sanPham.product_status = true;
                }
                else
                {
                    sanPham.product_status = false;
                }

                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_material = new SelectList(db.ChatLieux, "material_id", "material_name", sanPham.product_material);
            ViewBag.product_category = new SelectList(db.LoaiSanPhams, "category_id", "category_name", sanPham.product_category);
            ViewBag.product_color = new SelectList(db.MauSacs, "color_id", "color_name", sanPham.product_color);
            ViewBag.product_brand = new SelectList(db.ThuongHieux, "brand_id", "brand_name", sanPham.product_brand);
            return View(sanPham);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_material = new SelectList(db.ChatLieux, "material_id", "material_name", sanPham.product_material);
            ViewBag.product_category = new SelectList(db.LoaiSanPhams, "category_id", "category_name", sanPham.product_category);
            ViewBag.product_color = new SelectList(db.MauSacs, "color_id", "color_name", sanPham.product_color);
            ViewBag.product_brand = new SelectList(db.ThuongHieux, "brand_id", "brand_name", sanPham.product_brand);
            return View(sanPham);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,product_color,product_origin,product_size,product_price,product_image,product_material,product_quantity,product_createAt,product_status,product_category,product_brand")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                var urlTuongDoi = "/Content/assets/product-img/";

                sanPham.product_image = urlTuongDoi + sanPham.product_image;

                sanPham.product_createAt = DateTime.Now;

                if (sanPham.product_quantity > 0)
                {
                    sanPham.product_status = true;
                }
                else
                {
                    sanPham.product_status = false;
                }

                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_material = new SelectList(db.ChatLieux, "material_id", "material_name", sanPham.product_material);
            ViewBag.product_category = new SelectList(db.LoaiSanPhams, "category_id", "category_name", sanPham.product_category);
            ViewBag.product_color = new SelectList(db.MauSacs, "color_id", "color_name", sanPham.product_color);
            ViewBag.product_brand = new SelectList(db.ThuongHieux, "brand_id", "brand_name", sanPham.product_brand);
            return View(sanPham);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
