
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading
{
    public class Book
    {
        public string Author { get; set; } = "Unknown Author";
        public string Title { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public Book(string FilePath)
        {
            this.FilePath = FilePath;
            Title = Path.GetFileNameWithoutExtension(FilePath);
            Extension = Path.GetExtension(FilePath);
        }
        public void AddInformation(List<string> info)
        {
            Title = info[0];
            Author = info[1];
        }
    }
}
