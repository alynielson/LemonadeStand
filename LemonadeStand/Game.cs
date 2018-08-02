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
            int minDays = 7;
            int maxDays = 60;
            do
            {
                Console.WriteLine("How many days will you be running your lemonade stand for? Enter a number. Must be at least 7.");
                numberOfDays = UserInterface.ValidateNumberResponse(minDays, maxDays);
            }
            while (numberOfDays < minDays);
            
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
                        MakeDayHappen();
                        EndOfDayActions();
                        break;
                }  
            }
            UserInterface.DisplayEndResults(player1.name, player1.totalMoney, player1.totalMoneySpent, player1.totalMoneyGained,player1.popularity);
            Console.ReadLine();
            Console.Clear();
        }

        private void MakeDayHappen()
        {
            DisplayWeather();
            bool hasCustomers = days[currentDayIndex].CheckForNoCustomers();
            if (hasCustomers == true)
            {
                days[currentDayIndex].GetResults(random, player1);
            }
            days[currentDayIndex].DisplayResults();
            player1.MeltIce();
            player1.GetPopularity(days[currentDayIndex], numberOfDays);
            player1.DisplayMoneyResults(days[currentDayIndex].totalCupsBought);
            
        }

        private void EndOfDayActions()
        {
            UserInterface.GoBackToMenu();
            currentDayIndex++;
            player1.dailyMoneySpent = 0;
            player1.isOutOfSupplies = false;
        }

        private void DisplayWeather()
        {
            int currentDay = currentDayIndex + 1;
            Console.WriteLine($"Day {currentDay}: {days[currentDayIndex].weather.temperature} degrees, {days[currentDayIndex].weather.forecast}\n ");
        }

        private int WeatherForecastMenu()
        {
            int listMin = 1;
            int listMax = 3;
            int numberChoice;
            do
            {
                Console.WriteLine("(1) See predicted weather for today.\n(2) See predicted weather for the week.\n(3) Back to main menu.");
                numberChoice = UserInterface.ValidateNumberResponse(listMin, listMax);
            }
            while (numberChoice < listMin || numberChoice > listMax);
            return numberChoice;
        }

        private void DisplayWeatherForecast()
        {
            int numberChoice = WeatherForecastMenu();
            switch(numberChoice)
            {
                case 1:
                    Console.WriteLine($"Today: low of {days[currentDayIndex].weather.temperatureLow}, high of {days[currentDayIndex].weather.temperatureHigh}, possibly {days[currentDayIndex].weather.forecastPossibility}");
                    UserInterface.GoBackToMenu();
                    break;
                case 2:
                    for (int i=0; i < days.Count; i++)
                    {
                        int dayNumber = i + 1;
                        Console.WriteLine($"Day {dayNumber}: low of {days[i].weather.temperatureLow}, high of {days[i].weather.temperatureHigh}, {days[i].weather.forecastPossibility}");
                    }
                    UserInterface.GoBackToMenu();
                    break;
                case 3:
                    Console.Clear();
                    break;
            }
        }
          
            
    }

        

        
    
}
