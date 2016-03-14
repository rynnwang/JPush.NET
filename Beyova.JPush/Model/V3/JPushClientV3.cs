using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Beyova;

namespace Beyova.JPush.V3
{
    /// <summary>
    /// Class JPushClient.
    /// <example>
    /// Here is a sample based on .NET console application.
    /// <code>
    /// <![CDATA[
    /// using System;
    /// using System.Collections.Generic;
    /// using System.IO;
    /// using System.Net;
    /// using System.Text;
    /// using System.Threading;
    /// using Beyova.JPush.V3;
    /// using Newtonsoft.Json;
    ///
    /// namespace Beyova.JPush.Test
    /// {
    ///     class Program
    ///     {
    ///         static void Main(string[] args)
    ///         {
    ///             var appKey = "1234567890abcdef"; // Your App Key from JPush
    ///             var masterSecret = "1234567890abcdef"; // Your Master Secret from JPush
    ///
    ///             Dictionary<string, string> customizedValues = new Dictionary<string, string>();
    ///             customizedValues.Add("CK1", "CV1");
    ///             customizedValues.Add("CK2", "CV2");
    ///
    ///             JPushClientV3 client = new JPushClientV3(appKey, masterSecret);
    ///
    ///             Audience audience = new Audience();
    ///             // In JPush V3, tag can be multiple added with different values.
    ///             // In following code, it is to send push to those who are in ((Tag1 AND Tag2) AND (Tag3 OR Tag4))
    ///             // If you want to send to all, please use: audience.Add(PushTypeV3.Broadcast, null);
    ///             audience.Add(PushTypeV3.ByTagWithinAnd, new List<string>(new string[] { "Tag1", "Tag2" }));
    ///             audience.Add(PushTypeV3.ByTagWithinOr, new List<string>(new string[] { "Tag3", "Tag4" }));
    ///
    ///             // In JPush V3, Notification would not be display on screen, it would be transferred to app instead.
    ///             // And different platform can provide different notification data.
    ///             Notification notification = new Notification
    ///             {
    ///                 AndroidNotification = new AndroidNotificationParameters
    ///                 {
    ///                     Title = "JPush provides V3.",
    ///                     Alert = "JPush V2 would be retired soon.",
    ///                     CustomizedValues = customizedValues
    ///                 },
    ///                 iOSNotification = new iOSNotificationParameters
    ///                 {
    ///                     Badge = 1,
    ///                     Alert = "JPush V2 would be retired soon.",
    ///                     Sound = "YourSound",
    ///                     CustomizedValues = customizedValues
    ///                 }
    ///             };
    ///
    ///             var response = client.SendPushMessage(new PushMessageRequestV3
    ///             {
    ///                 Audience = audience,
    ///                 Platform = PushPlatform.AndroidAndiOS,
    ///                 IsTestEnvironment = true,
    ///                 AppMessage = new AppMessage
    ///                 {
    ///                     Content = "Hello, this is a test push of V3 from .NET. Have a nice day!",
    ///                     CustomizedValue = customizedValues
    ///                 },
    ///                 Notification = notification
    ///             });
    ///
    ///             Console.WriteLine(response.ResponseCode.ToString() + ":" + response.ResponseMessage);
    ///             Console.WriteLine("Push sent.");
    ///             Console.WriteLine(response.ResponseCode.ToString() + ":" + response.ResponseMessage);
    ///
    ///             List<string> idToCheck = new List<string>();
    ///             idToCheck.Add(response.MessageId);
    ///
    ///             var statusList = client.QueryPushMessageStatus(idToCheck);
    ///
    ///             Console.WriteLine("Status track is completed.");
    ///
    ///             if (statusList != null)
    ///             {
    ///                 foreach (var one in statusList)
    ///                 {
    ///                     Console.WriteLine(string.Format("Id: {0}, Android: {1}, iOS: {2}", one.MessageId, one.AndroidDeliveredCount, one.ApplePushNotificationDeliveredCount));
    ///                 }
    ///             }
    ///
    ///             Console.WriteLine("Press any key to exit.");
    ///             Console.Read();
    ///         }
    ///     }
    /// }
    ///    ]]>
    ///    </code>
    /// </example>
    /// If you already use JPushClient V2 (Class: <see cref="JPushClient"/>), you can continue to use that. Library would convert the old <see cref="PushMessageRequest"/> to <see cref="PushMessageRequestV3" />.
    /// NOTE: the parameter in constructor of <see cref="JPushClient"/> has lost effect. Whatever you set as true or false, SSL would be used according to JPush's new rule.
    /// RESTful API reference: http://docs.jpush.cn/display/dev/Push-API-v3   
    /// </summary>
    public class JPushClientV3
    {
        /// <summary>
        /// The remote base URL
        /// </summary>
        protected const string apiBaseUrl = "https://api.jpush.cn/v3/";

