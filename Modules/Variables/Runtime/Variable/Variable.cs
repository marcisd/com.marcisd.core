using System;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:39
===============================================================*/

namespace MSD
{
	[Serializable]
	public class Variable<T> : VariableBase,
		IValueChangeObservable<T>,
		IValueChangeObservable
	{
		[SerializeField]
		private T _initialValue = default;

		[field: NonSerialized]
		private T RuntimeValue { get; set; } = default;

		public event Action<T> OnValueChanged = delegate { };

		private event Action NonGenericOnValueChanged = delegate { };

		event Action IValueChangeObservable.OnValueChanged {
			add {
				NonGenericOnValueChanged += value;
			}

			remove {
				NonGenericOnValueChanged -= value;
			}
		}

		public T Value {
			get => RuntimeValue;
			set => SetValue(value);
		}

		public void SetValue(T value)
		{
			if (!EqualityComparer<T>.Default.Equals(RuntimeValue, value)) {
				RuntimeValue = value;
				OnValueChanged?.Invoke(RuntimeValue);
				NonGenericOnValueChanged?.Invoke();
			}
		}

		public void SetValueWithoutNotify(T value)
		{
			RuntimeValue = value;
		}

		public void Reset()
		{
			RuntimeValue = _initialValue;
		}

		internal override object GetRuntimeValue()
		{
			return RuntimeValue;
		}
	}

	public abstract class VariableBase
	{
		internal abstract object GetRuntimeValue();
	}
}
