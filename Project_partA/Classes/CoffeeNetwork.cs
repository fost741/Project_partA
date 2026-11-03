using System.Collections.Generic;

namespace Project_partA.Classes
{
    public class CoffeeNetwork
    {
        public string NetworkName
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
        public List<CoffeeKiosk> Kiosks { get; set; } = new List<CoffeeKiosk>();

        public void AddKiosk(CoffeeKiosk kiosk)
        {
            throw new NotImplementedException();
        }
        public decimal GetTotalRevenue()
        {
            throw new NotImplementedException();
        }
    }
}
