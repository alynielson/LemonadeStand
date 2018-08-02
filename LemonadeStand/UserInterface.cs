using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    public static class UserInterface
    {
        
        public static int DisplayMainMenu()
        {
            int numberChoice;
            bool isNumericAnswer;
            do
            {
                Console.WriteLine("Choose an option.");
                Console.WriteLine("(1) Display weather forecast for today or the week.");
                Console.WriteLine("(2) Change your price or recipe.");
                Console.WriteLine("(3) View your inventory and/or shop for more.");
                Console.WriteLine("(4) Start selling for the day!");
                string answer = Console.ReadLine();
                isNumericAnswer = Int32.TryParse(answer, out numberChoice);
            }
            while (!isNumericAnswer || numberChoice < 1 || numberChoice > 4);
            return numberChoice;

        }

        public static void GoBackToMenu()
        {
            Console.WriteLine("Press enter to go back to the menu.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void DisplayStorePrices(string name, int qty, double priceForQty1, int qty2, double priceForQty2, int qty3, double priceForQty3)
        {
                Console.Clear();
                Console.WriteLine($"How many {name} would you like to buy? Enter a number.");
                Console.WriteLine($"1. {qty} {name} for {priceForQty1}.");
                Console.WriteLine($"2. {qty2} {name} for {priceForQty2}.");
                Console.WriteLine($"3. {qty3} {name} for {priceForQty3}.");
                Console.WriteLine("4. Back to main store page.");
        }

        public static int ValidateNumberResponse()
        {
            string answer = Console.ReadLine();
            bool isNumeric = Int32.TryParse(answer, out int numberChoice);
            if (numberChoice == 0)
            {
                Console.WriteLine("You didn't enter a valid number! Press Enter to try again.");
                Console.ReadLine();
            }
            return numberChoice;
        }

        public static void DisplayEndResults(string name, double totalMoney, double totalMoneySpent, double totalMoneyGained, int popularity)
        {
            Console.WriteLine($"{name}, you end the game with ${totalMoney}.");
            Console.WriteLine($"You spent ${totalMoneySpent} and made ${totalMoneyGained} in sales.");
            Console.WriteLine($"Your popularity was ${popularity}%.");
            Console.ReadLine();
        }


    }
}
