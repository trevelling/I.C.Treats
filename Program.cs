﻿//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================


using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Globalization;
using S10258591_PRG2Assignment;
using static System.Formats.Asn1.AsnWriter;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;
using System.Security;

namespace IceCreamShop
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // QUEUE OF ORDERS FOR GOLD MEMBERS
                Queue<Order> pointCardGold = new Queue<Order>();

                // QUEUE OF ORDERS FOR REGULAR MEMBERS
                Queue<Order> pointCardRegular = new Queue<Order>();

                // LIST OF CUSTOMER OBJECTS
                List<Customer> customerList = new List<Customer>();

                List<Order> newOrderList = new List<Order>();

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
                            CreateNewOrder(customerList, pointCardGold, pointCardRegular, newOrderList);
                        }
                        else if (option == 5)
                        {
                            DisplayOrderDetails(customerList);
                        }
                        else if (option == 6)
                        {
                            // Modify order details [Brayden]
                            ModifyOrderDetails(customerList);
                        }
                        else if (option == 7)
                        {
                            // Advanced Option (a) - Process an order and checkout [Tevel]
                            ProcessOrderAndCheckout(customerList, pointCardGold, pointCardRegular, newOrderList);
                        }
                        else if (option == 8)
                        {
                            // Advanced Option (b) - Display monthly charged amounts breakdown & total charged amounts for the year [Brayden]
                            DisplayCharges();
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


        static void CreateNewOrder(List<Customer> customers, Queue<Order> pointCardGold, Queue<Order> pointCardRegular, List<Order> newOrderList)
        {
            try
            {

                DisplayAllCustomers(customers);
                Console.WriteLine("");

                int customerIndex = -1; // Initialize to an invalid index

                while (true)
                {
                    Console.Write("Please select a customer No. : ");
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out customerIndex) || customerIndex <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid customer number.");
                        continue;
                    }

                    customerIndex--; // Adjust to match the index
                    Console.WriteLine("");
                    break;
                }

                // Creating a new Order for the customer selected
                Order newOrder = customers[customerIndex].MakeOrder();
                UpdateFlavoursCSV(newOrder);
                UpdateToppingsCSV(newOrder);
                UpdateOptionsCSV(newOrder);
                newOrderList.Add(newOrder);

                // Checking which queue to put them in
                if (customers[customerIndex].Rewards.Tier == "Gold")
                {
                    pointCardGold.Enqueue(newOrder);
                }
                else
                {
                    pointCardRegular.Enqueue(newOrder);
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

        static void UpdateFlavoursCSV(Order orders)
        {
            try
            {
                // Path to the flavours.csv file
                string csvFilePath = "flavours.csv";

                // Check if the file exists
                if (!File.Exists(csvFilePath))
                {
                    Console.WriteLine($"Error: {csvFilePath} file not found.");
                    return;
                }
                
                // Open the file for appending
                try
                {
                    using (StreamWriter sw = new StreamWriter(csvFilePath, true))
                    {
                        // Extract flavor information from the order and write it to the file
                        foreach (var iceCream in orders.IceCreamList)
                        {
                            foreach (var order in iceCream.Flavours)
                            {
                                if (order.Premium == true)
                                {
                                    sw.WriteLine($"{order.Type},{2}");
                                }
                                else
                                {
                                    sw.WriteLine($"{order.Type},{0}");
                                }
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error writing to {csvFilePath}: {ex.Message}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Error: Unauthorized access to {csvFilePath}: {ex.Message}");
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine($"Error: The operation is not supported for {csvFilePath}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while writing to {csvFilePath}: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void UpdateToppingsCSV(Order orders)
        {
            try
            {
                // Path to the toppings.csv file
                string csvFilePath = "toppings.csv";

                // Check if the file exists
                if (!File.Exists(csvFilePath))
                {
                    Console.WriteLine($"Error: {csvFilePath} file not found.");
                    return;
                }

                // Open the file for appending
                try
                {
                    using (StreamWriter sw = new StreamWriter(csvFilePath, true))
                    {
                        // Extract flavor information from the order and write it to the file
                        foreach (var iceCream in orders.IceCreamList)
                        {
                            foreach (var order in iceCream.Toppings)
                            {
                                sw.WriteLine($"{order.Type},{1}");
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error writing to {csvFilePath}: {ex.Message}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Error: Unauthorized access to {csvFilePath}: {ex.Message}");
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine($"Error: The operation is not supported for {csvFilePath}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while writing to {csvFilePath}: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void UpdateOptionsCSV(Order orders)
        {
            try
            {
                // Path to the options.csv file
                string csvFilePath = "options.csv";

                // Check if the file exists
                if (!File.Exists(csvFilePath))
                {
                    Console.WriteLine($"Error: {csvFilePath} file not found.");
                    return;
                }

                // Open the file for appending
                try
                {
                    using (StreamWriter sw = new StreamWriter(csvFilePath, true))
                    {
                        // Extract flavor information from the order and write it to the file
                        foreach (var iceCream in orders.IceCreamList)
                        {
                            if (iceCream is Cup cupIceCream)
                            {
                                sw.WriteLine($"{cupIceCream.Option},{cupIceCream.Scoops},,,{cupIceCream.CalculatePrice()}");
                            }
                            else if (iceCream is Cone coneIceCream)
                            {
                                // Check if the Cone is dipped and convert boolean to string
                                string dippedString = coneIceCream.Dipped ? "TRUE" : "FALSE";

                                sw.WriteLine($"{coneIceCream.Option},{coneIceCream.Scoops},{dippedString},,{coneIceCream.CalculatePrice()}");
                            }
                            else if (iceCream is Waffle waffleIceCream)
                            {
                                sw.WriteLine($"{waffleIceCream.Option},{waffleIceCream.Scoops},,{waffleIceCream.WaffleFlavour},{waffleIceCream.CalculatePrice()}");
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error writing to {csvFilePath}: {ex.Message}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Error: Unauthorized access to {csvFilePath}: {ex.Message}");
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine($"Error: The operation is not supported for {csvFilePath}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while writing to {csvFilePath}: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void UpdateOrdersCSV(Order orders, Customer customers)
        {
            try
            {
                // Path to the options.csv file
                string csvFilePath = "orders.csv";

                // Check if the file exists
                if (!File.Exists(csvFilePath))
                {
                    Console.WriteLine($"Error: {csvFilePath} file not found.");
                    return;
                }

                // Open the file for appending
                try
                {
                    using (StreamWriter sw = new StreamWriter(csvFilePath, true))
                    {
                        // Extract flavor information from the order and write it to the file
                        foreach (var iceCream in orders.IceCreamList)
                        {
                            // Initialize arrays to store flavor and topping information
                            string[] flavorColumns = { "", "", "" };
                            string[] toppingColumns = { "", "", "", "" };

                            // Populate flavorColumns with flavor information
                            for (int i = 0; i < iceCream.Flavours.Count && i < 3; i++)
                            {
                                flavorColumns[i] = iceCream.Flavours[i].Type;
                            }

                            // Populate toppingColumns with topping information
                            for (int i = 0; i < iceCream.Toppings.Count && i < 4; i++)
                            {
                                toppingColumns[i] = iceCream.Toppings[i].Type;
                            }

                            // Check if the ice cream is a Cup, Cone, or Waffle and write to the file
                            if (iceCream is Cup cupIceCream)
                            {
                                sw.WriteLine($"{orders.Id},{customers.MemberID},{orders.TimeReceived},{orders.TimeFulfilled},{cupIceCream.Option},{cupIceCream.Scoops},,,{string.Join(",", flavorColumns)},{string.Join(",", toppingColumns)}");
                            }
                            else if (iceCream is Cone coneIceCream)
                            {
                                // Check if the Cone is dipped and convert boolean to string
                                string dippedString = coneIceCream.Dipped ? "TRUE" : "FALSE";

                                sw.WriteLine($"{orders.Id},{customers.MemberID},{orders.TimeReceived},{orders.TimeFulfilled},{coneIceCream.Option},{coneIceCream.Scoops},{dippedString},,{string.Join(",", flavorColumns)},{string.Join(",", toppingColumns)}");
                            }
                            else if (iceCream is Waffle waffleIceCream)
                            {
                                sw.WriteLine($"{orders.Id},{customers.MemberID},{orders.TimeReceived},{orders.TimeFulfilled},{waffleIceCream.Option},{waffleIceCream.Scoops},,,{waffleIceCream.WaffleFlavour},{string.Join(",", flavorColumns)},{string.Join(",", toppingColumns)}");
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error writing to {csvFilePath}: {ex.Message}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Error: Unauthorized access to {csvFilePath}: {ex.Message}");
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine($"Error: The operation is not supported for {csvFilePath}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while writing to {csvFilePath}: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }



        static void ProcessOrderAndCheckout(List<Customer> customers, Queue<Order> pointCardGold, Queue<Order> pointCardRegular, List<Order> newOrderList)
        {
            try
            {
                double mostExpensiveIceCreamPrice = 0.00;
                double totalBill = 0.00;
                bool birthday = false;
                bool ordersToProcess = false; 


                List<Order> ordersToRemove = new List<Order>(); // Keep track of orders to remove

                foreach (var customer in customers) // Search through customer list
                {
                    foreach (var order in newOrderList) // Search through the orders created
                    {
                        // Checks if the customer's current order id == matches the order id in newOrderList
                        if (customer.CurrentOrder != null && customer.CurrentOrder.Id == order.Id)
                        {
                            if (newOrderList.Count > 0)
                            {
                                ordersToProcess = true;

                                // Process orders from the gold members order queue
                                while (pointCardGold.Count > 0)
                                {
                                    pointCardGold.Dequeue();
                                }

                                // Process orders from the regular members order queue
                                while (pointCardRegular.Count > 0)
                                {
                                    pointCardRegular.Dequeue();
                                }

                                Console.WriteLine("");
                                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                Console.WriteLine($"{customer.Name} - {customer.MemberID}");

                                if (customer.IsBirthday())
                                {
                                    birthday = true;
                                }

                                Console.WriteLine($"Membership Status: {customer.Rewards.Tier}");

                                // Display all ice creams in all processed orders
                                Console.WriteLine("Ice Creams in all processed orders:");
                                Console.WriteLine(order);
                                
                                //customer.OrderHistory.Add(order);

                                foreach (var iceCream in order.IceCreamList)
                                {
                                    Console.WriteLine(iceCream);
                                    if (iceCream.CalculatePrice() > mostExpensiveIceCreamPrice)
                                    {
                                        mostExpensiveIceCreamPrice = iceCream.CalculatePrice();
                                    }
                                }

                                double orderMostExpensivePrice = order.IceCreamList.Max(iceCream => iceCream.CalculatePrice());

                                // Update the overall most expensive ice cream price
                                if (orderMostExpensivePrice > mostExpensiveIceCreamPrice)
                                {
                                    mostExpensiveIceCreamPrice = orderMostExpensivePrice;
                                }
                                totalBill += order.CalculateTotal();

                                // Checks if a birthday discount is applicable
                                if (mostExpensiveIceCreamPrice > 0 && birthday)
                                {
                                    // Calculated discounted bill
                                    totalBill -= mostExpensiveIceCreamPrice;

                                    Console.WriteLine($"ITS YOUR BIRTHDAY!!! - Total Bill Amount for all orders after Birthday discount: {totalBill:C}");
                                }
                                else
                                {
                                    Console.WriteLine($"Total Bill Amount for all orders: {totalBill:C}");
                                }

                                int redemptionPoints = (int)Math.Floor(totalBill * 0.72);

                                // Add redemption points to the customer's PointCard
                                customer.Rewards.AddPoints(redemptionPoints);

                                Console.WriteLine($"Points before redemption: {customer.Rewards.Points}");

                                // Set the time fulfilled for the entire order
                                DateTime timeFulfilled = DateTime.Now;
                                order.TimeFulfilled = timeFulfilled;

                                Console.WriteLine($"Time Fulfilled: {timeFulfilled.ToString("hh:mm:ss tt")}");

                                Console.WriteLine($"Points after redemption: {customer.Rewards.Points}");

                                // Prompt user to press any key for payment and increment punch card
                                Console.WriteLine("Press [enter] to make payment...");
                                Console.ReadKey();
                                Console.WriteLine("Payment has successfully been made");
                                // Increment the punch card for every ice cream in the order
                                customer.Rewards.Punch();
                                totalBill = 0;
                                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                                UpdateOrdersCSV(order, customer);

                                // Add the order to the list of orders to remove
                                ordersToRemove.Add(order);
                            }
                        }
                    }
                }

                // Remove the processed orders from the newOrderList
                foreach (var orderToRemove in ordersToRemove)
                {
                    newOrderList.Remove(orderToRemove);
                }

                // Display the message if there are no orders to process
                if (!ordersToProcess)
                {
                    Console.WriteLine("No orders to process.");
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
                    Console.WriteLine($"  {iceCream.Option} Ice Cream [{j}]");
                    List<Flavour> FlavourList = iceCream.Flavours;
                    List<Topping> ToppingList = iceCream.Toppings;
                    foreach (Flavour flavour in FlavourList)
                    {
                        Console.WriteLine($"    {flavour.Quantity} scoops of {flavour.Type}");
                    }
                    if (ToppingList.Count > 0)
                    {
                        Console.WriteLine($"    Topped with: {String.Join(", ", ToppingList)}");
                    }
                    j++;
                }
            }
        }
        static void DisplayAllCurrentOrders(Queue<Order> orders)
        {
            int i = 1;
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
                if (currentOrder == null && orderHistory == null) 
                {
                    Console.WriteLine("No data found. ");
                    return;
                }
                if (currentOrder != null)
                {
                    Console.WriteLine($"{customer.Name}'s Current Order\r\n-------------------");
                    Console.WriteLine("Date Received: " + currentOrder.TimeReceived.ToString("dd MMM yyyy, HH:mm"));
                    DisplayOrder(currentOrder);
                }
                if (orderHistory != null)
                {
                    Console.WriteLine($"{customer.Name}'s Past Orders\r\n-------------------");
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
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No customer data found.");
            }
            
        }
        
        static int IntValidation(int start, int end)
        {
            int option = 0;
            bool valid = false;
            while (!valid)
            {
                try
                {
                    string input = (Console.ReadLine());
                    option = int.Parse(input);
                    if (option < start || option > end)
                    {
                        valid = false;
                        Console.WriteLine($"Index not in range [{start}-{end}]! Try again.");
                        continue;
                    }
                    else
                    {
                        valid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
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
            return option;
        }
        static void ModifyOrderDetails(List<Customer> customerList)
        {
            DisplayAllCustomers(customerList);
            Console.Write("Choose customer: ");
            int option = IntValidation(1, customerList.Count);
            Customer customer = customerList[option - 1];
            Order currentOrder = customer.CurrentOrder;
            DisplayOrder(currentOrder);
            Console.WriteLine("[1] Choose an existing ice cream to modify\r\n[2] Add a new ice cream to the order\r\n[3] Choose an existing ice cream to delete from order\r\n[0] Cancel");
            Console.Write("Enter option: ");
            option = IntValidation(1, 3);
            List<IceCream> iceCreamList = currentOrder.IceCreamList;
            if (option == 1)
            {
                Console.Write("Choose Ice Cream to modify: ");
                option = IntValidation(1, iceCreamList.Count);
                currentOrder.ModifyIceCream(option - 1);
            }
            else if (option == 2)
            {
                IceCream iceCream = MakeIceCream();
                currentOrder.AddIceCream(iceCream);
            }
            else if (option == 3)
            {
                Console.Write("Choose Ice Cream to delete: ");
                option = IntValidation(1, iceCreamList.Count);
                currentOrder.DeleteIceCream(option - 1);
            }
        }

        static IceCream MakeIceCream()
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
                return new Cup(option, scoops, flavourList, toppingList);
            }
            else if (option.ToLower().Trim() == "cone")
            {
                return new Cone(option, scoops, flavourList, toppingList, false);
            }
            else
            {
                return new Waffle(option, scoops, flavourList, toppingList, waffleFlavour);
            }
        }

        static void DisplayCharges()
        {

        }
// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}