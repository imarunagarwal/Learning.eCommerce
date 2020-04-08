using System;

namespace ProductWebApi.SharedLayer.Dtos
{
    public class CartCheckoutDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
