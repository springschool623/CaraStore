using CaraLuggage.Controllers.ProxyPattern;
using CaraLuggage.Models;
using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Web;
using System.Web.Mvc;
using GoogleAuthentication.Services;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using CaraLuggage.Controllers.Singleton;
using System.Web.Helpers;
using System.Xml.Linq;

namespace QuanLyShopBanVali.Controllers
{
    public class LoginRegisterController : Controller
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        // GET: LoginRegister
        public ActionResult LoginSection()
        {
            var socialService = SocialAuthService.Instance;

            ViewBag.Url = socialService.GetFacebookLoginUrl("https://localhost:44357/FacebookRedirect");

            ViewBag.repsone = socialService.GetGoogleAuthUrl("https://localhost:44357/dang-nhap-google");

            return View();
        }


        public ActionResult RegisterSection()
        {
            return View();
        }

        //Lấy các thông tin Facebook
        public async Task<ActionResult> FacebookRedirect(string code, TaiKhoan taiKhoan, KhachHang khachHang)
        {
            var socialService = SocialAuthService.Instance;

            var result = await socialService.GetFacebookLoginDetails(code, "https://localhost:44357/FacebookRedirect");
            string name = result.name;
            string email = result.email;

            string newCustomerID;

            do
            {
                newCustomerID = GenerateRandomCustomerID();
            }

            while (db.KhachHangs.Any(p => p.customer_id == newCustomerID));

            taiKhoan.account_name = email;

            KhachHang checkCus = db.KhachHangs.FirstOrDefault(c => c.customer_account == email);

            if (checkCus != null)
            {
                KhachHang existedCus = db.KhachHangs.FirstOrDefault(k => k.customer_id == checkCus.customer_id);

                if (existedCus == null)
                {
                    khachHang.customer_id = newCustomerID;

                    khachHang.customer_name = name;

                    khachHang.customer_account = email;

                    db.KhachHangs.Add(khachHang);
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    Session["CusID"] = khachHang.customer_id;
                }

                Session["CusID"] = existedCus.customer_id;
                Session["isCustomer"] = "isCustomer";
                Session["Username"] = email;
                Session["LoginFB_GG"] = true;
            }

            else
            {
                khachHang.customer_id = newCustomerID;

                khachHang.customer_name = name;

                khachHang.customer_account = email;

                db.KhachHangs.Add(khachHang);
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                Session["CusID"] = khachHang.customer_id;
                Session["isCustomer"] = "isCustomer";
                Session["Username"] = email;
                Session["LoginFB_GG"] = true;
            }

            // Chuyển hướng đến trang chính
            return RedirectToAction("Index", "Home");
        }


        //Lấy các thông tin từ Google
        public async Task<ActionResult> GoogleLoginCallBack(string code, TaiKhoan taiKhoan, KhachHang khachHang)
        {
            var socialService = SocialAuthService.Instance;

            var result = await socialService.GetUserProfileFromGoogle(code);

            string name = result.Name;
            string email = result.Email;

            string newCustomerID;

            do
            {
                newCustomerID = GenerateRandomCustomerID();
            }

            while (db.KhachHangs.Any(p => p.customer_id == newCustomerID));
            taiKhoan.account_name = email;

            KhachHang checkCus = db.KhachHangs.FirstOrDefault(c => c.customer_account == email);

            if (checkCus != null)
            {
                KhachHang existedCus = db.KhachHangs.FirstOrDefault(k => k.customer_id == checkCus.customer_id);

                if (existedCus == null)
                {
                    khachHang.customer_id = newCustomerID;
                    khachHang.customer_name = name;

                    khachHang.customer_account = email;

                    db.KhachHangs.Add(khachHang);
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    Session["CusID"] = khachHang.customer_id;
                }

                Session["CusID"] = existedCus.customer_id;
                Session["isCustomer"] = "isCustomer";
                Session["Username"] = email;
                Session["LoginFB_GG"] = true;
            }

            else
            {
                khachHang.customer_id = newCustomerID;
                khachHang.customer_name = name;

                khachHang.customer_account = email;

                db.KhachHangs.Add(khachHang);
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                Session["CusID"] = khachHang.customer_id;
                Session["isCustomer"] = "isCustomer";
                Session["Username"] = email;
                Session["LoginFB_GG"] = true;
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LoginSection(LoginInfo loginInfo)
        {
            ILogin loginProxy = new LoginProxy();

            if (loginProxy.ValidateLogin(loginInfo))
            {
                if (loginProxy.CheckUserRole(loginInfo))
                {
                    // Set the "UserCode" session variable to the user code
                    Session["UserName"] = loginInfo.UserName;

                    KhachHang khachHang = db.KhachHangs.FirstOrDefault(c => c.customer_account == loginInfo.UserName);

                    Session["CusID"] = khachHang.customer_id;

                    Session["isCustomer"] = "isCustomer";

                    return RedirectToAction("Index", "Home");
                }
                else if (!loginProxy.CheckUserRole(loginInfo))
                {
                    Session["UserName"] = loginInfo.UserName;

                    return RedirectToAction("Index", "AdminDashboard");
                }
                else
                {
                    // Invalid login, return to the login page with an error message
                    ModelState.AddModelError("", "Invalid account name or password");

                    // Return the loginInfo object to repopulate the form with user-entered values
                    return View("LoginSection", loginInfo);
                }
            }
            else
            {
                // Invalid login, return to the login page with an error message
                ModelState.AddModelError("", "Account has been banned!!!");

                // Return the loginInfo object to repopulate the form with user-entered values
                return RedirectToAction("LoginSection", loginInfo);
            }
        }


        public static string GenerateRandomCustomerID()
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

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterSection([Bind(Include = "customer_id,customer_name,customer_phone,customer_address,customer_account")] KhachHang khachHang, 
            [Bind(Include = "account_name,account_password,account_createAt,account_status")] TaiKhoan taiKhoan)
        {
            string newCustomerID;

            do
            {
                newCustomerID = GenerateRandomCustomerID();
            }
            while (db.KhachHangs.Any(p => p.customer_id == newCustomerID));

            khachHang.customer_id = newCustomerID;

            if (ModelState.IsValid)
            {
                taiKhoan.account_createAt = DateTime.Now;
                taiKhoan.account_status = true;

                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();

                khachHang.customer_account = taiKhoan.account_name;
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home", khachHang);
        }

        public ActionResult Logout()
        {
            // Clear session
            Session.Clear();

            // Redirect to the login page
            return RedirectToAction("Index","Home");
        }
    }
}