using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaraLuggage.Controllers.ProxyPattern
{
    public interface ILogin
    {
        bool ValidateLogin(LoginInfo loginInfo);
        bool CheckUserRole(LoginInfo loginInfo);
    }
}
