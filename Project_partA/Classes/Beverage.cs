using Project_partA.Interfaces;

namespace Project_partA.Classes
{
    public class Beverage
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int VolumeMl { get; set; }
        public Dictionary<string, int> Recipe;

        public void Prepare()
        {
            throw new NotImplementedException();
        }
        public int GetPreparationTimeSec()
        {
            throw new NotImplementedException();
        }
    }
}
