using FlooringMaster.BLL;
using FlooringMaster.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.OrderLookupResponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMaster.UI.Workflows
{
    public class ListOrdersWorkflow
    {
        OrderManager manager = OrderManagerFactory.Create();

        public void Execute()
        {

            Console.Clear();
            Console.WriteLine("Enter a date. Example: 06/01/2013");
            Console.WriteLine("**************************");

            string OrderDate = ConsoleIO.GetDateFromUser("Enter an order date: ");
            DateTime Date = DateTime.Parse(OrderDate);

            var orders = OrdersByDate(Date);
            try
            {
                if (orders != null)
                {
                    var newOrderList = new List<Order>();
                    Order singleOrder = new Order();
                    var print = from c in newOrderList
                                where c.OrderDate == singleOrder.OrderDate
                                select c;

                    Console.Clear();
                    Console.WriteLine($"Orders for Date: {OrderDate}");
                    Console.WriteLine("****************************************");

                    foreach (Order newOrder in orders)
                    {
                        Console.WriteLine($"{newOrder.OrderNumber} | {OrderDate}");
                        Console.WriteLine($"{newOrder.CustomerName}");
                        Console.WriteLine($"{newOrder.State}");
                        Console.WriteLine($"Product: {newOrder.ProductType}");
                        Console.WriteLine($"Materials: {(newOrder.Area * newOrder.CostPerSquareFoot).ToString("C")}");
                        Console.WriteLine($"Labor: {(newOrder.Area * newOrder.LaborCostPerSquareFoot).ToString("C")}");
                        Console.WriteLine($"Tax: {(((newOrder.Area * newOrder.CostPerSquareFoot) + (newOrder.Area * newOrder.LaborCostPerSquareFoot)) * (newOrder.TaxRate / 100)).ToString("C")}");
                        Console.WriteLine($"Total: {((newOrder.Area * newOrder.CostPerSquareFoot) + (newOrder.Area * newOrder.LaborCostPerSquareFoot) + ((newOrder.MaterialCost + newOrder.LaborCost) * (newOrder.TaxRate / 100))).ToString("C")}");
                        Console.WriteLine("****************************************");
                    }
                    Console.ReadLine();
                }
                else
                {
                    Console.ReadLine();
                }
            }
            catch
            {
                Console.WriteLine("There has been a critical error. Orders unable to be looked up!");
                Console.WriteLine("Please contact IT.");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
            }

           

        }

        private List<Order> OrdersByDate(DateTime orderDate)
        {

            var response = manager.LookupOrder(orderDate);

            List<Order> orders = new List<Order>();

            if (response.Success)
            {
                orders = response.OrderList;
                return orders;
            }
            else
            {
                Console.WriteLine("No orders for this date.");
                Console.WriteLine("Press any key to return to Main Menu.");

            }
            return null;

        }
    }
 }
