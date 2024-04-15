using G3Ecommerce.Models;
using System;

namespace G3Ecommerce.Models
{
    public class CartItem
    {
        public CartItem() { }

        public CartItem(FoodItem foodItem, int quantity)
        {
            this.FoodItem = foodItem;
            this.Quantity = quantity;
        }

        public FoodItem FoodItem { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }

        public void AddQuantity(int quantity)
        {
            this.Quantity += quantity;
        }

        public decimal CalculateTotalPrice()
        {
            return FoodItem.Price * Quantity;
        }

        public string Display()
        {
            string displayString = string.Format("{0} ({1} at {2})",
                FoodItem.ItemName,
                Quantity.ToString(),
                CalculateTotalPrice().ToString("c")
            );
            return displayString;
        }
    }
}
