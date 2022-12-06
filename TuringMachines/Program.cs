using TuringMachines;

int selection;
var tm1 = new BinaryInvertTuringMachine();
var tm2 = new PowerOf2AsTuringMachine();

while (true)
{
    Console.WriteLine("Select Turing machine you wish to try:");
    Console.WriteLine("1. Binary number negative/positive to positive/negative");
    Console.WriteLine("2. Word is from L = { a^(2^n) | n >= 0 }?");
    Console.WriteLine();
    try
    {
        selection = Convert.ToInt32(Console.ReadLine());
    }
    catch
    {
        return;
    }
    string? inputStr;
    switch (selection)
    {
        case 1:
            Console.WriteLine("Binary number (string of 0s and 1s) to be inverted (42 -> -42): ");
            inputStr = Console.ReadLine();
            try
            {
                tm1.Run(inputStr?.ToArray());
                tm1.PrintTape();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            break;

        case 2:
            Console.WriteLine("Sequence of 'a' symbols:");
            inputStr = Console.ReadLine();
            try
            {
                var result = tm2.Run(inputStr?.ToArray());
                Console.Write("Length of entered sequence ");
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("is");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("is not");
                    Console.ResetColor();
                }
                Console.WriteLine(" a power of 2.");
                Console.WriteLine("Turing machine tape:");
                tm2.PrintTape();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            break;

        default:
            return;
    }
    Console.WriteLine();
    Console.ReadKey();
}
