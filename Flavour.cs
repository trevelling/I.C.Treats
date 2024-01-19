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
    class Flavour
    {
        public string Type { get; set; }
        public bool Premium { get; set; }
        public int Quantity { get; set; }

        public Flavour(){} // Default Constructor

        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Type} : {Quantity}";
        }
    }
}
