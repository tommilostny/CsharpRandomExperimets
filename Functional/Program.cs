using System.Numerics;
using System.Text.Json;

static void Print<T>(T value) => Console.WriteLine(value);
static void PrintArray<T>(T[] list) => Console.WriteLine(JsonSerializer.Serialize(list));


static int Length<T>(T[] list) => list switch
{
    [] => 0,
    [_, ..var xs] => 1 + Length(xs)
};

static T[] Merge<T>(T[] x, T[] y) where T : IComparable => (x, y) switch
{
    ([], var l2) => l2,
    (var l1, []) => l1,
    ([var h1, ..var t1], [var h2, ..var t2]) =>
    (
        h1.CompareTo(h2) < 0
            ? Merge(t1, y).Prepend(h1).ToArray()
            : Merge(x, t2).Prepend(h2).ToArray()
    )
};

static T Factorial<T>(T n) where T : IBinaryInteger<T> => n switch
{
    < 2 => T.One,
    _ => n * Factorial(n - T.One)
};

static T SumSquare<T>(T x, T y) where T : INumber<T>
{
    return XX() + YY();

    static T Sqr(T a, T b) => a * b;
    T XX() => Sqr(x, x);
    T YY() => Sqr(y, y);
}

static T[] Rev<T>(T[] list) => list switch
{
    [] => Array.Empty<T>(),
    [var x, ..var xs] => Rev(xs).Append(x).ToArray(),
};

static T[] Reverse<T>(T[] list)
{
    return Rev(list, Array.Empty<T>());

    static T[] Rev(T[] l1, T[] l2) => (l1, l2) switch
    {
        ([], var ys) => ys,
        ([var x, ..var xs], var ys) => Rev(xs, ys.Prepend(x).ToArray())
    };
}

static T SumList<T>(T[] list) where T : INumber<T> => list switch
{
    [] => T.Zero,
    [var x, ..var xs] => x + SumList(xs)
};

static T[] Concatenate<T>(params T[][] lists) => lists switch
{
    [] => Array.Empty<T>(),
    [var xs, ..var xss] => xs.Concat(Concatenate(xss)).ToArray()
};

static T[] SquareAll<T>(T[] list) where T : IMultiplyOperators<T,T,T> => list switch
{
    [] => Array.Empty<T>(),
    [var x, ..var xs] => SquareAll(xs).Prepend(x * x).ToArray(),
};

static int[] LengthAll<T>(params T[][] lists) => lists switch
{
    [] => Array.Empty<int>(),
    [var xs, ..var xss] => LengthAll(xss).Prepend(Length(xs)).ToArray()
};

static T MaxMatrix<T>(T[][] list) where T : INumber<T> => list.Max(xs => SumList(xs))!;

static T[] DropEvery<T>(T[] list, int i) => list switch
{
    [] => Array.Empty<T>(),
    var xs => xs.Take(i - 1).Concat(DropEvery(xs.Skip(i).ToArray(), i)).ToArray()
};

static T[] DropEvery_<T>(T[] list, int i)
{
    return list switch
    {
        [] => Array.Empty<T>(),
        var xs => Helper(xs, i)
    };

    T[] Helper(T[] list, int k) => (list, k) switch
    {
        ([], _) => Array.Empty<T>(),
        ([_, .. var xs], 1) => Helper(xs, i),
        ([var x, .. var xs], _) => Helper(xs, k - 1).Prepend(x).ToArray()
    };
}

Print(Length(Enumerable.Range(1, 200).ToArray()));

var res1 = Merge(new[] { 5, 11, 12, 15, 16 }, new[] { 2, 10, 13, 15, 20 });

PrintArray(res1);

var res2 = Merge(new[] { 5, 11.111, 12.9999999, 15, 16.1 }, new[] { 2, 10, 13, 15.3, 20 });

PrintArray(res2);

Print(Factorial(4));
Print(Factorial(5));
Print(Factorial(6));

Print(SumSquare(2, 2));

var res3 = Rev(Enumerable.Range(1, 10).ToArray());

PrintArray(res3);

var res4 = Reverse(Enumerable.Range(1, 10).ToArray());

PrintArray(res4);

Print(SumList(res4));
Print(SumList(new[] { 1, 2, 3 }));

var res5 = Concatenate(
    Enumerable.Range(1, 5).ToArray(),
    Enumerable.Range(69, 10).ToArray(),
    Enumerable.Range(420, 5).ToArray()
);

PrintArray(res5);

PrintArray(SquareAll(res4));
PrintArray(SquareAll(res1));
PrintArray(SquareAll(res2));

PrintArray(LengthAll(res4, res1, res3, res5));
PrintArray(LengthAll(res2));

Print(MaxMatrix(new int[][]
{
    new[] { 1, 2, 3,},
    new[] { 8, 1, 1 },
    new[] { 6, 2, 1 },
}));

PrintArray(DropEvery(Enumerable.Range(1, 8).ToArray(), 3));
PrintArray(DropEvery_(Enumerable.Range(1, 8).ToArray(), 3));
