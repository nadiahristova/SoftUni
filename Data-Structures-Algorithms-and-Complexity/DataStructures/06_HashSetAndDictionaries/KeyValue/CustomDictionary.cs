using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomDictionary
{
    public class CustomDictionary<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private LinkedList<KeyValue<TKey, TValue>>[] slots;
        public const int InitialCapacity = 16;
        public const float LoadFactor = 0.65f;
        public int Count { get; set; }
        public int Capacity
        {
            get { return this.slots.Length; }
        }
        public CustomDictionary()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
            this.Count = 0;
        }
        public CustomDictionary(int capacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }
        public void Add(TKey key, TValue value)
        {
            GrowIfNeeded();
            int slotNumber = this.FindSlotNum(key);
            var linkedEl = this.slots[slotNumber];
            if (linkedEl == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach(var el in this.slots[slotNumber])
            {
                if(el.Key.Equals(key))
                {
                    throw new ArgumentException("The key already exists");
                }
            }
            var newEl = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotNumber].AddLast(newEl);
            this.Count++;
        }
        private void GrowIfNeeded()
        {
            if((float)((this.Count + 1) / this.Capacity) >= LoadFactor)
            {
                this.Grow();
            }
        }
        private void Grow()
        {
            var newHashDictionary = new CustomDictionary<TKey, TValue>(2 * this.Capacity);
            foreach(var el in this)
            {
                newHashDictionary.Add(el.Key, el.Value);
            }
            this.slots = newHashDictionary.slots;
            this.Count = newHashDictionary.Count;
        }
        private int FindSlotNum(TKey key)
        {
            int slotNum = Math.Abs(key.GetHashCode()) % this.slots.Length;
            return slotNum;
        }
        public bool AddOrReplace(TKey key, TValue value)
        {
            var slotNum = this.FindSlotNum(key);
            if(this.slots[slotNum] == null)
            {
                this.slots[slotNum] = new LinkedList<KeyValue<TKey, TValue>>();
            }
            foreach(var el in this.slots[slotNum])
            {
                if(el.Key.Equals(key))
                {
                    el.Value = value;
                    return true;
                }
            }

            GrowIfNeeded();
            this.slots[slotNum].AddLast(new KeyValue<TKey, TValue>(key, value));
            this.Count++;
            return false;
        }
        public TValue Get(TKey key)
        {
            var el = this.Find(key);
            if(el == null)
            {
                throw new KeyNotFoundException("No existing key as "+ key);
            }
            return el.Value;
        }
        public KeyValue<TKey, TValue> Find(TKey key)
        {
            var slotNum = this.FindSlotNum(key);
            if (this.slots[slotNum] != null)
            {
                foreach (var element in this.slots[slotNum])
                {
                    if (element.Key.Equals(key))
                    {
                        return element;
                    }
                }
            }

            return null;
        }
        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            var el = this.Find(key);
            if(el != null)
            {
                value = el.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }
        public bool ContainsKey(TKey key)
        {
            var el = this.Find(key);
            return el != null;
        }
        public bool Remove(TKey key)
        {
            int slotNum = this.FindSlotNum(key);
            var els = this.slots[slotNum];
            if(els != null)
            {
                var currEl = els.First;
                while(currEl != null)
                {
                    if(currEl.Value.Key.Equals(key))
                    {
                        els.Remove(currEl);
                        this.Count--;
                        return true;
                    }
                    currEl = currEl.Next;
                }
            }
            return false;
        }
        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
            this.Count = 0;
        }
        public List<KeyValue<TKey, TValue>> ToList()
        {
            var list = new List<KeyValue<TKey, TValue>>();
            foreach(var pair in this)
            {
                list.Add(pair);
            }

            return list;
        }
        public IEnumerable<TKey> Keys
        {
            get
            {
                return this.Select(el => el.Key);
            }
        }
        public IEnumerable<TValue> Values
        {
            get
            {
                return this.Select(el => el.Value);
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach(var els in this.slots)
            {
                if(els != null)
                {
                    foreach(var el in els)
                    {
                        yield return el;
                    }
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
