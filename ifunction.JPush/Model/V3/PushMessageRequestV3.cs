using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ifunction.JPush.V3
{
    /// <summary>
    /// Class PushMessageRequest.
    /// <see cref="PushMessageRequest"/> and <see cref="PushMessage"/> work together to send out push request.
    /// See official RESTful API: http://docs.jpush.cn/display/dev/Push+API+v2
    /// </summary>
    [DataContract(Name = "PushMessageRequest")]
    [KnownType(typeof(PushTypeV3))]
    [KnownType(typeof(Audience))]
    [KnownType(typeof(PushPlatform))]
    public class PushMessageRequestV3
    {
        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>The notification.</value>
        [DataMember]
        public Notification Notification { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>The audience.</value>
        [DataMember]
        public Audience Audience { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataMember]
        public AppMessage AppMessage { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>The platform.</value>
        [DataMember]
        public PushPlatform Platform { get; set; }

        /// <summary>
        /// Gets or sets the life time.
        /// Unit: second.
        /// Max: 864000 Seconds (10 days).
        /// Default: 86400 Seconds (1 days).
        /// If set as 0, means no life time. Only the online destination would be get message at the time when push message delivers JPush.
        /// </summary>
        /// <value>The life time.</value>
        [DataMember]
        public int? LifeTime { get; set; }

        /// <summary>
        /// Gets or sets the override message unique identifier.
        /// </summary>
        /// <value>The override message unique identifier.</value>
        [DataMember]
        public string OverrideMessageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is test.
        /// For iOS only.
        /// Default: false.
        /// </summary>
        /// <value><c>true</c> if this instance is test; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsTestEnvironment { get; set; }
    }
}
