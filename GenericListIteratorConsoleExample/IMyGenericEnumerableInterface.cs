using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericListIteratorConsoleExample
{
	/// <summary>
	/// IMyGenericEnumerableInterface(T) is a basic collection interface for our example
	/// that requires the implementation of a GetMyEnumerator method that returns a
	/// IMyEnumeratorInterface(T).
	/// 
	/// The collection interface normally used in C# is IEnumerable(T).
	/// 
	/// Author: Robin Osborne
	/// Date: 2015-01-25
	/// Version: 0.1.0
	/// </summary>
	/// <typeparam name="T">The type of object this list will hold.</typeparam>
	public interface IMyGenericEnumerableInterface<T>
	{
		IMyEnumeratorInterface<T> GetMyEnumerator();
	}
}
