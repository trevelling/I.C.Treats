//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================

using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace IceCreamShop
{
    class Program
    {
        static void Main(string[] args)
        {  
            //Basic Features
            DisplayMenu();
        }

        static void DisplayMenu()
        {
            Console.WriteLine("------------- O P T I O N S -------------");
            Console.WriteLine("[1] List all customers");
            Console.WriteLine("[2] List all current orders");
            Console.WriteLine("[3] Register a new customer");
            Console.WriteLine("[4] Create a customer’s order");
            Console.WriteLine("[5] Display order details of a customer");
            Console.WriteLine("[6] Modify order details");
            Console.WriteLine("[0] Exit Menu");
            Console.WriteLine("-----------------------------------------");

            Console.Write("Enter your option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            if (option == 1)
            {
                // 1) List all customers 
                List<Customer> customers = initCustomers("customers.csv");
                DisplayAllCustomers(customers);
            }
        }

        static List<Customer> initCustomers(string filePath)
        {
            // Creating List of customer objects
            List<Customer> customers = new List<Customer>();

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

            return customers;
        }

        static void DisplayAllCustomers(List<Customer> customers)
        {
            Console.WriteLine("{0,-10} {1,-10} {2,-10}", "Name", "MemberID", "DOB");
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }
        }
    }
}