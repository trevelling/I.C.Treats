﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamShop
{
    class Cone : IceCream
    {
        public bool Dipped { get; set; }    

        public Cone(){} // Default Constructor

        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped) : base(option, scoops,
            flavours, toppings)
        {
            Dipped = dipped;
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

            // Toppings and Chocolate-dipped
            basePrice += 1.00 * Toppings.Count;

            // Chocolate-dipped
            if (Dipped)
            {
                basePrice += 2.00;
            }


            return basePrice;
        }

        public override string ToString()
        {
            return $"null";
        }
    }
}
