using System.Collections.Generic;
using System.Linq;

namespace Students_and_Courses
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Ensures the specified key exists in the dictionary.
        /// If the key does not exist, it is mapped to a new empty value.
        /// </summary>
        public static bool EnsudeKeyExists<Tkey, Tvalue>(
            this IDictionary<Tkey, Tvalue> dict, Tkey key)
            where Tvalue : new()
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, new Tvalue());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Appends a new value to the collection of values mapped to the specified
        /// dictionary key. If the collection does not exists for the specified key,
        /// a new empty collection is first created and mapped to this key.
        /// </summary>
        /// <param name="key">The key that holds a collection of values</param>
        /// <param name="value">The value to be added to the collection for this key</param>
        public static bool AppendValueToKey<TKey, TCollection, TValue>(
            this IDictionary<TKey, TCollection> dict, TKey key, TValue value)
                where TCollection: ICollection<TValue>, new()
        {
            bool contains = true;
            TCollection collection;
            if (!dict.TryGetValue(key, out collection))
            {
                collection = new TCollection();
                dict.Add(key, collection);
            }
            if (collection.Contains(value))
                contains = false;

            //we won't be checking for duplicates because SortedSet class does not allow duplicat values
            collection.Add(value);

            return contains;
        }
    }
}
