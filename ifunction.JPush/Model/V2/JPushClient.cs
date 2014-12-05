using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ifunction.JPush.V3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ifunction.JPush
{
    /// <summary>
    /// Class JPushClient.
    /// <example>
    /// Here is a sample based on .NET console application.
    /// <code>
    /// <![CDATA[
    ///  class Program
    ///   {
    ///        static void Main(string[] args)
    ///        {
    ///            var appKey = "1234567890abcdef"; // Your App Key from JPush
    ///            var masterSecret = "1234567890abcdef"; // Your Master Secret from JPush
    ///
    ///            Dictionary<string, string> customizedValues = new Dictionary<string, string>();
    ///            customizedValues.Add("CK1", "CV1");
    ///            customizedValues.Add("CK2", "CV2");
    ///
    ///            JPushClient client = new JPushClient(appKey, masterSecret, false);
    ///            var response = client.SendPushMessage(new PushMessageRequest
    ///                       {
    ///                           MessageType = MessageType.Notification,
    ///                           Platform = PushPlatform.Android,
    ///                           Description = "DotNET",
    ///                           PushType = PushType.Broadcast,
    ///                           IsTestEnvironment = true,
    ///                           Message = new PushMessage
    ///                           {
    ///                               Content = "Hello, this is a test push from .NET. Have a nice day!",
    ///                               PushTitle = "A title.",
    ///                               Sound = "YourSound",
    ///                               CustomizedValue = customizedValues
    ///                           }
    ///                       });
    ///
    ///            Console.WriteLine(response.ResponseCode.ToString() + ":" + response.ResponseMessage);
    ///            Console.WriteLine("Push sent.");
    ///            Console.WriteLine(response.ResponseCode.ToString() + ":" + response.ResponseMessage);
    ///
    ///
    ///            List<string> idToCheck = new List<string>();
    ///            idToCheck.Add(response.MessageId);
    ///            var statusList = client.QueryPushMessageStatus(idToCheck);
    ///
    ///            Console.WriteLine("Status track is completed.");
    ///
    ///            if (statusList != null)
    ///            {
    ///                foreach (var one in statusList)
    ///                {
    ///                    Console.WriteLine(string.Format("Id: {0}, Android: {1}, iOS: {2}", one.MessageId, one.AndroidDeliveredCount, one.ApplePushNotificationDeliveredCount));
    ///                }
    ///            }
    ///
    ///            Console.WriteLine("Press any key to exit.");
    ///            Console.Read();
    ///        }
    ///    }
    ///    ]]>
    ///    </code>
    /// </example>
    /// RESTful API reference: http://docs.jpush.cn/display/dev/Index
    /// </summary>
    [Obsolete("It client is based on V2. Please use JPushClientV3 instead.")]
    public class JPushClient
    {
        JPushClientV3 clientV3;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="JPushClient" /> class.
        /// </summary>
        /// <param name="appKey">The application key.</param>
        /// <param name="masterSecret">The master secret.</param>
        /// <param name="useSSL">if set to <c>true</c> [use SSL].</param>
        public JPushClient(string appKey, string masterSecret, bool useSSL = true)
        {
            clientV3 = new JPushClientV3(appKey, masterSecret);
        }

        #endregion

        #region Public method

        /// <summary>
        /// Sends the push message.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PushResponseCode.</returns>
        /// <exception cref="System.InvalidOperationException">Failed to send push message.</exception>
        public PushResponse SendPushMessage(PushMessageRequest request)
        {
            return clientV3.SendPushMessage(request.ToPushMessageRequestV3());
        }

        /// <summary>
        /// Queries the push message status.
        /// JPush has official limitation. One query support no more than 100 IDs. So if the input has more than 100 IDs, only the first 100 IDs would be queried.
        /// </summary>
        /// <param name="messageIdCollection">The message unique identifier collection.</param>
        /// <returns>List{PushMessageStatus}.</returns>
        /// <exception cref="System.InvalidOperationException">Failed to QueryPushMessageStatus.</exception>
        public List<PushMessageStatus> QueryPushMessageStatus(List<string> messageIdCollection)
        {
            return clientV3.QueryPushMessageStatus(messageIdCollection);
        }

        #endregion
    }
}
