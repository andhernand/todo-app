namespace Todo.App.Tests.Integration;

public static class DirectoryFinder
{
    public static string GetDirectoryContaining(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentNullException(nameof(fileName), "The file name cannot be null or whitespace.");
        }

        var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

        while (directory != null && !File.Exists(Path.Combine(directory.FullName, fileName)))
        {
            directory = directory.Parent;
        }

        if (directory == null)
        {
            throw new InvalidOperationException($"The directory containing {fileName} could not be found.");
        }

        return directory.FullName;
    }
}