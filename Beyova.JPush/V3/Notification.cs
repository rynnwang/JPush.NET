using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Beyova.JPush.V3
{
    /// <summary>
    /// Class Notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets or sets the android notification.
        /// </summary>
        /// <value>The android notification.</value>
        [JsonProperty(PropertyName = "android", NullValueHandling = NullValueHandling.Ignore)]
        public AndroidNotificationParameters AndroidNotification { get; set; }

        /// <summary>
        /// Gets or sets the i os notification.
        /// </summary>
        /// <value>The i os notification.</value>
        [JsonProperty(PropertyName = "ios", NullValueHandling = NullValueHandling.Ignore)]
        public iOSNotificationParameters iOSNotification { get; set; }

        /// <summary>
        /// Gets or sets the windows phone notification.
        /// </summary>
        /// <value>The windows phone notification.</value>
        [JsonProperty(PropertyName = "winphone", NullValueHandling = NullValueHandling.Ignore)]
        public WindowsPhoneNotificationParameters WindowsPhoneNotification { get; set; }

    }
}
