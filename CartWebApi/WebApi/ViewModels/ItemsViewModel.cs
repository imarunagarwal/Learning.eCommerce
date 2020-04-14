using System;

namespace CartWebApi.WebApi.ViewModels
{
    public class ItemsViewModel
    {
        public Guid ItemId { get; set; }

        public Guid CartId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
