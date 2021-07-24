using System;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:41
===============================================================*/

namespace MSD
{
	[Serializable]
	public abstract class GenericReference<TValue, TCustomVariable> : GenericReferenceBase
		where TCustomVariable : GenericCustomVariable<TValue>
	{
		[SerializeField]
		private bool _useConstant;

		[SerializeField]
		private TValue _constantValue;

		[SerializeField]
		private TCustomVariable _variable;

		protected GenericReference()
		{
			_useConstant = true;
			_constantValue = default;
			_variable = null;
		}

		protected GenericReference(TValue value)
		{
			_useConstant = true;
			_constantValue = value;
			_variable = null;
		}

		protected GenericReference(TCustomVariable variable)
		{
			_useConstant = false;
			_constantValue = variable != null ? variable.Value : default;
			_variable = variable;
		}

		public TValue Value {
			get { return _useConstant ? _constantValue : _variable.Value; }
		}

		public static implicit operator TValue(GenericReference<TValue, TCustomVariable> reference) => reference.Value;
	}

	public abstract class GenericReferenceBase { }
}
