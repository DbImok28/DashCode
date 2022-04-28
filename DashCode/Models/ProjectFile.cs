using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DashCode.Models
{
    public class ProjectFile
    {
        public ProjectFile()
        {
            Path = null;
            Name = null;
            Extension = null;
        }
        public ProjectFile(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileName(path);
            Extension = System.IO.Path.GetExtension(path);
        }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
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
            if (string.IsNullOrEmpty(Path) && Path != null)
            {
                StreamReader f = new StreamReader(Path);
                while (!f.EndOfStream)
                {
                    _Content += f.ReadLine();
                }
                f.Close();
            }
        }
        public void Save()
        {
            if (Path == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "All files (*.*)|*.*";
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Path = saveFileDialog1.FileName;
                    Name = System.IO.Path.GetFileName(Path);
                    Extension = System.IO.Path.GetExtension(Path);
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
