using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Store
    {
        double discountFactor1 = 0.9;
        double discountFactor2 = 0.8;
        int qtyIncreaseFactor = 2;
        double basePrice;
        int baseQty;
        int qty2;
        int qty3;
        double priceForQty1;
        double priceForQty2;
        double priceForQty3;

        private void CalculatePricesFor(Inventory itemToShopFor)
        {
            if (itemToShopFor.name == "cups")
            {
                basePrice = 0.035;
                baseQty = 25;
            }
            else if (itemToShopFor.name == "lemons")
            {
                basePrice = 0.09;
                baseQty = 10;
            }
            else if (itemToShopFor.name == "cups of sugar")
            {
                basePrice = 0.085;
                baseQty = 10;
            }
            else if (itemToShopFor.name == "ice cubes")
            {
                basePrice = 0.0087;
                baseQty = 100;
            }
            qty2 = baseQty * qtyIncreaseFactor;
            qty3 = qty2 * qtyIncreaseFactor;
            priceForQty1 = Math.Round(basePrice * baseQty, 2);
            priceForQty2 = Math.Round(basePrice * baseQty * qtyIncreaseFactor * discountFactor1, 2);
            priceForQty3 = Math.Round(basePrice * baseQty * qtyIncreaseFactor * qtyIncreaseFactor * discountFactor2, 2);
        }

        private int DisplayPrices(Inventory itemToShopFor)
        {
            int numberChoice;
            bool isNumeric;
            do
            {
                Console.WriteLine($"How many {itemToShopFor.name} would you like to buy? Enter a number.");
                Console.WriteLine($"1. {baseQty} {itemToShopFor.name} for {priceForQty1}.");
                Console.WriteLine($"2. {qty2} {itemToShopFor.name} for {priceForQty2}.");
                Console.WriteLine($"3. {qty3} {itemToShopFor.name} for {priceForQty3}.");
                string answer = Console.ReadLine();
                isNumeric = Int32.TryParse(answer, out numberChoice);
                if (!isNumeric || numberChoice < 1 || numberChoice > 3)
                {
                    Console.WriteLine("You didn't pick a number on the list! Try again.");
                }
            }
            while (!isNumeric || numberChoice < 1 || numberChoice > 3);
            return numberChoice;
        }

        private void purchaseItems(int numberChoice, Inventory itemToShopFor,Player player)
        {
            switch (numberChoice)
            {
                case 1:
                    itemToShopFor.quantity += baseQty;
                    player.totalMoney -= priceForQty1;
                    player.dailyMoney += priceForQty1;
                    break;
                case 2:
                    itemToShopFor.quantity += qty2;
                    player.totalMoney -= priceForQty2;
                    player.dailyMoney += priceForQty2;
                    break;
                case 3:
                    itemToShopFor.quantity += qty3;
                    player.totalMoney -= priceForQty3;
                    player.dailyMoney += priceForQty3;
                    break;
            }
            Console.Clear();
            Console.WriteLine($"{player.name}, you now have ${player.totalMoney} and {itemToShopFor.quantity} {itemToShopFor.name}.");
        }

        public void DisplayPricesAndMakePurchase(Inventory itemToShopFor, Player player)
        {
            CalculatePricesFor(itemToShopFor);
            int priceChoice = DisplayPrices(itemToShopFor);
            purchaseItems(priceChoice, itemToShopFor, player);
        }
    }

}
