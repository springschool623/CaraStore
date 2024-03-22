using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Dao
{
    public class UserFacebook
    {
        private readonly CaraLuggageDBEntities db;

        public string InsertForFacebook(LoginInfo loginInfo)
        {
            var user = db.TaiKhoans.SingleOrDefault(x => x.account_name == loginInfo.UserName);
            if (user == null)
            {
                var newTaiKhoan = new TaiKhoan
                {
                    account_name = loginInfo.UserName,
                };
                db.TaiKhoans.Add(newTaiKhoan);

                db.SaveChanges();

                return loginInfo.UserName;
            }
            else
            {
                return null;
            }
        }
    }
}