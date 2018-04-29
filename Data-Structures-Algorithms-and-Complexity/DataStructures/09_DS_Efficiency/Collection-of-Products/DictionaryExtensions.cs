using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace Collection_of_Products
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Ensures the specified key exists in the dictionary.
        /// If the key does not exist, it is mapped to a new empty value.
        /// </summary>
        public static void EnsureKeyExists<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, new TValue());
            }
        }

        /// <summary>
        /// Appends a new value to the collection of values mapped to the specified
        /// dictionary key. If the collection does not exists for the specified key,
        /// a new empty collection is first created and mapped to this key.
        /// </summary>
        /// <param name="key">The key that holds a collection of values</param>
        /// <param name="value">The value to be added to the collection for this key</param>
        public static void AppendValueToKey<TKey, TCollection, TValue>
            (this IDictionary<TKey, TCollection> dict, TKey key, TValue value)
            where TCollection : ICollection<TValue>, new()
        {
            TCollection collection;
            if (!dict.TryGetValue(key, out collection))
            {
                collection = new TCollection();
                dict.Add(key, collection);
            }

            collection.Add(value);
        }

        /// <summary>
        /// Appends a new value to the inner ordered multidictionary mapped to the specified
        /// dictionary key. If the ordered multidictionary does not exists for the specified key,
        /// a new empty ordered multidictionary is first created and mapped to this key.
        /// In order to work the extension method needs Wintellect PowerCollections to be installed.
        /// </summary>
        /// <param name="key">The key that holds an ordered multidictionary</param>
        /// <param name="innerKey">The key that holds a collection of ordered values</param>
        /// <param name="value">The value to be added to the inner ordered multidictionary for this innerKey</param>
        public static void AppendValueToInnerMultiDictionary<TKey, TInnerKey, TValue>
            (this IDictionary<TKey, OrderedMultiDictionary<TInnerKey, TValue>> dict, TKey key, TInnerKey innerKey, TValue value)
        {
            OrderedMultiDictionary<TInnerKey, TValue> multiDictionary;
            if (!dict.TryGetValue(key, out multiDictionary))
            {
                multiDictionary = new OrderedMultiDictionary<TInnerKey, TValue>(true);
                dict.Add(key, multiDictionary);
            }

            multiDictionary.Add(innerKey, value);
        }
    }
}
