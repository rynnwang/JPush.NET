using System;
using System.Runtime.Serialization;

namespace ifunction.JPush
{
    /// <summary>
    /// Enum PushResponseCode
    /// </summary>
    [DataContract]
    public enum PushResponseCode
    {
        /// <summary>
        /// Value indicating succeed
        /// </summary>
        [EnumMember]
        Succeed = 0,
        /// <summary>
        /// Value indicating service error
        /// </summary>
        [EnumMember]
        ServiceError = 10,
        /// <summary>
        /// Value indicating post only
        /// </summary>
        [EnumMember]
        PostOnly = 1001,
        /// <summary>
        /// Value indicating missing required parameter
        /// </summary>
        [EnumMember]
        MissingRequiredParameter = 1002,
        /// <summary>
        /// Value indicating invalid parameter
        /// </summary>
        [EnumMember]
        InvalidParameter = 1003,
        /// <summary>
        /// Value indicating failed verification code
        /// </summary>
        [EnumMember]
        FailedVerificationCode = 1004,
        /// <summary>
        /// Value indicating body too large
        /// </summary>
        [EnumMember]
        BodyTooLarge = 1005,
        /// <summary>
        /// Value indicating invalid user or password
        /// </summary>
        [EnumMember]
        InvalidUserOrPassword = 1006,
        /// <summary>
        /// Value indicating invalid receiver value
        /// </summary>
        [EnumMember]
        InvalidReceiverValue = 1007,
        /// <summary>
        /// Value indicating invalid application key
        /// </summary>
        [EnumMember]
        InvalidAppKey = 1008,
        /// <summary>
        /// Value indicating invalid message content
        /// </summary>
        [EnumMember]
        [Obsolete("No more used in JPush V3.")]
        InvalidMessageContent = 1010,
        /// <summary>
        /// Value indicating no destination reached
        /// </summary>
        [EnumMember]
        NoDestinationReached = 1011,
        /// <summary>
        /// Value indicating customized message not support
        /// </summary>
        [EnumMember]
        [Obsolete("No more used in JPush V3.")]
        CustomizedMessageNotSupport = 1012,
        /// <summary>
        /// Value indicating invalid content type
        /// </summary>
        [EnumMember]
        [Obsolete("No more used in JPush V3.")]
        InvalidContentType = 1013,
        /// <summary>
        /// Value indicating HTTP is not allowed. Please turn to use HTTPS.
        /// </summary>
        [EnumMember]
        HttpIsNotAllowed = 1020
    }
}
