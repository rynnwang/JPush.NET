using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Beyova.ExceptionSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Beyova.JPush
{
    /// <summary>
    /// Class PushMessageException.
    /// </summary>
    public class PushMessageException : Exception
    {
        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        /// <value>
        /// The HTTP status code.
        /// </value>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public virtual PushResponseCode Code { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public string MessageId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushMessageException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public PushMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Froms the HTTP operation exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        internal static PushMessageException FromHttpOperationException(HttpOperationException exception)
        {
            if (exception != null)
            {
                JToken json = exception.ExceptionReference.ResponseText.TryParseToJToken();
                var errorNode = json.SelectToken("error");

                if (errorNode != null)
                {
                    var code = errorNode.SelectToken("code").Value<int>().ParseToEnum<PushResponseCode>();
                    var message = errorNode.SelectToken("message")?.Value<string>();

                    return (code == PushResponseCode.NoDestinationReached && message.Equals("cannot find user by this audience", StringComparison.OrdinalIgnoreCase)) ?
                        new NoDestinationReachedException(message, exception)
                        {
                            HttpStatusCode = (HttpStatusCode)((int)exception.Code.Major),
                            MessageId = json.SelectToken("msg_id")?.Value<string>(),
                            Code = code
                        } :
                        new PushMessageException(message, exception)
                        {
                            HttpStatusCode = (HttpStatusCode)((int)exception.Code.Major),
                            MessageId = json.SelectToken("msg_id")?.Value<string>(),
                            Code = code
                        };
                }
            }

            return null;
        }
    }
}
