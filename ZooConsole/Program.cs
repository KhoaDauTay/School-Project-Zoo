using System;
using Animals;
using People;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// Contains logic for running the console application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runs the console application.
        /// </summary>
        /// <param name="args">The method arguments for the console application.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Como Zoo!");
            Zoo zoo = Zoo.NewZoo();
            ConsoleHelper.AttachDelegates(zoo);

            bool exit = false;

            string command;

            try
            {
                while (!exit)
                {
                    Console.Write("> ");
                    command = Console.ReadLine();
                    string[] commandWords = command.ToLower().Trim().Split();

                    switch (commandWords[0])
                    {
                        case "exit":
                            exit = true;
                            break;
                        case "restart":
                            zoo = Zoo.NewZoo();
                            ConsoleHelper.AttachDelegates(zoo);
                            Console.WriteLine("The zoo has been restarted.");
                            break;
                        case "help":
                            if (commandWords.Length == 1)
                            {
                                ConsoleHelper.ShowHelp();
                            }
                            else if (commandWords.Length == 2)
                            {
                                ConsoleHelper.ShowHelpDetail(commandWords[1]);
                            }
                            else
                            {
                                Console.WriteLine("Too many parameters were entered. The help command takes 1 or 2 parameters.");
                            }

                            break;
                        case "temp":
                            try
                            {
                                ConsoleHelper.SetTemperature(zoo, commandWords[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter a parameter for temperature.");
                            }

                            break;
                        case "show":
                            try
                            {
                                ConsoleHelper.ProcessShowCommand(zoo, commandWords[1], commandWords[2]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest] [name].");
                            }

                            break;
                        case "remove":
                            try
                            {
                                ConsoleHelper.ProcessRemoveCommand(zoo, commandWords[1], commandWords[2]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest] [name].");
                            }

                            break;
                        case "add":
                            try
                            {
                                ConsoleHelper.ProcessAddCommand(zoo, commandWords[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest].");
                            }

                            break;
                        case "save":
                            try
                            {
                                ConsoleHelper.SaveFile(zoo, commandWords[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please specify the name you would like to save as.");
                            }

                            break;
                        case "load":
                            try
                            {
                                ConsoleHelper.LoadFile(commandWords[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the file name you would like to load.");
                            }

                            break;
                        default:
                            Console.WriteLine("Invalid command entered.");

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}