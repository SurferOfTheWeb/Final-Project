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
        /*
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        */

        
    }
    if(choice == "3"){


    }

    Console.WriteLine();

} while (choice.ToLower() != "q");


logger.Info("Program ended");