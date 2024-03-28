using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaraLuggage.Controllers.StatePattern
{
    public interface OrderState
    {
        void ProcessOrder(DonHang donHang);
    }
}
