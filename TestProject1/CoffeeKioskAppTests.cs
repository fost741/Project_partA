using Project_partA.Enums;
using Project_partA.Classes;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyModel;

namespace TestProject1
{
    [TestClass]
    public sealed class CoffeeKioskAppTests
    {
        private CoffeeKiosk kiosk;
        private Customer customer;
        private IngredientStock stock;
        private Order order;
        private Beverage beverage;
        private CoffeeNetwork network;

        [TestInitialize]
        public void TestInitialize()
        {
            kiosk = new CoffeeKiosk();
            customer = new Customer();
            stock = new IngredientStock();
            order = new Order();
            beverage = new Beverage();
            network = new CoffeeNetwork();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        [DataRow("Tea")]
        [DataRow("Espresso")]
        [DataRow("Caramel Macchiato")]
        public void BeverageTest_Name_Valid(string expectedName)
        {
            beverage.Name = expectedName;
            string actualName = beverage.Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("A")]
        [DataRow("ab")]
        [DataRow("  x  ")]
        [DataRow("123")]
        [DataRow("@#$")]

        public void BeverageTest_Name_invalidLengthOrType(string invalidName)
        {
            Assert.Throws<ArgumentException>(() => beverage.Name = invalidName);
        }

        [TestMethod]
        [DataRow(0.01)]
        [DataRow(10.50)]
        [DataRow(99.99)]
        public void BeverageTest_Price_Valid(decimal expectedPrice)
        {
            beverage.Price = (decimal)expectedPrice;
            decimal actualPrice = beverage.Price;
            Assert.AreEqual(expectedPrice, actualPrice);
        }

        [TestMethod]
        [DataRow(-0.01)]
        [DataRow(-10.50)]
        [DataRow(-99.99)]
        [DataRow(0)]
        [DataRow(1000)]
        public void BeverageTest_Price_0_less_0_max(decimal invalidPrice)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => beverage.Price = (decimal)invalidPrice);
        }

