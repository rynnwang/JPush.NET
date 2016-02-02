using System.Runtime.Serialization;


namespace Beyova.JPush
{
    /// <summary>
    /// Class PushResponse.
    /// </summary>
    public class PushResponse
    {
        /// <summary>
        /// Gets or sets the send identity.
        /// </summary>
        /// <value>The send identity.</value>
        public string SendIdentity { get; set; }

        /// <summary>
        /// Gets or sets the message unique identifier.
        /// </summary>
        /// <value>The message unique identifier.</value>
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>The response code.</value>
        public PushResponseCode ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        /// <value>The response message.</value>
        public string ResponseMessage { get; set; }
    }
}
