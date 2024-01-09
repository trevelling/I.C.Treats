using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using S10258591_PRG2Assignment;

namespace IceCreamShop
{
    class Customer
    {
        public string Name { get; set; }
        public int MemberID { get; set; }
        public DateTime DOB { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; }
        public PointCard Rewards { get; set; }

        public Customer() {} // Default Constructor

        public Customer(string name, int memberID, DateTime dOB)
        {
            Name = name;
            MemberID = memberID;
            DOB = dOB;
        }

        public Order MakeOrder()
        {
            Order newOrder = new Order(OrderHistory.Count + 1, DateTime.Now);
            CurrentOrder = newOrder; 
            OrderHistory.Add(newOrder);
            return newOrder;
        }

        public bool IsBirthday()
        {
            DateTime today = DateTime.Today;
            if (today == DOB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"  {Name,-10} {MemberID,-10} {DOB.ToString("dd/MM/yyyy"),-10}";
        }
    }
}