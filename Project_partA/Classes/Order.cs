using System.Collections.Generic;
using Project_partA.Enums;

namespace Project_partA.Classes
{
    public class Order
    {
        public string OrderId
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
        public List<Beverage> Items { get; set; } = new List<Beverage>();
        public decimal TotalPrice
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
        public OrderStatus Status
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

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
