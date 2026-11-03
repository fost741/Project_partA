using System.Collections.Generic;
using Project_partA.Enums;

namespace Project_partA.Classes
{
    public class Order
    {
        public string OrderId { get; set; }
        public List<Beverage> Items { get; set; } = new List<Beverage>();
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }

        public void AddItem(Beverage beverage)
        {
            throw new NotImplementedException();
        }

        public void CompleteOrder()
        {
            throw new NotImplementedException();
        }
    }
}
