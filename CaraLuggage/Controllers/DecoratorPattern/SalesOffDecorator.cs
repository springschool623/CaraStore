using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Controllers.DecoratorPattern
{
    public abstract class SalesOffDecorator : ISalesOff
    {
        protected ISalesOff msalesOff;

        public SalesOffDecorator(ISalesOff salesOff)
        {
            msalesOff = salesOff;
        }

        public abstract double GetSalesPrice();
    }
}