using System;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:43
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Variables/Int")]
	public sealed class IntVariable : IntCustomVariable,
		IValueChangeObservable<int>,
		IValueChangeObservable
	{
		[SerializeField]
		private Variable<int> _variable = new Variable<int>();

		public event Action<int> OnValueChanged {
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

		public void AddValue(int value)
		{
			int newValue = _variable.Value + value;
			_variable.Value = newValue;
		}

		public void AddValueWithoutNotify(int value)
		{
			int newValue = _variable.Value + value;
			_variable.SetValueWithoutNotify(newValue);
		}

		public void SetValueWithoutNotify(int value) => _variable.SetValueWithoutNotify(value);

		public void Reset() => _variable.Reset();

		public void OnValidate() => _variable.Reset();

		protected override bool IsReadonly => false;

		protected override int GetValue() => _variable.Value;

		protected override void SetValue(int value) => _variable.Value = value;
	}
}
