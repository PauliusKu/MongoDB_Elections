using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Election_System_MongoDB
{
    class Menu
    {
        public void StartMenu()
        {
            string input;

            Functions func = new Functions();

            Console.WriteLine("Welcome to Election System! Choose a function");
            Menu:
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("You chosen Regsiter Candidate");
                        func.RegisterCandidate();
                        break;
                    case "2":
                        Console.WriteLine("You chosen Register Voter");
                        break;
                    case "3":
                        Console.WriteLine("You chosen Create Constituency");
                        break;
                    case "4":
                        Console.WriteLine("You chosen Vote");
                        break;
                    case "5":
                        Console.WriteLine("You chosen Calculate Result");
                        break;
                    case "6":
                        Console.WriteLine("You chosen Init Database");
                        break;
                    case "7":
                        Console.WriteLine("You chosen Exit");
                        goto ExitMenu;
                    default:
                        Console.WriteLine("Incorrect input");
                        goto Menu;
                }
                goto Menu;
            ExitMenu:
            Console.WriteLine("Thank You for using our great program!");
            Console.ReadKey();
        }
    }
}
