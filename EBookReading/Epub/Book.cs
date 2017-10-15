using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace EBookReading.Epub
{
    class Book
    {
        private List<Section> _bookContents;

        public List<Section> BookContents
        {
            get { return _bookContents; }
            set { _bookContents = value; }
        }

        public Book()
        {
            BookContents = new List<Section>();
        }

        public static void BuildBook(ref Container container, string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNodeList xmlNodeList;
            
            //In this section, we create a new Book object, which contains a list of Section objects (think of as chapters)
            //which contain a list of strings (the paragraphs in the chapters). The xmldoc.XmlResolver line is required to 
            //ignore the DTD line in the XHTML file. 
                        
            Section tempSection;
            xmlDocument.XmlResolver = null;

            //For each NavPoint item in the NavMap, we're going to load the src object, and pull in each paragraph in the chapter
            //as a separate string object in a section. Then we'll create a section for each chapter, and add each section to the book.

            foreach (NavPoint _navPoint in container.ContentOPF.ContentToC.ePubNavMap.NavMap)
            {
                tempSection = new Section();    
            
                //bug here currently if the navPoint Src contains a path, not just a file name.

                string[] href = Directory.GetFiles(path, _navPoint.Src, SearchOption.AllDirectories);
                //xmlDocument.Load(Path.Combine(path, _navPoint.Src));
                xmlDocument.Load(Path.GetFullPath(href[0]));
                xmlNodeList = xmlDocument.GetElementsByTagName("p");
                
                foreach (XmlNode _paragraph in xmlNodeList)
                {
                    tempSection.Paragraph.Add(_paragraph.InnerText);
                }
                
                container.FullBook.BookContents.Add(tempSection);
            }
        }
    }
}
