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
        public int temperatureLow;
        public int temperatureHigh;

        public Weather(Random random)
        {
            forecasts = new List<string> { "rainy", "cloudy", "hazy", "clear and sunny" };
            forecastRanking = GetForecastRanking(forecast);
            GetWeatherPrediction(random);
            temperature = GetTemperature(random);
            forecast = GetForecast(random);
        }

        private int GetPredictedTemperatureLow(Random random)
        {
            temperatureLow = random.Next(50, 86);
            return temperatureLow;
        }

        private int GetPredictedTemperatureRange(Random random, int temperatureLow)
        {
            int temperatureRange = random.Next(1, 16);
            return temperatureRange;
        }

        private int GetPredictedTemperatureHigh(Random random, int temperatureLow, int range)
        {
            temperatureHigh = random.Next(temperatureLow + 1, temperatureLow + 16);
            return temperatureHigh;
        }

        private void GetWeatherPrediction(Random random)
        {
            int temperatureLow = GetPredictedTemperatureLow(random);
            int temperatureRange = GetPredictedTemperatureRange(random, temperatureLow);
            int temperatureHigh = GetPredictedTemperatureHigh(random, temperatureLow, temperatureRange);
        }

        private int GetTemperature(Random random)
        {
            temperature = random.Next(temperatureLow, temperatureHigh+1);
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
