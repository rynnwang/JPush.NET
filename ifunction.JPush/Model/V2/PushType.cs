using System;
using System.Runtime.Serialization;

namespace ifunction.JPush
{
    /// <summary>
    /// Enum PushType
    /// </summary>
    [DataContract]
    [Obsolete("This enum is for v2 only. In v3, please use PushTypeV3.")]
    public enum PushType
    {
        /// <summary>
        /// The unknown
        /// </summary>
        [EnumMember]
        Unknown = 0,
        /// <summary>
        /// The by tag
        /// </summary>
        [EnumMember]
        ByTag = 2,
        /// <summary>
        /// The by alias
        /// </summary>
        [EnumMember]
        ByAlias = 3,
        /// <summary>
        /// The broadcast
        /// </summary>
        [EnumMember]
        Broadcast = 4,
        /// <summary>
        /// The by registration unique identifier
        /// </summary>
        [EnumMember]
        ByRegistrationId = 5
    }
}
