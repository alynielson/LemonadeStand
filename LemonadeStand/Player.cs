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
        public double dailyMoneySpent = 0;
        public double totalMoneySpent = 0;
        public double totalMoneyGained = 0;
        public int popularity=0;
        public string name;
        List<Inventory> typesOfInventory;
        public double cupPrice = 0.25;
        public int sugarPerPitcher = 4;
        public int icePerCup = 4;
        public int lemonsPerPitcher =4 ;
        public bool isStillShopping;
        int cupsInPitcher;
        public bool isOutOfSupplies;
        public bool isGameOver = false;
        Inventory lemons;
        Inventory cups;
        Inventory sugar;
        Inventory ice;

        public Player()
        {
            totalMoney = 20;
            popularity = 0;
            lemons = new Inventory("lemons");
            ice = new Inventory("ice cubes");
            cups = new Inventory("cups");
            sugar = new Inventory("cups of sugar");
            typesOfInventory = new List<Inventory> {cups,lemons,sugar,ice};
            
        }

        private string DetermineGainOrLoss(double amount)
        {
            string gainOrLoss = "loss";
            if (amount > 0)
            {
                gainOrLoss = "gain";
            }
            return gainOrLoss;
        }

        public bool CheckIfBankrupt(double priceOfItem)
        {
            bool isBankrupt = false;
            if (totalMoney < priceOfItem)
            {
                isBankrupt = true;
            }
            return isBankrupt;
        }

        public void DeclareBankruptcy()
        {
            Console.Clear();
            Console.WriteLine($"{name}, you've gone bankrupt! Your game is over!");
            totalMoney = 0;
            isGameOver = true;
            
        }

        private void DetermineNetChange(int totalCupsBought)
        {
            double moneyChange = GetChangeInMoney(totalCupsBought);
            totalMoney = ChangeTotalMoney(moneyChange);
            totalMoneySpent += dailyMoneySpent;
            totalMoneyGained += moneyChange;
            double netChange = moneyChange - dailyMoneySpent;
            string gainOrLoss = DetermineGainOrLoss(netChange);
            netChange = Math.Abs(netChange);
            Console.WriteLine($"{name}, today you spent ${dailyMoneySpent} at the store and made ${moneyChange} in sales.\nYour net {gainOrLoss} for the day is ${netChange}.");
        }
        private void DetermineTotalChange(int totalCupsBought)
        {
            double totalChange = totalMoney - 20;
            string gainOrLoss = DetermineGainOrLoss(totalChange);
            totalChange = Math.Abs(totalChange);
            Console.WriteLine($"You end the day with ${totalMoney} total. Your net {gainOrLoss} so far is ${totalChange}.");
        }

        public void DisplayMoneyResults(int totalCupsBought)
        {
            DetermineNetChange(totalCupsBought);
            DetermineTotalChange(totalCupsBought);
        }

        

        
        public void GetPopularity(Day day, int numberOfDays)
        {
            double percentageOfOneDay = 100 / Convert.ToDouble(numberOfDays);
            int addToPopularity = Convert.ToInt32(Convert.ToDouble(day.overallSatisfaction) * percentageOfOneDay / 100);
            popularity += addToPopularity;
            Console.WriteLine($"Your overall popularity is {popularity}%.");
        }

        public double GetChangeInMoney(int totalCupsBought)
        {
            double moneyChange = cupPrice * totalCupsBought;
            return moneyChange;
        }

        public double ChangeTotalMoney(double moneyChange)
        {
            totalMoney += moneyChange;
            return totalMoney;
        }
        public void MakePitcher()
        {
            if (lemons.quantity < lemonsPerPitcher || sugar.quantity < sugarPerPitcher)
            {
                isOutOfSupplies = true;
                Console.WriteLine("You ran out of supplies to make a new pitcher! Sold out for the day!");
            }
            else
            {
                lemons.quantity -= lemonsPerPitcher;
                sugar.quantity -= sugarPerPitcher;
                cupsInPitcher = 10;
            }
        }
        public void MakeCup()
        {
            if (cups.quantity < 1 || ice.quantity < icePerCup)
            {
                isOutOfSupplies = true;
                Console.WriteLine("You ran out of supplies to make another cup! Sold out for the day!");
            }
            else if (cupsInPitcher < 1)
            {
                MakePitcher();
                if (isOutOfSupplies == false)
                {
                    MakeCup();
                }     
            }
            else
            {
                cups.quantity--;
                ice.quantity -= icePerCup;
                cupsInPitcher--;
            }
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
                Console.WriteLine($"{name}, what would you like to buy more of? Enter a number.\n1. Cups\n2. Lemons\n3. Sugar\n4. Ice\n5. Back to menu");
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
                DisplayCurrentInventory();
                int shopFor = PickWhatToShopForInList();
                GoToStoreOrLeavePage(shopFor, player, store);
            }
            while (isStillShopping == true);
            
        }

        private void DisplayCurrentInventory()
        {
            foreach (Inventory inventory in typesOfInventory)
            {
                Console.WriteLine($"{inventory.name}: {inventory.quantity}");
            }
            Console.WriteLine($"Money: ${totalMoney}");
        }

        public void DetermineRecipeAndPrice()
        {
            isOutOfSupplies = false;
            do
            {
                isStillShopping = true;
                int action = DisplayRecipeAndPrice();
                ChangeRecipeOrPrice(action);
            }
            while (isStillShopping == true);
        }

        public void MeltIce()
        {
            if (ice.quantity > 0)
            {
                Console.WriteLine("The rest of your ice melted!");
                ice.quantity = 0;
            }
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
                Console.WriteLine($"5. Back to main menu");
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
            Console.Clear();
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
