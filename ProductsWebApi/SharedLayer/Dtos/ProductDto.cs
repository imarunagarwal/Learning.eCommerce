using System;

namespace ProductWebApi.SharedLayer.Dtos
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string VendorName { get; set; }

        public string VendorDetails { get; set; }

        public string ImageUrl { get; set; }
    }
}
