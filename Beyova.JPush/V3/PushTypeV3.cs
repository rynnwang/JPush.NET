using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Beyova.JPush.V3
{
    /// <summary>
    /// Enum PushTypeV3
    /// </summary>
    public enum PushTypeV3
    {
        /// <summary>
        /// The value indicating it is none
        /// </summary>
        None = 0,
        /// <summary>
        /// The value indicating it is broadcast
        /// </summary>
        Broadcast = 1,
        /// <summary>
        /// The value indicating it is by tag in OR operation.
        /// </summary>    
        ByTagWithinOr = 2,
        /// <summary>
        /// The value indicating it is by tag in AND operation.
        /// </summary>
        ByTagWithinAnd = 4,
        /// <summary>
        /// The value indicating it is by alias
        /// </summary>
        ByAlias = 8,
        /// <summary>
        /// The value indicating it is by registration unique identifier
        /// </summary>
        ByRegistrationId = 0x10
    }
}
