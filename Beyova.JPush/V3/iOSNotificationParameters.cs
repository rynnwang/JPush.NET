using System.Collections.Generic;
using Newtonsoft.Json;

namespace Beyova.JPush.V3
{
    /// <summary>
    /// Class iOSNotificationParameters.
    /// </summary>
    public class iOSNotificationParameters
    {
        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        /// <value>The alert.</value>
        [JsonProperty(PropertyName = "alert", NullValueHandling = NullValueHandling.Ignore)]
        public string Alert { get; set; }

        /// <summary>
        /// Gets or sets the sound.
        /// </summary>
        /// <value>The sound.</value>
        [JsonProperty(PropertyName = "sound", NullValueHandling = NullValueHandling.Ignore)]
        public string Sound { get; set; }

        /// <summary>
        /// Gets or sets the badge.
        /// </summary>
        /// <value>The badge.</value>
        [JsonProperty(PropertyName = "badge", NullValueHandling = NullValueHandling.Ignore)]
        public int? Badge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [silence mode].
        /// </summary>
        /// <value><c>null</c> if [silence mode] contains no value, <c>true</c> if [silence mode]; otherwise, <c>false</c>.</value>
        [JsonProperty(PropertyName = "content-available", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SilenceMode { get; set; }

        /// <summary>
        /// Gets or sets the customized values.
        /// </summary>
        /// <value>The customized values.</value>
        [JsonProperty(PropertyName = "extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> CustomizedValues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="iOSNotificationParameters" /> class.
        /// </summary>
        public iOSNotificationParameters()
        {
            this.CustomizedValues = new Dictionary<string, object>();
        }
    }
}
