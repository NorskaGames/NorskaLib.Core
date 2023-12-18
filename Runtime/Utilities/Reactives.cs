using System;
using System.Collections.Generic;

namespace NorskaLib.Utilities
{
    public class ReactiveReference<C> where C : class
    {
        private C reference;

        public C Get() => reference;

        /// <summary>
        /// NOTE: Values are compared using '==' operator.
        /// </summary>
        /// <typeparam name="C"></typeparam>
        public void Set(C reference)
        {
            if (this.reference == reference)
                return;

            this.reference = reference;
            if (reference == null && IsAssigned)
            {
                IsAssigned = false;
                onUnassigned?.Invoke();
            }
            else
            {
                IsAssigned = true;
                onAssigned?.Invoke(reference);
            }
        }

        /// <summary>
        /// Invoked when Value is changed to not null object reference.
        /// </summary>
        public Action<C> onAssigned;
        /// <summary>
        /// Invoked when Value is changed to null.
        /// </summary>
        public Action onUnassigned;

        /// <summary>
        /// Same as "Value != null", yet it is cached.
        /// </summary>
        public bool IsAssigned { get; private set;}

        public ReactiveReference()
        {
            reference = null;
            IsAssigned = false;
        }

        public ReactiveReference(C initial)
        {
            reference = initial;
            IsAssigned = initial != null;
        }
    }

    public class ReactiveValue<V> where V : struct
    {
        private V value;

        public V Get() => value;

        /// <summary>
        /// NOTE: Values are compared using 'EqualityComparer<V>.Default.Equals()' method.
        /// </summary>
        public void Set(V value)
        {
            if (EqualityComparer<V>.Default.Equals(this.value, value))
                return;

            this.value = value;
            onChanged?.Invoke(value);
        }

        public Action<V> onChanged;

        public ReactiveValue()
        {
            value = default(V);
        }

        public ReactiveValue(V initial)
        {
            value = initial;
        }
    }
}