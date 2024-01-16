using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamShop
{
    class Waffle : IceCream
    {
        public string WaffleFlavour { get; set; }

        public Waffle() : base() { } // Default Constructor

        
        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour) : base(option, scoops, flavours, toppings)
        {
            WaffleFlavour = waffleFlavour;
        }

        // Calculate price of waffle 
        public override double CalculatePrice()
        {
            double basePrice = 0;

            if (Scoops == 1)
            {
                basePrice = 7.00;
            }
            else if (Scoops == 2)
            {
                basePrice = 8.50;
            }
            else if (Scoops == 3)
            {
                basePrice = 9.50;
            }

            // Add extra cost for premium flavours
            foreach (var flavour in Flavours)
            {
                if (flavour.Premium)
                {
                    basePrice += 2.00 * flavour.Quantity;
                }
            }

            // Toppings 
            basePrice += 1.00 * Toppings.Count;

            // Waffle Flavour
            if (WaffleFlavour.ToLower() == "Red Velvet" || WaffleFlavour.ToLower() == "Charcoal" || WaffleFlavour.ToLower() == "Pandan")
            {
                basePrice += 3.00;
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

            string result = $"A {WaffleFlavour} {Option}-IceCream with {Scoops} scoop(s), Flavours: [{flavourString}], Toppings: [{toppingString}]";
            return result;
        }
    }
}
