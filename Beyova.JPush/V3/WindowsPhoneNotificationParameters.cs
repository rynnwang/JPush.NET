using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Beyova.JPush.V3
{
    /// <summary>
    /// Class WindowsPhoneNotificationParameters.
    /// </summary>
    public class WindowsPhoneNotificationParameters
    {
        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        /// <value>The alert.</value>
        [JsonProperty(PropertyName = "alert")]
        public string Alert { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the open page.
        /// </summary>
        /// <value>The open page.</value>
        [JsonProperty(PropertyName = "_open_page", NullValueHandling = NullValueHandling.Ignore)]
        public string OpenPage { get; set; }

        /// <summary>
        /// Gets or sets the customized values.
        /// </summary>
        /// <value>The customized values.</value>
        [JsonProperty(PropertyName = "extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> CustomizedValues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPhoneNotificationParameters" /> class.
        /// </summary>
        public WindowsPhoneNotificationParameters()
        {
            this.CustomizedValues = new Dictionary<string, string>();
        }
    }
}
