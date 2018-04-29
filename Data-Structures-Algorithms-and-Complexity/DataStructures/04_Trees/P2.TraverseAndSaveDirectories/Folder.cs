using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2.TraverseAndSaveDirectories
{
    class Folder
    {
        public string Name { get; set; }
        public IList<File> Files { get; set; }
        public IList<Folder> ChildFolders { get; set; }
        public Folder(string name)
        {
            this.Name = name;
            this.ChildFolders = new List<Folder>();
            this.Files = new List<File>();
        }
    }
}
