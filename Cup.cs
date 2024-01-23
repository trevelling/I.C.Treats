//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamShop
{
    class Cup : IceCream
    {
        public Cup(){} // Default Constructor

        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }

        public override double CalculatePrice()
        {
            double basePrice = 0;

            if (Scoops == 1)
            {
                basePrice = 4.00;
            }
            else if (Scoops == 2)
            {
                basePrice = 5.50;
            }
            else if (Scoops == 3)
            {
                basePrice = 6.50;
            }
            
            string filePathFlavoursCsv = "flavours.csv";
            using (StreamReader sr = new StreamReader(filePathFlavoursCsv))
            {
                // Skip the header line
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] lines = line.Split(',');

                    if (lines.Length == 2)
                    {
                        string flavour = lines[0].Trim();
                        string costStr = lines[1].Trim();
                        if (double.TryParse(costStr, out double cost))
                        {
                            foreach (var flavours in Flavours)
                            {
                                if (flavours.Type == flavour.ToLower() && flavours.Premium)
                                {
                                    basePrice += cost;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Invalid cost format: {costStr}. Skipping.");
                        }
                    }
                }
            }

            string filePathToppingsCsv = "toppings.csv";
            using (StreamReader sr = new StreamReader(filePathToppingsCsv))
            {
                // Skip the header line
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] lines = line.Split(',');

                    if (lines.Length == 2)
                    {
                        string topping = lines[0].Trim();
                        string costStr = lines[1].Trim();
                        if (double.TryParse(costStr, out double cost))
                        {
                            foreach (var toppings in Toppings)
                            {
                                if (toppings.Type == topping.ToLower())
                                {
                                    basePrice += cost;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Invalid cost format: {costStr}. Skipping.");
                        }
                    }
                }
            }


            return basePrice;
        }

        public override string ToString()
        {
            var flavourQuantities = Flavours
                .GroupBy(flavour => flavour.Type)  // Group by flavor type
                .Select(group => $"{group.Key} : {group.Sum(flavour => flavour.Quantity)}");

            string flavourString = string.Join(", ", flavourQuantities);
            string toppingString = string.Join(", ", Toppings.Select(topping => topping.ToString()));

            string result = $"A {Option}-IceCream with {Scoops} scoop(s), Flavours: [{flavourString}], Toppings: [{toppingString}]";
            return result;
        }
    }
}
