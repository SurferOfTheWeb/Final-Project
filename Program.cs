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
    Console.WriteLine("What would you like to do?\n\t1. Display products\n\t2. Add a product\n\t3. Edit a product\n\t4. View product info\n\t\"q\" to quit");
    choice = Console.ReadLine(); Console.Clear();
    logger.Info($"Main menu: option {choice} selected");

    if(choice == "1"){
        
        Console.WriteLine("Which products would you like to see?\n\t1. All products\n\t2. Only active products\n\t3. Only discontinued products"); 
        string productTypeChoice = Console.ReadLine();
        logger.Info($"Product choice: option {choice} selected");

        if (productTypeChoice == "1") displayProducts(true, true); 
        if (productTypeChoice == "2") displayProducts(true, false);
        if (productTypeChoice == "3") displayProducts(false, true);        
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
        displayProducts(true, true);
        Console.WriteLine("Which product would you like to edit?"); int productChoice = Convert.ToInt32(Console.ReadLine());
    }

    if(choice == "4"){
        displayProducts(true, true);
        Console.WriteLine("Which product would you like to expand? > "); int productChoice = Convert.ToInt32(Console.ReadLine());
        var entry = db.Products.First(p => p.ProductId == productChoice); Console.Clear();

        Console.WriteLine("\nProduct Name: " + entry.ProductName);
        Console.WriteLine("\tQuantity Per Unit: " + entry.QuantityPerUnit);
        Console.WriteLine("\tUnit Price: " + entry.UnitPrice);
        Console.WriteLine("\tUnits in Stock: " + entry.UnitsInStock);
        Console.WriteLine("\tUnits on Order: " + entry.UnitsOnOrder);
        Console.WriteLine("\tReorder Level: " + entry.ReorderLevel);
        Console.WriteLine("\tDiscontinued Status: " + entry.Discontinued);

    }

    Console.WriteLine();

} while (choice.ToLower() != "q");

Console.Clear();
logger.Info("Program ended");

void displayProducts(bool allowActive, bool allowDiscontinued){
    var query = db.Products.OrderBy(p => p.ProductId);

    foreach (var item in query){
        
        if(item.Discontinued && allowDiscontinued){
            Console.Write(item.ProductId + ". ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{item.ProductName}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else if (!item.Discontinued && allowActive) {
            Console.Write(item.ProductId + ". "); 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{item.ProductName}");
        }
    }

    Console.Write("\n");
    Console.ForegroundColor = ConsoleColor.White;
}