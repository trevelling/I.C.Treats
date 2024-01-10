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
            Flavours = new List<Flavour>();
            Toppings = new List<Topping>();
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

            return basePrice;
        }

        public override string ToString()
        {
            return $"{Option,-10} {Scoops,-10}";
        }
    }
}
