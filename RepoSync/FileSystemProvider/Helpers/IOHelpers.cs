using System;
using System.IO;
namespace RepoSync.Providers.FileSystemProvider.Helpers
{
    public class IOHelpers
    {
        public static void CreateInnerFolders(DirectoryInfo baseDirectory, string repositoryPath, bool includeLastPart = false)
        {
           
            string[] directoryParts = repositoryPath.Split(new char[]{ '/'},StringSplitOptions.RemoveEmptyEntries);
            string dirPointer = string.Empty;
            var length = includeLastPart ? directoryParts.Length : directoryParts.Length - 1;
            for (int i = 0; i < length; i++)
            {
                dirPointer += "\\" + directoryParts[i];
                Directory.CreateDirectory(baseDirectory.FullName + dirPointer);
            }
        }
    }
}
