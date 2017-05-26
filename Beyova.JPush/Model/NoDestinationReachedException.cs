using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Beyova.ExceptionSystem;
using Newtonsoft.Json;

namespace Beyova.JPush
{
    /// <summary>
    /// Class NoDestinationReachedException.
    /// </summary>
    public class NoDestinationReachedException : PushMessageException
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public override PushResponseCode Code
        {
            get
            {
                return PushResponseCode.NoDestinationReached;
            }
            set
            {
                //nothing
            }
        }

        /// <summary>
        /// Pushes the message exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NoDestinationReachedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
