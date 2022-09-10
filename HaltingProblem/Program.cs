static bool Holds(string fun, string arg)
{
    throw new NotImplementedException();
}

static void Weird(string fun)
{
    if (Holds(fun, fun))
    {
        while (true);
    }
    else return;
}

var weirdStr = @"static void Weird(string fun)
{
    if (Holds(fun, fun))
    {
        while (true);
    }
    else return;
}
";

Weird(weirdStr);