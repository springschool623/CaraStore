using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaraLuggage.Controllers.CommandPattern
{
    public class RemoveFromCartCommand : ICommand
    {
        private readonly CartReceiver _receiver;
        private readonly string _productId;

        public RemoveFromCartCommand(CartReceiver receiver, string productId)
        {
            _receiver = receiver;
            _productId = productId;
        }

        public void Execute()
        {
            _receiver.RemoveFromCart(_productId);
        }
    }
}