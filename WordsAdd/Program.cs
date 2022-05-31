var x = new Add("hehe", "hoho", "haha", "hihi", "huhu");
var y = new Add("this", "is", "awesome");
var z = new Add("lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit");

x.Print();
y.Print();
z.Print();

record Add(params string[] Words)
{
    public void Print() => Console.WriteLine($"${string.Join('$', Words)}$");
}
