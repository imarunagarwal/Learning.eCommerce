using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartWebApi.DataAccessLayer.Entities
{
    public class CartEntity
    {
        [Key]
        public Guid CartId { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<ItemsEntity> Items { get; set; }

        public bool IsCheckedOut { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
