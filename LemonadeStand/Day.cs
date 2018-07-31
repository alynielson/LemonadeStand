using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Day
    {
        public Weather weather;
        private List<Customer> customers;
        public int totalCupsBought;
        public int totalCustomersPurchased;

        public Day(Random random)
        {
            weather = new Weather(random);
        }
        
        public int numberOfPotentialCustomers;


        private void CalculatePossibleCustomers(int forecastRanking, int temperature, Random random)
        {
            double maxPointsFromForecasts = 50;
            double pointsPerForecast = maxPointsFromForecasts/weather.forecasts.Count ; 
            int maxRandomAmount = Convert.ToInt32(pointsPerForecast * forecastRanking);
            int pointsFromForecast = random.Next(0, maxRandomAmount);
            numberOfPotentialCustomers = temperature + pointsFromForecast;
        }

        public void GetPotentialCustomers(Day day, Random random)
        {
            int forecastRanking = day.weather.GetForecastRanking(day.weather.forecast);
            int temperature = day.weather.temperature;
            day.CalculatePossibleCustomers(forecastRanking, temperature, random);
            Console.WriteLine($"{day.numberOfPotentialCustomers} people will walk by your stand today.");
        }

        public void CreateCustomers()
        {
            customers = new List<Customer> { };
            for (int i = 0; i < numberOfPotentialCustomers; i ++)
            {
                Customer customer = new Customer();
                customers.Add(customer);
            }
        }

       public void DetermineCupsPurchased(double cupPrice, Random random, int popularity, Day day, Player player)
        {
            int forecastRanking = day.weather.GetForecastRanking(day.weather.forecast);
            int temperature = day.weather.temperature;
            totalCupsBought = 0;
            totalCustomersPurchased = 0;
            double satisfactionPoints = 0;
            foreach (Customer customer in customers)
            {
                int cupsWantingToPurchase = customer.DetermineIfPurchaseMade(cupPrice, random, temperature, forecastRanking, popularity);
                int cupsActuallyPurchased;
                if (cupsWantingToPurchase > 0)
                {
                    cupsActuallyPurchased = 0;
                    for (int i = 0; i < cupsWantingToPurchase; i++)
                    {
                        player.MakeCup();
                        if (player.isOutOfSupplies == false)
                        {
                            cupsActuallyPurchased++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    totalCupsBought += cupsActuallyPurchased;
                    customer.CheckIfCupsWerePurchased(cupsActuallyPurchased);
                }
                if (customer.didPurchaseCups == true)
                {
                    totalCustomersPurchased++;
                    satisfactionPoints+= customer.DetermineOverallSatisfaction(weather, player, random);
                }
                if (player.isOutOfSupplies == true)
                {
                    break;
                }
            }
            double overallSatisfaction = Convert.ToInt32(satisfactionPoints / totalCustomersPurchased);
            Console.WriteLine($"{totalCustomersPurchased} customers made a purchase out of {numberOfPotentialCustomers} possible.");
            Console.WriteLine($"{ totalCupsBought} cups total were sold.");
            Console.WriteLine($"Overall satisfaction was {overallSatisfaction}%.");
            Console.ReadLine();
            
        }


    }
}
