using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    internal interface IBindableProperty
    {
        event Action<object> ValueChanged;
        string Name { get; }
        object GetValue();
        void SetValue(object value);
        void Freed();
    }
}
