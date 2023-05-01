using NLog;
using System.Linq;
using EF_SQL_Practice.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

string path = Directory.GetCurrentDirectory() + "\\nlog.config";
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");
var db = new NWConsole_23_gplContext();
string choice;

do
{
    Console.WriteLine("What would you like to do?\n\t1. Display products\n\t2. Add a product\n\t3. Edit a record\n\t\"q\" to quit");
    choice = Console.ReadLine(); Console.Clear();
    logger.Info($"Main menu: option {choice} selected");

    if(choice == "1")
    {
        var query = db.Products.OrderBy(p => p.ProductName);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{query.Count()} records returned");
        Console.WriteLine("Which products would you like to see?\n\t1. All products\n\t2. Only active products\n\t3. Only discontinued products"); 
        string productTypeChoice = Console.ReadLine();
        logger.Info($"Product choice: option {choice} selected");

        foreach (var item in query)
        {
            if(item.Discontinued && (productTypeChoice == "1" || productTypeChoice == "3")){
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{item.ProductName}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (productTypeChoice == "1" || productTypeChoice == "2"){
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{item.ProductName}");
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
        
    }

    if(choice == "2"){

        var toBeAdded = new Product();

        Console.Write("Enter product name: "); toBeAdded.ProductName = Console.ReadLine();
        Console.Write("Enter quantity per unit: "); toBeAdded.QuantityPerUnit = Console.ReadLine();
        Console.Write("Enter price per unit: "); toBeAdded.UnitPrice = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter units in stock: "); toBeAdded.UnitsInStock = Convert.ToInt16(Console.ReadLine());
        Console.Write("Enter units on order: "); toBeAdded.UnitsOnOrder = Convert.ToInt16(Console.ReadLine());
        Console.Write("Enter reorder level: "); toBeAdded.ReorderLevel = Convert.ToInt16(Console.ReadLine());
        Console.Write("Is the product discontinued? (true or false)"); toBeAdded.Discontinued = Convert.ToBoolean( Console.ReadLine().ToLower());
        logger.Info($"Product addition: product successfully created");
        
        db.addProduct(toBeAdded);
        logger.Info($"Product addition: product successfully added to database");
        
    }
    
    if(choice == "3"){

    }

    Console.WriteLine();

} while (choice.ToLower() != "q");


logger.Info("Program ended");