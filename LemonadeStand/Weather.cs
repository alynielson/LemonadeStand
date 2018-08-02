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
        public string forecastPossibility;

        public Weather(Random random)
        {
            forecasts = new List<string> { "rainy", "cloudy", "hazy", "clear and sunny" };
            GetWeatherPrediction(random);
            temperature = GetTemperature(random);
            forecast = GetForecast(random);
            forecastRanking = GetForecastRanking(forecast);
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

        private string GetPredictedForecast(Random random)
        {
            int forecastPossibilityIndex = random.Next(0, 4);
            forecastPossibility = forecasts[forecastPossibilityIndex];
            return forecastPossibility;
        }

        private void GetWeatherPrediction(Random random)
        {
            int temperatureLow = GetPredictedTemperatureLow(random);
            int temperatureRange = GetPredictedTemperatureRange(random, temperatureLow);
            int temperatureHigh = GetPredictedTemperatureHigh(random, temperatureLow, temperatureRange);
            string forecastPossibility = GetPredictedForecast(random);
            
        }

        private int GetTemperature(Random random)
        {
            temperature = random.Next(temperatureLow, temperatureHigh+1);
            return temperature;
        }

        private string GetForecast(Random random)
        {
            int potentialForecast = forecasts.IndexOf(forecastPossibility);
            int min;
            int max;
            if (potentialForecast < 1)
            {
                min = potentialForecast;
                max = potentialForecast + 2;
            }
            else if (potentialForecast > (forecasts.Count - 2))
            {
                min = potentialForecast - 1;
                max = potentialForecast + 1;
            }
            else
            {
                min = potentialForecast - 1;
                max = potentialForecast + 2;
            }
            int forecastIndex = random.Next(min, max);
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
