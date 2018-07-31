using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Customer
    {
        
        int satisfactionByPrice;
        int satisfactionByRecipe;
        
        int satisfaction;
        

        public int CalculateLikelihoodByPrice(double cupPrice, Random random)
        {
            int percentageBasedOnPrice = 40;
            int percentageThatAffectsTotal = 60;
            int cents = Convert.ToInt32(cupPrice * 100);
            decimal pricePointsDouble = (100m-cents) / 100m * percentageBasedOnPrice;
            int max = Convert.ToInt32(pricePointsDouble);
            int pricePoints = random.Next(0, max);
            int randomPoints = random.Next(0, percentageThatAffectsTotal - percentageBasedOnPrice +1);
            int likelihoodByPrice = pricePoints + randomPoints;
            return likelihoodByPrice;
        }

        public int CalculateLikelihoodByWeather(int temperature, int forecastRanking)
        {
            int totalPercentage = 40;
            int temperaturePercentage = Convert.ToInt32(totalPercentage / 2);
            int forecastPercentage = totalPercentage - temperaturePercentage;
            int temperatureLikelihood = Convert.ToInt32((temperature * temperaturePercentage / 100));
            int forecastLikelihood = Convert.ToInt32((forecastRanking * forecastPercentage / 4));
            int likelihoodByWeather = temperatureLikelihood + forecastLikelihood;
            return likelihoodByWeather;
        }

        public int AddToLikelihoodIfPopular(int popularity)
        {
            int likelihoodByPopularity = 0;
            if (popularity > 0)
            {
                likelihoodByPopularity = Convert.ToInt32(popularity / 10);
            }
            return likelihoodByPopularity;
        }

        public int DetermineIfPurchaseMade(double cupPrice, Random random, int temperature, int forecastRanking, int popularity)
        {
            int cupsPurchased;
            int likelihoodByPrice = CalculateLikelihoodByPrice(cupPrice, random);
            int likelihoodByWeather = CalculateLikelihoodByWeather(temperature, forecastRanking);
            int likelihoodByPopularity = AddToLikelihoodIfPopular(popularity);
            int likelihoodOutOf100 = likelihoodByPrice + likelihoodByWeather;
            if (likelihoodOutOf100 < 100)
            {
                likelihoodOutOf100 += likelihoodByPopularity;
            }
            if (likelihoodOutOf100 > 100)
            {
                likelihoodOutOf100 = 100;
                cupsPurchased = 4;
            }
            else if (likelihoodOutOf100 >= 90)
            {
                cupsPurchased = 3;
            }
            else if (likelihoodOutOf100 >= 80)
            {
                cupsPurchased = 2;
            }
            else if (likelihoodOutOf100 >= 50)
            {
                cupsPurchased = 1;
            }
            else
            {
                cupsPurchased = 0;
            }
            return cupsPurchased;
        }

        public bool CheckIfCupsWerePurchased(int cupsPurchased)
        {
            bool purchasedCups = false;
            if (cupsPurchased > 0)
            {
                purchasedCups = true;
            }
            return purchasedCups;
        }
    }
}
