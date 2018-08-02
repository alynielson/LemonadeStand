using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Store
    {
        private double discountFactor1 = 0.9;
        private double discountFactor2 = 0.8;
        private int qtyIncreaseFactor = 2;
        private double basePrice;
        private int baseQty;
        private int qty2;
        private int qty3;
        private double priceForQty1;
        private double priceForQty2;
        private double priceForQty3;

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
            int listMin = 1;
            int listMax = 4;
            do
            {
                UserInterface.DisplayStorePrices(itemToShopFor.name, baseQty, priceForQty1, qty2, priceForQty2, qty3, priceForQty3);
                numberChoice = UserInterface.ValidateNumberResponse();
            }
            while (numberChoice < listMin || numberChoice > listMax);
            return numberChoice;
        }

        private double DeterminePriceToUse(int numberChoice)
        {
            double priceToUse;
            switch (numberChoice)
            {
                case 1:
                    priceToUse = priceForQty1;
                    return priceToUse;
                case 2:
                    priceToUse = priceForQty2;
                    return priceToUse;
                case 3:
                    priceToUse = priceForQty3;
                    return priceToUse;
                default:
                    return priceToUse = 0;
            }
        }

        private int DetermineQuantityToUse(int numberChoice)
        {
            int qtyToUse;
            switch (numberChoice)
            {
                case 1:
                    qtyToUse = baseQty;
                    return qtyToUse;
                case 2:
                    qtyToUse = qty2;
                    return qtyToUse;
                case 3:
                    qtyToUse = qty3;
                    return qtyToUse;
                default:
                    return qtyToUse = 0;
            }
        }

        private void DoTransaction(Inventory itemToShopFor, Player player, int qtyToUse, double priceToUse)
        {
            itemToShopFor.quantity += qtyToUse;
            player.totalMoney -= priceToUse;
            player.dailyMoneySpent += priceToUse;
        } 

        private void purchaseItems(int numberChoice, Inventory itemToShopFor,Player player)
        {
            double priceToUse = DeterminePriceToUse(numberChoice);
            int qtyToUse = DetermineQuantityToUse(numberChoice);
            bool isShopperBankrupt = player.CheckIfBankrupt(priceToUse);
            if (isShopperBankrupt == false)
            {
                DoTransaction(itemToShopFor, player, qtyToUse, priceToUse);
            }
            else
            {
                player.isStillShopping = false;
                player.DeclareBankruptcy();
            }
            if (player.isGameOver == false)
            {
                Console.Clear();
                Console.WriteLine($"{player.name}, you now have ${player.totalMoney} and {itemToShopFor.quantity} {itemToShopFor.name}.");
            }
        }

        public void DisplayPricesAndMakePurchase(Inventory itemToShopFor, Player player)
        {
            CalculatePricesFor(itemToShopFor);
            int priceChoice = DisplayPrices(itemToShopFor);
            purchaseItems(priceChoice, itemToShopFor, player);
        }
    }

}