        /// <summary>
        /// The report base URL format
        /// </summary>
        protected const string reportBaseUrlFormat = "https://report.jpush.cn/v2/";

        #region Property

        /// <summary>
        /// Gets or sets the application key.
        /// </summary>
        /// <value>The application key.</value>
        public string AppKey
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the master secret.
        /// </summary>
        /// <value>The master secret.</value>
        public string MasterSecret
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is test environment.
        /// </summary>
        /// <value><c>null</c> if [is test environment] contains no value, <c>true</c> if [is test environment]; otherwise, <c>false</c>.</value>
        public bool? IsTestEnvironment
        {
            get;
            protected set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="JPushClientV3" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public JPushClientV3(ConnectionString connectionString)
                    : this(connectionString?.AppKey, connectionString?.MasterSecret, connectionString?.IsTestEnvironment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JPushClient" /> class.
        /// </summary>
        /// <param name="appKey">The application key.</param>
        /// <param name="masterSecret">The master secret.</param>
        /// <param name="isTestEnvironment">The is test environment.</param>
        public JPushClientV3(string appKey, string masterSecret, bool? isTestEnvironment = null)
        {
            this.AppKey = appKey;
            this.MasterSecret = masterSecret;
            this.IsTestEnvironment = isTestEnvironment;
        }

        #endregion

        #region Public method

        /// <summary>
        /// Sends the push message.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PushResponseCode.</returns>
        /// <exception cref="System.InvalidOperationException">Failed to send push message.</exception>
        public PushResponse SendPushMessage(PushMessageRequestV3 request)
        {
            PushResponse result = new PushResponse();
            WebResponse response = null;

            try
            {
                var httpRequest = CreatePushRequest(request);
                var responseContent = httpRequest.ReadResponseAsText(Encoding.UTF8);

                JToken root = JToken.Parse(responseContent);
                result.MessageId = root.SelectToken("msg_id").Value<string>();

                var errorNode = root.SelectToken("error");

                if (errorNode == null)
                {
                    result.SendIdentity = root.SelectToken("sendno").Value<string>();
                    result.ResponseCode = PushResponseCode.Succeed;
                }
                else
                {
                    result.ResponseMessage = errorNode.SelectToken("message").Value<string>();
                    result.ResponseCode = (PushResponseCode)errorNode.SelectToken("code").Value<int>();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex.Handle("Failed to send push message.");
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
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
            // JPush has limitation officially. One query support no more than 100 IDs.
            const int limitation = 100;

            List<PushMessageStatus> result = new List<PushMessageStatus>();

            string idCollection = string.Empty;

            if (messageIdCollection.HasItem())
            {
                if (messageIdCollection.Count > limitation)
                {
                    messageIdCollection.RemoveRange(limitation, messageIdCollection.Count - limitation);
                }

                idCollection = string.Join(",", messageIdCollection);
            }

            if (!string.IsNullOrWhiteSpace(idCollection))
            {
                try
                {
                    var httpRequest = this.CreatePushQueryRequest(idCollection);
                    var responseBody = httpRequest.ReadResponseAsText(Encoding.UTF8);
                    result = JsonConvert.DeserializeObject<List<PushMessageStatus>>(responseBody);
                }
                catch (Exception ex)
                {
                    throw ex.Handle("Failed to QueryPushMessageStatus.");
                }
            }

            return result;
        }

        #endregion

        #region Protected method

        /// <summary>
        /// Creates the push request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>HttpWebRequest.</returns>
        protected HttpWebRequest CreatePushRequest(PushMessageRequestV3 request)
        {
            HttpWebRequest httpRequest = null;

            if (request != null)
            {
                httpRequest = (HttpWebRequest)HttpWebRequest.Create(apiBaseUrl + "push");
                FillAuthenticationV3(httpRequest);

                var jsonObject = new JObject();
                jsonObject.Add(request.Platform.ToJson());
                jsonObject.Add(request.Audience.ToJson());

                if (request.Notification == null && request.AppMessage == null)
                {
                    throw new ArgumentException("Both Notification and AppMessage are null.");
                }

                if (request.Notification != null)
                {
                    jsonObject.Add(new JProperty("notification", JObject.FromObject(request.Notification)));
                }

                if (request.AppMessage != null)
                {
                    jsonObject.Add(new JProperty("message", JObject.FromObject(request.AppMessage)));
                }

                jsonObject.Add(new JProperty("options", JObject.FromObject(CreateRequestOptions(request))));

                httpRequest.FillData("POST", jsonObject.ToString());
            }

            return httpRequest;
        }

        /// <summary>
        /// Creates the push query request.
        /// </summary>
        /// <param name="idCollectionString">The unique identifier collection string.</param>
        /// <returns>HttpWebRequest.</returns>
        protected HttpWebRequest CreatePushQueryRequest(string idCollectionString)
        {
            var httpRequest = (reportBaseUrlFormat + "received?msg_ids=" + idCollectionString.SafeToString()).CreateHttpWebRequest(HttpConstants.HttpMethod.Get);
            FillAuthentication(httpRequest);

            return httpRequest;
        }

        /// <summary>
        /// Fills the authentication.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        protected void FillAuthentication(HttpWebRequest httpRequest)
        {
            if (httpRequest != null)
            {
                httpRequest.Headers[HttpRequestHeader.Authorization] = GenerateQueryToken(this.AppKey.SafeToString(), this.MasterSecret.SafeToString());
                FillNetworkCredential(httpRequest);
            }
        }

        /// <summary>
        /// Fills the authentication v3.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        protected void FillAuthenticationV3(HttpWebRequest httpRequest)
        {
            if (httpRequest != null)
            {
                httpRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + GenerateQueryToken(this.AppKey.SafeToString(), this.MasterSecret.SafeToString());
                FillNetworkCredential(httpRequest);
            }
        }

        /// <summary>
        /// Creates the request option.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        protected Dictionary<string, object> CreateRequestOptions(PushMessageRequestV3 request)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (request != null)
            {
                result.Add("sendno", GenerateSendIdentity());

                if (request.LifeTime != null)
                {
                    result.Add("time_to_live", request.LifeTime.Value);
                }

                if (!string.IsNullOrWhiteSpace(request.OverrideMessageId))
                {
                    result.Add("override_msg_id", request.OverrideMessageId);
                }

                result.Add("apns_production", !(request.IsTestEnvironment ?? this.IsTestEnvironment ?? false));
            }

            return result;
        }

        /// <summary>
        /// Fills the network credential.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        protected void FillNetworkCredential(HttpWebRequest httpRequest)
        {
            if (httpRequest != null)
            {
                httpRequest.Credentials = new NetworkCredential(this.AppKey.SafeToString(), this.MasterSecret.SafeToString());
            }
        }

        /// <summary>
        /// Generates the query token.
        /// </summary>
        /// <param name="appKey">The application key.</param>
        /// <param name="masterSecret">The master secret.</param>
        /// <returns>System.String.</returns>
        protected static string GenerateQueryToken(string appKey, string masterSecret)
        {
            return string.Format("{0}:{1}", appKey, masterSecret).ToBase64();
        }

        /// <summary>
        /// Generates the send identity.
        /// The total milliseconds value of offset from UTC now to UTC 2014 Jan 1st.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected static int GenerateSendIdentity()
        {
            return (int)(((DateTime.UtcNow - new DateTime(2014, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds) % Int32.MaxValue);
        }

        #endregion
    }
}
