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
            if (points > 0)
            {
                Points += points;
            }
            if (Tier != "Gold")
            {
                if (Tier != "Silver")
                {
                    if (Points >= 50)
                    {
                        Tier = "Silver";
                    }
                    else if (Points >= 100)
                    {
                        Tier = "Gold";
                    }
                }
                else
                {
                    if (Points >= 150)
                    {
                        Tier = "Gold";
                    }
                }
            }
        
            
        }

        public void RedeemPoints(int points)
        {
            if (Points >= points)
            {
                Points -= points;
            }
        }

        public void Punch()
        {
            PunchCard++;
        }

        public override string ToString()
        {
            return $"Points: {Points} PunchCard: {PunchCard} Tier: {Tier}";
        }
    }

}
