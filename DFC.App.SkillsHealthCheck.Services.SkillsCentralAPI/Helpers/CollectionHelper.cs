using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers
{
    /// <summary>
    /// removes the need to 'to list' prior to 'for eaching'
    /// implements a null patterned to safe list
    /// </summary>
    public static class Collection
    {

        /// <summary>
        /// To safe list, null pattern safety
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICollection<T> Empty<T>()
        {
            return new List<T>();
        }

        /// <summary>
        /// Empties the and read only.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// an empty readonly safe collection
        /// </returns>
        public static IReadOnlyCollection<T> EmptyAndReadOnly<T>()
        {
            return new List<T>().SafeReadOnlyList();
        }

        /// <summary>
        /// As a safe list, null pattern safety
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        /// a safe collection
        /// </returns>
        public static ICollection<T> AsSafeList<T>(this IEnumerable<T> list)
        {
            return list.SafeList();
        }

        /// <summary>
        /// As a safe readonly list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        /// a readonly safe collection
        /// </returns>
        public static IReadOnlyCollection<T> AsSafeReadOnlyList<T>(this IEnumerable<T> list)
        {
            return list.SafeReadOnlyList();
        }

        /// <summary>
        /// Safe list, the private implemntation of null coalescing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        /// a safe list
        /// </returns>
        private static List<T> SafeList<T>(this IEnumerable<T> list)
        {
            return (list ?? new List<T>()).ToList();
        }

        /// <summary>
        /// Safes the read only list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        /// a safe readonly list
        /// </returns>
        private static ReadOnlyCollection<T> SafeReadOnlyList<T>(this IEnumerable<T> list)
        {
            return new ReadOnlyCollection<T>(list.SafeList());
        }
    }
}
