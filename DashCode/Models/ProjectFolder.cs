using DashCode.ViewModules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DashCode.Models
{
    [Serializable]
    public class ProjectFolder : BaseViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;
        public IEnumerable<ProjectFolder> SubDirectories
        {
            get
            {
                try
                {
                    return _DirectoryInfo
                       .EnumerateDirectories()
                       .Select(dir_info => new ProjectFolder(dir_info.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<ProjectFolder>();
            }
        }

        public IEnumerable<ProjectFile> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                       .EnumerateFiles()
                       .Select(file => new ProjectFile(file.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<ProjectFile>();
            }
        }

        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    return SubDirectories.Cast<object>().Concat(Files);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<object>();
            }
        }

        public DateTime CreationTime => _DirectoryInfo.CreationTime;

        public ProjectFolder(string Path) => _DirectoryInfo = new DirectoryInfo(Path);
        public ProjectFolder() => _DirectoryInfo = null;
        public string Path => _DirectoryInfo.FullName;
        public string Name => _DirectoryInfo.Name;
        public override string ToString()
        {
            return Name;
        }
    }
}
