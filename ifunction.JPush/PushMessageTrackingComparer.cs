using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ifunction.JPush
{
    /// <summary>
    /// Class PushMessageTracking.
    /// </summary>
    [DataContract]
    public class PushMessageTrackingComparer : IEqualityComparer<PushMessageTracking>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
        /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(PushMessageTracking x, PushMessageTracking y)
        {
            return x.MessageId.Equals(y.MessageId);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public int GetHashCode(PushMessageTracking obj)
        {
            return obj.MessageId.GetHashCode();
        }
    }
}
