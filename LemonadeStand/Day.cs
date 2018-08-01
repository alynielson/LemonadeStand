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
        private int totalCustomersPurchased;
        private int numberOfPotentialCustomers;
        private int overallSatisfaction;

        public Day(Random random)
        {
            weather = new Weather(random);
        }
        
        


        private void CalculatePossibleCustomers(Weather weather, Random random)
        {
            double maxPointsFromForecasts = 50;
            double pointsPerForecast = maxPointsFromForecasts/weather.forecasts.Count ; 
            int maxRandomAmount = Convert.ToInt32(pointsPerForecast * weather.forecastRanking);
            int pointsFromForecast = random.Next(0, maxRandomAmount);
            numberOfPotentialCustomers = weather.temperature + pointsFromForecast;
        }

        public void GetPotentialCustomers(Random random)
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

       public void GetResults(Random random, Player player)
        {
            overallSatisfaction = 0;
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
            overallSatisfaction = DetermineOverallSatisfaction(satisfactionPoints);
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

        public void DisplayResults()
        {
            Console.WriteLine($"{totalCustomersPurchased} customers made a purchase out of {numberOfPotentialCustomers} possible.");
            Console.WriteLine($"{ totalCupsBought} cups total were sold.");
            Console.WriteLine($"Overall satisfaction was {overallSatisfaction}%.");
            Console.ReadLine();
        }

    }
}
