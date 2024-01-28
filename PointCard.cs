//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================

using IceCreamShop;
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

        public void RedeemPoints(int redeemingAmount)
        {
            int maxRedeemablePoints;

            if (Tier == "Gold" || Tier == "Silver")
            {
                maxRedeemablePoints = Points; // Allow redeeming any amount for Gold and Silver tiers
            }
            else
            {
                maxRedeemablePoints = 0; // No redemption for other tiers
            }

            while (redeemingAmount > maxRedeemablePoints || redeemingAmount < 0)
            {
                Console.WriteLine("Invalid amount. Please enter a valid amount of points to redeem.");
                Console.Write($"Enter the amount of points to redeem (0 to {maxRedeemablePoints}): ");

                // Read user input and try to parse it to an integer
                if (!int.TryParse(Console.ReadLine(), out redeemingAmount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            Points -= redeemingAmount;
            Console.WriteLine($"Remaining Points: {Points}");
        }

        public void Punch()
        {
            PunchCard++;

            if (PunchCard == 10)
            {
                PunchCard = 0; // Reset the punch card after earning a free ice cream
            }
        }

        public override string ToString()
        {
            return $"Points: {Points} PunchCard: {PunchCard} Tier: {Tier}";
        }
    }

}
