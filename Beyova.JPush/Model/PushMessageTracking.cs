using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Beyova.JPush
{
    /// <summary>
    /// Class PushMessageTracking.
    /// </summary>
    public class PushMessageTracking
    {
        #region Property

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the created UTC stamp.
        /// </summary>
        /// <value>The created UTC stamp.</value>
        public DateTime CreatedUtcStamp { get; set; }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.MessageId.SafeToString();
        }
    }
}
