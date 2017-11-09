﻿using System;
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
        public List<string> GetFirstChapter()
        {
            var sections = book.NewContainer.ContentOPF.ContentToC.ePubNavMap.NavMap;
            int start = sections.Min(x => x.PlayOrder);
            return GetChapter(ref start, 0);
        }
        /// <summary>
        /// Gets a chapter from the book       
        /// </summary>
        /// <param name="ChapterTitle">Chapter title to search for</param>
        /// <param name="increment"> Which direction to get next chapter. Will be 0 if the user opens a chapter from ToC on left</param>
        /// <returns></returns>
        public List<string> GetChapter(string ChapterTitle)
        {
            var sections = book.NewContainer.ContentOPF.ContentToC.ePubNavMap.NavMap;
            MobiSection res = null;
            foreach (MobiNavPoint s in sections)
            {
                if (s.Text == ChapterTitle)
                {
                    res = book.NewContainer.FullBook.BookContents.Where(x => x.PlayOrder == s.PlayOrder).First();
                }
            }
            return res.Paragraph;
        }
        public List<string> GetChapter(ref int ChapterOrder, int increment)
        {
            //var sections = book.NewContainer.ContentOPF.ContentToC.ePubNavMap.NavMap;
            //if (sections.Count > 1)
            //{

            MobiSection res = null;
            if (ChapterOrder + increment < book.NewContainer.FullBook.BookContents.Count && ChapterOrder + increment >= 0)
            {
                res = book.NewContainer.FullBook.BookContents[ChapterOrder + increment];
                ChapterOrder += increment;
                return res.Paragraph;
            }
            //foreach (MobiNavPoint s in sections)
            //{
            //    if (s.PlayOrder == ChapterOrder + increment)
            //    {
            //        res = book.NewContainer.FullBook.BookContents.Where(x => x.PlayOrder == s.PlayOrder).FirstOrDefault();
            //    }
            //}
            //if (res != null)
            //{
            //    ChapterOrder += increment;
            //    return res.Paragraph;
            //}
            //else if (ChapterOrder + increment > sections.Min(x => x.PlayOrder) && ChapterOrder + increment <= sections.Max(x => x.PlayOrder))
            //{
            //    ChapterOrder += increment;
            //    return GetChapter(ref ChapterOrder, increment);
            //}
            else
            {
                return GetChapter(ref ChapterOrder, 0);
            }
            //}
            //else
            //{
            //    return LoadFromContent(ref ChapterOrder, increment);
            //}
        }
        //Loads from the raw content of the mobi book if the navmap is broken.
        public List<string> LoadFromContent(ref int ChapterOrder, int increment)
        {
            var sections = book.NewContainer.FullBook.BookContents;
            int order = ChapterOrder;
            MobiSection res = null;
            res = sections.Where(x => x.PlayOrder == order + increment).FirstOrDefault();
            if (res != null)
            {
                ChapterOrder += increment;
                return res.Paragraph;
            }
            else if (ChapterOrder + increment > sections.Min(x => x.PlayOrder) && ChapterOrder + increment <= sections.Max(x => x.PlayOrder))
            {
                ChapterOrder += increment;
                return LoadFromContent(ref ChapterOrder, increment);
            }
            else
            {
                return LoadFromContent(ref ChapterOrder, 0);
            }
        }
        /// <summary>
        /// Checks the NavMap and returns the number of the chapter title in the play order.
        /// </summary>
        /// <param name="ChapterTitle"></param>
        /// <returns></returns>
        public int GetPlayOrder(string ChapterTitle)
        {
            var sections = book.NewContainer.ContentOPF.ContentToC.ePubNavMap.NavMap;
            foreach (MobiNavPoint s in sections)
            {
                if (s.Text == ChapterTitle)
                {
                    return s.PlayOrder;
                }
            }
            return 0;
        }
        /// <summary>
        /// Returns the Table of Contents as a list of strings
        /// </summary>
        /// <returns></returns>
        public List<string> GetToC()
        {
            List<string> ToC = new List<string>();
            var NavMap = book.NewContainer.ContentOPF.ContentToC.ePubNavMap.NavMap;
            foreach (MobiNavPoint np in NavMap)
            {
                ToC.Add(np.Text);
            }
            return ToC;
        }
    }
}

