using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamShop
{
    class Order
    {
        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; }

        public Order(){} // Default Constructor

        public Order(int id, DateTime timeReceived)
        {
            Id = id;
            TimeReceived = timeReceived;
        }

        static void ModifyIceCream(int icecream)
        {

        }

        static void AddIceCream(IceCream iceCream)
        {
           

        }

        static void DeleteIceCream(int index)
        {
           
        }

        static double CalculateTotal()
        {
            double total = 0;
            return total;
        }

        public override string ToString()
        {
            return $"null";
        }
    }
}
