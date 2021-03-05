using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Guids;

namespace LandWind.Blog.Application.Authorize.OAuth
{
    public class StateManager
    {
        private static readonly ConcurrentDictionary<string, DateTime> _states = new ConcurrentDictionary<string, DateTime>(); 
        private static StateManager _instance = null;
        private static readonly object _lockObj = new object();
       
        public IGuidGenerator GuidGenerator { get; set; }
        protected StateManager() => GuidGenerator = SimpleGuidGenerator.Instance;
        public static StateManager Instance
        {
            get {
                lock (_lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new StateManager();
                    }
                    return _instance;
                }
            }
        }

        public string Get()
        {
            var state = GuidGenerator.Create().ToString();
            _states.TryAdd(state, DateTime.Now);

            return state;
        }

        public static bool IsExist(string state)
        {
            if (!_states.ContainsKey(state))
                return false;

            if (DateTime.Now.Subtract(_states[state]).TotalMinutes > 3)
            {
                _states.TryRemove(state, out _);
                return false;
            }

            return false;
        }

        public static void Remove(string state) => _states.TryRemove(state, out _);

    }
}
