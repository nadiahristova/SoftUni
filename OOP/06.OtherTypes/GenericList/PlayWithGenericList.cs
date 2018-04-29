namespace GenericList
{
    using System;

    class PlayWithGenericList
    {
        static void Main(string[] args)
        {
            var genericList = new GenericArrayList<int>() { 1, 2, 3, 4 };
            int[] array = { -15, -17, 117, 0 };

            Console.WriteLine("ArrayList:.");
            Console.WriteLine(genericList);

            genericList.Add(5);
            Console.WriteLine("\nAdded element.");
            Console.WriteLine(genericList);

            //Console.WriteLine(genericList[4]);
            //Console.WriteLine(genericList[-1]);

            Console.WriteLine("\nElement at index 3.");
            Console.WriteLine(genericList[3]);
            Console.WriteLine("\nChange element at index 3.");
            genericList[3] = 4;
            Console.WriteLine(genericList);

            Console.WriteLine("\nRemove element at index 1.");
            Console.WriteLine(genericList.RemoveAt(1));
            Console.WriteLine(genericList);

            Console.WriteLine("\nInsert element 2 at index 1.");
            genericList.Insert(2, 1);
            Console.WriteLine(genericList);

            Console.WriteLine("\nFind index by given value 2.");            
            Console.WriteLine(genericList.FindIndexByValue(2));

            Console.WriteLine("\nCheck if list contains value -555.");
            Console.WriteLine(genericList.Contains(-555));

            Console.WriteLine("\nAdd IEnumerable to list.");
            genericList.AddRange(array);
            Console.WriteLine(genericList);

            Console.Write("\nMin value in the list: ");
            Console.WriteLine(GenericArrayList<int>.Min(genericList));

            Console.Write("\nMax value in the list: ");
            Console.WriteLine(GenericArrayList<int>.Max(genericList));

            Console.WriteLine("\nClear list.");
            genericList.Clear();
            Console.WriteLine(genericList);

            //Console.WriteLine(genericList[0]);

            Console.WriteLine("\n" + GenericArrayList<int>.Version());
        }
    }
}
