using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Weather
    {
        public int temperature;
        public List<string> forecasts;
        public string forecast;
        public int forecastRanking;

        public Weather(Random random)
        {
            forecasts = new List<string> { "rainy", "cloudy", "hazy", "clear and sunny" };
            forecastRanking = GetForecastRanking(forecast);
            temperature = GetTemperature(random);
            forecast = GetForecast(random);
        }

        private int GetTemperature(Random random)
        {
            temperature = random.Next(50, 101);
            return temperature;
        }

        private string GetForecast(Random random)
        {
            int forecastIndex = random.Next(0, forecasts.Count);
            forecast = forecasts[forecastIndex];
            return forecast;
        }

        public int GetForecastRanking(string forecast)
        {
            forecastRanking = forecasts.IndexOf(forecast) + 1;
            return forecastRanking;
        }
    }
}
