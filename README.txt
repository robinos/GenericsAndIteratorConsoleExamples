Version 0.1.0
This Solution was created to house examples of generic classes/methods and iterators in C#.

Included Projects:
GenericListIteratorConsoleExample and GenericListEnumeratorConsoleExample

*************************************************************************************************

-GenericListIteratorConsoleExample Project-
A project to demonstrate a custom generic list with custom iterator to show how a custom
version can be implemented with comments on how the C# implementation with IEnumerable and
IEnumerator actually works. This project also provides an example of the Factory design
pattern.


-class GenericListIteratorProgram-
The main program.

- static void Main(string[] args)
Creates instances of MyGenericList<int> and MyGenericList<string> which are initialised with
values and then sent as inparameters to IteratorOverList<int> and <string> respectively.

- public static void IterateOverList<T>(MyGenericList<T> myList) 
IterateOverList<T> retrieves an object that implements the IMyEnumeratorInterface from the
GetMyEnumerator() method of myList and uses it to remove the 3rd item in the list (if it exists)
and then write the entire list to Console.



-public interface IMyEnumeratorInterface<T>-
A variation interface similar to IEnumerator that requires implementation of the following:
T Current{ get; }
bool MoveNext();
void Reset();
void Remove();
This differs from IEnumerator in that it does not require void Dispose() but does require
void Remove().



-public interface IMyGenericEnumerableInterface<T>-
A variation interface instead of IEnumerable that only requires implementation of the method:
IMyEnumeratorInterface<T> GetMyEnumerator();



-public class MyGenericList<T> : IMyGenericEnumerableInterface<T>-
A simple custom generic list (rather than List<T>) that implements IMyGenericEnumerableInterface.
It also contains the internal class MyGenericListIterator<T> where an implementation of 
IMyEnumeratorInterface<T> is defined. Get and Set throw IndexOutOfRangeException on attempts
to access outside of the array.  The internal array is defined with a beginning length of 10
which is doubled in length each time the actual size (number of elements) reaches that limit. 

Instance variables:
private const int defaultCapacity = 10;
private T[] array;
private int size;

Properties:
internal int Size - get and set for size (used by MyGenericListIterator)

Constructor:
public MyGenericList()

Methods:
public T Get(int i) - gets an item of type T from the array 
public void Set(int i, T t) - sets an item of type T into the array at position i
public void Add(T t) - adds a new item to the list and doubles the length if required
public IMyEnumeratorInterface<T> GetMyEnumerator() - returns a new MyGenericListIterator<T>



-internal class MyGenericListIterator<T> : IMyEnumeratorInterface<T>-
The MoveNext and Remove methods use Get and Set in 

Instance variables:
private MyGenericList<T> aList = null;
private int currentPosition = -1;
private T currentItem = default(T);

Properties:
public T Current - returns the current item

Constructor:
public MyGenericListIterator(MyGenericList<T> aList)

Methods:
public bool MoveNext() - If the next position would be valid, the current item is set and true
	is returned, otherwise false is returned 
public void Reset() - Resets currentPostion to -1 and currentItem to default(T)
public void Remove() - Removes the current item by moving all items after current down one index
	and reducing size by 1


*************************************************************************************************

-GenericListEnumeratorConsoleExample Project-
A project to demonstrate a custom generic list with IEnumerator(T) returned from the GetEnumerator
method of GenericList with help of the yield return statement. There is no internal class in this
version.


-class GenericListEnumeratorProgram-
The main program.

- static void Main(string[] args)
Creates instances of GenericList<int> and GenericList<string> which are initialised with
values and then sent as inparameters to IteratorOverList<int> and <string> respectively.

- public static void IterateOverList<T>(MyGenericList<T> myList) 
IterateOverList<T> uses a foreach loop. The 3rd item in the list (if it exists) is
automatically removed whle the list is written item by item to Console.



-public class GenericList<T> : IMyGenericEnumerableInterface<T>-
A simple custom generic list (rather than List<T>) that implements IEnumerable<T>. Get and
Set throw IndexOutOfRangeException on attempts to access outside of the array.  The internal
array is defined with a beginning length of 10 which is doubled in length each time the actual
size (number of elements) reaches that limit. 

Instance variables:
private const int defaultCapacity = 10;
private T[] array;
private int size;

Constructor:
public GenericList()

Methods:
public T Get(int i) - gets an item of type T from the array 
public void Set(int i, T t) - sets an item of type T into the array at position i
public void Add(T t) - adds a new item to the list and doubles the length if required
public IEnumerator<T> GetEnumerator() - uses yield return syntax to return all values
	in the array except for index 2 (which is also deleted from the array)
IEnumerator IEnumerable.GetEnumerator() - calls GetEnumerator above and returns the result

