using System;

namespace DashCode.Models
{
    [Serializable]
    public class ProjectFolder
    {
        public ProjectFolder()
        {
            Path = "";
            FolderName = "";
        }
        public ProjectFolder(string path)
        {
            Path = path;
            FolderName = new System.IO.DirectoryInfo(path).Name;
        }
        public string Path { get; set; }
        public string FolderName { get; set; }
        public override string ToString()
        {
            return Path;
        }
    }
}
