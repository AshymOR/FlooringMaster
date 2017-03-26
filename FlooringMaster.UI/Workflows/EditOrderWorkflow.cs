using FlooringMaster.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMaster.UI.Workflows
{
    public class EditOrderWorkflow
    {
        OrderManager manager = OrderManagerFactory.Create();
        private StateTaxLookupResponse taxResponse = new StateTaxLookupResponse();
        private ProductLookupResponse productResponse = new ProductLookupResponse();
        public List<Order> listOrderDate;
        int orderNumber;
        Order orderToEdit = new Order();
        Order newOrder = new Order();
        bool editOrder = false;
        bool finalOrder = false;
        string stateinput = "";
        string productInput = "";
        string confirmOrderinput;
        string userinput = "";
        int numberInput;
        

        public void Execute()
        {
            try
            {
                DateTime orderDate = DateTime.Parse(ConsoleIO.GetDateFromUser("Select a date for the order you wish to edit: "));
                listOrderDate = OrdersByDate(orderDate);
                if (listOrderDate != null)
                {

                    Console.Clear();
                    Console.WriteLine($"Orders for Date: {orderDate.ToShortDateString()}");
                    Console.WriteLine("****************************************");

                    foreach (Order editOrder in listOrderDate)
                    {
                        Console.WriteLine($"{editOrder.OrderNumber} | {editOrder.CustomerName}");
                        Console.WriteLine("****************************************");
                    }

                }
                orderNumber = ConsoleIO.GetNumberFromUser("Enter an order number to remove");

                if (true)
                {
                    orderToEdit = GetOrderByOrderNumber(orderDate, orderNumber);
                }
                while (editOrder == false)
                {
                    Console.Clear();
                    Console.WriteLine($"Edit order number: {orderToEdit.OrderNumber} with customer Name: {orderToEdit.CustomerName}?");
                    Console.WriteLine($"Y or N?");
                    string userinput = Console.ReadLine();

                    if (userinput.ToUpper() == "Y")
                    {
                        EditOrderData(orderToEdit, orderDate);
                        editOrder = true;
                    }
                    if (userinput.ToUpper() == "N")
                    {
                        Console.WriteLine("Order will not be edited.");
                        Console.WriteLine("Press any key to return to Main Menu");
                        Console.ReadLine();
                        editOrder = true;
                    }

                    while (finalOrder == false)
                    {

                        Console.Clear();
                        Console.WriteLine(ConsoleIO.OrderFormat, newOrder.OrderNumber, orderDate.ToShortDateString(), newOrder.CustomerName, newOrder.State, newOrder.ProductType, newOrder.MaterialCost, newOrder.LaborCost, newOrder.Tax, newOrder.Total);
                        Console.WriteLine("Would you like to finalize this edit? Enter Y or N?");
                        confirmOrderinput = Console.ReadLine();

                        if (confirmOrderinput.ToUpper() == "N")
                        {
                            Console.WriteLine("Edit Discarded");
                            Console.WriteLine("Press any key to return to main menu");
                            Console.ReadLine();
                            finalOrder = true;
                        }
                        if (confirmOrderinput.ToUpper() == "Y")
                        {
                            ProcessEdit(newOrder, orderToEdit);
                            finalOrder = true;
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("There has been a critical error. Order not edited!");
                Console.WriteLine("Please contact IT.");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
            }
        }

        private void ProcessEdit(Order newOrder, Order orderToEdit)
        {
            var response = manager.EditOrder(newOrder, orderToEdit, newOrder.OrderDate, newOrder.OrderNumber);

            if (response.Success)
            {
                Console.Clear();
                Console.WriteLine("Order successfully edited!");
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("An error has occurred. Order was not edited");
                Console.ReadLine();
            }
        }

        private Order EditOrderData(Order orderToEdit, DateTime orderDate)
        {
            newOrder.OrderDate = orderDate;
            newOrder.OrderNumber = orderToEdit.OrderNumber;

            userinput = GetEditedStringFromUser($"Enter customer name ({orderToEdit.CustomerName}) | Press enter if no change:  ");
            if (userinput == "")
            {
                newOrder.CustomerName = orderToEdit.CustomerName;
            }
            else
            {
                newOrder.CustomerName = userinput;
            }
            

            GetRequiredStateFromUser($"Enter a state ({orderToEdit.State}) | Press enter if no change: ", stateinput);
            newOrder.State = taxResponse.StateTaxInfo.StateAbbreviation;
            newOrder.TaxRate = taxResponse.StateTaxInfo.TaxRate;

            GetRequiredProductTypeFromUser($"Enter Product Type ({orderToEdit.ProductType}) | Press enter if no change: ", productInput);
            newOrder.ProductType = productResponse.ProductInfo.ProductType;
            newOrder.CostPerSquareFoot = productResponse.ProductInfo.CostPerSquareFoot;
            newOrder.LaborCostPerSquareFoot = productResponse.ProductInfo.LaborCostPerSquareFoot;

            numberInput = GetRequiredAreaFromUser($"Enter total area of order ({orderToEdit.Area}) | Press enter if no change: ");
            if (numberInput == 0)
            {
                newOrder.Area = orderToEdit.Area;
            }
            else
            {
                newOrder.Area = numberInput;
            }

            newOrder.LaborCost = newOrder.Area * newOrder.LaborCostPerSquareFoot;
            newOrder.MaterialCost = newOrder.Area * newOrder.CostPerSquareFoot;
            newOrder.Tax = ((newOrder.MaterialCost + newOrder.LaborCost) * (newOrder.TaxRate / 100));
            newOrder.Total = (newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax);



            return newOrder;

        }

        public static int GetRequiredAreaFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);

                string nada = Console.ReadLine();
                int input;

                if (string.IsNullOrEmpty(nada))
                {
                    input = 0;
                    return input;
                }
                else if (int.Parse(nada) < 100)
                {
                    Console.WriteLine("Area must be greater than 100.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    input = int.Parse(nada);
                    return input;
                }
            }
        }


        private static string GetEditedStringFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    return input;
                }
                else
                {
                    return input;
                }
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
                Console.WriteLine("Something has gone wrong please contact IT");
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

        private string GetRequiredStateFromUser(string prompt, string stateInput)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                stateInput = Console.ReadLine();

                if (stateInput.Length != 2 && stateInput.Length != 0)
                {
                    Console.WriteLine("Invalid entry, please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if(stateInput.Length == 0)
                {
                    stateInput = orderToEdit.State;
                    AssignStateInfo(stateInput);
                }
                else
                {
                    AssignStateInfo(stateInput);
                }
                return stateInput.ToUpper();
            }

        }

        private StateTax AssignStateInfo(string stateInput)
        {
            taxResponse = manager.LookupState(stateInput.ToUpper());
            if (taxResponse.Success)
            {
                return taxResponse.StateTaxInfo;
            }
            return null;
        }

        private string GetRequiredProductTypeFromUser(string prompt, string productInput)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                productInput = Console.ReadLine();
                productInput = productInput.ToUpper();

                if (productInput.ToUpper() == "WOOD" || productInput.ToUpper() == "CARPET" || productInput.ToUpper() == "TILE" || productInput.ToUpper() == "LAMINATE")
                {

                    AssignProductInfo(productInput);

                }
                else if (productInput.Length == 0)
                {
                    productInput = orderToEdit.ProductType.ToUpper();
                    AssignProductInfo(productInput);
                }
                else
                {
                    Console.WriteLine("Invalid entry, please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                return productInput;
            }
        }

        private Order AssignProductInfo(string productInput)
        {
            productResponse = manager.LookupProduct(productInput);
            if (productResponse.Success)
            {
                return productResponse.ProductInfo;
            }
            return null;
        }

    }
}
