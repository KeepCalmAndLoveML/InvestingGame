using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InvestingGame.Engine;
using InvestingGame.GameLogic;

namespace InvestingGame.ConsoleTest
{
    class Program
    {
        public static string Path = @"data.xml";
        public static List<Facility> Facilities;
        public static GameDataManager Manager = new GameDataManager(Path);

        public static void RegisterFacility()
        {
            Console.WriteLine("Now in facility registering mode.");
            Console.WriteLine("Choose a name for your facility (one line):");
            string name = Console.ReadLine();

            Console.WriteLine(@"Now write a description of your facility (multiple lines, finish input with the string ""Break;"" :");
            string description = string.Empty;

            string nextLine;
            while((nextLine = Console.ReadLine()) != "Break;")
                description += nextLine;

            Console.WriteLine("What is the type of your facility return?");
            Console.WriteLine("0 - For multiplicative (Money = Money * rate, rate = 1.5 is the same as + 50%), 1 - For additive (Money = Money + Sum)");
            string type = Console.ReadLine();

            Facility.ReturnItemType returnItemType;
            while(!Facility.ReturnItemFromString(type, out returnItemType))
            {
                Console.WriteLine("Invalid type. Please use 1 or 0");
                type = Console.ReadLine();
            }
            
            Console.WriteLine("Great! Now add boundaries for your facility.");
            Console.WriteLine("Input lines of the format: key value. Finish your input with string 0");

            Dictionary<double, double> boundaries = new Dictionary<double, double>();
            while((nextLine = Console.ReadLine()) != "0")
            {
                var args = nextLine.Split();
                double key, value;

                if(double.TryParse(args[0], out key))
                {
                    if(double.TryParse(args[1], out value))
                        boundaries.Add(key, value);
                    else
                        Console.WriteLine("Invalid value, please use numbers of format 111.111");
                }
                else
                    Console.WriteLine("Invalid key, please use numbers of format 111.111");
            }

            Facility f = new Facility(name, description, boundaries, returnItemType);
            Manager.AddFacility(f);
        }

        public static void EnumerateFacilities()
        {
            Console.WriteLine("======================================================================================");
            foreach(var facility in Facilities)
            { 
                Console.WriteLine($"Facility, name: {facility.Name}");
                Console.WriteLine($"Description: {facility.Description}");
                Console.WriteLine($"Return type: {(int)facility.FacilityReturnType}");
                Console.WriteLine($"Boundaries: {facility.Boundaries.First().Key} {facility.Boundaries.First().Value}...");
                Console.WriteLine("======================================================================================");
            }
        }

        public static void SaveChanges()
        {
            Console.WriteLine("Saving changes...");

            Manager.SaveTo(Path);

            Console.WriteLine("Saved all successfully");

            GameLoader loader = new GameLoader(Path);
            Facilities = loader.GetFacilities();
        }

        static void Main(string[] args)
        {
            Dictionary<string, Action> commands = new Dictionary<string, Action>
            {
                ["/list"] = EnumerateFacilities,
                ["/save"] = SaveChanges,
                ["/add"] = RegisterFacility
            };

            GameLoader loader = new GameLoader(Path);
            Facilities = loader.GetFacilities();

            while (true)
            {
                string command = Console.ReadLine();
                if (commands.ContainsKey(command))
                {
                    commands[command]();
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
        }
    }
}
