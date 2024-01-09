//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================

using System;
using System.IO;
using System.Collections.Generic;
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

            while (true)
            {
                DisplayMenu();

                Console.Write("Enter your option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    // 1) List all customers 
                    initCustomers("customers.csv", customerList);
                    DisplayAllCustomers(customerList);
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
        }
    }
}