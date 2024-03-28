using QuanLyShopBanVali.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Controllers.CommandPattern
{
    public class AddToCartCommand : ICommand
    {
        private readonly CartReceiver _receiver;
        private readonly string _productId;

        public AddToCartCommand(CartReceiver receiver, string productId)
        {
            _receiver = receiver;
            _productId = productId;
        }


        public void Execute()
        {
            _receiver.AddToCart(_productId);
        }
    }
}