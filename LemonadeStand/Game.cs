﻿using System;
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
            while (currentDayIndex < numberOfDays)
            {
                DisplayWeather();
                player1.Shop(player1, store);
                player1.DetermineRecipeAndPrice();
                days[currentDayIndex].GetPotentialCustomers(random);
                days[currentDayIndex].CreateCustomers();
                days[currentDayIndex].GetResults(random, player1);
                days[currentDayIndex].DisplayResults();
                player1.GetPopularity(days[currentDayIndex], numberOfDays);
                player1.DisplayResults(days[currentDayIndex].totalCupsBought);
                currentDayIndex++;
            }
        }

        private void DisplayWeather()
        {
            int currentDay = currentDayIndex + 1;
            Console.WriteLine($"Day {currentDay}: {days[currentDayIndex].weather.temperature} degrees, {days[currentDayIndex].weather.forecast}\n ");
        }

        

        
    }
}
