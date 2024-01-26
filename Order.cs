//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamShop
{
    class Order
    {
        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; }

        public Order() { } // Default Constructor

        public Order(int id, DateTime timeReceived)
        {
            Id = id;
            TimeReceived = timeReceived;
            IceCreamList = new List<IceCream>();
        }

        public void ModifyIceCream(int index)
        {
            string option;
            do
            {
                Console.WriteLine("Choose your option!");
                Console.Write("[Cup] [Cone] [Waffle]: ");
                option = Console.ReadLine().ToLower();
                Console.WriteLine("");

                if (option.ToLower() != "cup" && option.ToLower() != "cone" && option.ToLower() != "waffle")
                {
                    Console.WriteLine("Invalid option. Please enter a valid option.");
                }

            } while (option != "cup" && option != "cone" && option != "waffle");

            string waffleFlavour = "Plain";

            // Choosing waffle flavour if waffle is selected
            if (option.ToLower() == "waffle")
            {
                string validWaffleFlavour;
                do
                {
                    Console.WriteLine("Choose your waffle flavour!");
                    Console.Write("[Plain] [Red Velvet] [Charcoal] [Pandan]: ");
                    validWaffleFlavour = Console.ReadLine().ToLower();
                    Console.WriteLine("");

                    if (validWaffleFlavour.ToLower() != "plain" && validWaffleFlavour.ToLower() != "red velvet" &&
                        validWaffleFlavour.ToLower() != "charcoal" && validWaffleFlavour.ToLower() != "pandan" && validWaffleFlavour.ToLower() != "redvelvet")
                    {
                        Console.WriteLine("Invalid waffle flavour. Please enter a valid waffle flavour.");
                    }

                } while (validWaffleFlavour.ToLower() != "plain" && validWaffleFlavour.ToLower() != "red velvet" &&
                         validWaffleFlavour.ToLower() != "charcoal" && validWaffleFlavour.ToLower() != "pandan");

                waffleFlavour = validWaffleFlavour;
            }

            int scoops;

            while (true)
            {
                Console.WriteLine("Choose the no. of scoops!");
                Console.Write("[1] [2] [3]: ");
                scoops = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");

                if (scoops == 1 || scoops == 2 || scoops == 3)
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please enter a valid number (1, 2, or 3).");
            }

            List<Flavour> flavourList = new List<Flavour>();
            List<Topping> toppingList = new List<Topping>();

            // Choosing premium or non-premium flavour for each scoop
            for (int scoopNumber = 1; scoopNumber <= scoops; scoopNumber++)
            {
                bool validFlavour = false;

                do
                {
                    Console.WriteLine($"Choose your flavor for scoop {scoopNumber}!");
                    Console.Write("Would you like premium [y/n]: ");
                    string userInput = Console.ReadLine().ToLower();
                    bool premium = userInput.ToLower() == "y";
                    Console.WriteLine("");

                    if (userInput != "y" && userInput != "n")
                    {
                        Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                        // Prompt the user again by continuing the loop
                        continue;
                    }

                    Console.WriteLine($"Choose your {(premium ? "premium" : "non-premium")} flavour for scoop {scoopNumber}!");

                    if (premium)
                    {
                        Console.Write("[Durian] [Ube] [Sea Salt]: ");
                    }
                    else
                    {
                        Console.Write("[Vanilla] [Chocolate] [Strawberry]: ");
                    }

                    string flavour = Console.ReadLine().ToLower();
                    Console.WriteLine("");

                    // Check if the entered flavor is valid
                    if ((premium && (flavour.ToLower() == "durian" || flavour.ToLower() == "ube" || flavour.ToLower() == "seasalt" || flavour.ToLower() == "sea salt")) ||
                        (!premium && (flavour.ToLower() == "vanilla" || flavour.ToLower() == "chocolate" || flavour.ToLower() == "strawberry")))
                    {
                        validFlavour = true;
                        Flavour iceCreamFlavour = new Flavour(flavour, premium, 1);
                        flavourList.Add(iceCreamFlavour);
                    }
                    else
                    {
                        Console.WriteLine("Invalid flavor. Please enter a valid flavor.");
                    }
                } while (!validFlavour);
            }

            // Choosing toppings
            bool exitToppings = false; // Flag to control the loop
            for (int i = 0; i < 4 && !exitToppings; i++)
            {
                bool validTopping = false;

                do
                {
                    Console.Write("What topping would you like [Sprinkles, Mochi, Sago, Oreos] [\"N\" to finish]: ");
                    string topping = Console.ReadLine().ToLower();

                    if (topping == "n")
                    {
                        exitToppings = true;  // Set exitToppings to true to break out of the for loop
                        break;
                    }

                    // Check if the entered topping is valid
                    if (topping == "sprinkles" || topping == "mochi" || topping == "sago" || topping == "oreos")
                    {
                        Topping toppings = new Topping(topping);
                        toppingList.Add(toppings);
                        validTopping = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid topping. Please enter a valid topping.");
                    }
                } while (!validTopping);

            }

            if (option.ToLower().Trim() == "cup")
            {
                IceCreamList[index] = new Cup(option, scoops, flavourList, toppingList);
            }
            else if (option.ToLower().Trim() == "cone")
            {
                IceCreamList[index] = new Cone(option, scoops, flavourList, toppingList, false);
            }
            else
            {
                IceCreamList[index] = new Waffle(option, scoops, flavourList, toppingList, waffleFlavour);
            }
        }

        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int index)
        {
            IceCreamList.RemoveAt(index);
        }

        public double CalculateTotal()
        {
            double total = 0;

            foreach (var iceCream in IceCreamList)
            {
                total += iceCream.CalculatePrice();
            }

            return total;
        }

        public override string ToString()
        {
            return $"Order Id: {Id} Time Received: {TimeReceived}";
        }
    }
}