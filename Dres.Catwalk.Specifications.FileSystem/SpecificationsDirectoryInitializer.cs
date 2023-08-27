namespace Dres.Catwalk.Specifications.FileSystem;

public static class SpecificationsDirectoryInitializer
{
    public static void EnsureCreated()
    {
        if (!Directory.Exists(SpecificationsDirectory.Path))
        {
            Directory.CreateDirectory(SpecificationsDirectory.Path);
        }
    }
}