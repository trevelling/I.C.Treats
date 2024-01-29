//==========================================================
// Student Number : S10258591
// Student Name : Tevel Sho
// Partner Name : Brayden Saga
//==========================================================


using System.Globalization;
using S10258591_PRG2Assignment;


namespace IceCreamShop
{
    class Program
    {
        private static int nextOrderId = 1;

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

                // LIST OF NEW ORDERS CREATED
                List<Order> newOrderList = new List<Order>();

                initCustomers("customers.csv", customerList);
                ReadExistingCustomersFromCsv("orders.csv", customerList);

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
                            Console.Write("Enter the year: ");
                            int year = Convert.ToInt32(Console.ReadLine());
                            DisplayCharges(year, customerList);
                        }
                        else if (option == 0)
                        {
                            Console.WriteLine("Exiting I.C.Treats....");
                            Console.WriteLine("See you next time!      :)");
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

                    if (fields.Length == 6)
                    {
                        string name = fields[0].Trim();
                        int memberID = Convert.ToInt32(fields[1].Trim());
                        DateTime dob = Convert.ToDateTime(fields[2].Trim());
                        string membershipStatus = fields[3].Trim();
                        int membershipPoints = Convert.ToInt32(fields[4].Trim());
                        int punchCard = Convert.ToInt32(fields[5].Trim());
                        
                        // Create a new Customer object and add it to the list
                        Customer customer = new Customer(name, memberID, dob);
                        customer.Rewards.AddPoints(membershipPoints);
                        
                        for (int j = 0; j < punchCard; j++)
                        {
                            customer.Rewards.Punch();
                        }
                        customer.Rewards.Tier = membershipStatus;
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


// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// (Tevel's Methods)

        static void DisplayAllCustomers(List<Customer> customers)
        {
            try
            {
                if (customers == null || customers.Count == 0)
                {
                    Console.WriteLine("No customers to display.");
                    return;
                }

                Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-11} {4, -17} {5, -17} {6, -10}", "No.", "Name", "MemberID", "DOB", "MembershipStatus", "MembershipPoints", "PunchCard");

                for (int i = 0; i < customers.Count; i++)
                {
                    Console.WriteLine($"{$"[{i + 1}]",-4}{customers[i].ToString()}");
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
                Console.WriteLine("");

                // Input validation for Name (should only allow strings)
                string name;
                do
                {
                    Console.Write("Enter your name: ");
                    name = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit))
                    {
                        Console.WriteLine("Invalid name. Please enter a valid name.");
                    }
                } while (string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit));

                // Input validation for ID Number (should only allow positive integers of length 6)
                int id;
                do
                {
                    Console.Write("Enter your ID Number (e.g. 123456): ");
                    if (!int.TryParse(Console.ReadLine(), out id) || id <= 0 || id.ToString().Length != 6)
                    {
                        Console.WriteLine("Invalid Member ID format. Please enter a positive integer with 6 digits.");
                    }
                } while (id <= 0 || id.ToString().Length != 6);

                // Input validation for Date of Birth (should be in the format "dd/MM/yyyy")
                DateTime dob;
                do
                {
                    Console.Write("Enter your Date of Birth (dd/MM/yyyy): ");
                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out dob))
                    {
                        Console.WriteLine("Invalid Date of Birth format. Please enter a valid date in the format dd/MM/yyyy.");
                    }
                } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dob));

                // Rest of the code remains unchanged
                Customer newCustomer = new Customer(name, id, dob);
                PointCard pointCard = new PointCard(0, 0);
                newCustomer.Rewards = pointCard;
                customerList.Add(newCustomer);

                string filePath = "customers.csv";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{name},{id},{dob.ToString("dd/MM/yyyy")},{"Ordinary"},{0},{0}");
                }

                Console.WriteLine(
                    $"!NEW Customer! \n\n\t Name - {name} \n\t MemberID {id} \n\t DOB - {dob.ToString("dd/MM/yyyy")} \n\nhas been successfully registered");
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


        static void CreateNewOrder(List<Customer> customers, Queue<Order> pointCardGold, Queue<Order> pointCardRegular,
            List<Order> newOrderList)
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
                                sw.WriteLine(
                                    $"{cupIceCream.Option},{cupIceCream.Scoops},,,{cupIceCream.CalculatePrice()}");
                            }
                            else if (iceCream is Cone coneIceCream)
                            {
                                // Check if the Cone is dipped and convert boolean to string
                                string dippedString = coneIceCream.Dipped ? "TRUE" : "FALSE";

                                sw.WriteLine(
                                    $"{coneIceCream.Option},{coneIceCream.Scoops},{dippedString},,{coneIceCream.CalculatePrice()}");
                            }
                            else if (iceCream is Waffle waffleIceCream)
                            {
                                sw.WriteLine(
                                    $"{waffleIceCream.Option},{waffleIceCream.Scoops},,{waffleIceCream.WaffleFlavour},{waffleIceCream.CalculatePrice()}");
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

        static void EditLineInFile(string filePath, int orderId, DateTime newFulfillmentTime)
        {
            // Read all lines into a list
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            List<string> updatedLines = new List<string>();

            // Update lines with the specified order ID
            foreach (var line in lines)
            {
                // Split the line into columns
                string[] columns = line.Split(',');

                // Check if the first column matches the order ID
                if (int.TryParse(columns[0], out int currentOrderId) && currentOrderId == orderId)
                {
                    // Check if the TimeFulfilled column (index 3) is ''
                    if (columns[3].Trim().Equals("", StringComparison.OrdinalIgnoreCase))
                    {
                        // Replace '' with the new fulfillment time
                        columns[3] = newFulfillmentTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    }
                }

                // Re-join the columns into a single string and add to updatedLines
                updatedLines.Add(string.Join(",", columns));
            }

            // Write the updated lines back to the file
            File.WriteAllLines(filePath, updatedLines);
        }

        static void ProcessOrderAndCheckout(List<Customer> customers, Queue<Order> pointCardGold,
    Queue<Order> pointCardRegular, List<Order> newOrderList)
        {
            try
            {
                if (newOrderList.Count == 0)
                {
                    Console.WriteLine("No orders to process.");
                    return; // Exit the method if there are no orders to process
                }

                double mostExpensiveIceCreamPrice = 0.00;
                double totalBill = 0.00;
                bool birthday = false;
                bool ordersToProcess = false;

                List<Order> ordersToRemove = new List<Order>(); // Keep track of orders to remove

                foreach (var customer in customers) // Search through customer list
                {
                    Order order = newOrderList[0];
                    // Checks if the customer's current order id == matches the order id in newOrderList
                    if (customer.CurrentOrder != null && customer.CurrentOrder.Id == order.Id)
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
                        Console.WriteLine(
                            "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine($"{customer.Name} - {customer.MemberID} | Your Points: {customer.Rewards.Points} | test: {customer.Rewards.PunchCard}");

                        if (customer.IsBirthday())
                        {
                            birthday = true;
                        }

                        Console.WriteLine($"Membership Status: {customer.Rewards.Tier}");

                        // Display all ice creams in all processed orders
                        Console.WriteLine(order);
                        Console.WriteLine("");

                        Console.WriteLine("Your Order:");
                        foreach (var iceCream in order.IceCreamList)
                        {
                            Console.WriteLine(iceCream);
                            if (iceCream.CalculatePrice() > mostExpensiveIceCreamPrice)
                            {
                                mostExpensiveIceCreamPrice = iceCream.CalculatePrice();
                            }
                        }

                        double orderMostExpensivePrice =
                            order.IceCreamList.Max(iceCream => iceCream.CalculatePrice());

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

                            Console.WriteLine(
                                $"ITS YOUR BIRTHDAY!!! - Total Bill Amount for all orders after Birthday discount: {totalBill:C}");
                        }
                        else
                        {
                            Console.WriteLine($"Total Bill Amount for all orders: {totalBill:C}");
                        }

                        int redemptionPoints = (int)Math.Floor(totalBill * 0.72);

                        // Add redemption points to the customer's PointCard
                        customer.Rewards.AddPoints(redemptionPoints);

                        Console.WriteLine($"Points earned: {redemptionPoints}");
                        Console.WriteLine($"Final Points: {customer.Rewards.Points}");
                        Console.WriteLine("");

                        if (customer.Rewards.PunchCard >= 10)
                        {
                            Console.WriteLine("Congratulations! You've earned a free ice cream with your punch card! Redeeming...");
                            double firstIceCreamPrice = order.IceCreamList[0].CalculatePrice();
                            totalBill -= firstIceCreamPrice;
                            Console.WriteLine($"Final Bill: ${totalBill} ");
                            customer.Rewards.PunchCard = 0;
                        }

                        // Check the membership status after adding points
                        if ((customer.Rewards.Tier == "Silver" || customer.Rewards.Tier == "Gold") && totalBill > 0)
                        {
                            // Prompt the user for the option to redeem points
                            string option;
                            while (true)
                            {
                                Console.Write("Do you want to redeem your points for further discount [Y/N]: ");
                                option = Console.ReadLine().ToLower();

                                if (option == "y" || option == "n")
                                {
                                    if (option == "y")
                                    {
                                        // Prompt the user for the amount they want to redeem
                                        int redeemAmount;
                                        while (true)
                                        {
                                            Console.WriteLine($"Total points redeemable after new order: {customer.Rewards.Points}");
                                            Console.Write($"Enter the amount of points to redeem (1 point = $0.02): ");
                                            string redeemInput = Console.ReadLine();

                                            if (int.TryParse(redeemInput, out redeemAmount))
                                            {
                                                // Call the RedeemPoints method with the original redeemAmount
                                                customer.Rewards.RedeemPoints(redeemAmount); // 1 point = $0.02
                                                totalBill -=
                                                    redeemAmount * 0.02; // Deduct redeemed amount from the total bill
                                                Console.WriteLine("");
                                                Console.WriteLine($"Final Bill: {totalBill:C}");
                                                break; // Break out of the loop if valid input is provided
                                            }

                                            Console.WriteLine("Invalid input. Please enter a valid number.");
                                        }
                                    }

                                    break; // Break out of the loop if valid input is provided
                                }

                                Console.WriteLine("Invalid input. Please enter 'Y' or 'N'.");
                            }
                        }

                        // Prompt user to press any key for payment and increment punch card
                        Console.WriteLine("Press [enter] to make payment...");
                        Console.ReadKey();
                        Console.WriteLine("Payment has successfully been made");

                        customer.CurrentOrder = null;

                        // Set the time fulfilled for the entire order
                        DateTime timeFulfilled = DateTime.Now;
                        order.TimeFulfilled = timeFulfilled;
                        Console.WriteLine($"Time Fulfilled: {timeFulfilled.ToString("hh:mm:ss tt")}");
                        
                        totalBill = 0;
                        Console.WriteLine(
                            "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                        customer.Rewards.Punch();

                        // Add processed order to OrderHistory
                        customer.OrderHistory.Add(order);

                        EditLineInFile("orders.csv", order.Id, timeFulfilled);

                        // Add the order to the list of orders to remove
                        ordersToRemove.Add(order);
                    }

                }

                // Remove the processed orders from the newOrderList
                foreach (var orderToRemove in ordersToRemove)
                {
                    newOrderList.Remove(orderToRemove);
                }

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
                        //test
                        if (flavour.Type == "")
                        {
                            Console.WriteLine("No flavour");
                        }
                        Console.WriteLine($"    {flavour.Quantity} scoop(s) of {flavour.Type}");
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
                        valid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input is not a 32-bit signed integer. Please enter a 32-bit signed integer.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }

            Customer customer = customerList[option - 1];
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
                    Console.WriteLine($"Order ID: {currentOrder.Id}");
                    Console.WriteLine("Date Received: " + currentOrder.TimeReceived.ToString("dd MMM yyyy, HH:mm"));
                    DisplayOrder(currentOrder);
                }

                Console.WriteLine("");
                if (orderHistory != null)
                {
                    Console.WriteLine($"{customer.Name}'s Past Orders\r\n-------------------");
                    int i = 1;
                    foreach (Order order in orderHistory)
                    {
                        Console.WriteLine($"Order [{i}]");
                        Console.WriteLine($"Order ID: {order.Id}");
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
            if (customer.CurrentOrder == null)
            {
                Console.WriteLine("Customer has no current order.");
                return;
            }
            List<IceCream> iceCreamList = currentOrder.IceCreamList;

            DisplayOrder(currentOrder);
            Console.WriteLine(
                "[1] Choose an existing ice cream to modify\r\n[2] Add a new ice cream to the order\r\n[3] Choose an existing ice cream to delete from order\r\n[0] Cancel");
            Console.Write("Enter option: ");
            option = IntValidation(1, 3);
            
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
            else if (option == 0)
            {
                return;
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
                        validWaffleFlavour.ToLower() != "charcoal" && validWaffleFlavour.ToLower() != "pandan" &&
                        validWaffleFlavour.ToLower() != "redvelvet")
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

                    Console.WriteLine(
                        $"Choose your {(premium ? "premium" : "non-premium")} flavour for scoop {scoopNumber}!");

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
                    if ((premium && (flavour.ToLower() == "durian" || flavour.ToLower() == "ube" ||
                                     flavour.ToLower() == "seasalt" || flavour.ToLower() == "sea salt")) ||
                        (!premium && (flavour.ToLower() == "vanilla" || flavour.ToLower() == "chocolate" ||
                                      flavour.ToLower() == "strawberry")))
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
                        exitToppings = true; // Set exitToppings to true to break out of the for loop
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

        static void DisplayCharges(int year, List<Customer> customerList)
        {
            try
            {
                //Month to Price
                Dictionary<int, List<Double>> ordersList = new Dictionary<int, List<Double>>();
                for (int i = 0; i < 12; i++)
                {
                    ordersList[i] = new List<Double>();
                }
                foreach (Customer customer in customerList)
                {
                    
                    foreach (Order order in customer.OrderHistory)
                    {
                        if (order.TimeFulfilled.HasValue)
                        {
                            DateTime timeFulfilled = order.TimeFulfilled.Value;
                            if (timeFulfilled.Year == year)
                            {
                                double price = order.CalculateTotal();
                                // if its their birthday when they ordered it, the most expensive ice cream is free
                                if (customer.DOB.Month == order.TimeReceived.Month && customer.DOB.Day == order.TimeReceived.Day)
                                {
                                    double expensiveprice = 0;
                                    foreach (IceCream icecream in order.IceCreamList)
                                    {
                                        if (icecream.CalculatePrice() > expensiveprice)
                                        {
                                            expensiveprice = icecream.CalculatePrice();
                                        }
                                    }
                                    price -= expensiveprice;
                                }
                                ordersList[timeFulfilled.Month-1].Add(price);
                            }
                        }
                    }
                }

                double total = 0;
                for (int i = 0; i < 12; i++)
                {
                    Console.Write($"{new DateTime(1, i + 1, 1).ToString("MMM")} {year}:   $");
                    double monthTotal = 0;
                    foreach (double price in ordersList[i])
                    {
                        monthTotal += price;
                    }
                    Console.WriteLine(monthTotal);
                    total += monthTotal;
                }
                Console.WriteLine("Total:      $" + total);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error parsing order ID: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void ReadExistingCustomersFromCsv(string csvFilePath, List<Customer> customers)
        {
            Dictionary<int, Customer> customerLookup = new Dictionary<int, Customer>();
            Dictionary<int, int> orderIdToMemberIdMap = new Dictionary<int, int>();
            foreach (Customer customer in customers)
            {
                if (!customerLookup.ContainsKey(customer.MemberID))
                {
                    customerLookup.Add(customer.MemberID, customer);
                }
            }
            Dictionary<int, Order> ordersDict = new Dictionary<int, Order>();
            string[] lines = File.ReadAllLines(csvFilePath); //orders.csv
            for (int i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(',');
                if (columns[3] == "") {
                    continue;
                }
                int orderId = Convert.ToInt32(columns[0]);
                int memberId = Convert.ToInt32(columns[1]);
                orderIdToMemberIdMap[orderId] = memberId;

                string option = columns[4];
                int scoops = Convert.ToInt32(columns[5]);
                DateTime timeReceived = DateTime.Parse(columns[2]);
                DateTime timeFufilled = DateTime.Parse(columns[3]);
                List<String> fList = new List<String>() { columns[8], columns[9], columns[10] };
                List<Flavour> flavourList = new List<Flavour>();
                if (fList[0] == fList[1] && fList[1] == fList[2])  //If all the flavours are the same
                {
                    Flavour flavour = new Flavour(fList[0], IsPremium(fList[0]), 3);
                    flavourList.Add(flavour);
                }
                else if (fList[0] == fList[1] && fList[0] != "")
                {
                    Flavour flavour = new Flavour(fList[0], IsPremium(fList[0]), 2);
                    flavourList.Add(flavour);
                    Flavour flavour1 = new Flavour(fList[2], IsPremium(fList[2]), 1);
                    flavourList.Add(flavour1);
                }
                else if (fList[0] == fList[2] && fList[0] != "")
                {
                    Flavour flavour = new Flavour(fList[0], IsPremium(fList[0]), 2);
                    flavourList.Add(flavour);
                    Flavour flavour1 = new Flavour(fList[1], IsPremium(fList[1]), 1);
                    flavourList.Add(flavour1);
                }
                else if (fList[1] == fList[2] && fList[1] != "")
                {
                    Flavour flavour = new Flavour(fList[1], IsPremium(fList[1]), 2);
                    flavourList.Add(flavour);
                    Flavour flavour1 = new Flavour(fList[0], IsPremium(fList[0]), 1);
                    flavourList.Add(flavour1);
                }
                else
                {
                    foreach(String flvr in fList)
                    {
                        if (flvr != "")
                        {
                            Flavour flavour = new Flavour(flvr, IsPremium(flvr), 1);
                            flavourList.Add(flavour);
                        }
                    }
                }
                List<Topping> toppingList = new List<Topping>();
                for (int j = 11; j <= 14; j++)
                {
                    if (columns[j] != "")
                    {
                        Topping topping = new Topping(columns[j]);
                        toppingList.Add(topping);
                    }
                }
                // 3. Handle orders and ice creams
                if (!ordersDict.TryGetValue(orderId, out var order))
                {
                    order = new Order(orderId, timeReceived);
                    order.TimeFulfilled = timeFufilled;
                    ordersDict[orderId] = order;
                }
                IceCream iceCream = null;
                if (option == "Cone")
                {
                    bool dipped = Convert.ToBoolean(columns[6]);
                    iceCream = new Cone("Cone", scoops, flavourList, toppingList, dipped);
                }
                else if (option == "Waffle")
                {
                    string WaffleFlavour = columns[7];
                    iceCream = new Waffle("Waffle", scoops, flavourList, toppingList, WaffleFlavour);
                }
                else
                {
                    iceCream = new Cup("Cup", scoops, flavourList, toppingList);
                }
                order.AddIceCream(iceCream);
            }
            foreach (var orderEntry in ordersDict)
            {
                int orderId = orderEntry.Key;
                Order order = orderEntry.Value;

                if (orderIdToMemberIdMap.TryGetValue(orderId, out int memberId) &&
                    customerLookup.TryGetValue(memberId, out Customer customer))
                {
                    customer.OrderHistory.Add(order);
                }
            }
        }
        static bool IsPremium(string type)
        {
            type = type.ToLower();
            if (type == "ube" || type == "durian" || type == "sea salt")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}