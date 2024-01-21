//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================

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
        }// Tier of pointCard will never drop

        public void RedeemPoints(int bill)
        {
            double redemptionRate = 0.02; // 1 point = $0.02
            int maxRedeemablePoints;

            if (Tier == "Gold")
            {
                maxRedeemablePoints = Math.Min(Points, 100);
            }
            else if (Tier == "Silver")
            {
                maxRedeemablePoints = Math.Min(Points, 50);
            }
            else
            {
                maxRedeemablePoints = 0; // No redemption for other tiers
            }

            int redeemedAmount = Math.Min(maxRedeemablePoints, (int)(bill / redemptionRate));
            Points -= redeemedAmount;
            Console.WriteLine($"Redeemed {redeemedAmount} points. Remaining Points: {Points}");
        }

        public void Punch()
        {
            PunchCard++;

            if (PunchCard == 10)
            {
                Console.WriteLine("Congratulations! You've earned a free ice cream with your punch card! Redeeming...");
                PunchCard = 0; // Reset the punch card after earning a free ice cream
            }
        }

        public override string ToString()
        {
            return $"Points: {Points} PunchCard: {PunchCard} Tier: {Tier}";
        }
    }

}
