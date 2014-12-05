using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ifunction.JPush.V3
{
    /// <summary>
    /// Enum PushType
    /// </summary>
    [DataContract]
    public class Notification
    {
        /// <summary>
        /// Gets or sets the android notification.
        /// </summary>
        /// <value>The android notification.</value>
        [DataMember]
        [JsonProperty(PropertyName = "android", NullValueHandling = NullValueHandling.Ignore)]
        public AndroidNotificationParameters AndroidNotification { get; set; }

        /// <summary>
        /// Gets or sets the i os notification.
        /// </summary>
        /// <value>The i os notification.</value>
        [DataMember]
        [JsonProperty(PropertyName = "ios", NullValueHandling = NullValueHandling.Ignore)]
        public iOSNotificationParameters iOSNotification { get; set; }

        /// <summary>
        /// Gets or sets the windows phone notification.
        /// </summary>
        /// <value>The windows phone notification.</value>
        [DataMember]
        [JsonProperty(PropertyName = "winphone", NullValueHandling = NullValueHandling.Ignore)]
        public WindowsPhoneNotificationParameters WindowsPhoneNotification { get; set; }

    }
}
