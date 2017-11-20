
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading
{
    public class BookInfo
    {
        private string _author = "Unknown Author";
        private string _publisher = "Unknown Publisher";
        private string _date = "Unknown Publish Date";
        private string _title;
        private string _extension;
        private string _filePath;
        public string Title { get => _title; set => _title = value; }
        public string Author { get => _author; set => _author = value; }
        public string FilePath { get => _filePath; set => _filePath = value; }
        public string Extension { get => _extension; set => _extension = value; }
        public string Date { get => _date; set => _date = value; }
        public string Publisher { get => _publisher; set => _publisher = value; }

        public BookInfo(string FilePath)
        {
            this.FilePath = FilePath;
            Title = Path.GetFileNameWithoutExtension(FilePath);
            Extension = Path.GetExtension(FilePath);
        }
        public void AddInformation(List<string> info)
        {
            Title = info[0];
            Author = info[1];
            if (info.Count > 2)
            {
                Date = info[2];
                Publisher = info[3];
            }
        }
        public bool Contains(string target)
        {
            return _author.ToLower().Contains(target) || _title.ToLower().Contains(target) || _extension.ToLower().Contains(target);
        }
    }
}
