//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using S10258591_PRG2Assignment;

namespace IceCreamShop
{
    class Program
    {
        static void Main(string[] args)
        {
            //Basic Features
     
            List<Customer> customerList = new List<Customer>();
            initCustomers("customers.csv", customerList);


            while (true)
            {
                DisplayMenu();

                Console.Write("Enter your option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    // 1) List all customers 
                    DisplayAllCustomers(customerList);
                }
                if (option == 2)
                {
                    // BRAYDEN) List all current orders 
                }
                else if (option == 3)
                {
                    // Register new customers
                    RegisterCustomers(customerList);
                }
                else if (option == 4)
                {
                    CreateNewOrder(customerList);
                }
                else if (option == 5)
                {
                    Console.WriteLine('b');
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("------------- O P T I O N S -------------");
            Console.WriteLine("[1] List all customers");
            Console.WriteLine("[2] List all current orders");
            Console.WriteLine("[3] Register a new customer");
            Console.WriteLine("[4] Create a customer’s order");
            Console.WriteLine("[5] Display order details of a customer");
            Console.WriteLine("[6] Modify order details");
            Console.WriteLine("[0] Exit Menu");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("");
        }

        static void initCustomers(string filePath, List<Customer> customers)
        {
            try
            {
                // Read all lines from the CSV file
                string[] lines = File.ReadAllLines(filePath);

                // Skip the header line 
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');

                    if (fields.Length == 3)
                    {
                        string name = fields[0].Trim();
                        int memberID = Convert.ToInt32(fields[1].Trim());
                        DateTime dob = Convert.ToDateTime(fields[2].Trim());

                        // Create a new Customer object and add it to the list
                        Customer customer = new Customer(name, memberID, dob);
                        customers.Add(customer);
                    }
                    else
                    {
                        // Handle invalid line 
                        Console.WriteLine($"Skipping invalid line: {lines[i]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the CSV file: {ex.Message}");
            }
        }

        static void DisplayAllCustomers(List<Customer> customers)
        {
            Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-10}", "No.", "Name", "MemberID", "DOB");

            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"[{i+1}] {customers[i].ToString()}");
            }
           
        }

        static void RegisterCustomers(List<Customer> customerList)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
                
            Console.Write("Enter your ID Number: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter in your Date of Birth: ");
            DateTime dob = Convert.ToDateTime(Console.ReadLine());

            Customer newCustomer = new Customer(name, id, dob);
            PointCard pointCard = new PointCard(0, 0);
            newCustomer.Rewards = pointCard;
            customerList.Add(newCustomer);

            string filePath = "customers.csv";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{name},{id},{dob.ToString("dd/MM/yyyy")}");
            }
            Console.WriteLine($"!NEW Customer! \n\n\t Name - {name} \n\t MemberID {id} \n\t DOB - {dob.ToString("dd/MM/yyyy")} \n\nhas been successfully registered");
        }

        static void CreateNewOrder(List<Customer> customers)
        {
            DisplayAllCustomers(customers);

            Console.Write("Please select a customer: ");
            int customerIndex = Convert.ToInt32(Console.ReadLine()) - 1; // Adjusted to match the index

            // Creating a new Order for the customer selected
            Order newOrder = new Order(customerIndex, DateTime.Now);

            //Linking the new order to the customer's current order
            customers[customerIndex].CurrentOrder = newOrder;

            // Initialize flag for adding more ice creams
            bool addMoreIceCreams = true;

            while (addMoreIceCreams)
            {
                // Choosing Option
                Console.WriteLine("Choose your option!");
                Console.Write("[Cup] [Cone] [Waffle]: \n");
                string option = Console.ReadLine().ToLower();

                string waffleFlavour = "Plain";

                // Choosing waffle flavour if waffle is selected
                if (option.ToLower().Trim() == "waffle")
                {
                    Console.WriteLine("Choose your waffle flavour!");
                    Console.Write("[Plain] [Red Velvet] [Charcoal] [Pandan]: \n");
                    waffleFlavour = Console.ReadLine().ToLower();
                }

                // Choosing No. of Scoops
                Console.WriteLine("Choose the no. of scoops!");
                Console.Write("[1] [2] [3]: \n");
                int scoops = Convert.ToInt32(Console.ReadLine().ToLower());

                List<Flavour> flavourList = new List<Flavour>();
                List<Topping> toppingList = new List<Topping>();

                for (int scoopNumber = 1; scoopNumber <= scoops; scoopNumber++)
                {
                    // Choosing premium or non-premium flavour for each scoop
                    Console.WriteLine($"Choose your flavor for scoop {scoopNumber}!");
                    Console.Write("Would you like premium [y/n]: ");
                    string userInput = Console.ReadLine().ToLower();
                    bool premium = userInput.ToLower() == "y";

                    Console.WriteLine(
                        $"Choose your {(premium ? "premium" : "non-premium")} flavour for scoop {scoopNumber}!");
                    if (premium)
                    {
                        Console.Write("[Durian] [Ube] [Sea Salt]: \n");
                    }
                    else
                    {
                        Console.Write("[Vanilla] [Chocolate] [Strawberry]: \n");
                    }

                    string flavour = Console.ReadLine().ToLower();
                    Flavour iceCreamFlavour = new Flavour(flavour, premium, 1);
                    flavourList.Add(iceCreamFlavour);
                }

                // Choosing toppings

                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine("What topping would you like [Sprinkles, Mochi, Sago, Oreos] [\"N\" to finish]: \n");
                    string topping = Console.ReadLine().ToLower();

                    if (topping.ToLower() == "n")
                    {
                        break;
                    }
                    else
                    {
                        Topping toppings = new Topping(topping);
                        toppingList.Add(toppings);
                    }
                    
                }

                // Adding a new IceCream object based on the option
                IceCream iceCream;

                if (option.Trim() == "cup")
                {
                    iceCream = new Cup(option, scoops, flavourList, toppingList);
                }
                else if (option.ToLower().Trim() == "cone")
                {
                    iceCream = new Cone(option, scoops, flavourList, toppingList, false);
                }
                else if (option.ToLower().Trim() == "waffle")
                {
                    iceCream = new Waffle(option, scoops, flavourList, toppingList, waffleFlavour);
                }
                else
                {
                    Console.WriteLine("Invalid option");
                    return;
                }

                newOrder.AddIceCream(iceCream);

                Console.WriteLine($"A new {option}-IceCream with {scoops} scoop(s) was added!\n");

                // Prompting user if they want to add another ice cream
                Console.Write("Would you like to add another ice cream to the order? [Y/N]: ");
                string addMoreIceCreamsInput = Console.ReadLine().ToLower();
                addMoreIceCreams = addMoreIceCreamsInput == "y"; //if y sets to true, otherwise false
            }
        }
    }
}