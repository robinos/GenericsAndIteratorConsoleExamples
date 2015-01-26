using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericListIteratorConsoleExample
{
	/// <summary>
	/// The IMyEnumeratorInterface(T) is a basic iterator interface for this example. It
	/// requires the implementation of the Current property and the methods MoveNext,
	/// Reset and Remove.
	/// 
	/// The iterator interface normally used in C# is IEnumerator(T). These methods are meant
	/// to closely mirror IEnumerator(T) with the exception of Dispose which is not required
	/// in this example and Remove which has been added for demonstration purposes.
	/// 
	/// Author: Robin Osborne
	/// Date: 2015-01-25
	/// Version: 0.1.0
	/// </summary>
	/// <typeparam name="T">The type of object to iterate over.</typeparam>
	public interface IMyEnumeratorInterface<T>
	{
		T Current{ get; }
		bool MoveNext();
		void Reset();
		void Remove();
	}
}
