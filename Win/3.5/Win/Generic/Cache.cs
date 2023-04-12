using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

#if VER4
using System.Threading.Tasks;
#endif

namespace MControl.Generic
{
    internal class CacheTimeout
    {
        public DateTime LastAccess;
        public readonly int Timeout;

        public CacheTimeout(int timeout)
        {
            LastAccess = DateTime.UtcNow;
            Timeout = timeout;
        }

        public void Reset()
        {
            LastAccess= DateTime.UtcNow;
        }

        public bool IsTimeout()
        {
            return LastAccess.AddMinutes(Timeout) < DateTime.UtcNow; 
        }
    }


    ///// <summary>
    ///// Used to create a cache object of type Value with a string key.
    ///// </summary>
    ///// <typeparam name="Value">Type of object to hold in the cache.</typeparam>
    //internal class Cache<Value> : Cache<string, Value>
    //{
    //    internal Cache(int timeout) : base(timeout)
    //    {
    //    }
    //}

    /// <summary>
    /// A cache object used to store key value pairs until a timeout has expired.
    /// </summary>
    /// <typeparam name="Key">Type of the key for the class.</typeparam>
    /// <typeparam name="Value">Type of the value for the class.</typeparam>
    public class NetCache<Key, Value>
    {
        #region Fields

        private DateTime _nextServiceTime = DateTime.MinValue;
        private readonly Dictionary<Key, Value> _internalCache;
        private readonly Dictionary<Key, CacheTimeout> _lastAccessed;

        // The last time this process serviced the cache file.
        private readonly int _timeout;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs the cache clearing key value pairs after the timeout period 
        /// specified in minutes.
        /// </summary>
        /// <param name="timeout">Number of minutes to hold items in the cache for.</param>
        public NetCache(int timeout)
        {
            _internalCache = new Dictionary<Key, Value>();
            _lastAccessed = new Dictionary<Key, CacheTimeout>();
            _timeout = timeout;
        }

        #endregion

        #region Internal Members

        private void Reset(Key key)
        {
            if (_lastAccessed.ContainsKey(key))
            {
                _lastAccessed[key].Reset();
            }
        }

        /// <summary>
        /// Removes the specified key from the cache.
        /// </summary>
        /// <param name="key">Key to be removed.</param>
        public void Remove(Key key)
        {
            if (_internalCache.ContainsKey(key))
                _internalCache.Remove(key);
        }

        /// <summary>
        /// append or update item to cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeout"></param>
        public void Set(Key key, Value value, int timeout)
        {
            lock (this)
            {
                if (Contains(key))
                {
                    _internalCache[key] = value;
                    Reset(key);
                }
                else
                {
                    _internalCache.Add(key, value);
                    _lastAccessed[key] =new CacheTimeout(timeout);
                }
            }
        }

        /// <summary>
        /// Returns the value associated with the key.
        /// </summary>
        /// <param name="key">Key of the value being requested.</param>
        /// <returns>Value or null if not found.</returns>
        public Value this[Key key]
        {
            get
            {
                Value value;
                lock (this)
                {
                    if (_internalCache.TryGetValue(key, out value))
                        Reset(key);
                }
                CheckIfServiceRequired();
                return value;
            }
            set
            {
                lock (this)
                {
                    if (Contains(key))
                    {
                        _internalCache[key] = value;
                        Reset(key);
                    }
                    else
                    {
                        _internalCache.Add(key, value);
                        _lastAccessed[key] =new CacheTimeout(_timeout);
                    }
                }
            }
        }

        /// <summary>
        /// If the key exists in the cache then provide the value in the
        /// value parameter.
        /// </summary>
        /// <param name="key">Key of the value to be retrieved.</param>
        /// <param name="value">Set to the associated value if found.</param>
        /// <returns>True if the key was found in the list, otherwise false.</returns>
        public bool GetTryParse(Key key, out Value value)
        {
            bool result = false;
            if (key != null)
            {
                lock (this)
                {
                    result = _internalCache.TryGetValue(key, out value);
                    if (result)
                        Reset(key);
                }
            }
            else
            {
                value = default(Value);
            }
            CheckIfServiceRequired();
            return result;
        }

        /// <summary>
        /// Determines if the key is available in the cache.
        /// </summary>
        /// <param name="key">Key to be checked.</param>
        /// <returns>True if the key is found, otherwise false.</returns>
        public bool Contains(Key key)
        {
            bool result = false;
            if (key != null)
            {
                lock (this)
                {
                    result = _internalCache.ContainsKey(key);
                    if (result)
                        Reset(key);
                }
            }
            return result;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// If the time has passed the point another check of the cache is needed 
        /// start a thread to check the cache.
        /// </summary>
        private void CheckIfServiceRequired()
        {
            if (_nextServiceTime >= DateTime.UtcNow || _internalCache.Count <= 0) return;
            
            // Set the next service time to a date far in the future
            // to prevent another thread being started.
            _nextServiceTime = DateTime.MaxValue;
#if VER4
            Task.Factory.StartNew(() => ServiceCache(DateTime.UtcNow.AddMinutes(-_timeout)));
#else//elif VER2
            ThreadPool.QueueUserWorkItem(ServiceCache, DateTime.UtcNow.AddMinutes(-_timeout));
#endif
        }

        /// <summary>
        /// The main method of the thread to service the cache. Checks for old items
        /// and removes them.
        /// </summary>
        /// <param name="purgeDate">The date before which items should be removed.</param>
        private void ServiceCache(object purgeDate)
        {
            Queue<Key> purgeKeys = new Queue<Key>();

            // Obtain a list of the keys to be purged.
            lock (this)
            {
#if VER4
                foreach (Key key in
                    _lastAccessed.Keys.Where(key => (DateTime) _lastAccessed[key] < (DateTime) purgeDate))
                {
                    purgeKeys.Enqueue(key);
                }
#else//elif VER2
                foreach (Key key in _lastAccessed.Keys)
                {
                    if (_lastAccessed[key].IsTimeout())// < (DateTime) purgeDate)
                        purgeKeys.Enqueue(key);
                }
#endif
            }

            // Remove the keys from the lists.
            if (purgeKeys.Count > 0)
            {
                while (purgeKeys.Count > 0)
                {
                    Key key = purgeKeys.Dequeue();
                    if (key != null)
                    {
                        lock (this)
                        {
                            if (_lastAccessed[key].IsTimeout())// < (DateTime) purgeDate)
                            {
                                _lastAccessed.Remove(key);
                                _internalCache.Remove(key);
                            }
                        }
                    }
                }
            }

            // Set the next service time to one minute from now.
            _nextServiceTime = DateTime.UtcNow.AddMinutes(1);
        }

        #endregion
    }
}