using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Models
{
    public class Cart
    {
        public string productID { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }
        public int productQuantity { get; set; }
        public string productImage { get; set; }

        public Cart(string productId, string productName, double productPrice, int productQuantity, string productImage)
        {
            this.productID = productId;
            this.productName = productName;
            this.productPrice = productPrice;
            this.productQuantity = productQuantity;
            this.productImage = productImage;
        }
    }
}