using System;
using System.Collections.Generic;
using System.Text;
using HighAvailablityCoding.Models;

namespace HighAvailablityCoding
{
    /// <summary>
    /// High availablity runner.
    /// </summary>
    public class HighAvailablityRunner
    {
        private static HighAvailablityRunner highAvailablityRunner = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HighAvailablityCoding.HighAvailablityRunner"/> class.
        /// </summary>
        private HighAvailablityRunner()
        {

        }
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static HighAvailablityRunner Instance
        {
            get
            {
                lock (typeof(HighAvailablityRunner))
                {
                    if (null == highAvailablityRunner)
                    {
                        highAvailablityRunner = new HighAvailablityRunner();
                    }
                }
                return highAvailablityRunner;
            }
        }
        /// <summary>
        /// Prints the cluster.
        /// </summary>
        public void PrintCluster()
        {
            if (Cluster.Instance.Hosts.Count == 0)
            {
                Console.WriteLine("Cluster is Empty. Add host/node");
                return;
            }
            Cluster.Instance.Hosts.ForEach(host =>
            {
                Console.WriteLine("Host : {0}, Alive : {1}", host.Name, host.IsAlive);
            });
        }

        /// <summary>
        /// Adds the host.
        /// </summary>
        public void AddHost()
        {
            Console.WriteLine("Enter the Host name");
            string hostName = Console.ReadLine();
            Cluster.Instance.AddHost(hostName);
        }

        /// <summary>
        /// Adds the file.
        /// </summary>
        public void AddFile()
        {
            Console.WriteLine("Enter the File name");
            string fileName = Console.ReadLine();
            Cluster.Instance.AddFile(fileName);
        }

        /// <summary>
        /// Prints all hosts with files.
        /// </summary>
        public void PrintAllHostsWithFiles()
        {
            Cluster.Instance.Hosts.ForEach(host =>
            {
                Console.WriteLine("Host : {0}, Alive : {1}, : Files : {2}", host.Name, host.IsAlive, host.Files.Count);
                foreach(string file in host.Files)
                {
                    Console.Write("{0},",file);
                }
                Console.WriteLine();
            });
        }
        /// <summary>
        /// Finds the name of the hosts by file.
        /// </summary>
        public void FindHostsByFileName()
        {
            Console.WriteLine("Enter the file Name");
            string fileName = Console.ReadLine();
            List<String> hosts = LookUpHostsByFileName(fileName);
            Console.WriteLine ("File Name : {0}", fileName);
            hosts.ForEach(host => Console.WriteLine(host));
        }

        /// <summary>
        /// Finds the alternate host.
        /// </summary>
        public void FindAlternateHost()
        {
            Console.WriteLine("Enter the Host which is down");
            string deadHostName = Console.ReadLine();

            Host host = GetHost(deadHostName);
            host.IsAlive = false;

            foreach(string fileName in host.Files)
            {
                string alternateSourceHost = "";
                Host alternateDestinationHost = null;
                List<string> fileHosts= Index.Instance.LookUpFileByName(fileName);
                alternateSourceHost = fileHosts.Find(hostName => hostName != deadHostName);
                alternateDestinationHost = Cluster.Instance.RandomHostForReplication(fileName);
                Console.WriteLine("File : {0}, Alternate Source Host : {1}, Alternate Replication Host : {2}", fileName, alternateSourceHost, alternateDestinationHost.Name);
            }

        }

        /// <summary>
        /// Looks the name of the up hosts by file.
        /// </summary>
        /// <returns>The up hosts by file name.</returns>
        /// <param name="fileName">File name.</param>
        private List <string> LookUpHostsByFileName(string fileName)
        {
            return Index.Instance.LookUpFileByName(fileName);
        }
        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <returns>The host.</returns>
        /// <param name="hostName">Host name.</param>
        private Host GetHost(string hostName)
        {
            return Cluster.Instance.GetHost(hostName);
        }
    }
}
