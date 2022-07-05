using BenchmarkDotNet.Attributes;
using System.Text;

namespace StringBuilderContains;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    readonly string _searched = "ahoj";
    readonly StringBuilder _sb = new("vsjdnjlvndsvnjldj najkahojvvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs ajhoj");
    readonly StringBuilder _long = new("gusazzhdvisuzfrtguzhsdzufdsggzfsdzifsdzivszugzisdgvhisedgvzsgvzhshvbzugvhivizsegvzusgvzushgvzisgvzisgvzisgvisghivzszivgzsgvzisgvzusgzvusezuv");
    readonly StringBuilder _ahoj = new("ahoj");
    readonly StringBuilder _ahojLong = new("ahojfvhasbvidsdjvbsvbsdhvjbsdhvhsdvbhjsdbhvdhsvbhksdvbkjdsbvjksdvnjskdbvkjbkdvskdvkhdsvds");
    readonly StringBuilder _ahojLong2 = new("fvhasbvidsdjvbsvbsdhvjbsdhvhsdvbhjsdbhvdhsvbhksdvbkjdsbvahojjksdvnjskdbvkjbkdvskdvkhdsvds");

    [Benchmark]
    public bool BruteForce1()
    {
        return _sb.Contains(_searched);
    }

    [Benchmark]
    public bool RabinKarp1()
    {
        return _sb.ContainsRabinKarp(_searched);
    }

    [Benchmark]
    public bool ToStringContains1()
    {
        return _sb.ToString().Contains(_searched);
    }

    [Benchmark]
    public bool BruteForceLong()
    {
        return _long.Contains(_searched);
    }

    [Benchmark]
    public bool RabinKarpLong()
    {
        return _long.ContainsRabinKarp(_searched);
    }

    [Benchmark]
    public bool ToStringContainsLong()
    {
        return _long.ToString().Contains(_searched);
    }

    [Benchmark]
    public bool BruteForceAhoj()
    {
        return _ahoj.Contains(_searched);
    }

    [Benchmark]
    public bool RabinKarpAhoj()
    {
        return _ahoj.ContainsRabinKarp(_searched);
    }

    [Benchmark]
    public bool ToStringContainsAhoj()
    {
        return _ahoj.ToString().Contains(_searched);
    }

    [Benchmark]
    public bool BruteForceAhojLong()
    {
        return _ahojLong.Contains(_searched);
    }

    [Benchmark]
    public bool RabinKarpAhojLong()
    {
        return _ahojLong.ContainsRabinKarp(_searched);
    }

    [Benchmark]
    public bool ToStringContainsAhojLong()
    {
        return _ahojLong.ToString().Contains(_searched);
    }

    [Benchmark]
    public bool BruteForceAhojLong2()
    {
        return _ahojLong2.Contains(_searched);
    }

    [Benchmark]
    public bool RabinKarpAhojLong2()
    {
        return _ahojLong2.ContainsRabinKarp(_searched);
    }

    [Benchmark]
    public bool ToStringContainsAhojLong2()
    {
        return _ahojLong2.ToString().Contains(_searched);
    }
}
