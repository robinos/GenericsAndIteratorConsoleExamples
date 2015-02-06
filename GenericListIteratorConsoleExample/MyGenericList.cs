using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericListIteratorConsoleExample
{
	/// <summary>
	/// The MyGenericList(T) class implements the IMyGenericEnumerableInterface(T)
	/// and therefore must implement a method by the name of GetMyEnumerable that returns
	/// an object of the IMyEnumeratorInterface type.
	/// Once the MyGenericList is initialised in its constructor, T is set permanently
	/// (for that instance of MyGenericList) to that specific object type. If a
	/// MyGenericList(int) is created, objects of type string will not be able to be
	/// added to the list.
	/// 
	/// MyGenericList uses an array of default size 10 to hold objects of the given type.
	/// When the add method is called the current size is checked and if more size is needed,
	/// the size of the array is doubled.
	/// 
	/// -Constructor-
	/// MyGenericList() - initialises the internal array to a default capacity and current
	///		size to 0 (0 current elements)
	/// 
	/// -Methods-
	/// Get - Gets an item from the list in the given position or throws an exception if
	///		out of bounds
	/// Set - Sets an item into the list at the given position or throws an exception if
	///		out of bounds
	/// Add - Adds an item to the list, doubling the current list length if the end of the
	///		previously defined size has been reached
	/// GetMyEnumerator - Returns a MyGenericListEnumerator object (this is possible because it
	///		implements IMyEnumeratorInterface which is the expected return type)
	/// 
	/// -Internal classes-
	/// MyGenericListIterator - An iterator that implements the IMyEnumeratorInterface
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
	public class MyGenericList<T> : IMyGenericEnumerableInterface<T>
	{
		private const int defaultCapacity = 10;
		private T[] array;
		private int size;

		/// <summary>
		/// The constructor of MyGenericList initialises its internal array to a default
		/// capacity and initialises its current size to 0.
		/// </summary>
		public MyGenericList()
		{
			array = new T[defaultCapacity];
			size = 0;
		}

		/// <summary>
		/// The property Size is used MyGenericListIterator.
		/// </summary>
		internal int Size { get { return size; } set { size = value; } }

		/// <summary>
		/// The Get method gets an item of type T from the array at position i if that position
		/// is within the bounds of the array and otherwise casts an IndexOutOfRangeException.
		/// </summary>
		/// <param name="i">an array index with the position of the desired item</param>
		/// <returns>an item of type T that was found in the array at position i</returns>
		public T Get(int i)
		{
			if(i < 0 || i >= size)
				throw new IndexOutOfRangeException("MyGenericList.get: index out of bounds");

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
				throw new IndexOutOfRangeException("MyGenericList.set: index out of bounds");

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
			if(size == array.Length)
			{
				T[] old = array;
				array = new T[2 * old.Length];

				for (int i = 0; i < old.Length; i++)
					array[i] = old[i];
			}

			array[size++] = t;
		}

		/// <summary>
		/// The GetMyEnumerator method returns a new MyGenericListIterator object that has
		/// this instance of MyGenericList as a parameter.
		/// This is possible (looking at the return type) because MyGenericListIterator
		/// implements IMyEnumeratorInterface and is therefore IS of that type.
		/// </summary>
		/// <returns>An instance of a IMyEnumeratorInterface for iterating over collection of
		/// type T (in this case a MyGenericListIterator).</returns>
		public IMyEnumeratorInterface<T> GetMyEnumerator()
		{
			return new MyGenericListIterator<T>(this);
		}

		/// <summary>
		/// **************************************************************************
		/// The MyGenericListIterator is an internal class in this case but could
		/// have also been a separate class. What is important is that it implements
		/// the IMyEnumeratorInterface for objects of type T. It must implement the
		/// methods MoveNext, Reset and Remove, and the property Current. 
		/// 
		/// The iterator interface normally used in C# is IEnumerator(T). It requires the
		/// Current property for the current object in the currnt position of the list.
		/// It also requires the methods Reset(), to reset the current position to default,
		/// MoveNext(), which increments the current position and returns a bool
		/// true or false for if a next position actually exists, and Dispose() which frees
		/// up resources for memory purposes.
		/// 
		/// -Constructor-
		/// MyGenericListIterator(MyGenericList<T> aList) - aList is a reference to the list
		///		to be iterated over, and current position is set to -1
		/// 
		/// -Property-
		/// Current - the T object for the current position in the list (required)
		/// 
		/// -Methods-
		/// MoveNext - False if there is no valid next positon, otherwise true and sets the
		///		current item
		/// Reset - Resets the list to beginning position and the current item to default
		/// Remove - Removes an item from the list at the current position
		/// 
		/// </summary>
		/// <typeparam name="T">The object type to iterate over. This should be the same
		/// T as defined by the MyGenericList.</typeparam>
		internal class MyGenericListIterator<T> : IMyEnumeratorInterface<T>
		{
			private MyGenericList<T> aList = null;
			private int currentPosition = -1;
			private T currentItem = default(T);

			/// <summary> 
			/// The Current property which holds the item in the list corresponding
			/// to the current position if valid or otherwise a default item.
			/// </summary>
			public T Current { get { return currentItem; } }

			/// <summary>
			/// The constructor for MyGenericListIterator takes an instance of a
			/// MyGenericList(T) as an inparameter.  That list becomes the referenced
			/// list that is iterated over and the current position is set to -1.
			/// A MoveNext() is required to initialise and iterate to positon 0 so
			/// Current points to the first item at index 0.
			/// </summary>
			/// <param name="aList"></param>
			public MyGenericListIterator(MyGenericList<T> aList)
			{
				this.aList = aList;
				this.currentPosition = -1;
			}

			/// <summary>
			/// The MoveNext method returns false if the next position would be invalid or
			/// beyond the end of the list. Otherwise the method sets the current item to
			/// the object at the current position.
			/// </summary>
			/// <returns>True if the currentPosition is valid, or false if not valid or at/beyond
			/// the end of the list.</returns>
			public bool MoveNext()
			{
				//Note that currentPosition is incremented before the comparison is made.
				//This way the first MoveNext sets currentPosition to 0.
				if (aList == null || ++currentPosition + 1 > aList.Size) return false; 
				else
				{
					try
					{
						currentItem = aList.Get(currentPosition);
					}
					//This Exception should never be thrown except in the case of logical errors
					//within the code
					catch (IndexOutOfRangeException e)
					{
						Console.WriteLine(e.Message);
						//note: cannot return currentItem if null, not nullable
						currentItem = default(T);
					}
				}
				return true;
			}

			/// <summary>
			/// Resets the iterator to the beginning of the list again. The current item
			/// is et to default (cannot be nullable).
			/// </summary>
			public void Reset()
			{
				currentPosition = -1;
				currentItem = default(T);
			}

			/// <summary>
			/// The Remove method removes the current item by copying all items that
			/// come after it in the list to the index that comes before. The size of
			/// the list is then reduced by 1.
			/// </summary>
			public void Remove()
			{
				try
				{
					for (int i = currentPosition; i < aList.Size - 1; i++)
						aList.Set(i, aList.Get(i+1));
					aList.Size--;
				}
				//Catches exceptions and writes out a message to console
				catch (IndexOutOfRangeException e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

	}
}
