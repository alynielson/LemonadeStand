using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Player
    {
        private int totalMoney;
        private int popularity;
        private int name;
        Inventory lemons;
        Inventory ice;
        Inventory cups;
        Inventory sugar;

        private Player()
        {
            totalMoney = 20;
            popularity = 0;
            lemons = new Inventory();
            ice = new Inventory();
            cups = new Inventory();
            sugar = new Inventory();
        }
    }
}
