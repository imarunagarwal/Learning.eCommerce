using System;

namespace ProductWebApi.WebApi.ViewModels
{
    public class CartCheckoutViewModel
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
