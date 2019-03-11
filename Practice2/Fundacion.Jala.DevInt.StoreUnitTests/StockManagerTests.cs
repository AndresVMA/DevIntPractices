using Fundacion.Jala.DevInt.Practice2.Store;
using Fundacion.Jala.DevInt.Practice2.Store.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Fundacion.Jala.DevInt.StoreUnitTests
{
    [TestClass]
    public class StockManagerTests
    {
        StockManager _stockManager;
        Monitor _monitor;
        GameController _gameControllerXBox;
        GameController _gameControllerPS;
        public StockManagerTests()
        {
            _stockManager = new StockManager();
            _monitor = new Monitor()
            {
                Brand = "Samsung",
                Model = "SR-75",
                Inches = 32
            };
            _gameControllerXBox = new GameController()
            {
                Brand = "Xbox",
                Model = "Sport RSE"
            };
            _gameControllerPS = new GameController()
            {
                Brand = "PlayStation",
                Model = "MarioKart Edition"
            };
        }
        [TestMethod]
        public void When_Products_Are_Created_Then_FullProductName_Property_Contains_Name_Brand_And_Model()
        {
            Assert.AreEqual("32' Samsung-SR-75", _monitor.FullName);
            Assert.AreEqual("Xbox-Sport RSE", _gameControllerXBox.FullName);

        }
        [TestMethod]
        public void Should_Initialize_Product_Stock()
        {
            _stockManager.Add(_monitor, 2);
            _stockManager.Add(_gameControllerXBox, 5);
            _stockManager.Add(_gameControllerPS, 4);
            Assert.AreEqual(2, _stockManager.GetStockFor(nameof(Monitor)));
            Assert.AreEqual(9, _stockManager.GetStockFor(nameof(GameController)));
        }

        [TestMethod]
        public void Should_Increment_Product_Stock()
        {
            _stockManager.Add(_gameControllerPS, 4);
            _stockManager.Add(_gameControllerPS.Id, 2);
            Assert.AreEqual(6, _stockManager.GetStockFor(nameof(GameController), _gameControllerPS.FullName));
            
        }

        [TestMethod]
        public void Should_Get_Stock_By_Product_Id()
        {
            _stockManager.Add(_monitor, 7);
            var stock = _stockManager.GetStockFor(_monitor.Id);
            Assert.AreEqual(7, stock);
        }

        [TestMethod]
        public void Should_Remove_Product_From_Available_Items()
        {
            _stockManager.Add(_gameControllerPS, 4);
            var products = _stockManager.GetProductsBy(nameof(GameController));
            Assert.AreEqual(1, products.Count());
            _stockManager.Remove(_gameControllerPS.Id);
            products = _stockManager.GetProductsBy(nameof(GameController));
            var stock = _stockManager.GetStockFor(_gameControllerPS.Id);
            Assert.AreEqual(0, products.Count());
            Assert.AreEqual(0, stock);
        }

        [TestMethod]
        public void When_Product_Is_Sold_Then_Stock_Decreases_In_General()
        {
            _stockManager.Add(_monitor, 1);
            _stockManager.Add(_gameControllerXBox, 3);
            _stockManager.Add(_gameControllerPS, 6);
            _stockManager.Sell(_monitor.Id);
            _stockManager.Sell(_gameControllerXBox.Id);
            Assert.AreEqual(0, _stockManager.GetStockFor(nameof(Monitor)));
            Assert.AreEqual(8, _stockManager.GetStockFor(nameof(GameController)));
        }

        [TestMethod]
        public void When_Product_Is_Sold_Then_Stock_Decreases_For_That_Item()
        {
            _stockManager.Add(_gameControllerXBox, 2);
            _stockManager.Add(_gameControllerPS, 1);
            _stockManager.Sell(_gameControllerXBox.Id);
            Assert.AreEqual(1, _stockManager.GetStockFor(nameof(GameController), _gameControllerXBox.FullName));
            Assert.AreEqual(1, _stockManager.GetStockFor(nameof(GameController), _gameControllerPS.FullName));
        }

        [TestMethod]
        public void Should_Not_Add_Object_That_Is_Not_A_Product()
        {
            _stockManager.Add(new { Brand = "HP", Model = "1245", Inches = 32 }, 5);
            Assert.AreEqual(0, _stockManager.GetStockFor(nameof(Monitor)));
        }

        [TestMethod]
        public void Stock_Should_Be_Always_Positive()
        {
            _stockManager.Add(_monitor, 1);
            _stockManager.Add(_gameControllerXBox, 4);
            Assert.AreEqual(4, _stockManager.GetStockFor(nameof(GameController), _gameControllerXBox.FullName));
            _stockManager.Sell(_gameControllerXBox.Id, 2);
            _stockManager.Sell(_gameControllerXBox.Id, 4);
            Assert.AreEqual(0, _stockManager.GetStockFor(nameof(GameController), _gameControllerXBox.FullName));
        }

        [TestMethod]
        public void Should_Get_Products_By_Type_Without_Case_Sensitive_Product_Type()
        {
            _stockManager.Add(_monitor, 2);
            _stockManager.Add(_gameControllerXBox, 5);
            Assert.AreEqual(5, _stockManager.GetStockFor("GAMECONTROLLER", _gameControllerXBox.FullName));
            Assert.AreEqual(2, _stockManager.GetStockFor("MoNiToR", _monitor.FullName));
        }
    }
}
