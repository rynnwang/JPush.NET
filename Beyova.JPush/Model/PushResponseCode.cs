using System;
using System.Runtime.Serialization;

namespace Beyova.JPush
{
    /// <summary>
    /// Enum PushResponseCode
    /// </summary>
    public enum PushResponseCode
    {
        /// <summary>
        /// Value indicating succeed
        /// </summary>
        Succeed = 0,
        /// <summary>
        /// Value indicating service error
        /// </summary>
        ServiceError = 10,
        /// <summary>
        /// Value indicating post only
        /// </summary>
        PostOnly = 1001,
        /// <summary>
        /// Value indicating missing required parameter
        /// </summary>
        MissingRequiredParameter = 1002,
        /// <summary>
        /// Value indicating invalid parameter
        /// </summary>
        InvalidParameter = 1003,
        /// <summary>
        /// Value indicating failed verification code
        /// </summary>
        FailedVerificationCode = 1004,
        /// <summary>
        /// Value indicating body too large
        /// </summary>
        BodyTooLarge = 1005,
        /// <summary>
        /// Value indicating invalid user or password
        /// </summary>
        InvalidUserOrPassword = 1006,
        /// <summary>
        /// Value indicating invalid receiver value
        /// </summary>
        InvalidReceiverValue = 1007,
        /// <summary>
        /// Value indicating invalid application key
        /// </summary>
        InvalidAppKey = 1008,
        /// <summary>
        /// Value indicating invalid message content
        /// </summary>
        [Obsolete("No more used in JPush V3.")]
        InvalidMessageContent = 1010,
        /// <summary>
        /// Value indicating no destination reached
        /// </summary>
        NoDestinationReached = 1011,
        /// <summary>
        /// Value indicating customized message not support
        /// </summary>
        [Obsolete("No more used in JPush V3.")]
        CustomizedMessageNotSupport = 1012,
        /// <summary>
        /// Value indicating invalid content type
        /// </summary>
        [Obsolete("No more used in JPush V3.")]
        InvalidContentType = 1013,
        /// <summary>
        /// Value indicating HTTP is not allowed. Please turn to use HTTPS.
        /// </summary>
        HttpIsNotAllowed = 1020
    }
}
