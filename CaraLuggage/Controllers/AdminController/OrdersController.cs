using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaraLuggage.Controllers.StatePattern;
using CaraLuggage.Models;

namespace CaraLuggage.Controllers.AdminController
{
    public class OrdersController : Controller
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var donHangs = db.DonHangs.Include(d => d.KhachHang).Include(d => d.PhuongThucThanhToan).Include(d => d.NhanVien);
            return View(donHangs.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
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
            return View(donHang);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.order_customer = new SelectList(db.KhachHangs, "customer_id", "customer_name");
            ViewBag.order_payment = new SelectList(db.PhuongThucThanhToans, "payment_no", "payment_name");
            ViewBag.order_staff = new SelectList(db.NhanViens, "staff_id", "staff_name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_no,order_code,order_createAt,order_customer,order_staff,order_status,order_payment,order_totalPrice,order_modifiedAt")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_customer = new SelectList(db.KhachHangs, "customer_id", "customer_name", donHang.order_customer);
            ViewBag.order_payment = new SelectList(db.PhuongThucThanhToans, "payment_no", "payment_name", donHang.order_payment);
            ViewBag.order_staff = new SelectList(db.NhanViens, "staff_id", "staff_name", donHang.order_staff);
            return View(donHang);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.order_customer = new SelectList(db.KhachHangs, "customer_id", "customer_name", donHang.order_customer);
            ViewBag.order_payment = new SelectList(db.PhuongThucThanhToans, "payment_no", "payment_name", donHang.order_payment);
            ViewBag.order_staff = new SelectList(db.NhanViens, "staff_id", "staff_name", donHang.order_staff);
            ViewBag.order_status = new SelectList(new List<string>
            {
                "Chưa xác nhận",
                "Đã xác nhận",
                "Đang giao hàng",
                "Đã giao hàng"
            });
            return View(donHang);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_no,order_code,order_createAt,order_customer,order_staff,order_status,order_payment,order_totalPrice,order_modifiedAt")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                string currentAccount = Session["UserName"] as string;
                var nhanVien = db.NhanViens.FirstOrDefault(s => s.staff_account == currentAccount);
                db.Entry(donHang).State = EntityState.Modified;

                // Xác định trạng thái hiện tại của đơn hàng
                if (donHang.order_status == "Chưa xác nhận")
                {
                    donHang.changeState(new NotConfirmState());
                }
                else if (donHang.order_status == "Đã xác nhận")
                {
                    donHang.changeState(new ConfirmedState());
                }
                else if (donHang.order_status == "Đang giao hàng")
                {
                    donHang.changeState(new DeliveryState());
                }

                // Xử lý đơn hàng dựa trên trạng thái hiện tại
                donHang.ProcessOrder(donHang);

                donHang.order_modifiedAt = DateTime.Now;
                donHang.order_staff = nhanVien.staff_id;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_customer = new SelectList(db.KhachHangs, "customer_id", "customer_name", donHang.order_customer);
            ViewBag.order_payment = new SelectList(db.PhuongThucThanhToans, "payment_no", "payment_name", donHang.order_payment);
            ViewBag.order_staff = new SelectList(db.NhanViens, "staff_id", "staff_name", donHang.order_staff);
            return View(donHang);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
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
            return View(donHang);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);

            var chiTietDonHangs = db.ChiTietDonHangs.Where(d => d.od_orderno == donHang.order_no).ToList();

            foreach (var chiTiet in chiTietDonHangs)
            {
                db.ChiTietDonHangs.Remove(chiTiet);
            }

            db.DonHangs.Remove(donHang);
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
