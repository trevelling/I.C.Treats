using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamShop
{
    class Topping
    {
        public string Type { get; set; }

        public Topping(){} // Default Constructor

        public Topping(string type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return $"null";
        }
    }
}
