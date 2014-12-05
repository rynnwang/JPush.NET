using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ifunction.JPush.V3
{
    /// <summary>
    /// Enum PushType
    /// </summary>
    [DataContract(Name = "PushType")]
    public enum PushTypeV3
    {
        /// <summary>
        /// The value indicating it is none
        /// </summary>
        [EnumMember]
        None = 0,
        /// <summary>
        /// The value indicating it is broadcast
        /// </summary>
        [EnumMember]
        Broadcast = 1,
        /// <summary>
        /// The value indicating it is by tag in OR operation.
        /// </summary>
        [EnumMember]        
        ByTagWithinOr = 2,
        /// <summary>
        /// The value indicating it is by tag in AND operation.
        /// </summary>
        [EnumMember]
        ByTagWithinAnd = 4,
        /// <summary>
        /// The value indicating it is by alias
        /// </summary>
        [EnumMember]
        ByAlias = 8,
        /// <summary>
        /// The value indicating it is by registration unique identifier
        /// </summary>
        [EnumMember]
        ByRegistrationId = 0x10
    }
}
