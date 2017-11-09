using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EBookReading.Mobi
{
    class MobiBookContent
    {
        private List<MobiSection> _bookContents;
        private static string path;
        public List<MobiSection> BookContents
        {
            get { return _bookContents; }
            set { _bookContents = value; }
        }
        public MobiBookContent()
        {
            BookContents = new List<MobiSection>();
            path = System.IO.Path.GetTempPath() + "VoBookFiles";
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
        }

        public static void BuildBook(ref MobiContainer container,  string BookFilePath)
        {
            
            HtmlDocument htmlDoc = new HtmlDocument();            
            IEnumerable<HtmlNode> htmlNodeList;
            MobiSection tempSection;
            //xmlDocument.XmlResolver = null;
            //For each NavPoint item in the NavMap, we're going to load the src object, and pull in each paragraph in the chapter
            //as a separate string object in a section. Then we'll create a section for each chapter, and add each section to the book.
            htmlDoc.Load(BookFilePath, Encoding.UTF8);
            htmlNodeList = htmlDoc.DocumentNode.SelectNodes("//div").Where(x => x.InnerHtml.Length >= 5);
            Console.WriteLine();
            foreach (HtmlNode node in htmlNodeList)
            {
                
                tempSection = new MobiSection();
                tempSection.Paragraph.Add(node.OuterHtml);
                container.FullBook.BookContents.Add(tempSection);
            }            
        }
    }
}
