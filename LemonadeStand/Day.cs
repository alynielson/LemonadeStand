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

       public void DetermineCupsPurchased(double cupPrice, Random random, int popularity, Day day)
        {
            int forecastRanking = day.weather.GetForecastRanking(day.weather.forecast);
            int temperature = day.weather.temperature;
            int totalCupsBought = 0;
            int totalCustomersPurchased = 0;
            foreach (Customer customer in customers)
            {
                int cupsPurchased = customer.DetermineIfPurchaseMade(cupPrice, random, temperature, forecastRanking, popularity);
                totalCupsBought += cupsPurchased;
                if (customer.CheckIfCupsWerePurchased(cupsPurchased) == true)
                {
                    totalCustomersPurchased++;
                }
            }
            Console.WriteLine($"{totalCustomersPurchased} customers bought, {totalCupsBought} cups sold");
            Console.ReadLine();
            
        }


    }
}
