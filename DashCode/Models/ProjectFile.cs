using DashCode.ViewModules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DashCode.Models
{
    public class ProjectFile : BaseViewModel
    {

        public ProjectFile(string Path) => _FileInfo = new FileInfo(Path);
        public ProjectFile()
        {
            _FileInfo = null;
        }
        private FileInfo _FileInfo;
        public string Path => _FileInfo.FullName;
        public string Name => _FileInfo?.Name ?? "-";
        public DateTime CreationTime => _FileInfo.CreationTime;
        public string Extension => _FileInfo.Extension;
        private string _Content;
        public string Content
        {
            get
            {
                if (_Content == null)
                {
                    ReadContent();
                }
                return _Content;
            }
            set => _Content = value;
        }
        public override string ToString()
        {
            return Name;
        }
        public void ReadContent()
        {
            _Content = "";
            if (_FileInfo != null)
            {
                _Content = File.ReadAllText(Path);
            }
        }
        public void Save()
        {
            if (_FileInfo == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "All files (*.*)|*.*";
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _FileInfo = new FileInfo(saveFileDialog1.FileName);
                    File.WriteAllText(Path, Content);
                }
            }
            else
            {
                File.WriteAllText(Path, Content);
            }
        }
    }
}
