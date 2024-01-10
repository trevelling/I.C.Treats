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
        public int Points { get; set; }
        public int PunchCard { get; set; }
        public string Tier { get; set; }

        public PointCard(){} // Default Constructor

        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
        }

        public void AddPoints(int points)
        {
            Points += points;
        }

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

                    Console.WriteLine($"Redeemed {points} points. Deducted ${redemptionAmount} from the bill.");
                    Points -= points;
                }
                else
                {
                    Console.WriteLine("Only silver and gold members can redeem points.");
                }
            }
            else
            {
                Console.WriteLine("Insufficient points to redeem.");
            }

        }

        public void Punch()
        {
            PunchCard++;
            if (PunchCard == 10)
            {
                PunchCard = 0;
                Console.WriteLine("Punch card fully punched! You get the next ice cream free.");
            }
        }

        public override string ToString()
        {
            return $"null";
        }
    }

}
