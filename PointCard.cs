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

        static void AddPoints(int points)
        {

        }

        static void RedeemPoints(int points)
        {

        }

        static void Punch()
        {

        }

        public override string ToString()
        {
            return $"null";
        }
    }

}
