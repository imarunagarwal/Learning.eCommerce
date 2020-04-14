using System;
using System.ComponentModel.DataAnnotations;

namespace CartWebApi.DataAccessLayer.Entities
{
    public class ItemsEntity
    {
        [Key]
        public Guid ItemId { get; set; }

        public Guid CartId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
