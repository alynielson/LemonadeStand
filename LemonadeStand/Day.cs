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


        private void CalculatePossibleCustomers(Weather weather, Random random)
        {
            double maxPointsFromForecasts = 50;
            double pointsPerForecast = maxPointsFromForecasts/weather.forecasts.Count ; 
            int maxRandomAmount = Convert.ToInt32(pointsPerForecast * weather.forecastRanking);
            int pointsFromForecast = random.Next(0, maxRandomAmount);
            numberOfPotentialCustomers = weather.temperature + pointsFromForecast;
        }

        public void GetPotentialCustomers(Weather weather, Random random)
        {
            CalculatePossibleCustomers(weather, random);
            Console.WriteLine($"{numberOfPotentialCustomers} people walked by your stand today.");
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

       public void GetResults(Random random, Day day, Player player)
        {
            int forecastRanking = day.weather.GetForecastRanking(day.weather.forecast);
            int temperature = day.weather.temperature;
            totalCupsBought = 0;
            totalCustomersPurchased = 0;
            double satisfactionPoints = 0;
            foreach (Customer customer in customers)
            {
                int cupsWantingToPurchase = customer.DetermineIfPurchaseMade(player, random, weather);
                int cupsActuallyPurchased = 0;
                if (cupsWantingToPurchase > 0)
                {

                    cupsActuallyPurchased = customer.BuyLemonade(cupsWantingToPurchase, cupsActuallyPurchased, totalCupsBought, player);
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
            int overallSatisfaction = DetermineOverallSatisfaction(satisfactionPoints);
            
            Console.WriteLine($"{totalCustomersPurchased} customers made a purchase out of {numberOfPotentialCustomers} possible.");
            Console.WriteLine($"{ totalCupsBought} cups total were sold.");
            Console.WriteLine($"Overall satisfaction was {overallSatisfaction}%.");
            Console.ReadLine();
            
        }
        private int DetermineOverallSatisfaction(double satisfactionPoints)
        {
            int overallSatisfaction;
            if (satisfactionPoints == 0)
            {
                overallSatisfaction = 0;
            }
            else
            {
                overallSatisfaction = Convert.ToInt32(Math.Round((satisfactionPoints / totalCustomersPurchased),2));
            }
            return overallSatisfaction;
        }

    }
}
