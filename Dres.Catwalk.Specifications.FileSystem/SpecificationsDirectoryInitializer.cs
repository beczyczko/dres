namespace Dres.Catwalk.Specifications.FileSystem;

internal static class SpecificationsDirectoryInitializer
{
    public static void EnsureCreated(SpecificationsStorageFileSystemOptions specificationsStorageFileSystemOptions)
    {
        if (!Directory.Exists(specificationsStorageFileSystemOptions.BasePath))
        {
            Directory.CreateDirectory(specificationsStorageFileSystemOptions.BasePath);
        }
    }
}