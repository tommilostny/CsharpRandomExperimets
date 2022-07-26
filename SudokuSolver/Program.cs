/* Original board with 1 solution:
var grid = new SudokuGrid(new byte[][]
{
    new byte[]{ 5, 3, 0, 0, 7, 0, 0, 0, 0 },
    new byte[]{ 6, 0, 0, 1, 9, 5, 0, 0, 0 },
    new byte[]{ 0, 9, 8, 0, 0, 0, 0, 6, 0 },
    new byte[]{ 8, 0, 0, 0, 6, 0, 0, 0, 3 },
    new byte[]{ 4, 0, 0, 8, 0, 3, 0, 0, 1 },
    new byte[]{ 7, 0, 0, 0, 2, 0, 0, 0, 6 },
    new byte[]{ 0, 6, 0, 0, 0, 0, 2, 8, 0 },
    new byte[]{ 0, 0, 0, 4, 1, 9, 0, 0, 5 },
    new byte[]{ 0, 0, 0, 0, 8, 0, 0, 7, 9 },
});
*/
var grid = new SudokuGrid(new byte[][]
{
    new byte[]{ 5, 3, 0, 0, 7, 0, 0, 0, 0 },
    new byte[]{ 6, 0, 0, 1, 9, 5, 0, 0, 0 },
    new byte[]{ 0, 0, 8, 0, 0, 0, 0, 6, 0 },
    new byte[]{ 8, 0, 0, 0, 6, 0, 0, 0, 3 },
    new byte[]{ 4, 0, 0, 8, 0, 3, 0, 0, 1 },
    new byte[]{ 7, 0, 0, 0, 2, 0, 0, 0, 6 },
    new byte[]{ 0, 6, 0, 0, 0, 0, 2, 8, 0 },
    new byte[]{ 0, 0, 0, 4, 1, 9, 0, 0, 5 },
    new byte[]{ 0, 0, 0, 0, 8, 0, 0, 0, 0 },
});

grid.Print();
Console.WriteLine();
grid.Solve();

class SudokuGrid
{
    private readonly byte[][] _grid;

    public SudokuGrid(byte[][] grid)
    {
        _grid = grid;
    }

    public void Print()
    {
        foreach (var row in _grid)
        {
            foreach (var col in row)
            {
                Console.Write(' ');
                Console.Write(col);
            }
            Console.WriteLine();
        }
    }

    public bool Possible(int y, int x, int n)
    {
        for (byte i = 0; i < 9; i++)
        {
            if (_grid[y][i] == n || _grid[i][x] == n)
            {
                return false;
            }
        }
        var x0 = x / 3 * 3;
        var y0 = y / 3 * 3;
        for (byte i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_grid[y0 + i][x0 + j] == n)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Solve()
    {
        for (byte y = 0; y < 9; y++)
        {
            for (byte x = 0; x < 9; x++)
            {
                if (_grid[y][x] == 0)
                {
                    for (byte n = 0; n <= 9; n++)
                    {
                        if (Possible(y, x, n))
                        {
                            _grid[y][x] = n;
                            Solve();
                            _grid[y][x] = 0;
                        }
                    }
                    return;
                }
            }
        }
        Print();
        Console.WriteLine("More?");
        Console.ReadKey();
    }
}
