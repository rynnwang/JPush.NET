using System;
using System.Collections.Generic;
using Beyova.JPush;
using Beyova.JPush.V3;
using Newtonsoft.Json.Linq;

namespace Beyova
{
    /// <summary>
    /// Class Extension.
    /// </summary>
    internal static class Extension
    {
        /// <summary>
        /// Determines whether [contains] [the specified platform value].
        /// </summary>
        /// <param name="platformValue">The platform value.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [contains] [the specified platform value]; otherwise, <c>false</c>.</returns>
        public static bool Contains(this PushPlatform platformValue, PushPlatform value)
        {
            return ((int)platformValue & (int)value) > 0;
        }

        /// <summary>
        /// To the push message request v3.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Beyova.JPush.V3.PushMessageRequestV3.</returns>
        public static Beyova.JPush.V3.PushMessageRequestV3 ToPushMessageRequestV3(this PushMessageRequest request)
        {
            Beyova.JPush.V3.PushMessageRequestV3 result = null;

            if (request != null)
            {
                result = new Beyova.JPush.V3.PushMessageRequestV3()
                {
                    Platform = request.Platform,
                    IsTestEnvironment = request.IsTestEnvironment,
                    LifeTime = request.LifeTime,
                    OverrideMessageId = request.OverrideMessageId,
                    Notification = new Notification(),
                    AppMessage = new AppMessage
                    {
                        Content = request.Message.Content,
                        Title = request.Message.PushTitle,
                        CustomizedValue = request.Message.CustomizedValue
                    }
                };

                Audience audience = new Audience();
                List<string> list = new List<string>(request.ReceiverValue.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries));

                switch (request.PushType)
                {
                    case PushType.Broadcast:
                        audience.Add(PushTypeV3.Broadcast, null);
                        break;
                    case PushType.ByAlias:
                        audience.Add(PushTypeV3.ByAlias, list);
                        break;
                    case PushType.ByRegistrationId:
                        audience.Add(PushTypeV3.ByRegistrationId, list);
                        break;
                    case PushType.ByTag:
                        audience.Add(PushTypeV3.ByTagWithinOr, list);
                        break;
                    default:
                        break;
                }

                result.Audience = audience;

                Notification notification = new Notification();
                if ((request.Platform & PushPlatform.Android) == PushPlatform.Android)
                {
                    result.Notification.AndroidNotification = new AndroidNotificationParameters
                    {
                        Alert = request.Message.Content,
                        BuilderId = request.Message.BuilderId,
                        CustomizedValues = request.Message.CustomizedValue,
                        Title = request.Message.PushTitle
                    };
                }
                if ((request.Platform & PushPlatform.iOS) == PushPlatform.iOS)
                {
                    result.Notification.iOSNotification = new iOSNotificationParameters
                    {
                        Alert = request.Message.Content,
                        Sound = request.Message.Sound,
                        CustomizedValues = request.Message.CustomizedValue,
                        Badge = request.Message.BadgeValue
                    };
                }

            }

            return result;
        }

        /// <summary>
        /// To the push type v3.
        /// </summary>
        /// <param name="pushType">Type of the push.</param>
        /// <returns>Beyova.JPush.V3.PushTypeV3.</returns>
        public static Beyova.JPush.V3.PushTypeV3 ToPushTypeV3(this PushType pushType)
        {
            switch (pushType)
            {
                case PushType.Broadcast:
                    return Beyova.JPush.V3.PushTypeV3.Broadcast;
                case PushType.ByAlias:
                    return Beyova.JPush.V3.PushTypeV3.ByAlias;
                case PushType.ByRegistrationId:
                    return Beyova.JPush.V3.PushTypeV3.ByRegistrationId;
                case PushType.ByTag:
                    return Beyova.JPush.V3.PushTypeV3.ByTagWithinOr;
                case PushType.Unknown:
                default:
                    return Beyova.JPush.V3.PushTypeV3.None;
            }
        }

        /// <summary>
        /// Platforms to json.
        /// </summary>
        /// <param name="platform">The platform.</param>
        /// <returns>JProperty.</returns>
        public static JProperty ToJson(this PushPlatform platform)
        {
            const string propertyName = "platform";

            if (platform == PushPlatform.All)
            {
                return new JProperty(propertyName, "all");
            }
            else
            {
                JArray array = new JArray();

                foreach (PushPlatform one in (new PushPlatform[] { PushPlatform.Android, PushPlatform.iOS, PushPlatform.WindowsPhone }))
                {
                    if ((platform & one) == one)
                    {
                        array.Add(one.ToString().ToLowerInvariant());
                    }
                }

                return new JProperty(propertyName, array);
            }
        }

        /// <summary>
        /// Pushes the type to json.
        /// </summary>
        /// <param name="audience">The audience.</param>
        /// <returns>System.Object.</returns>
        public static JProperty ToJson(this Audience audience)
        {
            const string propertyName = "audience";

            if (audience != null)
            {
                if (audience.ContainsKey(PushTypeV3.Broadcast))
                {
                    return new JProperty(propertyName, "all");
                }
                else
                {
                    JObject obj = new JObject();

                    foreach (var one in audience.Keys)
                    {
                        switch (one)
                        {
                            case PushTypeV3.ByAlias:
                                obj.Add(new JProperty("alias", audience[one]));
                                break;
                            case PushTypeV3.ByRegistrationId:
                                obj.Add(new JProperty("registration_id", audience[one]));
                                break;
                            case PushTypeV3.ByTagWithinAnd:
                                obj.Add(new JProperty("tag_and", audience[one]));
                                break;
                            case PushTypeV3.ByTagWithinOr:
                                obj.Add(new JProperty("tag", audience[one]));
                                break;
                            default:
                                break;
                        }
                    }

                    return new JProperty(propertyName, obj);
                }
            }

            return null;
        }
    }
}
