using System;

namespace Framework
{
    /// <summary>
    /// 绑定数据类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BindableProperty<T> 
    {
        private Action<T, T> _onValueChanged;

        private Action<object> _valueChanged;

        private bool _isEnable = true;
        /// <summary>
        /// 是否启动绑定数据
        /// </summary>
        public bool IsEnable { get => _isEnable; set => _isEnable = value; }
        private bool _isForce = false;
        /// <summary>
        /// 强行刷新
        /// </summary>
        public bool IsForce { get => _isForce; set => _isForce = value; }

        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (IsForce || !Equals(_value, value))
                {
                    T old = _value;
                    _value = value;
                    if (IsEnable)
                    {
                        ValueChanged(old, _value);
                    }
                }
            }
        }

        public string Name { get; private set; }

        public BindableProperty() : this(default) { }
        public BindableProperty(T initValue) { this._value = initValue; }

        public override string ToString()
        {
            return (Value != null ? Value.ToString() : "null");
        }


        public void Freed()
        {
            _onValueChanged = null;
            _valueChanged = null;
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <returns></returns>
        public BindableProperty<T> Publish()
        {
            ValueChanged(_value, _value);
            return this;
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="action">Action(先前的值，现在的值)</param>
        /// <returns></returns>
        public BindableProperty<T> Subscribe(Action<T, T> action)
        {
            _onValueChanged += action;
            return this;
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="action">Action(先前的值，现在的值)</param>
        /// <returns></returns>
        public BindableProperty<T> Unsubscribe(Action<T, T> action)
        {
            _onValueChanged -= action;
            return this;
        }

        #region private

        private void ValueChanged(T oldValue, T newValue)
        {
            try
            {

                _valueChanged?.Invoke(newValue);
                _onValueChanged?.Invoke(oldValue, newValue);
            }
            catch (Exception ex)
            {
                GameApp.Instance.LogError(ex);
            }
        }

        #endregion

        #region operator

        public static implicit operator T(BindableProperty<T> prop)
        {
            return prop.Value;
        }
        public static implicit operator BindableProperty<T>(T value)
        {
            BindableProperty<T> temp = new BindableProperty<T>();
            temp.Value = value;
            return temp;
        }
        public static bool operator !=(BindableProperty<T> s1, BindableProperty<T> s2)
        {
            if (object.Equals(s1, null) || object.Equals(s2, null))
            {
                return true;
            }
            else if (object.Equals(s1, null) && object.Equals(s2, null))
            {
                return false;
            }
            else
            {
                return s1.Value.Equals(s2.Value);
            }
        }
        public static bool operator ==(BindableProperty<T> s1, BindableProperty<T> s2)
        {
            return !(s1 != s2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        #endregion

    }
}
