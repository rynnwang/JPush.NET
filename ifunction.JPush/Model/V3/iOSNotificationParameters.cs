using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ifunction.JPush.V3
{
    /// <summary>
    /// Enum PushType
    /// </summary>
    [DataContract]
    public class iOSNotificationParameters
    {
        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        /// <value>The alert.</value>
        [DataMember]
        [JsonProperty(PropertyName = "alert", NullValueHandling = NullValueHandling.Ignore)]
        public string Alert { get; set; }

        /// <summary>
        /// Gets or sets the sound.
        /// </summary>
        /// <value>The sound.</value>
        [DataMember]
        [JsonProperty(PropertyName = "sound", NullValueHandling = NullValueHandling.Ignore)]
        public string Sound { get; set; }

        /// <summary>
        /// Gets or sets the badge.
        /// </summary>
        /// <value>The badge.</value>
        [DataMember]
        [JsonProperty(PropertyName = "badge", NullValueHandling = NullValueHandling.Ignore)]
        public int? Badge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [silence mode].
        /// </summary>
        /// <value><c>null</c> if [silence mode] contains no value, <c>true</c> if [silence mode]; otherwise, <c>false</c>.</value>
        [DataMember]
        [JsonProperty(PropertyName = "content-available", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SilenceMode { get; set; }

        /// <summary>
        /// Gets or sets the customized values.
        /// </summary>
        /// <value>The customized values.</value>
        [DataMember]
        [JsonProperty(PropertyName = "extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> CustomizedValues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="iOSNotificationParameters" /> class.
        /// </summary>
        public iOSNotificationParameters()
        {
            this.CustomizedValues = new Dictionary<string, string>();
        }
    }
}
