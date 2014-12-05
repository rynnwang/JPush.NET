using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ifunction.JPush
{
    /// <summary>
    /// Class PushMessageStatus.
    /// </summary>
    [DataContract]
    public class PushMessageStatus
    {
        /// <summary>
        /// Gets or sets the message unique identifier.
        /// This value is decided by JPush Service.
        /// </summary>
        /// <value>The message unique identifier.</value>
        [DataMember(Name = "msg_id")]
        public int? MessageId { get; set; }

        /// <summary>
        /// Gets or sets the android delivered count.
        /// </summary>
        /// <value>The android delivered count.</value>
        [DataMember(Name = "android_received")]
        public int? AndroidDeliveredCount { get; set; }

        /// <summary>
        /// Gets or sets the apple push notification delivered count.
        /// </summary>
        /// <value>The apple push notification delivered count.</value>
        [DataMember(Name = "ios_apns_sent")]
        public int? ApplePushNotificationDeliveredCount { get; set; }
    }
}
