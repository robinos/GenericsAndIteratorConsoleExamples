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
	/// <typeparam name="T">The type of object this list will hold.</typeparam>
	public interface IMyGenericEnumerableInterface<T>
	{
		IMyEnumeratorInterface<T> GetMyEnumerator();
	}
}
