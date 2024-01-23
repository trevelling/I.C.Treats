using System;
using System.Collections.Generic;
using System.IO;

using System;
using System.Collections.Generic;
using System.IO;

namespace IceCreamShop
{
    public class FlavourCost
    {
        public Dictionary<string, double> FlavoursCostDict { get; set; }

        public void CalculateCost()
        {
            string filePath = "flavours.csv";
            FlavoursCostDict = new Dictionary<string, double>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] columns = line.Split(',');

                        if (columns.Length == 2)
                        {
                            string flavour = columns[0].Trim();
                            double cost = Convert.ToDouble(columns[1].Trim());

                            FlavoursCostDict[flavour] = cost;
                        }
                        else
                        {
                            Console.WriteLine($"Invalid line format: {line}. Skipping.");
                        }
                    }
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading from {filePath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}

