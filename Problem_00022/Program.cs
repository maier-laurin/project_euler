using System.IO;
using System.Collections.Generic;
using System.Linq;

Console.WriteLine("Starting Prolem 22");

string content = File.ReadAllText("0022_names.txt");

List<string> names = content
    .Split(',')                          // Split into an array
    .Select(name => name.Trim('"'))      // Remove " from start and end
    .ToList();                           // Convert to a List

//List<string> names_s = names.GetRange(0,4);


List<string> names_ss = QuickSort(names);
List<int> name_values = new(names_ss.Count);

foreach( string name in names_ss)
{
    name_values.Add(NameValue(name));
}

Console.WriteLine($"Loaded {names_ss.Count} names_s.");
int  total_count = 0;
for(int i = 0; i < name_values.Count; i++)
{
    ////Console.Write(i+1);
    ////Console.Write(" => ");
    ////Console.Write(names_ss[i]);
    ////Console.Write(": ");
    ////Console.WriteLine(name_values[i] * (i+1));
    total_count += name_values[i] * (i+1);
}
Console.WriteLine("---- TOTAL COUNT ----");
Console.Write(total_count);

//---- Functions and methods ---------------------------------------------------

//* string value
static int NameValue(string name)
{
    int nameScore = 0;

    foreach (char c in name)
    {
        nameScore += (char.ToUpper(c) - 64);
    }

    return nameScore;
}

//* string quicksort
static List<string> QuickSort(List<string> list)
{
    // Base case: If the list has 0 or 1 items, it's already sorted
    if (list.Count <= 1) 
    {
        return list;
    }
    // 1. Pick a pivot (we'll just take the middle element)
    string pivot = list[list.Count / 2];
    // 2. Partitioning: Use LINQ to easily split the data
    // String.CompareOrdinal is fast and handles alphabetical comparison
    List<string> left = list.Where(x => string.CompareOrdinal(x, pivot) < 0).ToList();
    List<string> middle = list.Where(x => x == pivot).ToList();
    List<string> right = list.Where(x => string.CompareOrdinal(x, pivot) > 0).ToList();

    // 3. Recursive step: Sort the left and right, then join them
    return QuickSort(left).Concat(middle).Concat(QuickSort(right)).ToList();
}