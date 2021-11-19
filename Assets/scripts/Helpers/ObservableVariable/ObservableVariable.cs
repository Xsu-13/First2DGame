using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ObservableVariable<T> : Iobservable
    {
        public event Action<object> OnChanged;

        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChanged?.Invoke(value);
            }
        }


        public ObservableVariable(T value=default)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

