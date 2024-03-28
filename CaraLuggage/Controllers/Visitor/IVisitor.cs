using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaraLuggage.Controllers.Visitor
{
    public interface IVisitor
    {
        void VisitDonHang(DonHang donHang);
    }
}
