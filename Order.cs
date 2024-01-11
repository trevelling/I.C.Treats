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
            IceCreamList = new List<IceCream>();
        }

        public void ModifyIceCream(int icecream)
        {
            
        }

        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
            
        }

        public void DeleteIceCream(int index)
        {
           
        }

        public double CalculateTotal()
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
