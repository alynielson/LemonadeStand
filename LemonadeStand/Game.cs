using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Game
    {
        private int numberOfDays;
        private Player player1;
        private Random random;
        private Store store;
        private List<Day> days;

        public Game()
        {
            random = new Random();
        }

        private void GetNumberOfDays()
        {
            Console.WriteLine("How many days will you be running your lemonade stand for? Enter a number. Must be at least 7.");
            string response = Console.ReadLine();
            numberOfDays = 0;
            bool isNumericAnswer = Int32.TryParse(response, out numberOfDays);
            if (!isNumericAnswer || numberOfDays < 7 )
            {
                Console.WriteLine("You didn't enter a number that is at least 7! Try again.");
                GetNumberOfDays();
            }
        }

        public void SetUpGame()
        {
            store = new Store();
            player1 = new Player();
            player1.GetPlayerName("Player One");
            GetNumberOfDays();
            days = new List<Day> { };
            for (int i = 0; i < numberOfDays; i++)
            {
                Day newDay = new Day();
                days.Add(newDay);
            }
            Console.WriteLine(days.Count);
            Console.ReadLine();
            Console.Clear();
        }

        public void StartGame()
        {
            player1.Shop(player1, store);
        }
    }
}
