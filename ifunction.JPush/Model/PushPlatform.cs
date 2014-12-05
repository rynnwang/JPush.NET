using System.Runtime.Serialization;

namespace ifunction.JPush
{
    /// <summary>
    /// Enum PushPlatform
    /// </summary>
    [DataContract]
    public enum PushPlatform
    {
        /// <summary>
        /// The none
        /// </summary>
        [EnumMember]
        None = 0,
        /// <summary>
        /// The android
        /// </summary>
        [EnumMember]
        Android = 1,
        /// <summary>
        /// The i os
        /// </summary>
        [EnumMember]
        iOS = 2,
        /// <summary>
        /// The windows phone
        /// </summary>
        [EnumMember]
        WindowsPhone = 4,
        /// <summary>
        /// The android + ios
        /// </summary>
        [EnumMember]
        AndroidAndiOS = Android | iOS,
        /// <summary>
        /// The android + windows phone
        /// </summary>
        [EnumMember]
        AndroidAndWindowsPhone = Android | WindowsPhone,
        /// <summary>
        /// The ios + windows phone
        /// </summary>
        [EnumMember]
        iOSAndWindowsPhone = iOS | WindowsPhone,
        /// <summary>
        /// The android + ios + windows phone
        /// </summary>
        [EnumMember]
        All = 0x100
    }
}
