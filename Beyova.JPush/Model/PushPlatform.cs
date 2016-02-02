using System.Runtime.Serialization;

namespace Beyova.JPush
{
    /// <summary>
    /// Enum PushPlatform
    /// </summary>
    public enum PushPlatform
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,
        /// <summary>
        /// The android
        /// </summary>
        Android = 1,
        /// <summary>
        /// The i os
        /// </summary>
        iOS = 2,
        /// <summary>
        /// The windows phone
        /// </summary>
        WindowsPhone = 4,
        /// <summary>
        /// The android + ios
        /// </summary>
        AndroidAndiOS = Android | iOS,
        /// <summary>
        /// The android + windows phone
        /// </summary>
        AndroidAndWindowsPhone = Android | WindowsPhone,
        /// <summary>
        /// The ios + windows phone
        /// </summary>
        iOSAndWindowsPhone = iOS | WindowsPhone,
        /// <summary>
        /// The android + ios + windows phone
        /// </summary>
        All = 0x100
    }
}
