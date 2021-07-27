using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       20/09/2019 19:08
===============================================================*/

namespace MSD
{
	/// <summary>
	/// Allows serialization of interfaces that are implemented by UnityEngine.Object classes.
	/// </summary>
	public abstract class SerializableInterface<T> : SerializableInterface where T : class
	{
		private static readonly string DEBUG_PREPEND = $"[{nameof(SerializableInterface)}]";

		public SerializableInterface(T value)
		{
			Value = value;
		}

		public T Value {
			get => _object as T;
			set {
				if (value is Object objectValue) {
					_object = objectValue;
				} else {
					Debugger.LogError(DEBUG_PREPEND, "Cannot assign an object that's not of type UnityEngine.Object!");
				}
			}
		}

		public static implicit operator T(SerializableInterface<T> serializableInterface)
		{
			return serializableInterface.Value;
		}
	}

	public abstract class SerializableInterface
	{
		[SerializeField]
		protected Object _object = null;

		public static implicit operator Object(SerializableInterface serializableInterface)
		{
			return serializableInterface._object;
		}
	}
}
