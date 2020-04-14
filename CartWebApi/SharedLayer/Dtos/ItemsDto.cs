using System;

namespace CartWebApi.SharedLayer.Dtos
{
    public class ItemsDto
    {
        public Guid ItemId { get; set; }

        public Guid CartId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
