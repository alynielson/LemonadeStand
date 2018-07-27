using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Weather
    {
        private int temperature;
        private List<string> forecasts;
        private string forecast;

        public Weather()
        {
            forecasts = new List<string> { "rainy", "cloudy", "hazy", "clear and sunny" };
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
    }
}
