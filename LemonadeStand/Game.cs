using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Game
    {
        public int numberOfDays;
        private Player player1;
        private Random random;
        private Store store;
        private List<Day> days;
        private int currentDayIndex = 0;
        

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
                Day newDay = new Day(random);
                days.Add(newDay);
            }
            Console.Clear();
            
        }

        public void PlayGame()
        {
            while (currentDayIndex < numberOfDays && player1.isGameOver == false)
            {
                int action = UserInterface.DisplayMainMenu();
                days[currentDayIndex].CalculatePossibleCustomers(days[currentDayIndex].weather, random);
                days[currentDayIndex].CreateCustomers();
                Console.Clear();
                switch (action)
                {
                    case 1:
                        DisplayWeatherForecast();
                        UserInterface.GoBackToMenu();
                        break;
                    case 2:
                        player1.DetermineRecipeAndPrice();
                        break;
                    case 3:
                        player1.Shop(player1, store);
                        if (player1.isGameOver == true)
                        {
                            break;
                        }
                        break;
                    case 4:
                        DisplayWeather();
                        bool hasCustomers = days[currentDayIndex].CheckForNoCustomers();
                        if (hasCustomers == true)
                        {
                            days[currentDayIndex].GetResults(random, player1);
                        }
                        days[currentDayIndex].DisplayResults();
                        player1.GetPopularity(days[currentDayIndex], numberOfDays);
                        player1.DisplayMoneyResults(days[currentDayIndex].totalCupsBought);
                        UserInterface.GoBackToMenu();
                        currentDayIndex++;
                        player1.dailyMoneySpent = 0;
                        break;
                }  
            }
            player1.DisplayEndResults();
            Console.ReadLine();
            Console.Clear();
        }

        private void DisplayWeather()
        {
            int currentDay = currentDayIndex + 1;
            Console.WriteLine($"Day {currentDay}: {days[currentDayIndex].weather.temperature} degrees, {days[currentDayIndex].weather.forecast}\n ");
        }

        private void DisplayWeatherForecast()
        {
            bool isNumeric;
            int numberChoice;
            do
            {
                Console.WriteLine("(1) See predicted weather for today.\n(2) See predicted weather for the week.\n(3) Back to main menu.");
                string answer = Console.ReadLine();
                Console.Clear();
                isNumeric = Int32.TryParse(answer, out numberChoice);
                if (!isNumeric || numberChoice < 1 || numberChoice > 3)
                {
                    Console.WriteLine("You didn't pick a number on the list! Try again.");
                }
            }
            while (!isNumeric || numberChoice < 1 || numberChoice > 3);
            switch(numberChoice)
            {
                case 1:
                    Console.WriteLine($"Today: low of {days[currentDayIndex].weather.temperatureLow}, high of {days[currentDayIndex].weather.temperatureHigh}");
                    break;
                case 2:
                    for (int i=0; i < days.Count; i++)
                    {
                        int dayNumber = i + 1;
                        Console.WriteLine($"Day {dayNumber}: low of {days[i].weather.temperatureLow}, high of {days[i].weather.temperatureHigh}");
                    }
                    break;
                case 3:
                    break;
            }
        }
          
            
    }

        

        
    
}