        [TestMethod]
        [DataRow(50)]
        [DataRow(250)]
        [DataRow(500)]
        public void BeverageTest_VolumeMl_Valid(int expectedVolume)
        {
            beverage.VolumeMl = expectedVolume;
            int actualVolume = beverage.VolumeMl;
            Assert.AreEqual(expectedVolume, actualVolume);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-50)]
        [DataRow(100)]
        [DataRow(300)]
        [DataRow(1000)]
        public void BeverageTest_VolumeMl_0_less_0_wrong_max(int invalidVolume)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => beverage.VolumeMl = invalidVolume);
        }

        [TestMethod]
        [DataRow()]
        public void BeverageTest_Prepare_Valid()
        {
            beverage.Name = "Espresso";
            beverage.Price = 2.50m;
            beverage.VolumeMl = 50;
            beverage.Recipe = new Dictionary<string, int>
            {
                { "Coffee Beans", 10 },
                { "Water", 50 }
            };

            beverage.Prepare();
            Assert.IsNotNull(beverage.Recipe);
        }

        [TestMethod]
        public void BeverageTest_Prepare_NullOrEmpty()
        {
            beverage.Recipe = null;
            Assert.Throws<InvalidOperationException>(() => beverage.Prepare());
            beverage.Recipe = new Dictionary<string, int>();
            Assert.Throws<InvalidOperationException>(() => beverage.Prepare());
        }

        [TestMethod]
        [DataRow("", 10)]
        [DataRow(" ", 10)]
        [DataRow("coffeeBeans", 0)]
        [DataRow("coffeeBeans", -5)]
        public void BeverageTest_Prepare_InvalidIngredient(string ingredient, int quantity)
        {
            beverage.Name = "Latte";
            beverage.Price = 5.0m;
            beverage.VolumeMl = 500;
            beverage.Recipe = new Dictionary<string, int>
            {
                { ingredient, quantity }
            };
            Assert.Throws<ArgumentException>(() => beverage.Prepare());
        }

        [TestMethod]
        public void BeverageTest_GetPreparationTimeSec_Valid()
        {
            beverage.Recipe = new Dictionary<string, int>
            {
                { "CoffeeBeans", 18 },
                { "Milk", 150 }
            };

            int prepTime = beverage.GetPreparationTimeSec();

            Assert.IsTrue(prepTime > 0);
        }

        [TestMethod]
        public void BeverageTest_GetPreparationTimeSec_NullOrEmpty()
        {
            beverage.Recipe = null;
            Assert.Throws<InvalidOperationException>(() => beverage.GetPreparationTimeSec());

            beverage.Recipe = new Dictionary<string, int>();
            Assert.Throws<InvalidOperationException>(() => beverage.GetPreparationTimeSec());
        }

        [TestMethod]
        [DataRow("CUST-01")]
        [DataRow("CUST-1001")]
        public void CustomerTest_CustomerId_Valid(string validId)
        {
            customer.CustomerId = validId;
            string customerId = customer.CustomerId;
            Assert.AreEqual(validId, customerId);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("1")]
        [DataRow("CUS01")]
        [DataRow("CUST 01")]
        public void CustomerTest_CustomerId_NullOrEmpty_WrongType(string validId)
        {
            Assert.Throws<ArgumentException>(() => customer.CustomerId = validId);
        }

        [TestMethod]
        [DataRow("John Doe")]
        [DataRow("John")]
        public void CustomerTest_FullName_Valid(string validName)
        {
            customer.FullName = validName;
            string fullName = customer.FullName;
            Assert.AreEqual(validName, fullName);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("J")]
        [DataRow("John123")]
        [DataRow("John123@")]
        public void CustomerTest_FullName_NullOrEmpty_WrongType(string invalidName)
        {
            Assert.Throws<ArgumentException>(() => customer.FullName = invalidName);
        }

        [TestMethod]
        [DataRow(0.0)]
        [DataRow(0.01)]
        [DataRow(100.0)]
        public void CusromerTest_BonusBalance_Valid(double balance)
        {
            customer.BonusBalance = (decimal)balance;
            decimal bonusBalance = customer.BonusBalance;
            Assert.AreEqual((decimal)balance, bonusBalance);
        }

        [TestMethod]
        [DataRow(-0.01)]
        [DataRow(-100.0)]
        public void CustomerTest_BonusBalance_Negative(double invalidBalance)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => customer.BonusBalance = (decimal)invalidBalance);
        }

        [TestMethod]
        [DataRow(0.0, 5.0, 5.0)]
        [DataRow(5.0, 2.0, 7.0)]
        public void CustomerTest_AddBonuses_Valid(double initialBalance, double bonusesToAdd, double expectedBalance)
        {
            customer.BonusBalance = (decimal)initialBalance;
            customer.AddBonuses((decimal)bonusesToAdd);

            Assert.AreEqual((decimal)expectedBalance, customer.BonusBalance);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-0.01)]
        [DataRow(-5.0)]
        public void CustomerTest_AddBonuses_0_Negative(double invalidBonuses)
        {
            customer.BonusBalance = 5.0m;
            Assert.Throws<ArgumentOutOfRangeException>(() => customer.AddBonuses((decimal)invalidBonuses));
        }

        [TestMethod]
        [DataRow(10.0, 5.0, 5.0)]
        [DataRow(5.0, 5.0, 0.0)]
        public void CustomerTest_SpendBonuses_Valid(double initialBalance, double bonusesToSpend, double expectedBalance)
        {
            customer.BonusBalance = (decimal)initialBalance;
            customer.SpendBonuses((decimal)bonusesToSpend);
            Assert.AreEqual((decimal)expectedBalance, customer.BonusBalance);
        }

        [TestMethod]
        [DataRow(0.0)]
        [DataRow(-0.01)]
        [DataRow(-5.0)]
        public void CustomerTest_SpendBonuses_0_Negative(double invalidBonuses)
        {
            customer.BonusBalance = 5.0m;
            Assert.Throws<ArgumentOutOfRangeException>(() => customer.SpendBonuses((decimal)invalidBonuses));
        }

        [TestMethod]
        [DataRow(5.0, 5.01)]
        [DataRow(0.0, 5.0)]
        [DataRow(5.0, 10.0)]
        public void CustomerTest_SpendBonuses_InsufficientBalance(double initialBalance, double bonusesToSpend)
        {
            customer.BonusBalance = (decimal)initialBalance;
            Assert.Throws<InvalidOperationException>(() => customer.SpendBonuses((decimal)bonusesToSpend));
        }

        [TestMethod]
        public void OrderTest_OrderID_Valid()
        {
            order.OrderId = "ORD-001";
            string orderId = order.OrderId;
            Assert.AreEqual("ORD-001", orderId);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("001")]
        public void OrderTest_OrderID_NullOrEmpty_WrongType(string invalidOrderId)
        {
            Assert.Throws<ArgumentException>(() => order.OrderId = invalidOrderId);
        }

        [TestMethod]
        [DataRow(10.50)]
        public void OrderTest_TotalPrice_Valid(double totalPrice)
        {
            order.TotalPrice = (decimal)totalPrice;
            Assert.AreEqual((decimal)totalPrice, order.TotalPrice);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow(-0.01)]
        [DataRow(-50.0)]
        public void OrderTest_TotalPrice_Null_NegativeValues(double invalidPrice)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => order.TotalPrice = (decimal)invalidPrice);
        }

        [TestMethod]
        public void OrderTest_AddItem_ValidBeveragesTotalPrice()
        {
            Beverage latte = new Beverage { Name = "Latte", Price = 5.50m, VolumeMl = 250 };
            Beverage espresso = new Beverage { Name = "Espresso", Price = 2.50m, VolumeMl = 50 };

            order.AddItem(latte);
            order.AddItem(espresso);

            Assert.AreEqual(2, order.Items.Count);
            Assert.AreEqual(8.00m, order.TotalPrice);
        }

        [TestMethod]
        public void OrderTest_AddItem_NullBeverage()
        {
            Assert.Throws<ArgumentNullException>(() => order.AddItem(null));
        }

        [TestMethod]
        public void OrderTest_AddItem_UninitializedOrZeroPriceBeverage()
        {
            Beverage invalidBeverage = new Beverage();
            Assert.Throws<InvalidOperationException>(() => order.AddItem(invalidBeverage));
        }

        [TestMethod]
        [DataRow(OrderStatus.Paid)]
        [DataRow(OrderStatus.Ready)]
        public void OrderTest_CompleteOrder_ValidStatusesWithItems(OrderStatus validInitialStatus)
        {
            Beverage latte = new Beverage { Name = "Latte", Price = 5.50m, VolumeMl = 250 };
            order.AddItem(latte);
            order.Status = validInitialStatus;

            order.CompleteOrder();

            Assert.AreEqual(OrderStatus.Completed, order.Status);
        }

        [TestMethod]
        public void OrderTest_CompleteOrder_EmptyItemsList()
        {
            order.Status = OrderStatus.Paid;
            Assert.Throws<InvalidOperationException>(() => order.CompleteOrder());
        }

        [TestMethod]
        [DataRow(OrderStatus.Waiting)]
        [DataRow(OrderStatus.Completed)]
        [DataRow(OrderStatus.Canceled)]
        public void OrderTest_CompleteOrder_InvalidOrderStatuses(OrderStatus invalidStatus)
        {
            Beverage latte = new Beverage { Name = "Latte", Price = 5.50m, VolumeMl = 250 };
            order.AddItem(latte);
            order.Status = invalidStatus;

            Assert.Throws<InvalidOperationException>(() => order.CompleteOrder());
        }

        [TestMethod]
        [DataRow("Tea", 100, 50, 150)]
        [DataRow("CoffeeBeans", 500, 250, 750)]
        public void IngredientStockTest_RefillStock_Valid(string item, int firstAdd, int secondAdd, int expectedTotal)
        {
            stock.RefillStock(item, firstAdd);
            stock.RefillStock(item, secondAdd);

            Assert.AreEqual(expectedTotal, stock.IngredientsMap[item]);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("A")]
        [DataRow("ab")]
        [DataRow("123")]
        public void IngredientStockTest_RefillStock_InvalidItemNames(string invalidItem)
        {
            Assert.Throws<ArgumentException>(() => stock.RefillStock(invalidItem, 100));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-500)]
        [DataRow(100001)]
        public void IngredientStockTest_RefillStock_ZeroNegativeOrExceed(int invalidAmount)
        {
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => stock.RefillStock("Milk", invalidAmount));
        }

        [TestMethod]
        [DataRow(100, 20, 80)]
        [DataRow(18, 18, 0)]
        public void IngredientStockTest_CheckAndDeduct_ValidAndBoundaryAmounts(int initialStock, int requiredAmount, int expectedRemaining)
        {
            stock.RefillStock("CoffeeBeans", initialStock);
            Dictionary<string, int> required = new Dictionary<string, int>
            {
                { "CoffeeBeans", requiredAmount }
            };

            bool result = stock.CheckAndDeduct(required);

            Assert.IsTrue(result);
            Assert.AreEqual(expectedRemaining, stock.IngredientsMap["CoffeeBeans"]);
        }

        [TestMethod]
        public void IngredientStockTest_CheckAndDeduct_InsufficientSingleOrMultiIngredient()
        {
            stock.RefillStock("CoffeeBeans", 50);
            stock.RefillStock("Milk", 100);
            Dictionary<string, int> required = new Dictionary<string, int>
            {
                { "CoffeeBeans", 20 },
                { "Milk", 101 }
            };

            bool result = stock.CheckAndDeduct(required);

            Assert.IsFalse(result);
            Assert.AreEqual(50, stock.IngredientsMap["CoffeeBeans"]);
            Assert.AreEqual(100, stock.IngredientsMap["Milk"]);
        }

        [TestMethod]
        public void IngredientStockTest_CheckAndDeduct_MissingIngredientKey()
        {
            stock.RefillStock("CoffeeBeans", 50);
            Dictionary<string, int> required = new Dictionary<string, int>
            {
                { "VanillaSyrup", 15 }
            };

            bool result = stock.CheckAndDeduct(required);

            Assert.IsFalse(result);
            Assert.AreEqual(50, stock.IngredientsMap["CoffeeBeans"]);
        }

        [TestMethod]
        public void IngredientStockTest_CheckAndDeduct_NullOrEmptyDictionary()
        {
            Assert.Throws<ArgumentNullException>(() => stock.CheckAndDeduct(null));
            Assert.Throws<ArgumentException>(() => stock.CheckAndDeduct(new Dictionary<string, int>()));
        }

        [TestMethod]
        [DataRow("", 10)]
        [DataRow("   ", 10)]
        [DataRow("CoffeeBeans", 0)]
        [DataRow("CoffeeBeans", -10)]
        public void IngredientStockTest_CheckAndDeduct_InvalidKeyOrNegativeQuantity(string key, int qty)
        {
            stock.RefillStock("CoffeeBeans", 50);
            Dictionary<string, int> required = new Dictionary<string, int>
            {
                { key, qty }
            };

            Assert.Throws<ArgumentException>(() => stock.CheckAndDeduct(required));
            Assert.AreEqual(50, stock.IngredientsMap["CoffeeBeans"]);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(105)]
        [DataRow(9999)]
        public void CoffeeKioskTest_KioskId_ValidPositiveIds(int validId)
        {
            kiosk.KioskId = validId;
            int actualId = kiosk.KioskId;

            Assert.AreEqual(validId, actualId);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-100)]
        public void CoffeeKioskTest_KioskId_ZeroOrNegativeIds(int invalidId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => kiosk.KioskId = invalidId);
        }

        [TestMethod]
        [DataRow("Sumska 1")]
        [DataRow("Nauky Ave, 14, Kharkiv")]
        public void CoffeeKioskTest_Address_ValidStrings(string validAddress)
        {
            kiosk.Address = validAddress;
            string actualAddress = kiosk.Address;

            Assert.AreEqual(validAddress, actualAddress);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("     ")]
        [DataRow("St 1")]
        [DataRow("12345")]
        public void CoffeeKioskTest_Address_InvalidOrTooShortStrings(string invalidAddress)
        {
            Assert.Throws<ArgumentException>(() => kiosk.Address = invalidAddress);
        }

        [TestMethod]
        public void CoffeeKioskTest_TurnOn_ValidConfiguration()
        {
            stock.RefillStock("CoffeeBeans", 200);
            kiosk.KioskId = 1;
            kiosk.Address = "Sumska St, 25";
            kiosk.Stock = stock;
            kiosk.Status = KioskStatus.Offline;

            kiosk.TurnOn();

            Assert.AreEqual(KioskStatus.Active, kiosk.Status);
        }

        [TestMethod]
        public void CoffeeKioskTest_TurnOn_NullStockEmptyStockOrMaintenanceStatus()
        {
            kiosk.Address = "Sumska St, 25";
            kiosk.Stock = null;
            Assert.Throws<InvalidOperationException>(() => kiosk.TurnOn());

            kiosk.Stock = stock;
            kiosk.Status = KioskStatus.Manintenance;
            Assert.Throws<InvalidOperationException>(() => kiosk.TurnOn());
        }

        [TestMethod]
        public void CoffeeKioskTest_ProcessOrder_ValidPaidOrderDeductsStockAndCompletesOrder()
        {
            stock.RefillStock("CoffeeBeans", 100);
            stock.RefillStock("Water", 500);
            kiosk.KioskId = 1;
            kiosk.Address = "Sumska St, 25";
            kiosk.Stock = stock;
            kiosk.Status = KioskStatus.Active;

            Beverage espresso = new Beverage
            {
                Name = "Espresso",
                Price = 2.50m,
                VolumeMl = 50,
                Recipe = new Dictionary<string, int>
                {
                    { "CoffeeBeans", 18 },
                    { "Water", 50 }
                }
            };
            order.OrderId = "ORD-101";
            order.AddItem(espresso);
            order.Status = OrderStatus.Paid;

            kiosk.ProcessOrder(order);

            Assert.AreEqual(OrderStatus.Completed, order.Status);
            Assert.AreEqual(82, kiosk.Stock.IngredientsMap["CoffeeBeans"]);
            Assert.AreEqual(450, kiosk.Stock.IngredientsMap["Water"]);
        }

        [TestMethod]
        public void CoffeeKioskTest_ProcessOrder_InsufficientIngredientsSetsOutOfIngredients()
        {
            stock.RefillStock("CoffeeBeans", 10);
            kiosk.KioskId = 1;
            kiosk.Address = "Sumska St, 25";
            kiosk.Stock = stock;
            kiosk.Status = KioskStatus.Active;

            Beverage espresso = new Beverage
            {
                Name = "Espresso",
                Price = 2.50m,
                VolumeMl = 50,
                Recipe = new Dictionary<string, int>
                {
                    { "CoffeeBeans", 18 }
                }
            };
            order.OrderId = "ORD-102";
            order.AddItem(espresso);
            order.Status = OrderStatus.Paid;

            Assert.Throws<InvalidOperationException>(() => kiosk.ProcessOrder(order));

            Assert.AreEqual(KioskStatus.OutOfIngredients, kiosk.Status);
            Assert.AreNotEqual(OrderStatus.Completed, order.Status);
        }

        [TestMethod]
        public void CoffeeKioskTest_ProcessOrder_NullOrEmptyOrder()
        {
            stock.RefillStock("CoffeeBeans", 100);
            kiosk.Stock = stock;
            kiosk.Status = KioskStatus.Active;

            Assert.Throws<ArgumentNullException>(() => kiosk.ProcessOrder(null));
            Assert.Throws<InvalidOperationException>(() => kiosk.ProcessOrder(order));
        }

        [TestMethod]
        [DataRow(OrderStatus.Waiting)]
        [DataRow(OrderStatus.Completed)]
        [DataRow(OrderStatus.Canceled)]
        public void CoffeeKioskTest_ProcessOrder_UnpaidOrClosedOrderStatuses(OrderStatus invalidOrderStatus)
        {
            stock.RefillStock("CoffeeBeans", 100);
            kiosk.Stock = stock;
            kiosk.Status = KioskStatus.Active;

            Beverage espresso = new Beverage
            {
                Name = "Espresso",
                Price = 2.50m,
                VolumeMl = 50,
                Recipe = new Dictionary<string, int> { { "CoffeeBeans", 18 } }
            };
            order.OrderId = "ORD-103";
            order.AddItem(espresso);
            order.Status = invalidOrderStatus;

            Assert.Throws<InvalidOperationException>(() => kiosk.ProcessOrder(order));
        }

        [TestMethod]
        [DataRow(KioskStatus.Offline)]
        [DataRow(KioskStatus.Manintenance)]
        [DataRow(KioskStatus.OutOfIngredients)]
        public void CoffeeKioskTest_ProcessOrder_InactiveKioskStatuses(KioskStatus inactiveStatus)
        {
            stock.RefillStock("CoffeeBeans", 100);
            kiosk.Stock = stock;
            kiosk.Status = inactiveStatus;

            Beverage espresso = new Beverage
            {
                Name = "Espresso",
                Price = 2.50m,
                VolumeMl = 50,
                Recipe = new Dictionary<string, int> { { "CoffeeBeans", 18 } }
            };
            order.OrderId = "ORD-104";
            order.AddItem(espresso);
            order.Status = OrderStatus.Paid;

            Assert.Throws<InvalidOperationException>(() => kiosk.ProcessOrder(order));
        }

        [TestMethod]
        [DataRow("Cup")]
        [DataRow("Coffee Place")]
        public void CoffeeNetworkTest_NetworkName_ValidStrings(string validNetworkName)
        {
            network.NetworkName = validNetworkName;
            string actualNetworkName = network.NetworkName;

            Assert.AreEqual(validNetworkName, actualNetworkName);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("AB")]
        [DataRow("123")]
        public void CoffeeNetworkTest_NetworkName_InvalidOrTooShortStrings(string invalidNetworkName)
        {
            Assert.Throws<ArgumentException>(() => network.NetworkName = invalidNetworkName);
        }

        [TestMethod]
        public void CoffeeNetworkTest_AddKiosk_ValidKiosks()
        {
            CoffeeKiosk kiosk1 = new CoffeeKiosk { KioskId = 1, Address = "Nauky Ave, 14" };
            CoffeeKiosk kiosk2 = new CoffeeKiosk { KioskId = 2, Address = "Sumska St, 25" };

            network.AddKiosk(kiosk1);
            network.AddKiosk(kiosk2);

            Assert.AreEqual(2, network.Kiosks.Count);
            Assert.AreSame(kiosk1, network.Kiosks[0]);
            Assert.AreSame(kiosk2, network.Kiosks[1]);
        }

        [TestMethod]
        public void CoffeeNetworkTest_AddKiosk_NullUninitializedOrDuplicateKiosk()
        {
            Assert.Throws<ArgumentNullException>(() => network.AddKiosk(null));

            CoffeeKiosk uninitializedKiosk = new CoffeeKiosk();
            Assert.Throws<InvalidOperationException>(() => network.AddKiosk(uninitializedKiosk));

            CoffeeKiosk firstKiosk = new CoffeeKiosk { KioskId = 1, Address = "Nauky Ave, 14" };
            CoffeeKiosk duplicateKiosk = new CoffeeKiosk { KioskId = 1, Address = "Pushkinska St, 10" };

            network.AddKiosk(firstKiosk);
            Assert.Throws<InvalidOperationException>(() => network.AddKiosk(duplicateKiosk));
        }

        [TestMethod]
        public void CoffeeNetworkTest_GetTotalRevenue_EmptyNetworkOrNoCompletedOrders()
        {
            decimal initialRevenue = network.GetTotalRevenue();

            Assert.AreEqual(0m, initialRevenue);
        }

        [TestMethod]
        public void CoffeeNetworkTest_GetTotalRevenue_CompletedOrdersAcrossKiosks()
        {
            IngredientStock stock1 = new IngredientStock();
            stock1.RefillStock("CoffeeBeans", 500);
            CoffeeKiosk kiosk1 = new CoffeeKiosk { KioskId = 1, Address = "Location 1", Stock = stock1 };
            kiosk1.TurnOn();

            IngredientStock stock2 = new IngredientStock();
            stock2.RefillStock("CoffeeBeans", 500);
            CoffeeKiosk kiosk2 = new CoffeeKiosk { KioskId = 2, Address = "Location 2", Stock = stock2 };
            kiosk2.TurnOn();

            Beverage americano = new Beverage
            {
                Name = "Americano",
                Price = 3.0m,
                VolumeMl = 250,
                Recipe = new Dictionary<string, int> { { "CoffeeBeans", 18 } }
            };
            Beverage cappuccino = new Beverage
            {
                Name = "Cappuccino",
                Price = 4.0m,
                VolumeMl = 250,
                Recipe = new Dictionary<string, int> { { "CoffeeBeans", 18 } }
            };

            Order order1 = new Order { OrderId = "ORD-1", Status = OrderStatus.Paid };
            order1.AddItem(americano);
            kiosk1.ProcessOrder(order1);

            Order order2 = new Order { OrderId = "ORD-2", Status = OrderStatus.Paid };
            order2.AddItem(cappuccino);
            order2.AddItem(americano);
            kiosk2.ProcessOrder(order2);

            network.AddKiosk(kiosk1);
            network.AddKiosk(kiosk2);

            decimal totalRevenue = network.GetTotalRevenue();

            Assert.AreEqual(10.0m, totalRevenue);
        }
    }
}
