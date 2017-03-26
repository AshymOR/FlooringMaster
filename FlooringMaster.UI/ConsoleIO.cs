using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMaster.UI
{
    public class ConsoleIO
    {
        public const string OrderFormat =
@"{0} {1}
{2}
{3}
Product: {4}
Materials: {5:c}
Labor: {6:c}
Tax: {7:c}
Total: {8:c} 
*************************";
        public static string GetRequiredStringFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter valid text");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        public static int GetRequiredAreaFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);

                int input = int.Parse(Console.ReadLine());


                if (input < 100)
                {
                    Console.WriteLine("Area must be greater than 100.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }
        //Delete
        public static string GetRequiredProductTypeFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (input != "Wood")
                {
                    Console.WriteLine("That is not a product that we carry.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }
        
        public static string GetDateFromUser(string prompt)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || input.Length != 10)
                {
                    Console.WriteLine("You must enter valid date");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        public static int GetNumberFromUser(string prompt)
        {
            int number;
            string userInput = "";

            do
            {
                Console.WriteLine(prompt);

                userInput = Console.ReadLine();

            } while (!int.TryParse(userInput, out number));

            return number;
        }

        public static string GetFutureDateFromUser(string prompt)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || input.Length != 10 || DateTime.Today >= DateTime.Parse(input))
                {
                    Console.WriteLine("You must enter valid future date");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                else
                {
                    return input;
                }
            }
        }
    }
}
