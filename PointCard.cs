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
            if (Tier == "Gold" && Points >= 100)
            {
                // Redeem points only if the customer is Gold tier and has at least 100 points
                int redeemedAmount = Math.Min(100, bill);
                Points -= redeemedAmount;
                Console.WriteLine($"Redeemed {redeemedAmount} points. Remaining Points: {Points}");
            }
            else if (Tier == "Silver" && Points >= 50)
            {
                // Redeem points only if the customer is Silver tier and has at least 50 points
                int redeemedAmount = Math.Min(50, bill);
                Points -= redeemedAmount;
                Console.WriteLine($"Redeemed {redeemedAmount} points. Remaining Points: {Points}");
            }
            // You can add more conditions for other tiers if needed
            else
            {
                Console.WriteLine("Customer cannot redeem points at this tier or insufficient points.");
            }
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
