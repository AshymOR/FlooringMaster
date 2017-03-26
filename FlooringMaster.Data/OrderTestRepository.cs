using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMaster.Data
{
    public class OrderTestRepository : IOrderRepository
    {
        private static List<Order> _order;

        public OrderTestRepository()
        {
            if (_order == null)
            {
                _order = new List<Order>
                {
                    new Order()
                    {
                        OrderDate = DateTime.Parse("06/01/2013"),
                        OrderNumber = 1,
                        CustomerName = "Dave",
                        Area = 200m,
                        State = "IN",
                        TaxRate = 6.00m,
                        ProductType = "Wood",
                        CostPerSquareFoot = 1.75m,
                        LaborCostPerSquareFoot = 2.10m
                    },

                    new Order()
                    {
                        OrderDate = DateTime.Parse("06/01/2013"),
                        OrderNumber = 2,
                        CustomerName = "Melissa",
                        Area = 150m,
                        State = "OH",
                        TaxRate = 6.25m,
                        ProductType = "Carpet",
                        CostPerSquareFoot = 2.25m,
                        LaborCostPerSquareFoot = 1.75m
                    },

                    new Order()
                    {
                        OrderDate = DateTime.Parse("06/01/2013"),
                        OrderNumber = 3,
                        CustomerName = "Patrick",
                        Area = 325m,
                        State = "MI",
                        TaxRate = 5.75m,
                        ProductType = "Wood",
                        CostPerSquareFoot = 5.15m,
                        LaborCostPerSquareFoot = 4.75m
                    }
                };
            }
        }

        public bool AddOrder(Order order)
        {
            _order.Add(order);
            return true;
        }

        public bool EditOrder(Order order, Order editOrder, DateTime orderDate, int orderNumber)
        {
            var orderEdit = _order.FirstOrDefault(a => a.OrderNumber == orderNumber);
            _order.Remove(orderEdit);
            _order.Add(order);

            return true;
        }


        public Order LoadOrder(string OrderDate)
        {
            return _order.FirstOrDefault(a => a.OrderDate == DateTime.Parse(OrderDate));
        }

        public Order LoadOrderByONumber(DateTime orderDate, int orderNumber)
        {

            var results = from o in _order
                          where o.OrderNumber == orderNumber && o.OrderDate == orderDate
                          select o;

            return results.Single();
        }

        public List<Order> LoadOrdersByDate(DateTime orderDate)
        {

            var results = from o in _order
                          where o.OrderDate == orderDate
                          select o;

                return results.ToList();
        }

        public bool RemoveOrder(Order order, DateTime orderDate)
        {
            _order.Remove(order);
            return true;
        }

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }

}
