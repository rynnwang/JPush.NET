using System;
using System.Runtime.Serialization;

namespace Beyova.JPush
{
    /// <summary>
    /// Enum MessageType
    /// </summary>
    [Obsolete("This enum is for v2 only. In v3, please use PushTypeV3.")]
    public enum MessageType
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,
        /// <summary>
        /// The notification
        /// </summary>
        Notification = 1,
        /// <summary>
        /// The customized message.
        /// For Android only.
        /// </summary>
        CustomizedMessage = 2
    }
}
