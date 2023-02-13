using System.Numerics;
using System.Text.Json;

static int Lenght<T>(T[] list) => list switch
{
    [] => 0,
    [_, ..var xs] => 1 + Lenght(xs)
};


static T[] Merge<T>(T[] x, T[] y) where T : IComparable => (x, y) switch
{
    ([], var l2) => l2,
    (var l1, []) => l1,
    ([var h1, ..var t1], [var h2, ..var t2]) => h1.CompareTo(h2) < 0
        ? new[] { h1 }.Concat(Merge(t1, y)).ToArray()
        : new[] { h2 }.Concat(Merge(x, t2)).ToArray()
};

static T Factorial<T>(T n) where T : IBinaryInteger<T> => n switch
{
    < 2 => T.One,
    _ => n * Factorial(n - T.One)
};


Console.WriteLine(Lenght(Enumerable.Range(1, 200).ToArray()));

var res = Merge(new[] { 5, 11, 12, 15, 16 }, new[] { 2, 10, 15, 20 });

Console.WriteLine(JsonSerializer.Serialize(res));

var res2 = Merge(new[] { 5, 11.111, 12, 15, 16.1 }, new[] { 2, 10, 15.3, 20 });

Console.WriteLine(JsonSerializer.Serialize(res2));

Console.WriteLine(Factorial(4));
Console.WriteLine(Factorial(5));
Console.WriteLine(Factorial(6));
