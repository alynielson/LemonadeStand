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
        private string name;
        Inventory lemons;
        Inventory ice;
        Inventory cups;
        Inventory sugar;

        public Player()
        {
            totalMoney = 20;
            popularity = 0;
            lemons = new Inventory();
            ice = new Inventory();
            cups = new Inventory();
            sugar = new Inventory();
        }

        public void GetPlayerName(string playerNumber)
        {
            Console.WriteLine($"{playerNumber}, what is your name?");
            name = Console.ReadLine();
            if (name.Length == 0)
            {
                Console.WriteLine("You didn't enter anything! Try again.");
                GetPlayerName(playerNumber);
            }
        }
    }
}
