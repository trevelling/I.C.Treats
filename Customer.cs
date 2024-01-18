using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using S10258591_PRG2Assignment;

namespace IceCreamShop
{
    class Customer
    {
        public string Name { get; set; }
        public int MemberID { get; set; }
        public DateTime DOB { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; }
        public PointCard Rewards { get; set; }

        public Customer() {} // Default Constructor

        public Customer(string name, int memberID, DateTime dOB)
        {
            Name = name;
            MemberID = memberID;
            DOB = dOB;

            if (Rewards == null) // Checks if the Customer has a PointCard already
            {
                // Sets a new Point Card for everytime a customer comes
                PointCard pointCard = new PointCard(0, 0);
                Rewards = pointCard;
            }
        }

        public Order MakeOrder()
        {
            int orderId = 1;
            // Creating a new Order object that contains the customer's MemberID and the time it was ordered
            Order newOrder = new Order(orderId, DateTime.Now);
            CurrentOrder = newOrder;



            // Every time a new order is created, give a new punch in the PunchCard
            if (Rewards != null)
            {
                Rewards.PunchCard += 1;
            }

            // Adding ice cream to the order
            bool addMoreIceCreams = true;

            while (addMoreIceCreams)
            {
                int scoops;
                bool dipped = false; 
                string option;
                string waffleFlavour = "Original"; // Default 

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


                // Choosing waffle flavour if waffle is selected
                if (option.ToLower() == "waffle")
                {
                    string validWaffleFlavour;
                    do
                    {
                        Console.WriteLine("Choose your waffle flavour!");
                        Console.Write("[Original] [Red Velvet] [Charcoal] [Pandan]: ");
                        validWaffleFlavour = Console.ReadLine().ToLower();
                        Console.WriteLine("");

                        if (validWaffleFlavour.ToLower() != "original" && validWaffleFlavour.ToLower() != "red velvet" &&
                            validWaffleFlavour.ToLower() != "charcoal" && validWaffleFlavour.ToLower() != "pandan" && validWaffleFlavour.ToLower() != "redvelvet")
                        {
                            Console.WriteLine("Invalid waffle flavour. Please enter a valid waffle flavour.");
                        }

                    } while (validWaffleFlavour.ToLower() != "original" && validWaffleFlavour.ToLower() != "red velvet" &&
                             validWaffleFlavour.ToLower() != "charcoal" && validWaffleFlavour.ToLower() != "pandan");

                    waffleFlavour = validWaffleFlavour;
                }

                // Choosing if cone is chocolate dipped if selected
                if (option.ToLower() == "cone")
                {
                    do
                    {
                        Console.Write("Do you want a chocolate dipped cone? [Y/N]: ");
                        string dippedInput = Console.ReadLine().ToLower();
                        Console.WriteLine("");

                        if (dippedInput == "y")
                        {
                            dipped = true;
                            break;
                        }
                        else if (dippedInput == "n")
                        {
                            dipped = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 'Y' for yes or 'N' for no.");
                        }
                    } while (true); // Loop until a valid input is received
                }


                while (true)
                {
                    Console.WriteLine("Choose the no. of scoops!");
                    Console.Write("[1] [2] [3]: ");
                    string scoopsInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(scoopsInput))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number (1, 2, or 3).");
                        continue;
                    }

                    if (int.TryParse(scoopsInput, out scoops) && (scoops == 1 || scoops == 2 || scoops == 3))
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
                        Console.Write("Would you like premium [Y/N]: ");
                        string userInput = Console.ReadLine().ToLower();
                        bool premium = userInput.ToLower() == "y";
                        Console.WriteLine("");

                        if (userInput != "y" && userInput != "n")
                        {
                            Console.WriteLine("Invalid input. Please enter 'Y' for yes or 'N' for no.");
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

                            /* Append the chosen flavor and its cost to the flavours.csv file
                            if (premium)
                            {
           
                            }
                            else
                            {
         
                            }
                            */
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
                            // Append the chosen topping and its cost to the toppings.csv file
       

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

                Console.WriteLine("");
                // Adding a new IceCream object based on the option
                IceCream iceCream = null;

                if (option.ToLower().Trim() == "cup")
                {
                    iceCream = new Cup(option, scoops, flavourList, toppingList);
                }
                else if (option.ToLower().Trim() == "cone")
                {
                    iceCream = new Cone(option, scoops, flavourList, toppingList, dipped);
                }
                else if (option.ToLower().Trim() == "waffle")
                {
                    iceCream = new Waffle(option, scoops, flavourList, toppingList, waffleFlavour);
                }
                else
                {
                    Console.WriteLine("Invalid option");
                }

                // Adding the ice cream to the current order
                CurrentOrder.AddIceCream(iceCream);

                // Displaying order details
                Console.WriteLine("---------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{Name} [{MemberID}]");
                Console.WriteLine($"{CurrentOrder}");
         

                for (int i = 0; i < CurrentOrder.IceCreamList.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {CurrentOrder.IceCreamList[i]}");
                }

                Console.WriteLine("---------------------------------------------------------------------------------------------------");
                Console.WriteLine("");

                if (newOrder.Id != CurrentOrder.Id)
                {
                    orderId += 1;
                }

                // Prompting user if they want to add another ice cream
                bool validInput = false;
                while (!validInput)
                {
                    Console.Write("Would you like to add another ice cream to the order? [Y/N]: ");
                    string addMoreIceCreamsInput = Console.ReadLine().ToLower();

                    if (addMoreIceCreamsInput == "y" || addMoreIceCreamsInput == "n")
                    {
                        addMoreIceCreams = addMoreIceCreamsInput == "y"; // if 'y', sets to true, otherwise false
                        validInput = true; // Exit the loop as the input is valid
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 'Y' for yes or 'N' for no.");
                    }
                }
            }
            return newOrder;
        }

        public bool IsBirthday()
        {
            DateTime today = DateTime.Today;

            // Check if the month and day of today are equal to the month and day of the DOB
            if (today.Month == DOB.Month && today.Day == DOB.Day)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"  {Name,-10} {MemberID,-10} {DOB.ToString("dd/MM/yyyy"),-10}";
        }
    }
}