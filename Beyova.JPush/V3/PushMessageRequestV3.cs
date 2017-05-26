using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Beyova.JPush.V3
{
    /// <summary>
    /// Class PushMessageRequestV3.
    /// </summary>
    public class PushMessageRequestV3
    {
        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>The notification.</value>
        public Notification Notification { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>The audience.</value>
        public Audience Audience { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public AppMessage AppMessage { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>The platform.</value>
        public PushPlatform Platform { get; set; }

        /// <summary>
        /// Gets or sets the life time.
        /// Unit: second.
        /// Max: 864000 Seconds (10 days).
        /// Default: 86400 Seconds (1 days).
        /// If set as 0, means no life time. Only the online destination would be get message at the time when push message delivers JPush.
        /// </summary>
        /// <value>The life time.</value>
        public int? LifeTime { get; set; }

        /// <summary>
        /// Gets or sets the override message unique identifier.
        /// </summary>
        /// <value>The override message unique identifier.</value>
        public string OverrideMessageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is test.
        /// For iOS only.
        /// Default: false.
        /// </summary>
        /// <value><c>true</c> if this instance is test; otherwise, <c>false</c>.</value>
        public bool? IsTestEnvironment { get; set; }
    }
}
