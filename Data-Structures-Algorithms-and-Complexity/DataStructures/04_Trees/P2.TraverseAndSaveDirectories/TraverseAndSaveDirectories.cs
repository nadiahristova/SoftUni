using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2.TraverseAndSaveDirectories
{
    class TraverseAndSaveDirectories
    {
        static void Main()
        {
            Folder rootFolder = new Folder(@"D:\Books");
            BuildFolderTree(rootFolder);

            Console.WriteLine("Folder: {0}\nSize: {1} bytes", rootFolder, GetFolderSize(rootFolder));
        }

        private static void BuildFolderTree(Folder rootFolder)
        {
            var childrenFolders = new DirectoryInfo(rootFolder.Name).GetDirectories().ToList();
            var childrenFiles = new DirectoryInfo(rootFolder.Name).GetFiles().ToList();

            foreach (var file in childrenFiles)
            {
                var currFile = new File(file.FullName, file.Length);
                rootFolder.Files.Add(currFile);
            }

            foreach (var childFolder in childrenFolders)
            {
                var currFolder = new Folder(childFolder.FullName);
                rootFolder.ChildFolders.Add(currFolder);
                BuildFolderTree(currFolder);
            }
        }
        public static long GetFolderSize(Folder rootFolder)
        {
            long childFilesSize = 0;

            if (rootFolder == null)
                return 0;

            foreach (var folder in rootFolder.ChildFolders)
            {
                childFilesSize += GetFolderSize(folder);
            }

            return childFilesSize + rootFolder.Files.Sum(f => f.Size);
        }
    }
}
