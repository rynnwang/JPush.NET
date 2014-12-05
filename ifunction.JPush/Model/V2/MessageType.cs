using System;
using System.Runtime.Serialization;

namespace ifunction.JPush
{
    /// <summary>
    /// Enum MessageType
    /// </summary>
    [DataContract]
    [Obsolete("This enum is for v2 only. In v3, please use PushTypeV3.")]
    public enum MessageType
    {
        /// <summary>
        /// The none
        /// </summary>
        [EnumMember]
        None = 0,
        /// <summary>
        /// The notification
        /// </summary>
        [EnumMember]
        Notification = 1,
        /// <summary>
        /// The customized message.
        /// For Android only.
        /// </summary>
        [EnumMember]
        CustomizedMessage = 2
    }
}
