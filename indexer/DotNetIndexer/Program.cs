using DotNetIndexer;

Console.WriteLine(".NET Indexer");
Directory.CreateDirectory("output/dotnet/temp");

foreach (string argument in args)
{
    await Indexer.GetDirectoryContentAsync(argument);
}
Indexer.Merge();