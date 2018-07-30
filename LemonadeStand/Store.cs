using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Store
    {
        //Inventory lemons;
        //Inventory ice;
        //Inventory cups;
        //Inventory sugar;
        double discountFactor1 = 0.9;
        double discountFactor2 = 0.8;
        int qtyIncreaseFactor = 2;
        
        
        public void GetPricesFor(Inventory itemToShopFor)
        {
            double basePrice = 0;
            int baseQty = 0;
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
            else if (itemToShopFor.name == "sugar")
            {
                basePrice = 0.085;
                baseQty = 10;
            }
            else if (itemToShopFor.name == "ice")
            {
                basePrice = 0.0087;
                baseQty = 100;
            }
            double middleQty = baseQty * qtyIncreaseFactor;
            double highestQty = middleQty * qtyIncreaseFactor;
            double highestPrice = Math.Round(basePrice * baseQty,2);
            double middlePrice = Math.Round(basePrice * baseQty * qtyIncreaseFactor * discountFactor1,2);
            double lowestPrice = Math.Round(basePrice * baseQty * qtyIncreaseFactor * qtyIncreaseFactor * discountFactor2, 2);
            Console.WriteLine($"How many {itemToShopFor.name} would you like to buy? Enter a number.");
            Console.WriteLine($"1. {baseQty} {itemToShopFor.name} for {highestPrice}");
            Console.WriteLine($"2. {middleQty} {itemToShopFor.name} for {middlePrice}");
            Console.WriteLine($"3. {highestQty} {itemToShopFor.name} for {lowestPrice}");
            Console.ReadLine();
        }
    }
}
