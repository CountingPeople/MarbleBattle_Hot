using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework
{
    public sealed class UnityLogger : ILogger
    {
        private readonly string Name;
        public UnityLogger(string name)
        {
            IsEnable = true;
            Name = name;
        }
        public bool IsEnable { get; set; }

        public void Log(string message)
        {
            if (IsEnable)
            {
                string msg = message ?? "null";
                Debug.Log($"[{Name}]:{msg}");
            }
        }

        public void Log(object message)
        {
            if (IsEnable)
            {
                string msg = message == null ? "null" : message.ToString();
                Debug.Log($"[{Name}]:{msg}");
            }
        }

        public void LogWarning(string message)
        {
            if (IsEnable)
            {
                string msg = message ?? "null";
                Debug.LogWarning($"[{Name}]:{msg}");
            }
        }

        public void LogWarning(object message)
        {
            if (IsEnable)
            {
                string msg = message == null ? "null" : message.ToString();
                Debug.LogWarning($"[{Name}]:{msg}");
            }
        }

        public void LogError(string message)
        {
            if (IsEnable)
            {
                string msg = message ?? "null";
                Debug.LogError($"[{Name}]:{msg}");
            }
        }

        public void LogError(object message)
        {
            if (IsEnable)
            {
                string msg = message == null ? "null" : message.ToString();
                Debug.LogError($"[{Name}]:{msg}");
            }
        }
    }
}
