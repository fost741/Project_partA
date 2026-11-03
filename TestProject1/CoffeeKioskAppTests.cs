using Project_partA.Enums;
using Project_partA.Classes;
using System.Collections.Generic;

namespace TestProject1
{
    [TestClass]
    public sealed class CoffeeKioskAppTests
    {
        private CoffeeKiosk kiosk;
        private Customer customer;
        private IngredientStock stock;

        [TestInitialize]
        public void TestInitialize()
        {
            kiosk = new CoffeeKiosk();
            customer = new Customer();
            stock = new IngredientStock();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void Test_CoffeeKiosk_KioskId_Property()
        {
            Assert.Throws<System.NotImplementedException>(() => {int id = kiosk.KioskId;});
        }

        [TestMethod]
        public void Test_CoffeeKiosk_Address_Property()
        {
            Assert.Throws<System.NotImplementedException>(() => {string addr = kiosk.Address;});
        }

        [TestMethod]
        public void Test_CoffeeKiosk_TurnOn_Method()
        {
            Assert.Throws<System.NotImplementedException>(() => kiosk.TurnOn());
        }

        [TestMethod]
        public void Test_CoffeeKiosk_ProcessOrder_Method()
        {
            Order order = new Order();
            Assert.Throws<System.NotImplementedException>(() => kiosk.ProcessOrder(order));
        }

        [TestMethod]
        public void Test_Customer_BonusBalance_Property()
        {
            Assert.Throws<System.NotImplementedException>(() => {decimal balance = customer.BonusBalance;});
        }

        [TestMethod]
        public void Test_Customer_AddBonuses_Method()
        {
            Assert.Throws<System.NotImplementedException>(() => customer.AddBonuses(10m));
        }

        [TestMethod]
        public void Test_IngredientStock_CheckAndDeduct_Method()
        {
            Dictionary<string, int> required = new Dictionary<string, int>();
            Assert.Throws<System.NotImplementedException>(() => stock.CheckAndDeduct(required));
        }
    }
}
    

