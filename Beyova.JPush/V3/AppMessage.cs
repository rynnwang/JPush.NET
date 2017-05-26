using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Beyova.JPush.V3
{
    /// <summary>
    /// Class AppMessage.
    /// </summary>
    public class AppMessage
    {
        #region Property

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonProperty(PropertyName = "msg_content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        [JsonProperty(PropertyName = "content_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the customized value.
        /// </summary>
        /// <value>The customized value.</value>
        [JsonProperty(PropertyName = "extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> CustomizedValue { get; set; }

        #endregion


    }
}
