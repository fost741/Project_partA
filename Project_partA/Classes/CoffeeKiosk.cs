using Project_partA.Enums;

namespace Project_partA.Classes
{
    public class CoffeeKiosk
    {
        public int KioskId { get; set; }
        public string Address { get; set; }
        public IngredientStock Stock { get; set; }
        public KioskStatus Status { get; set; }

        public void TurnOn()
        {
            throw new NotImplementedException();
        }

        public void ProcessOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
