using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaraLuggage.Models;

namespace QuanLyShopBanVali.Controllers
{
    public class ProductDetailController : Controller
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        // GET: ProductDetail
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham product = db.SanPhams.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var brands = db.ThuongHieux.ToList();

            // Tạo một Dictionary để lưu trữ brand_name tương ứng với mỗi brand_id
            Dictionary<int, string> brandNames = new Dictionary<int, string>();

            // Duyệt qua danh sách thương hiệu và thêm vào Dictionary
            foreach (var brand in brands)
            {
                brandNames.Add(brand.brand_id, brand.brand_name);
            }

            ViewBag.BrandNames = brandNames;

            // Lấy danh sách sản phẩm
            var relatedProduct = db.SanPhams.Where(p => p.product_brand == product.product_brand || p.product_category == product.product_category).Take(4).ToList();

            ViewBag.relatedProduct = relatedProduct;

            var productCates = db.SanPhams.Where(p => p.product_category == product.product_category).ToList();

            ViewBag.sameCate = productCates;

            return View(product);
        }
    }
}