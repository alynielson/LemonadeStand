using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Inventory
    {
        public int quantity;
        public string name;

        public Inventory(string type)
        {
            name = type;
            quantity = 0;
        }

       
    }
}
