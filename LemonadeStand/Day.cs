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
        
        int numberOfPotentialCustomers;

        private void GetNumberOfPossibleCustomers(Weather weather, Player player)
        {
               
        }

        
        
    }
}
