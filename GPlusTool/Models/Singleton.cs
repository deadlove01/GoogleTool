using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPlusTool.Models
{
    public class Singleton<T> where T : new()
    {
        private static T _instance;
        private static object syncRoot = new Object();

        private Singleton() { }
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(syncRoot)
                    {
                        if (_instance == null)
                            _instance = new T();
                    }
                    
                }
                  

                return _instance;
            }
            private set { }

        }
    }
}
