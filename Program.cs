//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================


using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using S10258591_PRG2Assignment;
using static System.Formats.Asn1.AsnWriter;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;

namespace IceCreamShop
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                //LIST OF ORDERS FOR GOLD MEMBERS
                Queue<Order> pointCardGold = new Queue<Order>();

                //LIST OF ORDERS FOR REGULAR MEMBERS
                Queue<Order> pointCardRegular = new Queue<Order>();
           
                List<Customer> customerList = new List<Customer>();
                initCustomers("customers.csv", customerList);

                while (true)
                {
                    try
                    {
                        DisplayMenu();
                        Console.Write("Enter your option: ");
                        int option = Convert.ToInt32(Console.ReadLine());

                        if (option == 1)
                        {
                            // 1) List all customers [Tevel]
                            DisplayAllCustomers(customerList);
                        }
                        else if (option == 2)                          
                        {
                            Console.WriteLine("Gold Membership Queue\r\n------------------------------");
                            DisplayAllCurrentOrders(pointCardGold);
                            Console.WriteLine("\r\nRegular Membership Queue\r\n------------------------------");
                            DisplayAllCurrentOrders(pointCardRegular);
                        }
                        else if (option == 3)
                        {
                            // Register new customers [Tevel]
                            RegisterCustomers(customerList);
                        }
                        else if (option == 4)
                        {
                            // Creates a customer's order [Tevel]
                            CreateNewOrder(customerList, pointCardGold, pointCardRegular);
                        }
                        else if (option == 5) 
                        {
                            DisplayOrderDetails(customerList);
                        }
                        else if (option == 6) 
                        {
                            // Modify order details [Brayden]
                            ModifyOrderDetails();
                        }
                        else if (option == 7) 
                        {
                            // Advanced Option (a) - Process an order and checkout [Tevel]
                            ProcessOrderAndCheckout(customerList, pointCardGold, pointCardRegular);
                        }
                        else if (option == 8)
                        {
                            // Advanced Option (b) - Display monthly charged amounts breakdown & total charged amounts for the year [Brayden]
                            DisplayChargess();
                        }
                        else if (option == 0)
                        {
                            Console.WriteLine("Exiting I.C.Treats....");
                            Console.WriteLine("See you next time!      :)");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please choose a valid option.");
                        }
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number [0-8].");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Input exceeds the range of a valid integer. Please enter a smaller number.");
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
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
            Console.WriteLine("[7] Process an order and checkout");
            Console.WriteLine("[8] Display monthly charges breakdown");
            Console.WriteLine("[0] Exit Menu");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("");
        }

// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// (Tevel's Methods)

        static void initCustomers(string filePath, List<Customer> customers)
        {
            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Error: File not found at {filePath}");
                    return;
                }

                // Read all lines from the CSV file
                string[] lines = File.ReadAllLines(filePath);

                // Check if the file is empty
                if (lines.Length == 0)
                {
                    Console.WriteLine($"Error: The file at {filePath} is empty");
                    return;
                }

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
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading the CSV file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void DisplayAllCustomers(List<Customer> customers)
        {
            try
            {
                if (customers == null || customers.Count == 0)
                {
                    Console.WriteLine("No customers to display.");
                    return;
                }

                Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-10}", "No.", "Name", "MemberID", "DOB");

                for (int i = 0; i < customers.Count; i++)
                {
                    Console.WriteLine($"{$"[{i + 1}]", -5}{customers[i].ToString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying customers: {ex.Message}");
            }
        }

        static void RegisterCustomers(List<Customer> customerList)
        {
            try
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

            catch (IOException ex)
            {
                Console.WriteLine($"Error writing to the file: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Unauthorized access to the file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void CreateNewOrder(List<Customer> customers, Queue<Order> pointCardGold, Queue<Order> pointCardRegular)
        {
            try
            {
                DisplayAllCustomers(customers);
                Console.WriteLine("");

                Console.Write("Please select a customer No. : ");
                int customerIndex = Convert.ToInt32(Console.ReadLine()) - 1; // Adjusted to match the index
                Console.WriteLine("");

                // Creating a new Order for the customer selected
                Order newOrder = new Order(customers[customerIndex].MemberID, DateTime.Now);

                // Linking the new order to the customer's current order
                customers[customerIndex].CurrentOrder = newOrder;

                // Initialize OrderHistory if it's null
                if (customers[customerIndex].OrderHistory == null)
                {
                    customers[customerIndex].OrderHistory = new List<Order>();
                }

                // Initialize flag for adding more ice creams
                bool addMoreIceCreams = true;

                while (addMoreIceCreams)
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

                    // Adding a new IceCream object based on the option
                    IceCream iceCream;

                    if (option.ToLower().Trim() == "cup")
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

                    Console.WriteLine("");

                    // Prompting user if they want to add another ice cream
                    Console.Write("Would you like to add another ice cream to the order? [Y/N]: ");
                    string addMoreIceCreamsInput = Console.ReadLine().ToLower();
                    addMoreIceCreams = addMoreIceCreamsInput == "y"; // if y sets to true, otherwise false
                }

                // Adding the new order to the customer's OrderHistory only if it's not already added
                if (!customers[customerIndex].OrderHistory.Contains(newOrder))
                {
                    customers[customerIndex].OrderHistory.Add(newOrder);
                }

                Console.WriteLine("");

                if (customers[customerIndex].OrderHistory.Count > 0)
                {
                    Console.WriteLine("---------------------------------------------------------------------------------------------------");

                    for (int orderIndex = 0; orderIndex < customers[customerIndex].OrderHistory.Count; orderIndex++)
                    {
                        var order = customers[customerIndex].OrderHistory[orderIndex];

                        Console.WriteLine($"Order ID: {order.Id}, Time Received: {order.TimeReceived}");
                        Console.WriteLine("");
                        Console.WriteLine("ORDER:");

                        for (int i = 0; i < order.IceCreamList.Count; i++)
                        {
                            Console.WriteLine($"IceCream [{i + 1}]: {order.IceCreamList[i]}");
                        }
                    }

                    Console.WriteLine("---------------------------------------------------------------------------------------------------");
                    Console.WriteLine("");
                    
                    if (customers[customerIndex].Rewards.Tier == "Gold")
                    {
                        Console.WriteLine("Order placed in the gold members order queue.");
                        pointCardGold.Enqueue(newOrder);

                    }
                    else
                    {
                        pointCardRegular.Enqueue(newOrder);
                        Console.WriteLine($"{pointCardRegular.Count} test, added new order");
                    }
                }

            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input format. Please enter a valid format: {ex.Message}");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Overflow exception. The entered value is too large: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

        }

        static void ProcessOrderAndCheckout(List<Customer> customers, Queue<Order> pointCardGold, Queue<Order> pointCardRegular)
        {
            try
            {
                // Check if there are any orders in both queues
                if (pointCardGold.Count == 0 && pointCardRegular.Count == 0)
                {
                    Console.WriteLine("No orders to process.");
                    return;
                }

                Dictionary<int, List<Order>> customerOrders = new Dictionary<int, List<Order>>();

                // Dequeue all orders from the gold members order queue
                while (pointCardGold.Count > 0)
                {
                    Order currentOrder = pointCardGold.Dequeue();
                    ProcessOrder(currentOrder, customerOrders);
                }

                // Dequeue all orders from the regular members order queue
                while (pointCardRegular.Count > 0)
                {
                    Order currentOrder = pointCardRegular.Dequeue();
                    ProcessOrder(currentOrder, customerOrders);
                }

                // Display all ice creams in all processed orders
                Console.WriteLine("Ice Creams in all processed orders:");

                foreach (var customerOrder in customerOrders)
                {
                    int memberId = customerOrder.Key;
                    List<Order> orders = customerOrder.Value;

                    Console.WriteLine($"Member ID: {memberId}");
                    foreach (var order in orders)
                    {
                        foreach (var iceCream in order.IceCreamList)
                        {
                            Console.WriteLine(iceCream);
                        }
                    }
                }

                // Calculate and display the total bill amount for all orders combined
                double totalBill = customerOrders.Values.SelectMany(orders => orders).Sum(order => order.CalculateTotal());
                Console.WriteLine($"Total Bill Amount for all orders: {totalBill:C}");

                // Display membership status and points of the customer (assuming all orders belong to the first customer for simplicity)
                if (customerOrders.Count > 0)
                {
                    int firstCustomerId = customerOrders.Keys.First();
                    if (customers.Any(c => c.MemberID == firstCustomerId))
                    {
                        Console.WriteLine($"Membership Status: {customers.First(c => c.MemberID == firstCustomerId).Rewards.Tier}");
                        //double points = totalBill * 0.72;
                        //Console.WriteLine($"Points: {customers.First(c => c.MemberID == firstCustomerId).Rewards.Points}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void ProcessOrder(Order order, Dictionary<int, List<Order>> customerOrders)
        {
            // Add the order to the dictionary based on the customer's member ID
            int memberId = order.Id; 
            if (!customerOrders.ContainsKey(memberId))
            {
                customerOrders[memberId] = new List<Order>();
            }

            customerOrders[memberId].Add(order);
        }



        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // (Brayden's Methods)
        static void DisplayOrder(Order order)
        {
            if (order != null)
            {
                int j = 1;
                List<IceCream> IceCreamList = order.IceCreamList;
                foreach (IceCream iceCream in IceCreamList)
                {
                    Console.WriteLine($"  Ice Cream [{j}]");
                    List<Flavour> FlavourList = iceCream.Flavours;
                    List<Topping> ToppingList = iceCream.Toppings;
                    foreach (Flavour flavour in FlavourList)
                    {
                        Console.WriteLine($"    {flavour.Quantity} scoops of {flavour.Type}");
                    }
                    Console.WriteLine($"    Topped with: {String.Join(", ", ToppingList)}");
                    j++;
                }
            }
            
        }
        static void DisplayAllCurrentOrders(Queue<Order> orders)
        {
            int i = 1;
            Console.WriteLine(orders.Count + "test");
            foreach (Order order in orders)
            {   
                
                Console.WriteLine($"Order No:[{i}]");
                DisplayOrder(order);
                i++;
            }
        }

        static void DisplayOrderDetails(List<Customer> customerList)
        {
            DisplayAllCustomers(customerList);
            bool valid = false;
            int option = 0;
            while (!valid)
            {
                Console.Write("Enter index of customer to select: ");
                string input = Console.ReadLine();
                try
                {
                    option = int.Parse(input);
                    if (option < 1 || option > customerList.Count)
                    {
                        valid = false;
                        Console.WriteLine("Index not in range! Try again.");
                        continue;
                    }
                    else
                    {
                        valid= true;
                    }
                }
                catch (FormatException) { Console.WriteLine("Invalid input. Please enter a valid number."); }
                catch (OverflowException) { Console.WriteLine("Input is not a 32-bit signed integer. Please enter a 32-bit signed integer."); }
                catch (Exception ex) { Console.WriteLine($"An unexpected error occurred: {ex.Message}"); }
            }
            Customer customer = customerList[option-1];
            try
            {
                List<Order> orderHistory = customer.OrderHistory;
                Order currentOrder = customer.CurrentOrder;
                if (currentOrder == null || orderHistory == null) 
                {
                    Console.WriteLine("No data found. ");
                    return;
                }
                Console.WriteLine("Current Order\r\n-------------------");
                Console.WriteLine("Date Received: " + currentOrder.TimeReceived.ToString("dd MMM yyyy, HH:mm"));
                DisplayOrder(currentOrder);
                Console.WriteLine("Past Orders\r\n-------------------");
                int i = 1;
                foreach (Order order in orderHistory)
                {
                    Console.WriteLine($"Order [{i}]");
                    Console.WriteLine("Date Received:  " + order.TimeReceived.ToString("dd MMM yyyy, HH:mm"));
                    if (order.TimeFulfilled.HasValue)
                    {
                        DateTime? time = order.TimeFulfilled;
                        Console.WriteLine("Date Fulfilled: " + time?.ToString("dd MMM yyyy, HH:mm"));
                    }
                    else
                    {
                        Console.WriteLine("Not yet fulfilled");
                    }
                    DisplayOrder(order);
                    i++;
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No customer data found.");
            }
            
        }
        

        static void ModifyOrderDetails()
        {

        }

        static void DisplayChargess()
        {

        }
// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}