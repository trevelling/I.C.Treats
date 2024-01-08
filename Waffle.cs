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

        public Waffle(){} // Default Constructor

        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour) : base(option, scoops,
            flavours, toppings)
        {
            WaffleFlavour = waffleFlavour;
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
