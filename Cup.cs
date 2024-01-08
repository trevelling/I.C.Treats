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

        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops,
            flavours, toppings)
        {
            
        }

        public override double CalculatePrice()
        {
            double price = 0;
            return price;
        }

        public override string ToString()
        {
            return $"null";
        }
    }
}
