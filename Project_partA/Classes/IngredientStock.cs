using System.Collections.Generic;

namespace Project_partA.Classes
{
    public class IngredientStock
    {
        public Dictionary<string, int> IngredientsMap { get; set; } = new Dictionary<string, int>();

        public bool CheckAndDeduct(Dictionary<string, int> required)
        {
            throw new NotImplementedException();
        }

        public void RefillStock(string item, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
