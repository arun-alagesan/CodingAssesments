using System;
using System.Collections.Generic;
using System.Diagnostics;
using HighAvailablityCoding.Models;

namespace HighAvailablityCoding
{
    /// <summary>
    /// Singleton Cluster class that exits only one instance of it.
    /// </summary>
    public class Cluster
    {
        private static Cluster cluster = null;
        private List<Host> hosts = null;
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Cluster Instance
        {
            get
            {
                lock (typeof(Cluster)){
                    if (null == cluster)
                    {
                        cluster = new Cluster();
                    }
                }
                return cluster;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HighAvailablityCoding.Cluster"/> class.
        /// </summary>
        private Cluster()
        {
            hosts = new List<Host>();
        }
        /// <summary>
        /// Gets the hosts.
        /// </summary>
        /// <value>The hosts.</value>
        public List<Host> Hosts
        {
            get{
                return hosts;
            }
        }
        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <returns>The host.</returns>
        /// <param name="hostName">Host name.</param>
        public Host GetHost(string hostName)
        {
            return this.hosts.Find(host => host.Name == hostName);
        }
        /// <summary>
        /// Adds the host.
        /// </summary>
        /// <returns><c>true</c>, if host was added, <c>false</c> otherwise.</returns>
        /// <param name="hostName">Host name.</param>
        public bool AddHost(string hostName)
        {
            try
            {
                this.hosts.Add(new Host(hostName));
                return true;
            }
            catch(Exception oEx)
            {
                DebugPrintException(oEx);
                return false;
            }
        }
        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <returns><c>true</c>, if file was added, <c>false</c> otherwise.</returns>
        /// <param name="fileName">File name.</param>
        public bool AddFile(string fileName)
        {
            try
            {
                //Pick a random host from the cluster to add the file and replicate
                Host host = RandomHostFromCluster();
                if (AddFile(host, fileName))
                {
                    //Pick a host for replication
                    Host hostToReplicate = RandomHostForReplication(fileName);
                    return AddFile(hostToReplicate, fileName);
                }
                else
                {
                    return false;
                }
            }
            catch(Exception oEx)
            {
                DebugPrintException(oEx);
                return false;
            }
        }
        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <returns><c>true</c>, if file was added, <c>false</c> otherwise.</returns>
        /// <param name="host">Host.</param>
        /// <param name="fileName">File name.</param>
        private bool AddFile(Host host, string fileName)
        {
            try
            {
                host.Files.Add(fileName);
                Index.Instance.AddtoDictionary(fileName, host.Name);
                return true;
            }
            catch(Exception oEx)
            {
                DebugPrintException(oEx);
                return false;
            }
        }
        /// <summary>
        /// Randoms the host for replication.
        /// </summary>
        /// <returns>The host for replication.</returns>
        /// <param name="fileName">File name.</param>
        public Host RandomHostForReplication(string fileName)
        {
            List<Host> aliveHosts = this.hosts.FindAll(host => host.IsAlive);
            Host hostToReplicate = null;
            do
            {
                hostToReplicate = aliveHosts[new Random().Next(aliveHosts.Count)];

            } while (hostToReplicate.Files.Contains(fileName));
            return hostToReplicate;
        }
        /// <summary>
        /// Randoms the host from cluster.
        /// </summary>
        /// <returns>The host from cluster.</returns>
        private Host RandomHostFromCluster()
        {
            int hostCount = this.hosts.Count;
            Host randomhost = null;
            do
            {
                randomhost = this.hosts[new Random().Next(hostCount)];
            } while (!randomhost.IsAlive);
            return randomhost;
        }
        /// <summary>
        /// Debugs the print exception.
        /// </summary>
        /// <param name="oEx">O ex.</param>
        private void DebugPrintException(Exception oEx)
        {
            Debug.Print("Error Occured while adding the Host");
            Debug.Print("Message : {0}", oEx.Message);
            Debug.Print("Stack : {0}", oEx.StackTrace);
        }
    }
}
