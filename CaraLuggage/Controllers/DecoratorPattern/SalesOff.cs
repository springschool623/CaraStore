using CaraLuggage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CaraLuggage.Controllers.DecoratorPattern
{
    public class SalesOff : ISalesOff
    {
        private CaraLuggageDBEntities db = new CaraLuggageDBEntities();
        public double _price;

        public SalesOff() { }
        public SalesOff(string _productId)
        {
            var sanPham = db.SanPhams.FirstOrDefault(p => p.product_id == _productId);
            _price = (double)sanPham.product_price;
        }

        public double GetSalesPrice()
        {
            return _price;
        }
    }
}