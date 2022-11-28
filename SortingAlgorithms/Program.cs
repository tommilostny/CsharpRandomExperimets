using SortingAlgorithms;
using System.Text.Json;

//QuickSort.Sort(stackalloc int[]{ 1, 3, 9, 8, 2, 7, 5 });

await TestWithRandoms(infiniteRepeat: true, runSync: true, runAsync: false);
await TestOnAVSPrimes();


static async Task TestOnAVSPrimes()
{
    int[] primes;
    int count = 0;
    Console.WriteLine("Loading primes...");
    using (var sr = new StreamReader("../../../primes.txt"))
    {
        while (!sr.EndOfStream)
        {
            await sr.ReadLineAsync();
            count++;
        }
        sr.BaseStream.Position = 0;
        primes = new int[count];
        for (int i = 0; i < count; i++)
        {
            primes[i] = Convert.ToInt32(await sr.ReadLineAsync());
        }
    }

    Console.WriteLine($"Loaded {primes.Length} (count: {count}) primes.\nSorting...");
    var startTime = DateTime.Now;

    QuickSort.Sort(primes.AsSpan());
    
    var stopTime = DateTime.Now;

    Console.Write($"Numbers sorted in ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write((stopTime - startTime).TotalMilliseconds);
    Console.ResetColor();
    Console.WriteLine(" ms\n\nSaving to file sortedprimes.txt");

    using var sw = new StreamWriter("../../../sortedprimes.txt");
    for (int i = 0; i < count; i++)
    {
        await sw.WriteLineAsync(primes[i].ToString());
    }
}


static async Task TestWithRandoms(bool infiniteRepeat, bool runAsync, bool runSync)
{
    var random = new Random();
    var numbers = 42;
    int[] ints, intsCopy;
    double[] doubles, doublesCopy;
    start:
    Console.Write("How many numbers to sort, sir? ");
    try
    {
        var input = Console.ReadLine();
        numbers = Convert.ToInt32(input);
        if (numbers < 0)
            throw new ArgumentOutOfRangeException($"{input} ∉ <0;{int.MaxValue}>");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        goto start;
    }
    var upperBound = 100;//random.Next();
    ints = Enumerable.Range(1, numbers).Select(x => random.Next() % upperBound).ToArray();
    doubles = ints.Select(x => Convert.ToDouble(x) + random.NextDouble()).ToArray();

    if (runAsync)
    {
        intsCopy = new int[ints.Length];
        ints.CopyTo(intsCopy, 0);
        doublesCopy = new double[doubles.Length];
        doubles.CopyTo(doublesCopy, 0);
    }
    else
    {
        intsCopy = ints;
        doublesCopy = doubles;
    }

    if (numbers <= 100)
    {
        Console.WriteLine(JsonSerializer.Serialize(ints));
        Console.WriteLine();
        Console.WriteLine(JsonSerializer.Serialize(doubles));
        Console.WriteLine();

        Console.WriteLine("Press any key to sort...");
        Console.ReadKey();
    }

    if (runAsync)
    {
        var startTime = DateTime.Now;

        await QuickSort.SortAsync(ints);
        await QuickSort.SortAsync(doubles);

        var stopTime = DateTime.Now;

        Console.Write($"\nASYNC: {ints.Length} int and {doubles.Length} double numbers sorted in ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write((stopTime - startTime).TotalMilliseconds);
        Console.ResetColor();
        Console.WriteLine(" ms\n");
    }
    if (runSync)
    {
        var startTime = DateTime.Now;

        QuickSort.Sort(intsCopy.AsSpan());
        QuickSort.Sort(doublesCopy.AsSpan());

        var stopTime = DateTime.Now;

        Console.Write($"SYNC: {ints.Length} int and {doubles.Length} double numbers sorted in ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write((stopTime - startTime).TotalMilliseconds);
        Console.ResetColor();
        Console.WriteLine(" ms\n");
    }

    if (numbers <= 100)
    {
        Console.WriteLine(JsonSerializer.Serialize(ints));
        Console.WriteLine();
        Console.WriteLine(JsonSerializer.Serialize(doubles));
    }

    if (infiniteRepeat)
        goto start;
}