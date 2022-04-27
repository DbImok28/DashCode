namespace DashCode.Models
{
    public class ProjectFolder
    {
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
