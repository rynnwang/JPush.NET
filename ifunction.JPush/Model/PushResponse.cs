using System.Runtime.Serialization;


namespace ifunction.JPush
{
    /// <summary>
    /// Class PushResponse.
    /// </summary>
    [DataContract]
    [KnownType(typeof(PushResponseCode))]
    public class PushResponse
    {
        /// <summary>
        /// Gets or sets the send identity.
        /// </summary>
        /// <value>The send identity.</value>
        [DataMember]
        public string SendIdentity { get; set; }

        /// <summary>
        /// Gets or sets the message unique identifier.
        /// </summary>
        /// <value>The message unique identifier.</value>
        [DataMember]
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>The response code.</value>
        [DataMember]
        public PushResponseCode ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        /// <value>The response message.</value>
        [DataMember]
        public string ResponseMessage { get; set; }
    }
}
