using FlooringMaster.BLL;
using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMaster.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        OrderManager manager = OrderManagerFactory.Create();
        public List<Order> listOrderDate;
        int orderNumber;
        Order orderToRemove = new Order();
        bool removeOrder = false;

        public void Execute()
        {
            try
            {
                DateTime orderDate = DateTime.Parse(ConsoleIO.GetDateFromUser("Select a date for the order you wish to remove: "));
                listOrderDate = OrdersByDate(orderDate);
                if (listOrderDate != null)
                {
                    Console.Clear();
                    Console.WriteLine("Remove Order");
                    Console.WriteLine($"Orders with date: {orderDate.ToShortDateString()}");

                    var newOrderList = new List<Order>();
                    Order singleOrder = new Order();
                    var print = from c in newOrderList
                                where c.OrderDate == singleOrder.OrderDate
                                select c;

                    Console.Clear();
                    Console.WriteLine($"Orders for Date: {orderDate.ToShortDateString()}");
                    Console.WriteLine("****************************************");

                    foreach (Order removeOrder in listOrderDate)
                    {
                        Console.WriteLine($"{removeOrder.OrderNumber} | {removeOrder.CustomerName}");
                        Console.WriteLine("****************************************");
                    }

                }

                orderNumber = ConsoleIO.GetNumberFromUser("Enter an order number to remove");

                if (true)
                {
                    orderToRemove = GetOrderByOrderNumber(orderDate, orderNumber);
                }
                while (removeOrder == false)
                {
                    Console.Clear();
                    Console.WriteLine($"Remove order number: {orderToRemove.OrderNumber} with customer Name: {orderToRemove.CustomerName}?");
                    Console.WriteLine($"Y or N?");
                    string userinput = Console.ReadLine();

                    if (userinput.ToUpper() == "Y")
                    {
                        RemoveOrder(orderToRemove, orderDate);
                        removeOrder = true;
                    }
                    else if (userinput.ToUpper() == "N")
                    {
                        Console.WriteLine("Order not removed.");
                        Console.WriteLine("Press any key to return to Main Menu");
                        Console.ReadLine();
                        removeOrder = true;
                    }
                    else if (userinput.ToUpper() != "Y" || userinput.ToUpper() != "N")
                    {
                    Console.Clear();
                    Console.WriteLine("Not a valid entry. Press any key to try again.");
                    Console.ReadLine();
                    removeOrder = false;
                    }

                }
            }
            catch
            {
                Console.WriteLine("There has been a critical error. Order not removed!");
                Console.WriteLine("Please contact IT.");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
            }
        }

        private void RemoveOrder(Order orderToRemove, DateTime orderDate)
        {
            var response = manager.RemoveOrder(orderToRemove, orderDate);

            if (response.Success)
            {
                Console.Clear();
                Console.WriteLine("Order succesfully removed!");
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("An error has occured. Order not added.");
                Console.ReadLine();
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
                Console.WriteLine("There are no orders for that date.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Execute();
            }
            return null;
        }

        private Order GetOrderByOrderNumber(DateTime orderDate, int orderNumber)
        {
            var response = manager.GetOrderByONumber(orderDate, orderNumber);

            if (response.Success)
            {
                return response.Order;
            }
            else
            {
                Console.WriteLine("Something has gone wrong please contact IT");
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                Execute();
            }
            return null;
        }



    }
}
