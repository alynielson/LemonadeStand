using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Player
    {
        private double totalMoney;
        private int popularity;
        private string name;
        Inventory lemons;
        Inventory ice;
        Inventory cups;
        Inventory sugar;
        List<Inventory> typesOfInventory;
        Store store;
        int inventoryNumberChoice;

        public Player()
        {
            totalMoney = 20;
            popularity = 0;
            lemons = new Inventory("lemons");
            ice = new Inventory("ice");
            cups = new Inventory("cups");
            sugar = new Inventory("sugar");
            typesOfInventory = new List<Inventory> {cups,lemons,sugar,ice};
            store = new Store();
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

        private int PickWhatToShopForInList()
        {
            Console.WriteLine($"{name}, what would you like to buy more of? Enter a number.\n1. Cups\n2. Lemons\n3. Sugar\n4.Ice\n5. I'm done shopping!");
            string answer = Console.ReadLine();
            int numberChoice;
            bool isNumeric = Int32.TryParse(answer, out numberChoice);
            while (!isNumeric || numberChoice < 1 || numberChoice > (typesOfInventory.Count + 1))
            {
                Console.WriteLine("You didn't pick a number on the list! Try again.");
                PickWhatToShopForInList();
            }
            inventoryNumberChoice = numberChoice - 1;
            return inventoryNumberChoice;
        }

        private void GoToStoreOrLeavePage(int inventoryNumberChoice)
        {
            int numberToLeavePage = typesOfInventory.Count;
            if (inventoryNumberChoice == numberToLeavePage)
            {
                //go to Recipe page
            }
            else
            {
                store.GetPricesFor(typesOfInventory[inventoryNumberChoice]);
            }
        }

        public void Shop()
        {
            int shopFor = PickWhatToShopForInList();
            GoToStoreOrLeavePage(shopFor);
        }
    }
}
