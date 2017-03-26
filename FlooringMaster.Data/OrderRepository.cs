using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMaster.Data
{
    public class OrderRepository : IOrderRepository 
    {
        public string CreateFile(DateTime orderDate)
        {
            orderDate = DateTime.Parse(orderDate.ToShortDateString());
            string filemiddle = orderDate.ToString("MMddyyyy");
            string filePath = string.Concat(@".\Orders_" + filemiddle + ".txt");
            return filePath;
        }

        public List<Order> ReadFile (DateTime orderDate)
        {
            string filePath = "";
            filePath = CreateFile(orderDate);

            List<Order> orders = new List<Order>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    sr.ReadLine();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Order newOrder = new Order();

                        string[] columns = line.Split(',');
                        newOrder.OrderNumber = int.Parse(columns[0]);
                        newOrder.CustomerName = columns[1];
                        newOrder.State = columns[2];
                        newOrder.TaxRate = decimal.Parse(columns[3]);
                        newOrder.ProductType = columns[4];
                        newOrder.Area = decimal.Parse(columns[5]);
                        newOrder.CostPerSquareFoot = decimal.Parse(columns[6]);
                        newOrder.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        newOrder.MaterialCost = decimal.Parse(columns[8]);
                        newOrder.LaborCost = decimal.Parse(columns[9]);
                        newOrder.Tax = decimal.Parse(columns[10]);
                        newOrder.Total = decimal.Parse(columns[11]);

                        orders.Add(newOrder);

                    }
                }

            }

            return orders;
        }


        private string CreateOrder(Order order)
        {
            //foreach loop here to increment order# ?
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",order.OrderNumber.ToString(),order.CustomerName,order.State,order.TaxRate.ToString(),order.ProductType,order.Area.ToString(),order.CostPerSquareFoot.ToString(),order.LaborCostPerSquareFoot.ToString(),order.MaterialCost.ToString(),order.LaborCost.ToString(),order.Tax.ToString(),order.Total.ToString());

        }

        public void CreateOrderFile(List<Order> orders, string file)
        {
            if (File.Exists(file))
                File.Delete(file);

            using (StreamWriter sr = new StreamWriter(file, false))
            {
                sr.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (var order in orders)
                {
                    sr.WriteLine(CreateOrder(order));
                }
            }
        }


        public List<Order> LoadOrdersByDate(DateTime OrderDate)
        {
            var orders = ReadFile(OrderDate);
            return orders;

        }

        public Order LoadOrderByONumber(DateTime orderDate, int orderNumber)
        {
            var orders = ReadFile(orderDate);
            var ordertoberemoved = orders.FirstOrDefault(a => a.OrderNumber == orderNumber);
            return ordertoberemoved;
        }

        public bool AddOrder(Order order)
        {
            var orders = ReadFile(order.OrderDate);
            orders.Add(order);

            var file = string.Concat(@".\Orders_" + order.OrderDate.ToString("MMddyyyy") + ".txt");

            CreateOrderFile(orders, file);

            return true;
        }

        public bool RemoveOrder(Order order, DateTime orderDate)
        {
            var orders = ReadFile(orderDate);

            orders.Remove(orders.Single(x => x.OrderNumber == order.OrderNumber));

            var file = string.Concat(@".\Orders_" + orderDate.ToString("MMddyyyy") + ".txt");

            CreateOrderFile(orders, file);

            return true;
        }

        public bool EditOrder(Order order, Order editOrder, DateTime orderDate, int orderNumber)
        {
            var orders = ReadFile(orderDate);
            orders.Remove((orders.Single(x => x.OrderNumber == editOrder.OrderNumber)));
            orders.Add(order);

            var file = string.Concat(@".\Orders_" + orderDate.ToString("MMddyyyy") + ".txt");

            CreateOrderFile(orders, file);

            return true;
        }


    }
}
