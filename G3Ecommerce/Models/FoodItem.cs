﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G3Ecommerce.Models
{
    public class FoodItem
    {
        public FoodItem() { }

        public FoodItem(int id, string itemName, decimal price, string itemDescription, string itemImage)
        {
            this.Id = id;
            this.ItemName = itemName;
            this.Price = price;
            this.ItemDescription = itemDescription;
            this.ItemImage = itemImage;
        }

        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string ItemDescription { get; set; }

        public string ItemImage { get; set; }

        public string Display()
        {
            string displayString = string.Format("{0} ({1})",
                ItemName,
                Price.ToString("c")
            );
            return displayString;
        }
    }
}