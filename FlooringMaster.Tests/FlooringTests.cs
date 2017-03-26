using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringMastery.Models.OrderLookupResponse;
using FlooringMaster.BLL;
using FlooringMastery.Models.Responses;
using FlooringMastery.Models.Interfaces;
using FlooringMaster.Data;
using FlooringMastery.Models;

namespace FlooringMaster.Tests
{
    [TestFixture]
    public class FlooringTests
    {
        OrderTestRepository testrepo = new OrderTestRepository();
        [TestCase("06/01/2017", 1, "Dave", 200, "IN", 6.00, "Wood", 1.75, 2.10, true)]
        [TestCase("07/29/2018", 4, "Patrick", 100, "KY", 4.25, "Tile", 2.75, 4.10, true)]
        public void AddOrderTests(string OrderDate, int OrderNumber, string CustomerName, decimal Area, string State, decimal TaxRate, string Product, decimal CostPerSquareFoot, decimal LaborCostPerSquareFoot, bool expectedResult)
        {
            
            OrderManager manager = new OrderManager(new OrderTestRepository(), new StateTaxTestRepository(), new ProductTestRepository());

            Order order = new Order();

            order.OrderDate = DateTime.Parse(OrderDate);
            order.OrderNumber = OrderNumber;
            order.CustomerName = CustomerName;
            order.Area = Area;
            order.State = State;
            order.TaxRate = TaxRate;
            order.ProductType = Product;
            order.CostPerSquareFoot = CostPerSquareFoot;
            order.LaborCostPerSquareFoot = LaborCostPerSquareFoot;
            order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
            order.MaterialCost = order.Area * order.CostPerSquareFoot;
            order.Tax = ((order.MaterialCost + order.LaborCost) * (order.TaxRate / 100));
            order.Total = (order.MaterialCost + order.LaborCost + order.Tax);

            OrderLookupResponse response = manager.AddOrder(order);

            Assert.AreEqual(expectedResult, response.Success);

        }

        [TestCase("08/11/2019", 2, "James", 175, "MI", 6.00, "Wood", 1.75, 2.10, true)]
        [TestCase("07/21/2017", 3, "Max", 400, "OH", 4.25, "Tile", 2.75, 4.10, true)]
        public void RemoveOrderTest(string OrderDate, int OrderNumber, string CustomerName, decimal Area, string State, decimal TaxRate, string Product, decimal CostPerSquareFoot, decimal LaborCostPerSquareFoot, bool expectedResult)
        {

            OrderManager manager = new OrderManager(new OrderTestRepository(), new StateTaxTestRepository(), new ProductTestRepository());

            Order order = new Order();

            order.OrderDate = DateTime.Parse(OrderDate);
            order.OrderNumber = OrderNumber;
            order.CustomerName = CustomerName;
            order.Area = Area;
            order.State = State;
            order.TaxRate = TaxRate;
            order.ProductType = Product;
            order.CostPerSquareFoot = CostPerSquareFoot;
            order.LaborCostPerSquareFoot = LaborCostPerSquareFoot;
            order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
            order.MaterialCost = order.Area * order.CostPerSquareFoot;
            order.Tax = ((order.MaterialCost + order.LaborCost) * (order.TaxRate / 100));
            order.Total = (order.MaterialCost + order.LaborCost + order.Tax);

            OrderLookupResponse response = manager.RemoveOrder(order, order.OrderDate);



            Assert.AreEqual(expectedResult, response.Success);

        }


        [TestCase("04/12/2018", 6, "Melissa", 325, "IN", 6.00, "Laminate", 1.75, 2.10, true)]
        [TestCase("11/15/2017", 8, "Gloria", 200, "KY", 4.25, "Carpet", 2.75, 4.10, true)]
        public void EditOrderTests(string OrderDate, int OrderNumber, string CustomerName, decimal Area, string State, decimal TaxRate, string Product, decimal CostPerSquareFoot, decimal LaborCostPerSquareFoot, bool expectedResult)
        {

            OrderManager manager = new OrderManager(new OrderTestRepository(), new StateTaxTestRepository(), new ProductTestRepository());

            Order order = new Order();

            order.OrderDate = DateTime.Parse(OrderDate);
            order.OrderNumber = OrderNumber;
            order.CustomerName = CustomerName;
            order.Area = Area;
            order.State = State;
            order.TaxRate = TaxRate;
            order.ProductType = Product;
            order.CostPerSquareFoot = CostPerSquareFoot;
            order.LaborCostPerSquareFoot = LaborCostPerSquareFoot;
            order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
            order.MaterialCost = order.Area * order.CostPerSquareFoot;
            order.Tax = ((order.MaterialCost + order.LaborCost) * (order.TaxRate / 100));
            order.Total = (order.MaterialCost + order.LaborCost + order.Tax);

            Order editOrder = new Order();

            editOrder.OrderDate = DateTime.Parse(OrderDate);
            editOrder.OrderNumber = order.OrderNumber;
            editOrder.CustomerName = "Patrick";
            editOrder.Area = 100;
            editOrder.State = order.State;
            editOrder.TaxRate = order.TaxRate;
            editOrder.ProductType = order.ProductType;
            editOrder.CostPerSquareFoot = order.CostPerSquareFoot;
            editOrder.LaborCostPerSquareFoot = order.LaborCostPerSquareFoot;
            editOrder.LaborCost = editOrder.Area * editOrder.LaborCostPerSquareFoot;
            editOrder.MaterialCost = editOrder.Area * editOrder.CostPerSquareFoot;
            editOrder.Tax = ((editOrder.MaterialCost + editOrder.LaborCost) * (editOrder.TaxRate / 100));
            editOrder.Total = (editOrder.MaterialCost + editOrder.LaborCost + editOrder.Tax);



            OrderLookupResponse response = manager.EditOrder(order, editOrder, order.OrderDate, order.OrderNumber);



            Assert.AreEqual(expectedResult, response.Success);

        }
    }
}
