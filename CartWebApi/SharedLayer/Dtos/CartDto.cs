using System;
using System.Collections.Generic;

namespace CartWebApi.SharedLayer.Dtos
{
    public class CartDto
    {
        public Guid CartId { get; set; }

        public Guid UserId { get; set; }

        public ICollection<ItemsDto> Items { get; set; }

        public bool IsCheckedOut { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
