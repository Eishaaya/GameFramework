using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public class Indextionary<T, TValue>
    {
        public int Count { get => keyList.Count; }

        Dictionary<T, TValue> dictionary;
        List<T> keyList;
        public Indextionary()
        {
            dictionary = new Dictionary<T, TValue>();
            keyList = new List<T>();
        }

        public TValue this[T key]
        {
            get => dictionary[key];
            set
            {
                if (dictionary.TryAdd(key, value))
                {
                    keyList.Add(key);
                }
                else
                {
                    dictionary[key] = value;
                }
            }
        }
        public TValue this[Index index]
        {
            get => dictionary[keyList[index]];
            set
            {
                this[keyList[index]] = value;
            }
        }

        public void Add(T key, TValue value)
        {
            dictionary.Add(key, value);
            keyList.Add(key);
        }

        public void AddRange(params KeyValuePair<T, TValue>[] pairs)
        {
            foreach (var pair in pairs)
            {
                Add(pair.Key, pair.Value);
            }
        }

        internal void Switch(KeyValuePair<T, TValue>[] keyValuePairs)
        {
            foreach (var pair in keyValuePairs)
            {
                this[pair.Key] = pair.Value;
            }
        }
    }
}
