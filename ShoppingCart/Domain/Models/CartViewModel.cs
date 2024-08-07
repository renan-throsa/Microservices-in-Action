﻿namespace ShoppingCart.Domain.Models
{
    public record CartViewModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        private readonly HashSet<CartItemViewModel> items = new();

        public IEnumerable<CartItemViewModel> Items => items;

    }
}
