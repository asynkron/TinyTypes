// See https://aka.ms/new-console-template for more information

using TinyTypes;

Console.WriteLine("Hello, World!");

IO<int> stuff = async () =>
{
    await Task.Delay(1000);
    throw new ArgumentException("Foo!");
    Console.WriteLine("Hello");
    return 1;
};

var x = await stuff.Try()();

switch (x)
{
    case Some<int>(var i):
        Console.WriteLine("Got Int " + i);
        break;
    case Some<Exception>(var ex):
        Console.WriteLine("Got exception " + ex);
        break;
}