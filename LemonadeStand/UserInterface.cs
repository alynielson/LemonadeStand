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

        
    }
}
