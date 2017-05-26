using System;
using System.Runtime.Serialization;

namespace Beyova.JPush
{
    /// <summary>
    /// Enum PushType
    /// </summary>
    [Obsolete("This enum is for v2 only. In v3, please use PushTypeV3.")]
    public enum PushType
    {
        /// <summary>
        /// The unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The by tag
        /// </summary>
        ByTag = 2,
        /// <summary>
        /// The by alias
        /// </summary>
        ByAlias = 3,
        /// <summary>
        /// The broadcast
        /// </summary>
        Broadcast = 4,
        /// <summary>
        /// The by registration unique identifier
        /// </summary>
        ByRegistrationId = 5
    }
}
