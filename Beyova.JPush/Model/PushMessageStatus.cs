using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Beyova.JPush
{
    /// <summary>
    /// Class PushMessageStatus.
    /// </summary>
    public class PushMessageStatus
    {
        /// <summary>
        /// Gets or sets the message unique identifier.
        /// This value is decided by JPush Service.
        /// </summary>
        /// <value>The message unique identifier.</value>
        [JsonProperty(PropertyName = "msg_id")]
        public int? MessageId { get; set; }

        /// <summary>
        /// Gets or sets the android delivered count.
        /// </summary>
        /// <value>The android delivered count.</value>
        [JsonProperty(PropertyName = "android_received")]
        public int? AndroidDeliveredCount { get; set; }

        /// <summary>
        /// Gets or sets the apple push notification delivered count.
        /// </summary>
        /// <value>The apple push notification delivered count.</value>
        [JsonProperty(PropertyName = "ios_apns_sent")]
        public int? ApplePushNotificationDeliveredCount { get; set; }
    }
}
