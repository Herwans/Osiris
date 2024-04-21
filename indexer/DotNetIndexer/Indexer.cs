namespace DotNetIndexer
{
    public class Indexer
    {
        public static async Task GetDirectoryContentAsync(string path)
        {
            string file = Path.Combine("dotnet", "temp", Guid.NewGuid() + ".txt");
            try
            {
                await File.WriteAllLinesAsync(file, Directory.GetFiles(path));
            }
            catch (IOException)
            {
            }

            await Parallel.ForEachAsync(GetDirectories(path), async (dir, _) =>
            {
                await GetDirectoryContentAsync(dir);
            });
        }

        public static void Merge(string output)
        {
            string[] files = Directory.GetFiles(Path.Combine("dotnet", "temp"));

            using var outputStream = File.Create(output);
            foreach (string file in files)
            {
                using var inputStream = File.OpenRead(file);
                inputStream.CopyTo(outputStream);
                inputStream.Dispose();
                File.Delete(file);
            }

            Directory.Delete(Path.Combine("dotnet", "temp"));
        }

        public static string[] GetFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path);
            }
            catch (UnauthorizedAccessException)
            {
                return [];
            }
        }

        public static string[] GetDirectories(string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch (UnauthorizedAccessException)
            {
                return [];
            }
        }
    }
}