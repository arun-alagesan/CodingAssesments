using System;
using System.Collections.Generic;

namespace HighAvailablityCoding.Models
{
    /// <summary>
    /// Host. DTO Class to hold the Host and mapped files list contined in it
    /// </summary>
    public class Host
    {
        HashSet<string> files = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HighAvailablityCoding.Models.Host"/> class.
        /// </summary>
        public Host()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HighAvailablityCoding.Models.Host"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        public Host(string name)
        {
            this.Name = name;
            this.files = new HashSet<string>();
            this.IsAlive = true;
        }
        /// <summary>
        /// Gets or sets the Name of the Host.
        /// </summary>
        /// <value>Name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets the files. Its a Hashset Type to avoid duplicate files
        /// </summary>
        /// <value>The files.</value>
        public HashSet<string> Files { get { return files; } }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:HighAvailablityCoding.Models.Host"/> is alive.
        /// </summary>
        /// <value><c>true</c> if is alive; otherwise, <c>false</c>.</value>
        public bool IsAlive { get; set; }
    }
}
