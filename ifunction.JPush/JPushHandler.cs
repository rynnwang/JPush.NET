using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ifunction.JPush.V3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ifunction.JPush
{
    /// <summary>
    /// Class <c>JPushHandler</c>. Class <c>JPushHandler</c> is inherited from class <c>JPushClient</c>. The difference is, class <c>JPushHandler</c> has already involve the maintenance logic to automatically track JPush Message status. You only need to assign appropriate delegate to update status result to your storage. Since a background monitor thread would be initialized in each instance of class <c>JPushHandler</c>, it is strongly recommended to use JPushHandler as singleton. Another minor difference is, class <c>JPushClient</c> uses SSL always, which can be noticed by its constructor.
    /// <example>
    /// Here is a sample based on .NET console application.
    /// <code>
    /// <![CDATA[
    ///namespace ifunction.JPush.Test
    ///{
    ///    static class Program
    ///    {
    ///        static void Main(string[] args)
    ///        {
    ///            var appKey = "1234567890abcdef"; // Your App Key from JPush
    ///            var masterSecret = "1234567890abcdef"; // Your Master Secret from JPush
    ///
    ///            Dictionary<string, string> customizedValues = new Dictionary<string, string>();
    ///            customizedValues.Add("CK1", "CV1");
    ///            customizedValues.Add("CK2", "CV2");
    ///
    ///            JPushHandler handler = new JPushHandler(
    ///                appKey,                              // Your app key
    ///                masterSecret,                        // Your master secret
    ///                CreatePushMessageLogDelegate,        // Delegate for creating push message log. Generally, it is to save log in database or any other media for later tracking in UI, but to make it simple here, just console write lines of information in JSON format.
    ///                UpdatePushMessageStatus,             // Delegate for updating push message status. Generally, it is to update status in database, but to make it simple here, just console write lines of information.
    ///                null,                                // Delegate for initializing message tracking information. Generally, it is to read message id collection which need to be tracked from database, especially it occurs when machine is rebooted. To make it simple here, leave null for doing nothing.                
    ///                null,                                // Delegate for reporting exception. Generally, it is to write log in file, event log or database. To make it simple here, leave null for doing nothing.
    ///                2                                    // Interval value for monitoring thread for get update for message status. By default, it is 60 in seconds. It is set 2 seconds here for sample.
    ///                );
    ///
    ///            handler.SendPushMessage(new PushMessageRequest
    ///                             {
    ///                                 MessageType = MessageType.Notification,
    ///                                 Platform = PushPlatform.Android,
    ///                                 Description = "DotNET",
    ///                                 PushType = PushType.Broadcast,
    ///                                 IsTestEnvironment = true,
    ///                                 Message = new PushMessage
    ///                                 {
    ///                                     Content = "Hello, this is a test push from .NET. Have a nice day!",
    ///                                     PushTitle = "A title.",
    ///                                     Sound = "YourSound",
    ///                                     CustomizedValue = customizedValues
    ///                                 }
    ///                             });
    ///
    ///            Console.WriteLine("Tracking IDs in list:");
    ///            foreach (var one in handler.MessageTrackingId)
    ///            {
    ///                Console.WriteLine(one);
    ///            }
    ///            Console.WriteLine("---------- END ----------");
    ///
    ///            Thread.Sleep(10 * 1000); // To sleep 10 seconds here to make sure the monitoring thread in hander has been invoked.
    ///
    ///            Console.WriteLine("Press any key to exit.");
    ///            Console.Read();
    ///        }
    ///
    ///        public static void CreatePushMessageLogDelegate(PushMessageRequest messageRequest, PushResponse response)
    ///        {
    ///            Console.WriteLine(string.Format(@"Push Log {0}
    ///Request:
    ///{1}
    ///
    ///Response:
    ///{2}", JsonConvert.SerializeObject(messageRequest), JsonConvert.SerializeObject(response)));
    ///        }
    ///
    ///        public static void CreatePushMessageLog(PushMessageRequest messageRequest, PushResponse response)
    ///        {
    ///            Console.WriteLine(string.Format(@"Push Log {0}
    ///Request:
    ///{1}
    ///Response:
    ///{2}", JsonConvert.SerializeObject(messageRequest), JsonConvert.SerializeObject(response)));
    ///        }
    ///
    ///        public static void UpdatePushMessageStatus(List<PushMessageStatus> pushMessageStatus)
    ///        {
    ///            if (pushMessageStatus != null)
    ///            {
    ///                foreach (var one in pushMessageStatus)
    ///                {
    ///                    Console.WriteLine(string.Format(@"Push Message Status Update: {0}
    ///Android: {1}
    ///iOS: {2}", one.MessageId, one.AndroidDeliveredCount, one.ApplePushNotificationDeliveredCount));
    ///                }
    ///            }
    ///        }
    ///    }
    ///}
    /// ]]>
    /// </code>
    /// </example>
    /// RESTful API reference: http://docs.jpush.cn/display/dev/Index
    /// </summary>
    public class JPushHandler
    {
        /// <summary>
        /// The client
        /// </summary>
        protected JPushClientV3 client;

        #region Delegates

        /// <summary>
        /// Delegate InitializeMessageTrackingId
        /// </summary>
        /// <returns>IEnumerable{System.String}.</returns>
        public delegate IEnumerable<string> InitializeMessageTrackingIdDelegate();

        /// <summary>
        /// Delegate CreatePushMessageLog
        /// </summary>
        /// <param name="messageRequest">The message request.</param>
        /// <param name="response">The response.</param>
        public delegate void CreatePushMessageLogDelegate(PushMessageRequestV3 messageRequest, PushResponse response);

        /// <summary>
        /// Delegate UpdatePushMessageStatusDelegate
        /// </summary>
        /// <param name="pushMessageStatus">The push message status.</param>
        public delegate void UpdatePushMessageStatusDelegate(List<PushMessageStatus> pushMessageStatus);

        /// <summary>
        /// Delegate ReportExceptionDelegate
        /// </summary>
        /// <param name="exception">The exception.</param>
        public delegate void ReportExceptionDelegate(Exception exception);

        #endregion

        #region Observers

        /// <summary>
        /// The push message status tracking life cycle
        /// </summary>
        protected const int pushMessageStatusTrackingLifeCycle = 10 * 24 * 60; //10 days. Defined by JPush official.

        /// <summary>
        /// The push message tracking list
        /// </summary>
        protected List<PushMessageTracking> pushMessageTrackingList;

        /// <summary>
        /// The data locker
        /// </summary>
        protected object dataLocker = new object();

        /// <summary>
        /// The monitor thread
        /// </summary>
        protected Thread monitorThread = null;

        /// <summary>
        /// Monitors the thread delegate.
        /// </summary>
        protected void MonitorThreadDelegate()
        {
            while (true)
            {
                try
                {
                    List<string> idCollection = new List<string>();

                    lock (dataLocker)
                    {
                        var nowUtcTime = DateTime.UtcNow;

                        for (var i = 0; i < this.pushMessageTrackingList.Count; )
                        {
                            var one = this.pushMessageTrackingList[i];
                            if ((nowUtcTime - one.CreatedUtcStamp).TotalMinutes > pushMessageStatusTrackingLifeCycle)
                            {
                                this.pushMessageTrackingList.RemoveAt(i);
                            }
                            else
                            {
                                idCollection.Add(one.MessageId);
                                i++;
                            }
                        }
                    }

                    List<PushMessageStatus> result = new List<PushMessageStatus>();

                    while (idCollection.Count > 0)
                    {
                        var listToOperate = new List<string>();
                        listToOperate.AddRange(idCollection.Take(idCollection.Count > 100 ? 100 : idCollection.Count));

                        var queryResult = client.QueryPushMessageStatus(listToOperate);

                        if (queryResult != null)
                        {
                            result.AddRange(queryResult);
                        }

                        idCollection.RemoveRange(0, listToOperate.Count);
                    }

                    if (this.UpdateDelegate != null)
                    {
                        this.UpdateDelegate.Invoke(result);
                    }
                }
                catch (Exception ex)
                {
                    this.HandleException(new Exception("Failed to run monitor work.", ex));
                }

                Thread.Sleep(this.IntervalInSecond * 1000);
            }
        }

        /// <summary>
        /// Initializes the background thread.
        /// </summary>
        protected void InitializeBackgroundThread()
        {
            if (monitorThread == null)
            {
                lock (dataLocker)
                {
                    if (monitorThread == null)
                    {
                        monitorThread = new Thread(new ThreadStart(MonitorThreadDelegate));
                        monitorThread.IsBackground = true;

                        try
                        {
                            if (this.InitializeDelegate != null)
                            {
                                var idCollection = this.InitializeDelegate.Invoke();
                            }
                        }
                        catch (Exception ex)
                        {
                            HandleException(new InvalidOperationException("Failed to initialize message tracking list.", ex));
                        }

                        monitorThread.Start();
                    }
                }
            }
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the interval in second.
        /// </summary>
        /// <value>The interval in second.</value>
        public int IntervalInSecond { get; protected set; }

        /// <summary>
        /// Gets or sets the create delegate.
        /// </summary>
        /// <value>The create delegate.</value>
        public CreatePushMessageLogDelegate CreateDelegate
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the update delegate.
        /// </summary>
        /// <value>The update delegate.</value>
        public UpdatePushMessageStatusDelegate UpdateDelegate
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the initialize delegate.
        /// </summary>
        /// <value>The initialize delegate.</value>
        public InitializeMessageTrackingIdDelegate InitializeDelegate
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the exception delegate.
        /// </summary>
        /// <value>The exception delegate.</value>
        public ReportExceptionDelegate ExceptionDelegate
        {
            get;
            protected set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="JPushClient" /> class.
        /// </summary>
        /// <param name="appKey">The application key.</param>
        /// <param name="masterSecret">The master secret.</param>
        /// <param name="createPushMessageLogDelegate">The create push message log delegate.</param>
        /// <param name="updatePushMessageStatusDelegate">The update push message status delegate.</param>
        /// <param name="initializeDelegate">The initialize delegate.</param>
        /// <param name="exceptionDelegate">The exception delegate.</param>
        /// <param name="intervalInSecond">The interval in second.</param>
        public JPushHandler(string appKey, string masterSecret,
            CreatePushMessageLogDelegate createPushMessageLogDelegate = null,
            UpdatePushMessageStatusDelegate updatePushMessageStatusDelegate = null,
            InitializeMessageTrackingIdDelegate initializeDelegate = null,
            ReportExceptionDelegate exceptionDelegate = null,
            int intervalInSecond = 60)
        {
            client = new JPushClientV3(appKey, masterSecret);

            if (intervalInSecond < 1)
            {
                intervalInSecond = 60;
            }

            this.IntervalInSecond = intervalInSecond;
            this.pushMessageTrackingList = new List<PushMessageTracking>();

            this.CreateDelegate = createPushMessageLogDelegate;
            this.UpdateDelegate = updatePushMessageStatusDelegate;
            this.InitializeDelegate = initializeDelegate;
            this.ExceptionDelegate = exceptionDelegate;

            InitializeBackgroundThread();
        }

        #endregion

        #region Publis Methods

        /// <summary>
        /// Sends the push message.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PushResponseCode.</returns>
        public PushResponse SendPushMessage(PushMessageRequest request)
        {
            if (request != null)
            {
                return SendPushMessage(request.ToPushMessageRequestV3());
            }
            return null;
        }

        /// <summary>
        /// Sends the push message.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>PushResponse.</returns>
        public PushResponse SendPushMessage(PushMessageRequestV3 request)
        {
            try
            {
                var response = client.SendPushMessage(request);

                if (this.CreateDelegate != null)
                {
                    this.CreateDelegate.Invoke(request, response);
                }

                if (response != null && !string.IsNullOrWhiteSpace(response.MessageId))
                {
                    this.AddMessageTrackingId(new PushMessageTracking { MessageId = response.MessageId, CreatedUtcStamp = DateTime.UtcNow });
                }

                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(new Exception("Failed to send push message", ex));
            }

            return null;
        }

        /// <summary>
        /// Adds the message tracking identifier.
        /// </summary>
        /// <param name="messageTracking">The message tracking.</param>
        public void AddMessageTrackingId(PushMessageTracking messageTracking)
        {
            lock (dataLocker)
            {
                this.AddMessageTrackingIdWithoutLocker(messageTracking);
            }
        }

        /// <summary>
        /// Removes the message tracking.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        public void RemoveMessageTracking(string messageId)
        {
            lock (dataLocker)
            {
                this.RemoveMessageTrackingWithoutLocker(messageId);
            }
        }

        /// <summary>
        /// Gets the message tracking identifier.
        /// </summary>
        /// <value>The message tracking identifier.</value>
        public IList<string> MessageTrackingId
        {
            get
            {
                IList<string> result = new List<string>();

                lock (dataLocker)
                {
                    foreach (var one in this.pushMessageTrackingList)
                    {
                        result.Add(one.MessageId);
                    }
                }

                return result;
            }
        }

        #endregion

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <exception cref="System.InvalidOperationException">Failed to handle exception with specific delegate.</exception>
        protected void HandleException(Exception exception)
        {
            try
            {
                if (exception != null && this.ExceptionDelegate != null)
                {
                    this.ExceptionDelegate.Invoke(exception);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to handle exception with specific delegate.", ex);
            }
        }

        /// <summary>
        /// Finds the message tracking by identifier.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>PushMessageTracking.</returns>
        protected PushMessageTracking FindMessageTrackingByIdWithoutLocker(string messageId, out int index)
        {
            index = -1;

            if (!string.IsNullOrWhiteSpace(messageId))
            {
                foreach (var one in this.pushMessageTrackingList)
                {
                    index++;

                    if (one.MessageId == messageId)
                    {
                        return one;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Adds the message tracking identifier without locker.
        /// </summary>
        /// <param name="messageTracking">The message tracking.</param>
        protected void AddMessageTrackingIdWithoutLocker(PushMessageTracking messageTracking)
        {
            if (messageTracking != null && !string.IsNullOrWhiteSpace(messageTracking.MessageId))
            {
                int index;

                if (FindMessageTrackingByIdWithoutLocker(messageTracking.MessageId, out index) == null)
                {
                    this.pushMessageTrackingList.Add(messageTracking);
                }
            }
        }

        /// <summary>
        /// Removes the message tracking without locker.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        protected void RemoveMessageTrackingWithoutLocker(string messageId)
        {
            if (!string.IsNullOrWhiteSpace(messageId))
            {
                int index;
                var item = FindMessageTrackingByIdWithoutLocker(messageId, out index);

                if (item != null && index > -1)
                {
                    this.pushMessageTrackingList.RemoveAt(index);
                }
            }
        }
    }
}
