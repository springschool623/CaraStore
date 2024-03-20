using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Controllers.DecoratorPattern
{
    public class BlackFridaySalesDecorator : SalesOffDecorator
    {
        public BlackFridaySalesDecorator(ISalesOff sales) : base(sales)
        {
        }

        public override double GetSalesPrice()
        {
            double priceBefore = msalesOff.GetSalesPrice();
            double saleRate = 0.1;
            double priceAfter = priceBefore * saleRate;

            return priceBefore - priceAfter;
        }
    }
}