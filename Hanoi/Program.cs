Hanoi(4, "A", "B", "C");

static void Move(string f, string t)
{
    Console.WriteLine($"Move disc from {f} to {t}!");
}

//n = number of disks
//f = 'from' position
//h = 'helper' position
//t = 'target' position
static void Hanoi(int n, string f, string h, string t)
{
    if (n == 0)
    {
        return;
    }
    Hanoi(n - 1, f, t, h);
    Move(f, t);
    Hanoi(n - 1, h, f, t);
}
