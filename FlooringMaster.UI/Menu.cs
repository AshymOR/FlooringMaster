using FlooringMaster.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMaster.UI
{
    public class Menu
    {
        public static void DisplayMenu()
        {

            Console.Clear();
            Console.WriteLine("***************************************");
            Console.WriteLine("* Fette's Floors Ordering System");
            Console.WriteLine("*");
            Console.WriteLine("* 1. Display Orders");
            Console.WriteLine("* 2. Add an Order");
            Console.WriteLine("* 3. Edit an Order");
            Console.WriteLine("* 4. Remove an Order");
            Console.WriteLine("* 5. Quit");
            Console.WriteLine("*");
            Console.WriteLine("***************************************");
        }

        private static bool ProcessChoice()
        {
            string userinput = Console.ReadLine();

            switch (userinput)
            {
                case "1":
                    ListOrdersWorkflow listorderworkflow = new ListOrdersWorkflow();
                    listorderworkflow.Execute();
                    break;
                case "2":
                    AddOrderWorkflow addorderworkflow = new AddOrderWorkflow();
                    addorderworkflow.Execute();
                    break;
                case "3":
                    EditOrderWorkflow editorderworkflow = new EditOrderWorkflow();
                    editorderworkflow.Execute();
                    break;
                case "4":
                    RemoveOrderWorkflow removeorderworkflow = new RemoveOrderWorkflow();
                    removeorderworkflow.Execute();
                    break;
                case "5":
                    return false;
                default:
                    Console.WriteLine("That is not a valid Choice. Press any key to continue.");
                    Console.ReadKey();
                    break;

            }
            return true;
        }

        public static void Start()
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                DisplayMenu();
                continueRunning = ProcessChoice();
            }
        }

    }         
}

    

