using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ifunction.JPush
{
    /// <summary>
    /// Class PushMessageTracking.
    /// </summary>
    [DataContract]
    public class PushMessageTracking
    {
        #region Property

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        [DataMember]
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the created UTC stamp.
        /// </summary>
        /// <value>The created UTC stamp.</value>
        [DataMember]
        public DateTime CreatedUtcStamp { get; set; }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.MessageId.GetStringValue();
        }
    }
}
