using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading.Mobi
{
    class MobiReader
    {
        MobiBook book;
        /// <summary>
        ///Creates a new epub book and loads all the content in
        ///
        /// </summary>
        /// <param name="FilePath">Direct file path to the epub file</param>
        public void CreateBook(string FilePath)
        {
            book = new MobiBook();
            book.LoadBook(FilePath);
        }
        public void CreatePartialBook(string FilePath)
        {
            book = new MobiBook();
            book.LoadInfo(FilePath);
        }
        public string GetTitle()
        {
            return book.NewContainer.ContentOPF.Title;
        }
        public string GetAuthor()
        {
            return book.NewContainer.ContentOPF.Author;
        }
        public string GetPublisher()
        {
            return book.NewContainer.ContentOPF.Publisher;
        }
        public string GetPublishDate()
        {
            return book.NewContainer.ContentOPF.Date;
        }
        public string GetBookPath()
        {
            return book.HTMlDirectory;
        }      
        public string GetChapter(ref int ChapterOrder, int increment)
        {                
            var navmap = book.NewContainer.ContentOPF.ContentToC.MobiNavMap.NavMap;
            string filepos = "";
            if (ChapterOrder - 1 + increment < navmap.Count && ChapterOrder + increment >= 0)
            {
                foreach (MobiNavPoint s in navmap)
                {
                    if (s.PlayOrder == ChapterOrder + increment)
                    {
                        filepos = s.Src;
                        ChapterOrder += increment;
                        break;
                    }
                }
            }
            else
            {
                return GetChapter(ref ChapterOrder, 0);
            }
            return filepos;            
        }       
        /// <summary>
        /// Checks the NavMap and returns the number of the chapter title in the play order.
        /// </summary>
        /// <param name="ChapterTitle"></param>
        /// <returns></returns>
        public int GetPlayOrder(string ChapterTitle)
        {
            var navmap = book.NewContainer.ContentOPF.ContentToC.MobiNavMap.NavMap;
            int playorder = 0;
            foreach (MobiNavPoint s in navmap)
            {
                if (s.Text == ChapterTitle)
                {
                    playorder = s.PlayOrder;
                }
            }
            return playorder;
        }
        public string GetFilePos(string ChapterTitle)
        {
            string filepos = "";
            var navmap = book.NewContainer.ContentOPF.ContentToC.MobiNavMap.NavMap;
            foreach (MobiNavPoint s in navmap)
            {
                if (s.Text == ChapterTitle)
                {
                    filepos = s.Src;
                }
            }
            return filepos;
        }
        /// <summary>
        /// Returns the Table of Contents as a list of strings
        /// </summary>
        /// <returns></returns>
        public List<string> GetToC()
        {
            List<string> ToC = new List<string>();
            var NavMap = book.NewContainer.ContentOPF.ContentToC.MobiNavMap.NavMap;
            foreach (MobiNavPoint np in NavMap)
            {
                ToC.Add(np.Text);
            }
            return ToC;
        }
    }
}

