using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Channels;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Press q to quit");

            bool quitFlag = false;
            while (!quitFlag)
            {
                Console.WriteLine("Please enter SKU IDs.");
                // string userInput;
                string SKuInputs = Console.ReadLine();
                if (SKuInputs == "q")
                {
                    quitFlag = true;
                }
                else
                {
                    Program.GetPromotions(SKuInputs);
                    Console.ReadLine();
                }
            }
           
        }


        public static void GetPromotions(string sKuInputs)
        {
            char[] Skus = sKuInputs.ToCharArray();
            if (Skus.Length == 0)
            {
                // Display message to user to provide parameters.
                Console.WriteLine("Please enter Valid SKU IDs.");
                return;
            }
            else
            {
                var SkusA = Skus.Where(x => x == 'A').ToList();
                var SkusB = Skus.Where(x => x == 'B').ToList();
                var SkusC = Skus.Where(x => x == 'C').ToList();
                var SkusD = Skus.Where(x => x == 'D').ToList();
                var SkusCD = Skus.Where(x => x == 'C' || x == 'D').ToList();

                var strategies = new List<object>();
                IList<Sku> objSkus = new List<Sku>();
                bool isAddedItems = false;

                if (SkusC != null && SkusD != null)
                {
                    if (SkusC.Count > 0 && SkusD.Count > 0)
                    {
                        isAddedItems = true;
                    }

                }
                foreach (Char sku in Skus)
                {

                    if (sku == 'A' || sku == 'B')
                    {
                        objSkus.Add(new Sku(sku));
                    }
                    if (!isAddedItems && (sku == 'C' || sku == 'D'))
                    {
                        objSkus.Add(new Sku(sku));
                    }

                }

                if (isAddedItems)
                {
                    objSkus.Add(new Sku('&'));
                }

                if (SkusA != null)
                {
                    IPricingStrategy strategyA = new PricingStategyA();
                    strategies.Add(strategyA);
                }
                if (SkusB != null)
                {
                    IPricingStrategy strategyB = new PricingStrategyB();
                    strategies.Add(strategyB);
                }


                if (SkusCD != null && SkusCD.Count >=2)
                {
                    IPricingStrategy strategyCandD = new PricingStrategyCandD();
                    // strategyCandD.Sku = '&';
                    strategies.Add(strategyCandD);
                }
                else if (SkusC != null && SkusC.Count > 0)
                {
                    IPricingStrategy strategyC = new PricingStrategyC();

                    strategies.Add(strategyC);
                }
                else if (SkusD != null && SkusD.Count > 0)
                {
                    IPricingStrategy strategyD = new PricingStrategyD();
                    strategies.Add(strategyD);
                }



                //List<IPricingStrategy>  objStrategies =strategies.
                List<IPricingStrategy> fooList = strategies.OfType<IPricingStrategy>().ToList();
                Cashier cashier = new Cashier(fooList);
                double intResult = cashier.Checkout(objSkus);

                Console.WriteLine("The Total Order value " + intResult);
            }

            Console.Read();
        }



    }
}
