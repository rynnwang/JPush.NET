using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Beyova.JPush.V3
{
    /// <summary>
    /// Class ConnectionString.
    /// </summary>
    public class ConnectionString
    {
        /// <summary>
        /// Gets or sets the application key.
        /// </summary>
        /// <value>The application key.</value>
        public string AppKey
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the master secret.
        /// </summary>
        /// <value>The master secret.</value>
        public string MasterSecret
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is test environment.
        /// </summary>
        /// <value><c>null</c> if [is test environment] contains no value, <c>true</c> if [is test environment]; otherwise, <c>false</c>.</value>
        public bool? IsTestEnvironment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionString" /> class.
        /// </summary>
        /// <param name="appKey">The application key.</param>
        /// <param name="masterSecret">The master secret.</param>
        /// <param name="isTestEnvironment">if set to <c>true</c> [is test environment].</param>
        public ConnectionString(string appKey, string masterSecret, bool? isTestEnvironment = null)
        {
            this.AppKey = appKey;
            this.MasterSecret = masterSecret;
        }
    }
}
