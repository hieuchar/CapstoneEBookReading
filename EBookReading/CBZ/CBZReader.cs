using SharpCompress.Archives.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EBookReading.CBZ
{
    public class CBZReader : ComicBookAbstract
    {
        CBZBook book;
        public override void CreateBook(string FilePath)
        {            
            book = new CBZBook();
            book.LoadBook(FilePath);            
        }

        public override string GetExtension()
        {
            return Path.GetExtension(book.LocalDirectory);
        }

        public override int GetMaxPage()
        {
            return book.Entries.Count();
        }

        public override Image GetPage(int PageNumber)
        {
            return book.LoadPage(PageNumber);
        }
    }
}
