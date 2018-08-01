using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Customer
    {
        
        
        public bool didPurchaseCups;
        

        public int BuyLemonade(int cupsWantingToPurchase, int cupsActuallyPurchased, int totalCupsBought, Player player)
        {
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
            return cupsActuallyPurchased;
            
        }

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

        public int DetermineIfPurchaseMade(Player player, Random random, Weather weather)
        {
            int cupsPurchased;
            int likelihoodByPrice = CalculateLikelihoodByPrice(player.cupPrice, random);
            int likelihoodByWeather = CalculateLikelihoodByWeather(weather.temperature, weather.forecastRanking);
            int likelihoodByPopularity = AddToLikelihoodIfPopular(player.popularity);
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

        public void CheckIfCupsWerePurchased(int cupsPurchased)
        {
            if (cupsPurchased > 0)
            {
                didPurchaseCups = true;
            }
            else
            {
                didPurchaseCups = false;
            }
        }
        

        public double DetermineIceSatisfaction(Weather weather, Player player, Random random)
        {
            double optimumIcePerCup;
            if (weather.temperature >= 82)
            {
                optimumIcePerCup = Convert.ToDouble(random.Next(4,9));
            }
            else if (weather.temperature >= 67)
            {
                optimumIcePerCup = Convert.ToDouble(random.Next(3,6));
            }
            else
            {
                optimumIcePerCup = Convert.ToDouble(random.Next(1,4));
            }
            double difference = Math.Abs(optimumIcePerCup - player.icePerCup);
            if (difference > optimumIcePerCup)
            {
                difference = optimumIcePerCup;
            }
            double fractionDifference = difference / optimumIcePerCup;
            double iceSatisfaction = 100 - fractionDifference * 100;
            return iceSatisfaction;
        }

        public double DetermineSugarSatisfaction(Weather weather, Player player, Random random)
        {
            double optimumSugarAmount;
            if (weather.forecast == weather.forecasts[3])
            {
                optimumSugarAmount = Convert.ToDouble(random.Next(7, 11));
            }
            else if (weather.forecast == weather.forecasts[2])
            {
                optimumSugarAmount = Convert.ToDouble(random.Next(5, 9));
            }
            else if (weather.forecast == weather.forecasts[1])
            {
                optimumSugarAmount = Convert.ToDouble(random.Next(3, 7));
            }
            else
            {
                optimumSugarAmount = Convert.ToDouble(random.Next(1, 5));
            }
            double difference = Math.Abs(optimumSugarAmount - player.sugarPerPitcher);
            if (difference > optimumSugarAmount)
            {
                difference = optimumSugarAmount;
            }
            double fractionDifference = difference / optimumSugarAmount;
            double sugarSatisfaction = 100 - fractionDifference * 100;
            return sugarSatisfaction;
        }

        public double DetermineLemonSatisfaction(Weather weather, Player player, Random random)
        {
            double number = Convert.ToDouble(random.Next(1, 21));
            double optimumLemonToSugarRatio = number / 10;
            double actualRatio = Convert.ToDouble(player.lemonsPerPitcher) / Convert.ToDouble(player.sugarPerPitcher);
            double difference = Math.Abs(optimumLemonToSugarRatio - actualRatio);
            if (difference > optimumLemonToSugarRatio)
            {
                difference = optimumLemonToSugarRatio;
            }
            double fractionDifference = difference / optimumLemonToSugarRatio;
            double lemonSatisfaction = 100 - fractionDifference * 100;
            return lemonSatisfaction;
        }

        public double DetermineOverallSatisfaction(Weather weather, Player player, Random random)
        {
            double iceSatisfaction = DetermineIceSatisfaction(weather, player, random);
            double sugarSatisfaction = DetermineSugarSatisfaction(weather, player, random);
            double lemonSatisfaction = DetermineLemonSatisfaction(weather, player, random);
            double overallSatisfaction = (iceSatisfaction / 3 + lemonSatisfaction / 3 + sugarSatisfaction / 3);
            return overallSatisfaction;
        }
    }
}
