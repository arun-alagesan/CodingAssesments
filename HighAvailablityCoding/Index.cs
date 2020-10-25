using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace HighAvailablityCoding
{
    /// <summary>
    /// Index. Singleton class
    /// </summary>
    public class Index
    {
        private static Index index = null;
        Dictionary<string, List<string>> fileDictionary = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HighAvailablityCoding.Index"/> class.
        /// </summary>
        private Index()
        {
            fileDictionary = new Dictionary<string, List<string>>();
        }
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Index Instance
        {
            get
            {
                lock (typeof(Index))
                {
                    if (null == index)
                    {
                        index = new Index();
                    }
                }
                return index;
            }
        }
        /// <summary>
        /// Looks the name of the up file by.
        /// </summary>
        /// <returns>The up file by name.</returns>
        /// <param name="filename">Filename.</param>
        public List<string> LookUpFileByName(string filename)
        {
            List<string> hosts = null;
            if (fileDictionary.ContainsKey(filename))
            {
                fileDictionary.TryGetValue(filename, out hosts);
            }
            return hosts;
        }
        /// <summary>
        /// Addtos the dictionary.
        /// </summary>
        /// <returns><c>true</c>, if dictionary was addtoed, <c>false</c> otherwise.</returns>
        /// <param name="fileName">File name.</param>
        /// <param name="hostName">Host name.</param>
        public bool AddtoDictionary(string fileName, string hostName)
        {
            try
            {
                if (fileDictionary.ContainsKey(fileName))
                {
                    fileDictionary[fileName].Add(hostName);
                }
                else
                {
                    fileDictionary.Add(fileName,new List<string>() { hostName });
                }
                return true;
            }
            catch(Exception oEx)
            {
                Debug.Print("Exception Occured while indexing the File to HostMapping");
                Debug.Print("Exception : {0}", oEx.Message);
                throw;
            }
        }
    }
}
