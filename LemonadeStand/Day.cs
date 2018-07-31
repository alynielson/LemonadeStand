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
        

        public Day(Random random)
        {
            weather = new Weather(random);
        }
        
        public int numberOfPotentialCustomers;


        public void GetNumberOfPossibleCustomers(int forecastRanking, int temperature, int popularity, Random random)
        {
            double maxPointsFromForecasts = 50;
            double pointsPerForecast = maxPointsFromForecasts/weather.forecasts.Count ; 
            int maxRandomAmount = Convert.ToInt32(pointsPerForecast * forecastRanking);
            int pointsFromForecast = random.Next(0, maxRandomAmount);
            if (popularity > 1)
            {
                int maxPopularityEffect = 10;
                int popularityEffect = Convert.ToInt32(popularity / maxPopularityEffect);
            }
            numberOfPotentialCustomers = temperature + pointsFromForecast + popularity;
        }

        
        
    }
}
