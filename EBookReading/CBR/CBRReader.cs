using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EBookReading.CBR
{
    public class CBRReader : ComicBookAbstract
    {
        CBRBook book;
        public override void CreateBook(string FilePath)
        {
            book = new CBRBook();
            book.LoadBook(FilePath);
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
