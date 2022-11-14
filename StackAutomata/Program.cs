using StackAutomata;

while (true)
{
    Console.WriteLine("Input string (Σ = {a, b, A, 0, B}):");

    var input = Console.ReadLine();

    var automata = new TINdu2StackAutomaton(input);

    Console.WriteLine(automata.Run());
}