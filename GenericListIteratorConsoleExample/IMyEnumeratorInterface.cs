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
	/// 
	/// Copyright (C) 2015 Robin Osborne
	/// This program is free software: you can redistribute it and/or modify
	/// it under the terms of the GNU General Public License as published by
	/// the Free Software Foundation, either version 3 of the License, or
	/// (at your option) any later version.

	/// This program is distributed in the hope that it will be useful,
	/// but WITHOUT ANY WARRANTY; without even the implied warranty of
	/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	/// GNU General Public License for more details.
	/// You should have received a copy of the GNU General Public License
	/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
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
