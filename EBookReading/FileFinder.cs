using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading
{
    public class FileManipulator
    {
        //Search a directory for all ebooks within it
        public List<string> FindFiles(string path)
        {
            
            var pdfList = System.IO.Directory.GetFiles(path, "*.pdf");
            var epubList = System.IO.Directory.GetFiles(path, "*.epub");
            var mobiList = System.IO.Directory.GetFiles(path, "*.mobi");
            var txtList = System.IO.Directory.GetFiles(path, "*.txt");
            List<string> result = new List<string>();
            result.AddRange(pdfList);
            result.AddRange(epubList);
            result.AddRange(mobiList);
            result.AddRange(txtList);
            return result;
        } 

    }
}
