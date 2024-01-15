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
        }//Tier of pointCard will never drop

        public void RedeemPoints(int bill)
        {

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
