using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ifunction.JPush.V3
{
    /// <summary>
    /// Enum PushType
    /// </summary>
    [DataContract]
    public class AndroidNotificationParameters
    {
        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        /// <value>The alert.</value>
        [DataMember]
        [JsonProperty(PropertyName = "alert")]
        public string Alert { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [DataMember]
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the builder identifier.
        /// </summary>
        /// <value>The builder identifier.</value>
        [DataMember]
        [JsonProperty(PropertyName = "builder_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? BuilderId { get; set; }

        /// <summary>
        /// Gets or sets the customized values.
        /// </summary>
        /// <value>The customized values.</value>
        [DataMember]
        [JsonProperty(PropertyName = "extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> CustomizedValues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AndroidNotificationParameters" /> class.
        /// </summary>
        public AndroidNotificationParameters()
        {
            this.CustomizedValues = new Dictionary<string, string>();
        }
    }
}
