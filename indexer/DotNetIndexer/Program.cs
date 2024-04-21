using DotNetIndexer;

if (args.Count() < 2)
{
    Console.WriteLine("Not enough arguments");
    return;
}

Console.WriteLine(".NET Indexer");
Directory.CreateDirectory("dotnet/temp");

await Indexer.GetDirectoryContentAsync(args[1]);
Indexer.Merge(args[0]);