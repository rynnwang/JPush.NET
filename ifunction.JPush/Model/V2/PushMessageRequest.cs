using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ifunction.JPush
{
    /// <summary>
    /// Class PushMessageRequest.
    /// <see cref="PushMessageRequest"/> and <see cref="PushMessage"/> work together to send out push request.
    /// See official RESTful API: http://docs.jpush.cn/display/dev/Push+API+v2
    /// </summary>
    [DataContract]
    [KnownType(typeof(PushMessage))]
    [KnownType(typeof(PushType))]
    [KnownType(typeof(MessageType))]
    [KnownType(typeof(PushPlatform))]
    [Obsolete("Please use class PushMessageRequestV3 in JPush v3.")]
    public class PushMessageRequest
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataMember]
        public PushMessage Message { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        [Obsolete("Please use Audience of PushMessageRequestV3 instead in JPush v3.")]
        public PushType PushType { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        [DataMember]
        [Obsolete("This property is not supported in JPush v3.")]
        public MessageType MessageType { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>The platform.</value>
        [DataMember]
        public PushPlatform Platform { get; set; }

        /// <summary>
        /// Gets or sets the receiver value.
        /// </summary>
        /// <value>The receiver value.</value>
        [DataMember]
        [Obsolete("Please use Audience of PushMessageRequestV3 instead in JPush v3.")]
        public string ReceiverValue { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        [Obsolete("This property is not supported in JPush v3.")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the life time.
        /// Unit: second.
        /// Max: 864000 Seconds (10 days).
        /// Default: 86400 Seconds (1 days).      
        /// </summary>
        /// <value>The life time.</value>
        [DataMember]
        public int LifeTime { get; set; }

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
