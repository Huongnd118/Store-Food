
using Microsoft.AspNetCore.Mvc;

namespace Store_Food.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public void AddItem(Food food, int quantity)
        {
            CartLine? line = Lines
                 .Where(p => p.Food.FoodId == food.FoodId).FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Food = food,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Food food) =>
            Lines.RemoveAll(l => l.Food.FoodId == food.FoodId);
        public decimal ComputeTotalValues()
        {
            return (decimal)Lines.Sum(e => e.Food?.FoodPrice * e.Quantity);
        }
        public void Clear() => Lines.Clear();
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Food Food { get; set; } = new();
        public int Quantity { get; set; }
    }
}
