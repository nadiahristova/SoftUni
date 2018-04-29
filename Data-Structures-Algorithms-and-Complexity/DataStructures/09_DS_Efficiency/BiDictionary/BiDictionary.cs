namespace BiDictionary
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;
    public class BiDictionary<K1, K2, T>
    {
        private Dictionary<K1, Bag<T>> valuesByFirstKey = new Dictionary<K1,Bag<T>>();
        private Dictionary<K2, Bag<T>> valuesBySecondKey = new Dictionary<K2,Bag<T>>();
        private Dictionary<Tuple<K1, K2>, Bag<T>> valuesByBothKeys = new Dictionary<Tuple<K1,K2>,Bag<T>>();

        public void Add(K1 key1, K2 key2, T value)
        {
            this.valuesByFirstKey.AppendValueToKey(key1, value);
            this.valuesBySecondKey.AppendValueToKey(key2, value);
            var tuple = new Tuple<K1, K2>(key1, key2);
            this.valuesByBothKeys.AppendValueToKey(tuple, value);
        }
        public IEnumerable<T> Find(K1 key1, K2 key2)
        {
            var tuple = new Tuple<K1, K2>(key1, key2);
            return this.valuesByBothKeys.GetValuesForKey(tuple);
        }
        public IEnumerable<T> FindByKey1(K1 key1)
        {
            return this.valuesByFirstKey.GetValuesForKey(key1);
        }
        public IEnumerable<T> FindByKey2(K2 key2)
        {
            return this.valuesBySecondKey.GetValuesForKey(key2);
        }
        public bool Remove(K1 key1, K2 key2)
        {
            var tuple = new Tuple<K1, K2>(key1, key2);
            
            if (this.valuesByBothKeys.ContainsKey(tuple))
            {
                IEnumerable<T> bagOfValues = this.valuesByBothKeys[tuple];
                this.valuesByBothKeys.Remove(tuple);

                this.valuesByFirstKey[key1].RemoveMany(bagOfValues);
                this.valuesBySecondKey[key2].RemoveMany(bagOfValues);

                return true;
            }

            return false;
        }
    }
}
