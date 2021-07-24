using System;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:51
===============================================================*/

namespace MSD
{
	[CreateAssetMenu(menuName = "MSD/Core/Variables/String")]
	public sealed class StringVariable : StringCustomVariable,
		IValueChangeObservable<string>,
		IValueChangeObservable
	{
		[SerializeField]
		private Variable<string> _variable = new Variable<string>();

		public event Action<string> OnValueChanged {
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

		public void SetValueWithoutNotify(string value) => _variable.SetValueWithoutNotify(value);

		public void Reset() => _variable.Reset();

		public void OnValidate() => _variable.Reset();

		protected override bool IsReadonly => false;

		protected override string GetValue() => _variable.Value;

		protected override void SetValue(string value) => _variable.Value = value;
	}
}
