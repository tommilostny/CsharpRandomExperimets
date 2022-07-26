using BenchmarkDotNet.Running;
using StringBuilderContains;
using System.Text;

//BenchmarkRunner.Run<Benchmarks>();
//return;

//Console.WriteLine(Path.Combine("C:\\McRAI\\weby\\backup\\", "C:\\McRAI\\weby\\dentunit\\kalendar"));
//return;

    
var sb = new StringBuilder("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs ahoj");

var splitted = sb.Split('a');

for (int i = 0; i < splitted.Count; i++)
{
    Console.Write('\'');
    Console.Write(splitted[i]);
    Console.WriteLine('\'');
}

splitted.JoinInto(sb, ';');
Console.WriteLine(sb);

var path = Path.Combine("tommi", "piedpiper", "s03e01.c");
Console.WriteLine(path);
Console.WriteLine(File.Exists(path));

/*
Console.WriteLine(path);
Console.WriteLine(Regex.IsMatch(path, @"\\templates\\.+\\product_detail\.php"));

path = "plugins/templates/Uploaded/product_detail.php";
Console.WriteLine(path);
Console.WriteLine(Regex.IsMatch(path, @"(\\|/)templates(\\|/).+(\\|/)product_detail\.php"));
 */

var searched = "ahoj";
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs ahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bcahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bcdahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bcdeahoj");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bcdea");
BM("vsjahojdnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahoassvsvsvs bcdea");
BM("vsjahodnjlvndsvnjldj najkavvjsahojnvhn ajjsdnkjdvnk jnahoassvsvsvs bcdea");
BM("vsjahodnjlvndsvnjldj najkavvjsanvhn ajjsdnkjdvnk jnahojassvsvsvs bcdea");


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
