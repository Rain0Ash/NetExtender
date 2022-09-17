// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NetExtender.Utilities.EntityFrameworkCore
{
    public static class ChangeTrackerUtilities
    {
        public static IEnumerable<EntityEntry> AsEnumerable(this ChangeTracker tracker)
        {
            if (tracker is null)
            {
                throw new ArgumentNullException(nameof(tracker));
            }

            return tracker.Entries();
        }
        
        public static IEnumerable<EntityEntry<T>> AsEnumerable<T>(this ChangeTracker tracker) where T : class
        {
            if (tracker is null)
            {
                throw new ArgumentNullException(nameof(tracker));
            }

            return tracker.Entries<T>();
        }

        public static IEnumerator<EntityEntry> GetEnumerator(this ChangeTracker tracker)
        {
            if (tracker is null)
            {
                throw new ArgumentNullException(nameof(tracker));
            }

            return tracker.Entries().GetEnumerator();
        }
    }
}