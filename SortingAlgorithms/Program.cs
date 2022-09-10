using SortingAlgorithms;
using System.Text.Json;

var random = new Random();
var numbers = 42;

var ints = Enumerable.Range(1, numbers).Select(x => random.Next() % 100).ToArray();
var doubles = ints.Select(x => Convert.ToDouble(x) + random.NextDouble()).ToArray();

Console.WriteLine(JsonSerializer.Serialize(ints));
Console.WriteLine();
Console.WriteLine(JsonSerializer.Serialize(doubles));
Console.WriteLine();

Console.ReadKey();

QuickSort.Sort(ints);
QuickSort.Sort(doubles);

Console.WriteLine(JsonSerializer.Serialize(ints));
Console.WriteLine();
Console.WriteLine(JsonSerializer.Serialize(doubles));

Console.ReadKey();