// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;

namespace NetExtender.Utilities.Delegates
{
	public readonly struct ValueAction : IValueAction<ValueAction>
	{
		public static implicit operator Action?(ValueAction value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction first, ValueAction second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction first, ValueAction second)
		{
			return !(first == second);
		}

		public Action? Delegate { get; }

		public Int32 Arguments
		{
			get
			{
				return 0;
			}
		}

		public MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public ValueAction(Action @delegate)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
		}

		public Object?[] GetArguments()
		{
			return Array.Empty<Object?>();
		}

		public void Invoke()
		{
			Delegate?.Invoke();
		}

		Object? IValueDelegate<ValueAction>.Invoke()
		{
			Invoke();
			return null;
		}

		public override Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override Boolean Equals(Object? other)
		{
			return other is ValueAction @delegate && Equals(@delegate);
		}

		public Boolean Equals(ValueAction other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			get
			{
				throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
			}
			set
			{
				throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
			}
		}
	}

	public  struct ValueAction<T> : IValueAction<ValueAction<T>>
	{
		public static implicit operator Action<T>?(ValueAction<T> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T> first, ValueAction<T> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T> first, ValueAction<T> second)
		{
			return !(first == second);
		}

		public Action<T>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 1;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}

		public T Argument { get; set; }

		public ValueAction(Action<T> @delegate, T argument)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			Argument = argument;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { Argument } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(Argument);
		}

		readonly Object? IValueDelegate<ValueAction<T>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => Argument,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						Argument = (T) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2> : IValueAction<ValueAction<T1, T2>>
	{
		public static implicit operator Action<T1, T2>?(ValueAction<T1, T2> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2> first, ValueAction<T1, T2> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2> first, ValueAction<T1, T2> second)
		{
			return !(first == second);
		}

		public Action<T1, T2>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 2;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }

		public ValueAction(Action<T1, T2> @delegate, T1 first, T2 second)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3> : IValueAction<ValueAction<T1, T2, T3>>
	{
		public static implicit operator Action<T1, T2, T3>?(ValueAction<T1, T2, T3> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3> first, ValueAction<T1, T2, T3> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3> first, ValueAction<T1, T2, T3> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 3;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }

		public ValueAction(Action<T1, T2, T3> @delegate, T1 first, T2 second, T3 third)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4> : IValueAction<ValueAction<T1, T2, T3, T4>>
	{
		public static implicit operator Action<T1, T2, T3, T4>?(ValueAction<T1, T2, T3, T4> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4> first, ValueAction<T1, T2, T3, T4> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4> first, ValueAction<T1, T2, T3, T4> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 4;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4> @delegate, T1 first, T2 second, T3 third, T4 fourth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5> : IValueAction<ValueAction<T1, T2, T3, T4, T5>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5>?(ValueAction<T1, T2, T3, T4, T5> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5> first, ValueAction<T1, T2, T3, T4, T5> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5> first, ValueAction<T1, T2, T3, T4, T5> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 5;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6>?(ValueAction<T1, T2, T3, T4, T5, T6> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6> first, ValueAction<T1, T2, T3, T4, T5, T6> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6> first, ValueAction<T1, T2, T3, T4, T5, T6> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 6;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7>?(ValueAction<T1, T2, T3, T4, T5, T6, T7> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7> first, ValueAction<T1, T2, T3, T4, T5, T6, T7> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7> first, ValueAction<T1, T2, T3, T4, T5, T6, T7> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 7;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7, T8> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7, T8>?(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7, T8>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 8;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7, T8> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7, T8> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>?(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 9;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>?(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 10;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>?(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 11;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>?(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 12;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>?(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 13;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }
		public T13 Thirteenth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
			Thirteenth = thirteenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					12 => Thirteenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					case 12:
						Thirteenth = (T13) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>?(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 14;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }
		public T13 Thirteenth { get; set; }
		public T14 Fourteenth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
			Thirteenth = thirteenth;
			Fourteenth = fourteenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					12 => Thirteenth,
					13 => Fourteenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					case 12:
						Thirteenth = (T13) value!;
						return;
					case 13:
						Fourteenth = (T14) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>?(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 15;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }
		public T13 Thirteenth { get; set; }
		public T14 Fourteenth { get; set; }
		public T15 Fifteenth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
			Thirteenth = thirteenth;
			Fourteenth = fourteenth;
			Fifteenth = fifteenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					12 => Thirteenth,
					13 => Fourteenth,
					14 => Fifteenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					case 12:
						Thirteenth = (T13) value!;
						return;
					case 13:
						Fourteenth = (T14) value!;
						return;
					case 14:
						Fifteenth = (T15) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : IValueAction<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>
	{
		public static implicit operator Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>?(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> first, ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> second)
		{
			return !(first == second);
		}

		public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 16;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }
		public T13 Thirteenth { get; set; }
		public T14 Fourteenth { get; set; }
		public T15 Fifteenth { get; set; }
		public T16 Sixteenth { get; set; }

		public ValueAction(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
			Thirteenth = thirteenth;
			Fourteenth = fourteenth;
			Fifteenth = fifteenth;
			Sixteenth = sixteenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth, Sixteenth } : Array.Empty<Object?>();
		}

		public readonly void Invoke()
		{
			Delegate?.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth, Sixteenth);
		}

		readonly Object? IValueDelegate<ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>.Invoke()
		{
			Invoke();
			return null;
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					12 => Thirteenth,
					13 => Fourteenth,
					14 => Fifteenth,
					15 => Sixteenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					case 12:
						Thirteenth = (T13) value!;
						return;
					case 13:
						Fourteenth = (T14) value!;
						return;
					case 14:
						Fifteenth = (T15) value!;
						return;
					case 15:
						Sixteenth = (T16) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public readonly struct ValueFunc<TResult> : IValueFunc<ValueFunc<TResult>, TResult>
	{
		public static implicit operator Func<TResult>?(ValueFunc<TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<TResult> first, ValueFunc<TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<TResult> first, ValueFunc<TResult> second)
		{
			return !(first == second);
		}

		public Func<TResult>? Delegate { get; }

		public Int32 Arguments
		{
			get
			{
				return 0;
			}
		}

		public MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public ValueFunc(Func<TResult> @delegate)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
		}

		public Object?[] GetArguments()
		{
			return Array.Empty<Object?>();
		}

		public TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke() : default!;
		}

		Object? IValueDelegate<ValueFunc<TResult>>.Invoke()
		{
			return Invoke();
		}

		public override Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override Boolean Equals(Object? other)
		{
			return other is ValueFunc<TResult> @delegate && Equals(@delegate);
		}

		public Boolean Equals(ValueFunc<TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			get
			{
				throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
			}
			set
			{
				throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
			}
		}
	}

	public  struct ValueFunc<T, TResult> : IValueFunc<ValueFunc<T, TResult>, TResult>
	{
		public static implicit operator Func<T, TResult>?(ValueFunc<T, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T, TResult> first, ValueFunc<T, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T, TResult> first, ValueFunc<T, TResult> second)
		{
			return !(first == second);
		}

		public Func<T, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 1;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}

		public T Argument { get; set; }

		public ValueFunc(Func<T, TResult> @delegate, T argument)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			Argument = argument;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { Argument } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(Argument) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => Argument,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						Argument = (T) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, TResult> : IValueFunc<ValueFunc<T1, T2, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, TResult>?(ValueFunc<T1, T2, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, TResult> first, ValueFunc<T1, T2, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, TResult> first, ValueFunc<T1, T2, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 2;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }

		public ValueFunc(Func<T1, T2, TResult> @delegate, T1 first, T2 second)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, TResult> : IValueFunc<ValueFunc<T1, T2, T3, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, TResult>?(ValueFunc<T1, T2, T3, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, TResult> first, ValueFunc<T1, T2, T3, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, TResult> first, ValueFunc<T1, T2, T3, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 3;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }

		public ValueFunc(Func<T1, T2, T3, TResult> @delegate, T1 first, T2 second, T3 third)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, TResult>?(ValueFunc<T1, T2, T3, T4, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, TResult> first, ValueFunc<T1, T2, T3, T4, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, TResult> first, ValueFunc<T1, T2, T3, T4, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 4;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, TResult>?(ValueFunc<T1, T2, T3, T4, T5, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, TResult> first, ValueFunc<T1, T2, T3, T4, T5, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, TResult> first, ValueFunc<T1, T2, T3, T4, T5, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 5;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 6;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 7;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 8;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 9;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 10;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 11;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 12;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 13;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }
		public T13 Thirteenth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
			Thirteenth = thirteenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					12 => Thirteenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					case 12:
						Thirteenth = (T13) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 14;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }
		public T13 Thirteenth { get; set; }
		public T14 Fourteenth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
			Thirteenth = thirteenth;
			Fourteenth = fourteenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					12 => Thirteenth,
					13 => Fourteenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					case 12:
						Thirteenth = (T13) value!;
						return;
					case 13:
						Fourteenth = (T14) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 15;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }
		public T13 Thirteenth { get; set; }
		public T14 Fourteenth { get; set; }
		public T15 Fifteenth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
			Thirteenth = thirteenth;
			Fourteenth = fourteenth;
			Fifteenth = fifteenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					12 => Thirteenth,
					13 => Fourteenth,
					14 => Fifteenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					case 12:
						Thirteenth = (T13) value!;
						return;
					case 13:
						Fourteenth = (T14) value!;
						return;
					case 14:
						Fifteenth = (T15) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}

	public  struct ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> : IValueFunc<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>, TResult>
	{
		public static implicit operator Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>?(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> value)
		{
			return value.Delegate;
		}

		public static Boolean operator ==(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> first, ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> second)
		{
			return !(first == second);
		}

		public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>? Delegate { get; }

		public readonly Int32 Arguments
		{
			get
			{
				return 16;
			}
		}

		public readonly MethodInfo? Method
		{
			get
			{
				return Delegate?.Method;
			}
		}


		public T1 First { get; set; }
		public T2 Second { get; set; }
		public T3 Third { get; set; }
		public T4 Fourth { get; set; }
		public T5 Fifth { get; set; }
		public T6 Sixth { get; set; }
		public T7 Seventh { get; set; }
		public T8 Eighth { get; set; }
		public T9 Ninth { get; set; }
		public T10 Tenth { get; set; }
		public T11 Eleventh { get; set; }
		public T12 Twelfth { get; set; }
		public T13 Thirteenth { get; set; }
		public T14 Fourteenth { get; set; }
		public T15 Fifteenth { get; set; }
		public T16 Sixteenth { get; set; }

		public ValueFunc(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> @delegate, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
		{
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			First = first;
			Second = second;
			Third = third;
			Fourth = fourth;
			Fifth = fifth;
			Sixth = sixth;
			Seventh = seventh;
			Eighth = eighth;
			Ninth = ninth;
			Tenth = tenth;
			Eleventh = eleventh;
			Twelfth = twelfth;
			Thirteenth = thirteenth;
			Fourteenth = fourteenth;
			Fifteenth = fifteenth;
			Sixteenth = sixteenth;
		}

		public readonly Object?[] GetArguments()
		{
			return Delegate is not null ? new Object?[] { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth, Sixteenth } : Array.Empty<Object?>();
		}

		public readonly TResult Invoke()
		{
			return Delegate is not null ? Delegate.Invoke(First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth, Sixteenth) : default!;
		}

		readonly Object? IValueDelegate<ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>>.Invoke()
		{
			return Invoke();
		}

		public override readonly Int32 GetHashCode()
		{
			return Delegate?.GetHashCode() ?? 0;
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other is ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> @delegate && Equals(@delegate);
		}

		public readonly Boolean Equals(ValueFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> other)
		{
			return Equals(Delegate, other.Delegate);
		}

		public override readonly String? ToString()
		{
			return Delegate?.ToString();
		}

		public Object? this[Int32 argument]
		{
			readonly get
			{
				return argument switch
				{
					0 => First,
					1 => Second,
					2 => Third,
					3 => Fourth,
					4 => Fifth,
					5 => Sixth,
					6 => Seventh,
					7 => Eighth,
					8 => Ninth,
					9 => Tenth,
					10 => Eleventh,
					11 => Twelfth,
					12 => Thirteenth,
					13 => Fourteenth,
					14 => Fifteenth,
					15 => Sixteenth,
					_ => throw new ArgumentOutOfRangeException(nameof(argument), argument, null)
				};
			}
			set
			{
				switch(argument)
				{
					case 0:
						First = (T1) value!;
						return;
					case 1:
						Second = (T2) value!;
						return;
					case 2:
						Third = (T3) value!;
						return;
					case 3:
						Fourth = (T4) value!;
						return;
					case 4:
						Fifth = (T5) value!;
						return;
					case 5:
						Sixth = (T6) value!;
						return;
					case 6:
						Seventh = (T7) value!;
						return;
					case 7:
						Eighth = (T8) value!;
						return;
					case 8:
						Ninth = (T9) value!;
						return;
					case 9:
						Tenth = (T10) value!;
						return;
					case 10:
						Eleventh = (T11) value!;
						return;
					case 11:
						Twelfth = (T12) value!;
						return;
					case 12:
						Thirteenth = (T13) value!;
						return;
					case 13:
						Fourteenth = (T14) value!;
						return;
					case 14:
						Fifteenth = (T15) value!;
						return;
					case 15:
						Sixteenth = (T16) value!;
						return;
					default:
						throw new ArgumentOutOfRangeException(nameof(argument), argument, null);
				}
			}
		}
	}
}