using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericListEnumeratorConsoleExample
{
	/// <summary>
	/// The GenericList(T) class implements the IEnumerable(T) interface and therefore
	/// must implement a method by the name of GetEnumerable that returns an object of
	/// the IEnumerator(T) type. It is also required to implicity implement the method
	/// IEnumerable.GetEnumerable that returns an object of the IEnumerator type. This
	/// is because IEnumerable(T) implements IEnumerable.
	/// Once the GenericList is initialised in its constructor, T is set permanently
	/// (for that instance of GenericList) to that specific object type. If a
	/// GenericList(int) is created, objects of type string will not be able to be
	/// added to the list.
	/// 
	/// GenericList uses an array of default size 10 to hold objects of the given type.
	/// When the add method is called the current size is checked and if more size is needed,
	/// the size of the array is doubled.
	/// 
	/// -Constructor-
	/// GenericList() - initialises the internal array to a default capacity and current
	///		size to 0 (0 current elements)
	/// 
	/// -Methods-
	/// Get - Gets an item from the list in the given position or throws an exception if
	///		out of bounds
	/// Set - Sets an item into the list at the given position or throws an exception if
	///		out of bounds
	/// Add - Adds an item to the list, doubling the current list length if the end of the
	///		previously defined size has been reached
	/// GetEnumerator - Returns an IEnumerator(T) object that functions as defined with
	///		yield return (Required for IEnumerable(T))
	///	IEnumerable.GetEnumerator - Returns an IEnumerator object by calling the other
	///		GetEnumerator method. (Required for IEnumerable which is implemented by IEnumerable(T))
	/// 
	/// Author: Robin Osborne
	/// Date: 2015-02-05
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
	public class GenericList<T> : IEnumerable<T>
	{
		private const int defaultCapacity = 10;
		internal T[] array;
		private int size;

		/// <summary>
		/// The constructor of GenericList initialises its internal array to a default
		/// capacity and initialises its current size to 0.
		/// </summary>
		public GenericList()
		{
			array = new T[defaultCapacity];
			size = 0;
		}

		/// <summary>
		/// The Get method gets an item of type T from the array at position i if that position
		/// is within the bounds of the array and otherwise casts an IndexOutOfRangeException.
		/// </summary>
		/// <param name="i">an array index with the position of the desired item</param>
		/// <returns>an item of type T that was found in the array at position i</returns>
		public T Get(int i)
		{
			if (i < 0 || i >= size)
				throw new IndexOutOfRangeException("GenericList.get: index out of bounds");

			return array[i];
		}

		/// <summary>
		/// The Set method sets the item t into the array at position i if that position is
		/// within the bounds of the array and otherwise casts an IndexOutOfRangeException.
		/// </summary>
		/// <param name="i">an array index with the desired position of the item</param>
		/// <param name="t">an item of type T to be inserted into the array at position i</param>
		public void Set(int i, T t)
		{
			if (i < 0 || i >= size)
				throw new IndexOutOfRangeException("GenericList.set: index out of bounds");

			array[i] = t;
		}

		/// <summary>
		/// The add method checks to see if the size variable is equal to the
		/// length of the array. If it is, a new array of double the old size
		/// replaces the current array and all objects from the old array are
		/// copied to the new array. In either case, an object of type T is
		/// added to the array and then size is increased by 1.
		/// </summary>
		/// <param name="t">A object of type T</param>
		public void Add(T t)
		{
			if (size == array.Length)
			{
				T[] old = array;
				array = new T[2 * old.Length];

				for (int i = 0; i < old.Length; i++)
					array[i] = old[i];
			}

			array[size++] = t;
		}

		/// <summary>
		/// The GetEnumerator method operates as an instance of IEnumerator(T). It returns
		/// all iterated values of the array except for the item at index 2 (if one exists)
		/// which is then also removed, reducing the size of the array by 1.
		/// </summary>
		/// <returns>An instance of a IEnumerator(T) for iterating over collection of
		/// type T</returns>
		public IEnumerator<T> GetEnumerator()
		{
			//Loop through the array
			for (int index = 0; index < size; index++)
			{
				//If the index is 2 (3rd position)
				if (index == 2)
				{
					try
					{
						//move all items after the current index to the previous index
						for (int i = index; i < size - 1; i++)
							Set(i, Get(i + 1));
						//reduce the size of the array
						size--;
						//return the new index 2 (which was index 3)
						yield return array[(index + 0) % size];
					}
					finally
					{

					}
				}
				//For all other indexes, just return the value at that array index
				else
					yield return array[(index + 0) % size];
			}
		}

		/// <summary>
		/// The IEnumerable.GetEnumerator method returns an IEnumerator object (in
		/// this case an IEnumerable(T), functioning according to the preceding
		/// method GetEnumerator).
		/// </summary>
		/// <returns>An instance of an IEnumerator for iterating over a collection</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
