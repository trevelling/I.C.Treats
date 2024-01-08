using System;
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
            double price = 0;
            return price;
        }

        public override string ToString()
        {
            return $"null";
        }
    }
}
