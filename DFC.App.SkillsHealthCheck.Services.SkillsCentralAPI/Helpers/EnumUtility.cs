using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers
{    /// <summary>
     /// offers a number of encapsulated enum operations 
     /// </summary>
     /// <typeparam name="TSet">The type of the set.</typeparam>
    public static class FromSet<TSet> where TSet : struct, IComparable, IFormattable
    {
        const string Expression = @"([^A-Za-z0-9\.\$])|([A-Z])(?=[A-Z][a-z])|([^\-\$\.0-9])(?=\$?[0-9]+(?:\.[0-9]+)?)|([0-9])(?=[^\.0-9])|([a-z])(?=[A-Z])";
        static Regex _toHumanReadable = new Regex(Expression);

        /// <summary>
        /// Gets the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static TSet Get(string item)
        {
            return Get(item, default(TSet));
        }

        /// <summary>
        /// Gets the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static TSet Get(int item)
        {
            return Get(item.ToString(), default(TSet));
        }

        /// <summary>
        /// Gets the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// the item as in the TSet or the default
        /// </returns>
        public static TSet Get(string item, TSet defaultValue)
        {
            TSet value;
            var result = Enum.TryParse(item, true, out value);
            return result ? value : defaultValue;
        }

        /// <summary>
        /// Gets the 'names' of the items from underlying set.
        /// </summary>
        /// <returns>
        /// a collection of string
        /// </returns>
        public static IEnumerable<string> GetNames()
        {
            return Enum.GetNames(typeof(TSet)).AsSafeList();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns>
        /// a collection of TSet
        /// </returns>
        public static IEnumerable<TSet> GetItems()
        {
            return Enum.GetValues(typeof(TSet)).OfType<TSet>();
        }

        /// <summary>
        /// takes the incoming item in the set and turns it into text
        /// </summary>
        /// <param name="setItem">The set item.</param>
        /// <returns>
        /// a human readable version of the set item
        /// </returns>
        public static string ToText(TSet setItem)
        {
            return _toHumanReadable.Replace(setItem.ToString(), "$2$3$4$5 ");
        }
    }
}
