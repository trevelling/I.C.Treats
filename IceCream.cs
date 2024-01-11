using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamShop
{
    abstract class IceCream
    {
        public string Option { get; private set; }
        public int Scoops { get; private set; }
        public List<Flavour> Flavours { get; private set; }
        public List<Topping> Toppings { get; private set; }

        public IceCream()
        {
            Option = string.Empty;
            Scoops = 0;
            Flavours = new List<Flavour>();
            Toppings = new List<Topping>();
        } // Default Constructor

        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }

        public abstract double CalculatePrice();

        public override string ToString()
        {
            return $"Option: {Option} Scoops: {Scoops} Flavours: {string.Join(", ", Flavours)} Toppings: {string.Join(", ", Toppings)} ";
        }
    }
}
