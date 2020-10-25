using System;

namespace HighAvailablityCoding
{
    /// <summary>
    /// Main class.
    /// </summary>
    class MainClass
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            while(true)
            {
                PrintUsage();
                switch (Console.ReadLine())
                {
                    case "0":
                        Console.Clear();
                        PrintUsage();
                        break;
                    case "1":
                        //Prints the nodes or host and its state
                        Console.Clear();
                        HighAvailablityRunner.Instance.PrintCluster();
                        break;
                    case "2":
                        //Adds node or host to the cluster
                        Console.Clear();
                        HighAvailablityRunner.Instance.AddHost();
                        break;
                    case "3":
                        //Adds the file to a random host in the cluster an replicate to alternate host
                        //and updates the dictionary with file and its hosts. 
                        //Later the dictionary is looked up for fetching the hosts where the files are located 
                        Console.Clear();
                        HighAvailablityRunner.Instance.AddFile();
                        break;
                    case "4":
                        //Prints each hosts in the cluster an the files it has in it
                        Console.Clear();
                        HighAvailablityRunner.Instance.PrintAllHostsWithFiles();
                        break;
                    case "5":
                        //Looks up the dictionary to fetch the list of host where the file can be located
                        Console.Clear();
                        HighAvailablityRunner.Instance.FindHostsByFileName();
                        break;
                    case "6":
                        //Finds the host by mane and marks it alive state to false
                        //Scancs the list of file it holds and looks up the dictionary to get the alternate source host
                        //Then get a random host for replication which is alive and dosent contain the file
                        Console.Clear();
                        HighAvailablityRunner.Instance.FindAlternateHost();
                        break;
                    case "7":
                        //Exits the program
                        Console.Clear();
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        PrintUsage();
                        break;
                }
            }

        }

        /// <summary>
        /// Prints the usage.
        /// </summary>
        private static void PrintUsage()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(new String('*',10));
            Console.WriteLine("0. Print Usage. Key in the appropriate number for the operation");
            Console.WriteLine("1. To List the nodes and its status alive or down.");
            Console.WriteLine("2. Add Node/Host to the cluster");
            Console.WriteLine("3. Add File : Note File will be added one of the nodes and replicated to next available node");
            Console.WriteLine("4. List Nodes and the files it contains");
            Console.WriteLine("5. Find all the hosts in the Cluster for given File name");
            Console.WriteLine("6. Host Down! Find Alternate hosts to replicate the file");
            Console.WriteLine("7. Exit"); 
        }
    }
}
