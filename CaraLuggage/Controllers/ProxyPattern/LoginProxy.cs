using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Controllers.ProxyPattern
{
    public class LoginProxy : ILogin
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        private LoginService _login;

        public LoginProxy()
        {
            _login = new LoginService(db);
        }

        public bool ValidateLogin(LoginInfo loginInfo)
        {
            return _login.ValidateLogin(loginInfo);
        }

        public bool CheckUserRole(LoginInfo loginInfo)
        {
            return _login.CheckUserRole(loginInfo);
        }
    }
}