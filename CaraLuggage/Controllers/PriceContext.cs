using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Controllers
{
    public class PriceContext
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();

        public double CalculatePrice(double originalPrice)
        {
            originalPrice = (double)db.SanPhams.FirstOrDefault().product_price;
            return originalPrice;
        }
    }
}