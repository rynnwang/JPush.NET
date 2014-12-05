using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ifunction.JPush.V3
{
    /// <summary>
    /// Class PushMessage
    /// </summary>
    [DataContract]
    [KnownType(typeof(PushTypeV3))]
    public class Audience : Dictionary<PushTypeV3, List<string>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Audience"/> class.
        /// </summary>
        public Audience()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Audience"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public Audience(Dictionary<PushTypeV3, List<string>> dictionary)
            : base(dictionary)
        {
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="pushType">Type of the push.</param>
        /// <param name="item">The item.</param>
        public void AddItem(PushTypeV3 pushType, string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                if (!this.ContainsKey(pushType))
                {
                    this.Add(pushType, new List<string>());
                }

                if (!this[pushType].Contains(item))
                {
                    this[pushType].Add(item);
                }
            }
        }
    }
}
