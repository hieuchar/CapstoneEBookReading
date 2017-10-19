using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace EBookReading.Epub
{
    public class EpubReader
    {
        EpubBook book;
        /// <summary>
        ///Creates a new epub book and loads all the content in
        ///
        /// </summary>
        /// <param name="FilePath">Direct file path to the epub file</param>
        public void CreateBook(string FilePath)
        {
            book = new EpubBook();            
            book.LoadBook(FilePath);  
        }
        public string GetTitle()
        {
            return book.NewContainer.ContentOPF.Title;
        }
        public string GetAuthor()
        {
            return book.NewContainer.ContentOPF.Author;
        }
        public List<string> GetFirstChapter()
        {
            return book.NewContainer.FullBook.BookContents[0].Paragraph;
        }
        public List<string> GetChapter(string ChapterTitle)
        {
            var sections = book.NewContainer.ContentOPF.ContentToC.ePubNavMap.NavMap;
            Section res = null;
            foreach(NavPoint s in sections)
            {
                if(s.Text == ChapterTitle)
                {                    
                     res = book.NewContainer.FullBook.BookContents.Where(x=> x.PlayOrder == s.PlayOrder).First();
                }
            }
            return res.Paragraph;
        }
        public List<string> GetToC()
        {
            List<string> ToC = new List<string>();
            var NavMap = book.NewContainer.ContentOPF.ContentToC.ePubNavMap.NavMap;
            foreach (NavPoint np in NavMap)
            {
                ToC.Add(np.Text);
            }
            return ToC;
        }
    }
}
