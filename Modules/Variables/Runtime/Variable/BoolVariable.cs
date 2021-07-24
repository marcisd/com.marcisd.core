using System;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:49
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Variables/Bool")]
	public sealed class BoolVariable : BoolCustomVariable,
		IValueChangeObservable<bool>,
		IValueChangeObservable
	{
		[SerializeField]
		private Variable<bool> _variable = new Variable<bool>();

		public event Action<bool> OnValueChanged {
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

		public void SetValueWithoutNotify(bool value) => _variable.SetValueWithoutNotify(value);

		public void Reset() => _variable.Reset();

		public void OnValidate() => _variable.Reset();

		protected override bool IsReadonly => false;

		protected override bool GetValue() => _variable.Value;

		protected override void SetValue(bool value) => _variable.Value = value;
	}
}
