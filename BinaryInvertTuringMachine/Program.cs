using TuringMachines;

//Get binary number input from user.
Console.WriteLine("Binary number (string of 0s and 1s) to be inverted (42 -> -42): ");
var inputStr = Console.ReadLine();
try
{
    var tm1 = new BinaryInvertTuringMachine(inputStr);
    tm1.Run();
    tm1.PrintTape();
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
	return 1;
}
return 0;
