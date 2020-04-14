using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.DataAccessLayer.Entities
{
    public class ProductEntity
    {
        [Key]
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string VendorName { get; set; }

        public string VendorDetails { get; set; }

        public string ImageUrl { get; set; }

        public float Price { get; set; }
    }
}
