using System;
using System.Collections.Generic;
using System.Drawing;
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

            if (Rewards == null) // Checks if the Customer has a PointCard already
            {
                // Sets a new Point Card for everytime a customer comes
                PointCard pointCard = new PointCard(0, 0);
                Rewards = pointCard;
            }
        }

        public Order MakeOrder()
        {
            // Creating a newOrder object that contains customer's MemberID and time that it was ordered
            Order newOrder = new Order(MemberID , DateTime.Now);
            CurrentOrder = newOrder;

            // Everytime a new order is created, gives a new Punch in the PunchCard
            Rewards.PunchCard += 1;
            OrderHistory.Add(CurrentOrder);
            return newOrder;
        }

        public bool IsBirthday()
        {
            DateTime today = DateTime.Today;

            // Check if the month and day of today are equal to the month and day of the DOB
            if (today.Month == DOB.Month && today.Day == DOB.Day)
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