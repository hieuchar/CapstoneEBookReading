using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.IO.Compression;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Text.RegularExpressions;

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

        public static void BuildBook(ref Container container, ref ZipArchive z)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNodeCollection htmlNodeList;
            //XmlDocument xmlDocument = new XmlDocument();
            //XmlNodeList xmlNodeList;

            //In this section, we create a new Book object, which contains a list of Section objects (think of as chapters)
            //which contain a list of strings (the paragraphs in the chapters). The xmldoc.XmlResolver line is required to 
            //ignore the DTD line in the XHTML file. 

            Section tempSection;
            //xmlDocument.XmlResolver = null;

            //For each NavPoint item in the NavMap, we're going to load the src object, and pull in each paragraph in the chapter
            //as a separate string object in a section. Then we'll create a section for each chapter, and add each section to the book.
            if (container.ContentOPF.ContentToC.ePubNavMap.NavMap.Count <= 1)
            {
                foreach (string filename in container.ContentOPF.ContentSpine.ItemRef)
                {
                    tempSection = new Section();
                    var x = z.Entries.Where(y => y.Name.Contains(filename));
                    ZipArchiveEntry ZipSection = z.GetEntry(x.First().FullName);
                    htmlDoc.Load(ZipSection.Open(), Encoding.UTF8);
                    htmlNodeList = htmlDoc.DocumentNode.SelectNodes("//p");
                    if (htmlNodeList != null)
                    {
                        foreach (HtmlNode _paragraph in htmlNodeList)
                        {
                            tempSection.Paragraph.Add(_paragraph.OuterHtml);
                        }
                    }
                    container.FullBook.BookContents.Add(tempSection);
                }
            }
            else
            {
                foreach (NavPoint _navPoint in container.ContentOPF.ContentToC.ePubNavMap.NavMap)
                {
                    
                    tempSection = new Section();
                    tempSection.PlayOrder = _navPoint.PlayOrder;
                    var x = z.Entries.Where(y => y.Name.Contains(_navPoint.Src));
                    ZipArchiveEntry ZipSection = z.GetEntry(x.First().FullName);
                    htmlDoc.Load(ZipSection.Open(), Encoding.UTF8);
                    htmlNodeList = htmlDoc.DocumentNode.SelectNodes("//p");
                    if (htmlNodeList != null)
                    {
                        foreach (HtmlNode _paragraph in htmlNodeList)
                        {
                            if (_paragraph.OuterHtml.Contains("img"))
                            {
                                string path = System.IO.Path.GetTempPath();

                                HtmlNodeCollection ChildNodes = _paragraph.ChildNodes;                                
                                string input = _paragraph.InnerHtml;
                                string original = Regex.Match(input, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;

                                if (original.Length > 1)
                                {
                                    string temp = original.Replace("\\", "");
                                    string[] imageValue = temp.Split('/');
                                    var image = z.Entries.Where(img => img.FullName.Contains(imageValue[imageValue.Count() - 1])).First();
                                    path += imageValue[imageValue.Count() - 1];
                                    image.ExtractToFile(path, true);
                                    _paragraph.InnerHtml = _paragraph.InnerHtml.Replace(original, "file://" + path);
                                }
                            }
                            tempSection.Paragraph.Add(_paragraph.OuterHtml);
                        }
                    }
                    htmlNodeList = htmlDoc.DocumentNode.SelectNodes("//image");
                    if(htmlNodeList != null)
                    {
                        foreach(HtmlNode _image in htmlNodeList)
                        {
                            string path = System.IO.Path.GetTempPath();
                            var y = _image.GetAttributeValue("xlink:href", null);
                            if (y != null)
                            {
                                string[] imageValue = y.Split('/');
                                var image = z.Entries.Where(img => img.FullName.Contains(imageValue[imageValue.Count() - 1])).First();
                                path += imageValue[imageValue.Count() - 1];
                                image.ExtractToFile(path, true);                                
                                _image.SetAttributeValue("xlink:href", "file://" + path);                                
                            }
                            string s = _image.OuterHtml.Replace("image", "img");
                            s = s.Replace("xlink:href", "src");
                            tempSection.Paragraph.Add(s);
                        }
                    }
                    //htmlNodeList = htmlDoc.DocumentNode.SelectNodes("//img");
                    //if(htmlNodeList != null)
                    //{
                    //    foreach (HtmlNode _image in htmlNodeList)
                    //    {
                    //        string path = System.IO.Path.GetTempPath();
                    //        var y = _image.GetAttributeValue("src", null);
                    //        if(y != null)
                    //        {
                    //            string[] imageValue = y.Split('/');
                    //            var image = z.Entries.Where(img => img.FullName.Contains(imageValue[imageValue.Count() - 1])).First();
                    //            path += imageValue[imageValue.Count() - 1];
                    //            image.ExtractToFile(path, true);
                    //            _image.SetAttributeValue("src", "file://" + path);
                    //        }
                    //        tempSection.Paragraph.Add(_image.OuterHtml);

                    //    }
                    //}
                    container.FullBook.BookContents.Add(tempSection);
                }
            }
        }
    }
}
