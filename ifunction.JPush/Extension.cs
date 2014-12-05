using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ifunction.JPush;
using ifunction.JPush.V3;
using Newtonsoft.Json.Linq;

namespace ifunction
{
    /// <summary>
    /// Class Extension.
    /// </summary>
    internal static class Extension
    {
        #region MD5

        /// <summary>
        /// To the md5.
        /// </summary>
        /// <param name="stringObject">The string object.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException">ToMD5</exception>
        public static string ToMD5(this string stringObject, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            try
            {
                byte[] data = encoding.GetBytes(stringObject);
                return ToMD5(data).ToUpperInvariant();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("ToMD5", ex);
            }
        }

        /// <summary>
        /// To the md5.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException">ToMD5</exception>
        public static string ToMD5(this byte[] bytes)
        {
            try
            {
                MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
                byte[] hash_byte = md5Provider.ComputeHash(bytes);
                string result = System.BitConverter.ToString(hash_byte);
                return result.Replace("-", "").ToUpperInvariant();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("ToMD5", ex);
            }
        }

        #endregion

        #region SHA1

        /// <summary>
        /// Encrypts to SHA1.
        /// </summary>
        /// <param name="stringObject">The string object.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException">ToSHA1</exception>
        public static string ToSHA1(this string stringObject, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            try
            {
                return ToSHA1(encoding.GetBytes(stringObject));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("ToSHA1", ex);
            }
        }

        /// <summary>
        /// Encrypts to SH a1.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException">ToSHA1</exception>
        public static string ToSHA1(this byte[] data)
        {
            try
            {
                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    var hash_byte = sha1.ComputeHash(data);
                    string result = System.BitConverter.ToString(hash_byte);
                    return result.Replace("-", "").ToUpperInvariant();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("ToSHA1", ex);
            }
        }


        #endregion

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="anyObject">Any object.</param>
        /// <param name="defaultString">The default string.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue(this string anyObject, string defaultString = "")
        {
            return !string.IsNullOrWhiteSpace(anyObject) ? anyObject : defaultString;
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="anyObject">Any object.</param>
        /// <param name="defaultString">The default string.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue(this object anyObject, string defaultString = "")
        {
            return anyObject != null ? anyObject.ToString() : defaultString;
        }

        /// <summary>
        /// Checks the null reference.
        /// </summary>
        /// <param name="anyObject">Any object.</param>
        /// <param name="objectIdentity">The object identity.</param>
        /// <exception cref="System.NullReferenceException">Object [ + objectIdentity.GetStringValue() + ] is null.</exception>
        public static void CheckNullReference(this object anyObject, string objectIdentity = null)
        {
            if (anyObject == null)
            {
                throw new NullReferenceException("Object [" + objectIdentity.GetStringValue() + "] is null.");
            }
        }

        /// <summary>
        /// To base64. Default encoding is UTF-8.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>System.String.</returns>
        public static string ToBase64(this string source, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            try
            {
                byte[] bytes = encoding.GetBytes(source);
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Merges the specified container.
        /// </summary>
        /// <typeparam name="TKey">The type of the attribute key.</typeparam>
        /// <typeparam name="TValue">The type of the attribute value.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Merge<TKey, TValue>(this Dictionary<TKey, TValue> container, TKey key, TValue value)
        {
            if (container != null)
            {
                if (container.ContainsKey(key))
                {
                    container[key] = value;
                }
                else
                {
                    container.Add(key, value);
                }
            }
        }

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
        /// <returns>ifunction.JPush.V3.PushMessageRequestV3.</returns>
        public static ifunction.JPush.V3.PushMessageRequestV3 ToPushMessageRequestV3(this PushMessageRequest request)
        {
            ifunction.JPush.V3.PushMessageRequestV3 result = null;

            if (request != null)
            {
                result = new ifunction.JPush.V3.PushMessageRequestV3()
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
        /// <returns>ifunction.JPush.V3.PushTypeV3.</returns>
        public static ifunction.JPush.V3.PushTypeV3 ToPushTypeV3(this PushType pushType)
        {
            switch (pushType)
            {
                case PushType.Broadcast:
                    return ifunction.JPush.V3.PushTypeV3.Broadcast;
                case PushType.ByAlias:
                    return ifunction.JPush.V3.PushTypeV3.ByAlias;
                case PushType.ByRegistrationId:
                    return ifunction.JPush.V3.PushTypeV3.ByRegistrationId;
                case PushType.ByTag:
                    return ifunction.JPush.V3.PushTypeV3.ByTagWithinOr;
                case PushType.Unknown:
                default:
                    return ifunction.JPush.V3.PushTypeV3.None;
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
