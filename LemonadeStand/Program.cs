using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Lemonade Stand!");
            Game newGame = new Game();
            newGame.StartGame();
        }
    }
}
