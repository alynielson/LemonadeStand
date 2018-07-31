﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Player
    {
        public double totalMoney;
        public int popularity=0;
        public string name;
        List<Inventory> typesOfInventory;
        double cupPrice = 0.25;
        int sugarPerPitcher = 4;
        int icePerCup = 4;
        int lemonsPerPitcher =4 ;
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
            int numberChoice;
            bool isNumeric;
            int maxNumberChoice = typesOfInventory.Count + 1;
            do
            {
                Console.WriteLine($"{name}, what would you like to buy more of? Enter a number.\n1. Cups\n2. Lemons\n3. Sugar\n4. Ice\n5. I'm done shopping!");
                string answer = Console.ReadLine();
                isNumeric = Int32.TryParse(answer, out numberChoice);
                if (isNumeric == false || numberChoice < 1 || numberChoice > maxNumberChoice)
                {
                    Console.WriteLine("You didn't pick a number on the list! Try again.");
                }
            }
            while (isNumeric == false || numberChoice < 1 || numberChoice > maxNumberChoice);
            int inventoryNumberChoice = numberChoice - 1;
            return inventoryNumberChoice;
        }

        private void GoToStoreOrLeavePage(int inventoryNumberChoice, Player player, Store store)
        {
            int numberToLeavePage = typesOfInventory.Count;
            if (inventoryNumberChoice == numberToLeavePage)
            {
                isStillShopping = false;
                Console.Clear();
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

        public void DetermineRecipeAndPrice()
        {
            do
            {
                isStillShopping = true;
                int action = DisplayRecipeAndPrice();
                ChangeRecipeOrPrice(action);
            }
            while (isStillShopping == true);
        }

        private int DisplayRecipeAndPrice()
        {
            bool isNumeric;
            int numberChoice;
            do
            {
                Console.WriteLine($"{name}'s Price/Quality Control. Enter a number to edit.");
                Console.WriteLine($"1. Price per cup: ${cupPrice}");
                Console.WriteLine($"2. Lemons per pitcher: {lemonsPerPitcher} lemons");
                Console.WriteLine($"3. Sugar per pitcher: {sugarPerPitcher} cups");
                Console.WriteLine($"4. Ice per cup: {icePerCup} cubes");
                Console.WriteLine($"5. Start selling!");
                string answer = Console.ReadLine();
                isNumeric = Int32.TryParse(answer, out numberChoice);
                if (!isNumeric || numberChoice < 1 || numberChoice > 5)
                {
                    Console.WriteLine("You didn't pick a number on the list! Try again.");
                }
            }
            while (!isNumeric || numberChoice < 1 || numberChoice > 5);
            return numberChoice;
        }

        private void ChangeRecipeOrPrice(int numberChoice)
        {
            Console.Clear();
            switch (numberChoice)
            {
                case 1:
                    ChangeCupPrice();
                    break;
                case 2:
                    ChangeLemonsPerPitcher();
                    break;
                case 3:
                    ChangeSugarPerPitcher();
                    break;
                case 4:
                    ChangeIcePerCup();
                    break;
                case 5:
                    isStillShopping = false;
                    break;
            }
        }

        private void ChangeCupPrice()
        {
            int number;
            bool isNumeric;
            do
            {
                Console.WriteLine("Enter the number of cents to sell your cups for.");
                string answer = Console.ReadLine();
                isNumeric = Int32.TryParse(answer, out number);
                if (!isNumeric || number < 1 || number > 99)
                {
                    Console.WriteLine("The price must be from 1 to 99 cents. Try again.");
                }
            }
            while (!isNumeric || number < 1 || number > 99);
            cupPrice = ConvertCentsToDollars(number);
        }

        private double ConvertCentsToDollars(int cents)
        {
            double centsDouble = Convert.ToDouble(cents);
            double centsPerDollar = 100;
            cupPrice = centsDouble / centsPerDollar;
            return cupPrice;
        }

        private void ChangeLemonsPerPitcher()
        {
            int number;
            bool isNumeric;
            do
            {
                Console.WriteLine("How many lemons will you use per pitcher?");
                string answer = Console.ReadLine();
                isNumeric = Int32.TryParse(answer, out number);
                if (!isNumeric || number < 1)
                {
                    Console.WriteLine("You didn't enter a valid number! Try again.");
                }
            }
            while (!isNumeric || number < 1);
            lemonsPerPitcher = number;
        }

        private void ChangeSugarPerPitcher()
        {
            int number;
            bool isNumeric;
            do
            {
                Console.WriteLine("How many cups of sugar will you use per pitcher?");
                string answer = Console.ReadLine();
                isNumeric = Int32.TryParse(answer, out number);
                if (!isNumeric || number < 1)
                {
                    Console.WriteLine("You didn't enter a valid number! Try again.");
                }
            }
            while (!isNumeric || number < 1);
            sugarPerPitcher = number;
        }

        private void ChangeIcePerCup()
        {
            int number;
            bool isNumeric;
            do
            {
                Console.WriteLine("How much ice will you use per cup?");
                string answer = Console.ReadLine();
                isNumeric = Int32.TryParse(answer, out number);
                if (!isNumeric || number < 1)
                {
                    Console.WriteLine("You didn't enter a valid number! Try again.");
                }
            }
            while (!isNumeric || number < 1);
            icePerCup = number;
        }
    }
}
