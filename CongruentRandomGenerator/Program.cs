uint SEED = (uint)DateTime.Now.Millisecond;
uint ix = SEED;
uint from = 0;
uint to = 100;

double Random()
{
    ix = ix * 69069u + 1u;
    return ix / (uint.MaxValue + 1.0);
}

uint NextRandom() => (uint)((Random() * (to - from)) + from);

void WriteVal(uint value)
{
    Console.Write(SEED);
    Console.Write('\t');
    Console.Write(ix);
    Console.Write('\t');
    Console.WriteLine(value);
}

uint first = NextRandom();
WriteVal(first);
uint next;
do
{
    next = NextRandom();
    WriteVal(next);
}
while (next != first);
