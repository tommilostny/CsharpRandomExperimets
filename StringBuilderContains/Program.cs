using BenchmarkDotNet.Running;
using StringBuilderContains;
using System.Text;

//BenchmarkRunner.Run<Benchmarks>();
//return;

var searched = "ahoj";
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs ahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bcahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bcdahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bcdeahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bcdea");

//BM("\"vsjlvndsvnjldj najvhn ajjsdnkjdvnk jnahoassvsvsvs ajhoj\"");
//BM("gusazzhdvisuzfrtguzhsdzufdsggzfsdzifsdzivszugzisdgvhisedgvzsgvzhshvbzugvhivizsegvzusgvzushgvzisgvzisgvzisgvisghivzszivgzsgvzisgvzusgzvusezuv");
//BM(string.Empty);
//BM("ahoj");
//BM("ahoj");
//BM("ahoj");
//BM("ahoj");
//BM("\"vsjlvndsvnjldj najvhn ajjsdnkjdvnk jnahoassvsvsvs ajhoj\"");
//BM("\"vsjlvndsvnjldj najvhn ajjsdnkjdvnk jnahoassvsvsvs ajhoj\"");
//BM("vsjahojdnjlvndsvnjldj najkahojvvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs ajhoj");
//BM("vsjahojdnjlvndsvnjldj najkahojvvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs ajhoj");
//BM("vsjahojdnjlvndsvnjldj najkahojvvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs ajhoj");
//BM("gusazzhdvisuzfrtguzhsdzufdsggzfsdzifsdzivszugzisdgvhisedgvzsgvzhshvbzugvhivizsegvzusgvzushgvzisgvzisgvzisgvisghivzszivgzsgvzisgvzusgzvusezuv");
//BM("gusazzhdvisuzfrtguzhsdzufdsggzfsdzifsdzivszugzisdgvhisedgvzsgvzhshvbzugvhivizsegvzusgvzushgvzisgvzisgvzisgvisghivzszivgzsgvzisgvzusgzvusezuv");
//BM("gusazzhdvisuzfrtguzhsdzufdsggzfsdzifsdzivszugzisdgvhisedgvzsgvzhshvbzugvhivizsegvzusgvzushgvzisgvzisgvzisgvisghivzszivgzsgvzisgvzusgzvusezuv");
//BM("gusazzhdvisuzfrtguzhsdzufdsggzfsdzifsdzivszugzisdgvhisedgvzsgvzhshvbzugvhivizsegvzusgvzushgvzisgvzisgvzisgvisghivzszivgzsgvzisgvzusgzvusezuv");


void BM(string testStr)
{
    Console.WriteLine(testStr);

    var sb = new StringBuilder(testStr);

    Console.WriteLine("Brute force:");

    var now = DateTime.Now;
    Console.WriteLine(sb.Contains(searched));
    Console.WriteLine((DateTime.Now - now).TotalMilliseconds);

    Console.WriteLine("\nRabin-Karp:");

    now = DateTime.Now;
    Console.WriteLine(sb.ContainsRabinKarp(searched));
    Console.WriteLine((DateTime.Now - now).TotalMilliseconds);

    Console.WriteLine("\nToString().Contains(...):");

    now = DateTime.Now;
    Console.WriteLine(sb.ToString().Contains(searched));
    Console.WriteLine((DateTime.Now - now).TotalMilliseconds);

    Console.WriteLine("-------------------------------------------");
}