using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Controllers.ProxyPattern
{
    public class LoginProxy : ILoginProxy
    {
        private readonly CaraLuggageDBEntities db;

        public LoginProxy(CaraLuggageDBEntities dbContext)
        {
            db = dbContext;
        }

        public bool ValidateLogin(LoginInfo loginInfo)
        {
            // Ví dụ:
            var accountRegistered = db.TaiKhoans.Any(r => r.account_name == loginInfo.UserName && r.account_password == loginInfo.Password && r.account_status == true);

            // Trả về kết quả kiểm tra
            return accountRegistered;
        }

        public bool CheckUserRole(LoginInfo loginInfo)
        {
            // Check if the loginInfo is for a Student
            var isCustomer = db.KhachHangs.Any(u => u.customer_account == loginInfo.UserName);

            return isCustomer;
        }
    }
}