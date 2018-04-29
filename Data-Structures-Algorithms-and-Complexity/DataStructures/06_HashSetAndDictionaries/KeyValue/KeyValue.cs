using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDictionary
{
    public class KeyValue<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public KeyValue(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }
        public override bool Equals(object other)
        {
            var element = (KeyValue<TKey, TValue>)other;
            if(element==null) return false;
            var equals = object.Equals(this.Key, element.Key) && object.Equals(this.Value, element.Value);
            return equals;
        }
        public override int GetHashCode()
        {
            return this.CombineHashCodes(this.Key.GetHashCode(), this.Value.GetHashCode());
        }
        private int CombineHashCodes(int p1, int p2)
        {
            return ((p1 << 5) + p1) ^ p2;
        }
        public override string ToString()
        {
            return string.Format(" [{0} -> {1}]", this.Key, this.Value);
        }
    }
}
