using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Controllers.StatePattern
{
    public class NotConfirmState : OrderState
    {
        private DonHang donHang;
        public void SetDonHang(DonHang donHang)
        {
            this.donHang = donHang;
        }

        public void ProcessOrder(DonHang donHang)
        {
            donHang.order_status = "Đã xác nhận";
        }
    }
}