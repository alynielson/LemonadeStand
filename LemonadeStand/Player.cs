using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Player
    {
        public double totalMoney;
        private int popularity;
        public string name;
        List<Inventory> typesOfInventory;
        
        bool isStillShopping;

        public Player()
        {
            totalMoney = 20;
            popularity = 0;
            Inventory lemons = new Inventory("lemons");
            Inventory ice = new Inventory("ice cubes");
            Inventory cups = new Inventory("cups");
            Inventory sugar = new Inventory("cups of sugar");
            typesOfInventory = new List<Inventory> {cups,lemons,sugar,ice};
            
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
            Console.WriteLine($"{name}, what would you like to buy more of? Enter a number.\n1. Cups\n2. Lemons\n3. Sugar\n4. Ice\n5. I'm done shopping!");
            string answer = Console.ReadLine();
            int numberChoice;
            bool isNumeric = Int32.TryParse(answer, out numberChoice);
            int maxNumberChoice = typesOfInventory.Count + 1;
            while (!isNumeric || numberChoice < 1 || numberChoice > maxNumberChoice)
            {
                Console.WriteLine("You didn't pick a number on the list! Try again.");
                PickWhatToShopForInList();
            }
            int inventoryNumberChoice = numberChoice - 1;
            return inventoryNumberChoice;
        }

        private void GoToStoreOrLeavePage(int inventoryNumberChoice, Player player, Store store)
        {
            int numberToLeavePage = typesOfInventory.Count;
            if (inventoryNumberChoice == numberToLeavePage)
            {
                isStillShopping = false;
                Console.WriteLine("You're done shopping!");
                Console.ReadLine();
            }
            else
            {
                store.DisplayPricesAndMakePurchase(typesOfInventory[inventoryNumberChoice], player);
            }
        }

        public void Shop(Player player, Store store)
        {
            isStillShopping = true;
            do
            {
                DisplayCurrentInventory(player);
                int shopFor = PickWhatToShopForInList();
                GoToStoreOrLeavePage(shopFor, player, store);
            }
            while (isStillShopping == true);
            
        }

        private void DisplayCurrentInventory(Player player)
        {
            Console.WriteLine($"{name}'s Current Status");
            foreach (Inventory inventory in typesOfInventory)
            {
                Console.WriteLine($"{inventory.name}: {inventory.quantity}");
            }
            Console.WriteLine($"Money: ${player.totalMoney}");
        }
    }
}
