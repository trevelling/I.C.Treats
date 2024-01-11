using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10258591_PRG2Assignment
{
    class PointCard
    {
        public int Points { get; private set; }
        public int PunchCard { get; private set; }
        public string Tier { get; private set; }

        public PointCard()
        {
            Points = 0;
            PunchCard = 0;
            Tier = "Ordinary";
        } // Default Constructor
        
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
            if (Points >= 100)
            {
                Tier = "Gold";
            }
            else if (Points >= 50)
            {
                Tier = "Silver";
            }
            else
            {
                Tier = "Ordinary"; 
            }
        }

        public void AddPoints(int points)
        {
            Points += points;
            if (Points >= 100 && Tier != "Gold")
            {
                Tier = "Gold";
            }
            else if (Points >= 50 && Tier != "Gold" && Tier != "Silver")
            {
                Tier = "Silver";
            }
        }//Tier of pointcard will not drop

        public void RedeemPoints(int points)
        {
            if (Points >= points)
            {
                // Only silver and gold members can redeem points
                if (Tier.ToLower() == "silver" || Tier.ToLower() == "gold")
                {
                    double redemptionAmount = points * 0.02;

                    // If the member is gold, place the order in the special gold members order queue
                    if (Tier.ToLower() == "gold")
                    {
                        Console.WriteLine("Order placed in the gold members order queue.");
                    }

        static void Punch()
        {
            
        }

        public override string ToString()
        {
            return $"Points: {Points} PunchCard: {PunchCard} Tier: {Tier}";
        }
    }

}
