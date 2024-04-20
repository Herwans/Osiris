namespace DotNetIndexer
{
    public class Indexer
    {
        public static async Task GetDirectoryContentAsync(string path)
        {
            string file = Path.Combine("output", "dotnet", "temp", Guid.NewGuid() + ".txt");
            try
            {
                await File.WriteAllLinesAsync(file, Directory.GetFiles(path));
            }
            catch (IOException e)
            {
                Console.WriteLine("Error : " + e.Message);
            }

            await Parallel.ForEachAsync(GetDirectories(path), async (dir, _) =>
            {
                await GetDirectoryContentAsync(dir);
            });
        }

        public static void Merge(string output)
        {
            string[] files = Directory.GetFiles(Path.Combine("output", "dotnet", "temp"));

            using var outputStream = File.Create(output);
            foreach (string file in files)
            {
                using var inputStream = File.OpenRead(file);
                inputStream.CopyTo(outputStream);
            }
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