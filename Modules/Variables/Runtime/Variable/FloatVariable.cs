using System;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:45
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Variables/Float")]
	public sealed class FloatVariable : FloatCustomVariable,
		IValueChangeObservable<float>,
		IValueChangeObservable
	{
		[SerializeField]
		private Variable<float> _variable = new Variable<float>();

		public event Action<float> OnValueChanged {
			add {
				_variable.OnValueChanged += value;
			}

			remove {
				_variable.OnValueChanged -= value;
			}
		}

		event Action IValueChangeObservable.OnValueChanged {
			add {
				((IValueChangeObservable)_variable).OnValueChanged += value;
			}

			remove {
				((IValueChangeObservable)_variable).OnValueChanged -= value;
			}
		}

		public void AddValue(float value)
		{
			float newValue = _variable.Value + value;
			_variable.Value = newValue;
		}

		public void AddValueWithoutNotify(float value)
		{
			float newValue = _variable.Value + value;
			_variable.SetValueWithoutNotify(newValue);
		}

		public void SetValueWithoutNotify(float value) => _variable.SetValueWithoutNotify(value);

		public void Reset() => _variable.Reset();

		public void OnValidate() => _variable.Reset();

		protected override bool IsReadonly => false;

		protected override float GetValue() => _variable.Value;

		protected override void SetValue(float value) => _variable.Value = value;
	}
}
