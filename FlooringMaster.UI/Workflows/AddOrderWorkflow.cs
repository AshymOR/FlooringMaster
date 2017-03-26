using FlooringMaster.BLL;
using FlooringMaster.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlooringMaster.UI.Workflows
{
    public class AddOrderWorkflow
    {
        OrderManager manager = OrderManagerFactory.Create();
        private StateTaxLookupResponse taxResponse = new StateTaxLookupResponse();
        private ProductLookupResponse productResponse = new ProductLookupResponse();


        public void Execute()
        {

                DateTime orderInputDate;
                string orderStringDate;
                Order newOrder = new Order();
                string stateInput = "";
                string productInput = "";
                string confirmOrderinput;
                bool addOrder = false;
                

                Console.Clear();
                Console.WriteLine("Add Order");

                orderInputDate = DateTime.Parse(ConsoleIO.GetFutureDateFromUser("Enter a future date for this new order: "));

                orderStringDate = orderInputDate.ToString("MMddyyyy");
                newOrder.OrderDate = orderInputDate;

                var getOrders = manager.LookupAddOrder(newOrder.OrderDate);


                newOrder.OrderNumber = OrderManager.AssignOrderNumber(manager.LookupAddOrder(newOrder.OrderDate));
                newOrder.CustomerName = ConsoleIO.GetRequiredStringFromUser("Enter Customer Name: ");

                GetRequiredStateFromUser("Enter a state for this order:", stateInput);
                newOrder.State = taxResponse.StateTaxInfo.StateAbbreviation;
                newOrder.TaxRate = taxResponse.StateTaxInfo.TaxRate;

                GetRequiredProductTypeFromUser("Enter a product type for this Customer's order. (Carpet, Laminate, Tile, Wood)", productInput);
                newOrder.ProductType = productResponse.ProductInfo.ProductType;
                newOrder.CostPerSquareFoot = productResponse.ProductInfo.CostPerSquareFoot;
                newOrder.LaborCostPerSquareFoot = productResponse.ProductInfo.LaborCostPerSquareFoot;

                newOrder.Area = ConsoleIO.GetRequiredAreaFromUser("Enter total area of order. Must be greater than 100.");

                newOrder.LaborCost = newOrder.Area * newOrder.LaborCostPerSquareFoot;
                newOrder.MaterialCost = newOrder.Area * newOrder.CostPerSquareFoot;
                newOrder.Tax = ((newOrder.MaterialCost + newOrder.LaborCost) * (newOrder.TaxRate / 100));
                newOrder.Total = (newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax);

                while (addOrder == false)
                {
                    Console.Clear();
                    Console.WriteLine(ConsoleIO.OrderFormat, newOrder.OrderNumber, orderInputDate.ToShortDateString(), newOrder.CustomerName, newOrder.State, newOrder.ProductType, newOrder.MaterialCost, newOrder.LaborCost, newOrder.Tax, newOrder.Total);
                    Console.WriteLine("Would you like to add this order? Enter Y or N.");
                    confirmOrderinput = Console.ReadLine();

                    if (confirmOrderinput.ToUpper() == "N")
                    {
                        Console.WriteLine("Order discarded");
                        Console.WriteLine("Press any key to return to main menu");
                        Console.ReadLine();
                        addOrder = true;
                    }
                    if (confirmOrderinput.ToUpper() == "Y")
                    {
                        ProcessOrder(newOrder);
                        addOrder = true;
                    }
                }

        }


        private string GetRequiredStateFromUser(string prompt, string stateInput)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                stateInput = Console.ReadLine();

                if (stateInput.Length != 2)
                {
                    Console.WriteLine("Invalid entry, please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (stateInput.ToUpper() != "OH" && stateInput.ToUpper() != "PA" && stateInput.ToUpper() != "MI" && stateInput.ToUpper() != "IN")
                {
                    Console.WriteLine("Invalid entry, only OH, PA, MI, and IN are state we do business in.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    AssignStateInfo(stateInput);
                    return stateInput.ToUpper();
                }
               
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
                    return productInput;
                }
                else
                {
                    Console.WriteLine("Invalid entry, please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                
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

        public void ProcessOrder(Order order)
        {
            var response = manager.AddOrder(order);

            if (response.Success)
            {
                Console.Clear();
                Console.WriteLine("Order succesfully added!");
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("An Error has occurred. Order not added.");
                Console.ReadLine();
            }
            
        }

    }
}
