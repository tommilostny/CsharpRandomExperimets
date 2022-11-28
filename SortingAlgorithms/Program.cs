using SortingAlgorithms;
using System.Text.Json;

await TestWithRandoms(infiniteRepeat: true);
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
    await QuickSort.SortAsync(primes);
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


static async Task TestWithRandoms(bool infiniteRepeat)
{
    var random = new Random();
    var numbers = 42;
    int[] ints;
    double[] doubles;
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

    if (numbers <= 100)
    {
        Console.WriteLine(JsonSerializer.Serialize(ints));
        Console.WriteLine();
        Console.WriteLine(JsonSerializer.Serialize(doubles));
        Console.WriteLine();

        Console.WriteLine("Press any key to sort...");
        Console.ReadKey();
    }
    var startTime = DateTime.Now;

    await QuickSort.SortAsync(ints);
    await QuickSort.SortAsync(doubles);

    var stopTime = DateTime.Now;

    if (numbers <= 100)
    {
        Console.WriteLine(JsonSerializer.Serialize(ints));
        Console.WriteLine();
        Console.WriteLine(JsonSerializer.Serialize(doubles));
    }

    Console.Write($"\n{ints.Length} int and {doubles.Length} double numbers sorted in ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write((stopTime - startTime).TotalMilliseconds);
    Console.ResetColor();
    Console.WriteLine(" ms\n");

    if (infiniteRepeat)
        goto start;
}