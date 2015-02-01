using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericListIteratorConsoleExample
{
	/// <summary>
	/// GenericListIteratorProgram tests the MyGenericList class and its
	/// internal iterator class that implements the iMyEnumeratorInterface interface.
	/// We do not need to know the details of how it is implemented as long as
	/// it fullfills the 'contract' of the interface.
	/// 
	/// Note: The IEnumerator(T) GetEnumerator() method is the normal C# equivalent
	/// that allows lists, for example, to be used with foreach loops. The method here
	/// is the similarly named GetMyEnumerator().
	/// 
	/// This project in general is also an example of the Factory design pattern.
	/// 
	/// -Methods-
	/// Main - Creates two lists of objects from MyGenericList, one int and one string, 
	///		initialises them with values and calls IterateOverList with each list.
	/// IterateOverList(T) - A generic method that creates an IMyEnumeratorInterface
	///		object of the given type by calling the implemented method GetMyEnumerator
	///		in the given MyGenericList. The 3rd item is removed and then all items are
	///		written to console.
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
	class GenericListIteratorProgram
	{
		/// <summary>
		/// Main creates two MyGenericList objects, one of type int and one
		/// of type string. They are both initialised with values and then
		/// call the generic method IterateOverList for each which removes the 3rd
		/// value in the list and then writes the list to console.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			MyGenericList<int> myIntegerList = new MyGenericList<int>();
			MyGenericList<string> myStringList = new MyGenericList<string>();

			//MyGenericList add (MyGenericList is initialised to a size 10 array but
			//grows as the list grows)
			for (int i = 15; i > 0; i--)
				myIntegerList.Add(i);

			string[] aToQ = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
								"o", "p", "q" };
			foreach (string letter in aToQ)
				myStringList.Add(letter);

		    //Show the integer list with the 3rd item removed
			IterateOverList<int>(myIntegerList);
			Console.ReadKey();

			//Show the string list with the 3rd item removed
			IterateOverList<string>(myStringList);
			Console.ReadKey();
		}

		/// <summary>
		/// IterateOverList removes the 3rd item of a list and then
		/// writes it to console.
		/// </summary>
		/// <typeparam name="T">The type of iterator</typeparam>
		/// <param name="myList">The list to iterate over</param>
		public static void IterateOverList<T>(MyGenericList<T> myList)
		{
			//Get list iterator
			IMyEnumeratorInterface<T> myEnumerator = myList.GetMyEnumerator();

			//Remove the 3rd item in the list (if a 3rd item exists)
			if (myEnumerator.MoveNext())
			{
				if (myEnumerator.MoveNext())
				{
					if (myEnumerator.MoveNext())
						myEnumerator.Remove(); //removes the third item
				}
			}

			//Resets to index -1
			myEnumerator.Reset();
			
			//Write all items in the list to the console
			//This is basically a foreach loop
			Console.WriteLine();
			while (myEnumerator.MoveNext())
			{
				Console.Write(myEnumerator.Current+" ");
			}
		}
	}
}
