using System.Collections.Generic;

namespace Project_partA.Classes
{
    public class CoffeeNetwork
    {
        public string NetworkName { get; set; }
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
