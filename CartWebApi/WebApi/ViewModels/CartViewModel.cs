using System;
using System.Collections.Generic;

namespace CartWebApi.WebApi.ViewModels
{
    public class CartViewModel
    {
        public Guid CartId { get; set; }

        public Guid UserId { get; set; }

        public ICollection<ItemsViewModel> Items { get; set; }
    }
}
