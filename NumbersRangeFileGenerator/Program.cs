// Generates or counts (char) choices in file (regarding SZZ questions).

const string path =
    @"C:\Users\tommi\OneDrive - Vysoké učení technické v Brně\_SZZ_IBT\wants_notwants.txt";

if (!File.Exists(path))
{
    await GenerateChoicesFile(path);
}
else
{
    await CountChoicesInFile(path);
}

Console.Write("\nPress any key to close this window...");
Console.ReadKey();


static async Task GenerateChoicesFile(string path)
{
    using var sw = new StreamWriter(path, append: false);

    foreach (var number in Enumerable.Range(1, 44))
    {
        if (number < 10)
        {
            await sw.WriteAsync('0');
        }
        await sw.WriteAsync(number.ToString());
        await sw.WriteLineAsync(": ");
    }

    Console.WriteLine("File created. Provide choices and run this program again.");
}

static async Task CountChoicesInFile(string path)
{
    using var sr = new StreamReader(path);
    var choices = new ChoicesDictionary<char, int>();

    while (!sr.EndOfStream)
    {
        var line = await sr.ReadLineAsync();
        if (line is null)
        {
            continue;
        }
        choices[line.Last()] = line.GetIntFromStart();
    }

    choices.PrintAllChoices();
}
