using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CaraLuggage.Controllers.DecoratorPattern
{
    public class WinterSalesDecorator : SalesOffDecorator
    {
        public WinterSalesDecorator(ISalesOff sales) : base(sales)
        {
        }

        public override double GetSalesPrice()
        {
            double priceBefore = msalesOff.GetSalesPrice();
            double saleRate = 0.2;
            double priceAfter = priceBefore * saleRate;

            return priceBefore - priceAfter;
        }
    }
}