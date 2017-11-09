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
        private static string path;
        public List<Section> BookContents
        {
            get { return _bookContents; }
            set { _bookContents = value; }
        }
        public Book()
        {
            BookContents = new List<Section>();
            path = System.IO.Path.GetTempPath() + "VoBookFiles";
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
        }
        public static Section Build(ref Container container, ref ZipArchive z, HtmlNodeCollection htmlNodeList)
        {
            Section TempSection = new Section();

            return TempSection;
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
                int counter = 0;
                foreach (string filename in container.ContentOPF.ContentSpine.ItemRef)
                {
                    tempSection = new Section();
                    tempSection.PlayOrder = counter++;
                    var x = z.Entries.Where(y => y.Name.Contains(filename));
                    ZipArchiveEntry ZipSection = z.GetEntry(x.First().FullName);
                    htmlDoc.Load(ZipSection.Open(), Encoding.UTF8);
                    htmlNodeList = htmlDoc.DocumentNode.SelectNodes("//p");
                    if (htmlNodeList != null)
                    {
                        foreach (HtmlNode _paragraph in htmlNodeList)
                        {
                            path = System.IO.Path.GetTempPath() + "BookImages";
                            if (_paragraph.OuterHtml.Contains("img"))
                            {
                                string input = _paragraph.InnerHtml;
                                string original = Regex.Match(input, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                                if (original.Length > 1)
                                {
                                    string temp = original.Replace("\\", "");
                                    string[] imageValue = temp.Split('/');
                                    var image = z.Entries.Where(img => img.FullName.Contains(imageValue[imageValue.Count() - 1])).First();
                                    path += "\\" + imageValue[imageValue.Count() - 1];
                                   
                                    image.ExtractToFile(path, true);
                                    _paragraph.InnerHtml = _paragraph.InnerHtml.Replace(original, "file://" + path);
                                }

                            }
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
                    htmlNodeList = htmlDoc.DocumentNode.SelectNodes("//body");
                    if (htmlNodeList != null)
                    {
                        foreach (HtmlNode _paragraph in htmlNodeList)
                        {
                            if (_paragraph.OuterHtml.Contains("img"))
                            {
                                path = System.IO.Path.GetTempPath() + "BookImages";
                                string input = _paragraph.InnerHtml;
                                string original = Regex.Match(input, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;

                                if (original.Length > 1)
                                {
                                    string temp = original;
                                    string[] imageValue = temp.Split('/');
                                    var image = z.Entries.Where(img => img.FullName.Contains(imageValue[imageValue.Count() - 1])).First();
                                    path += "\\" + imageValue[imageValue.Count() - 1];
                                    image.ExtractToFile(path, true);
                                    _paragraph.InnerHtml = _paragraph.InnerHtml.Replace(original, "file://" + path);
                                }
                            }
                            else if (_paragraph.OuterHtml.Contains("image"))
                            {
                                path = System.IO.Path.GetTempPath() + "BookImages";
                                string input = _paragraph.InnerHtml;
                                string s = input.Replace("xlink:href", "src");
                                string original = Regex.Match(s, "src=[\\][\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                                if (original.Length > 1)
                                {
                                    string temp = original;
                                    string[] imageValue = temp.Split('/');
                                    var image = z.Entries.Where(img => img.FullName.Contains(imageValue[imageValue.Count() - 1])).First();
                                    path += "\\" + imageValue[imageValue.Count() - 1];
                                    image.ExtractToFile(path, true);
                                    _paragraph.InnerHtml = _paragraph.InnerHtml.Replace(original, "file://" + path);
                                    _paragraph.InnerHtml = _paragraph.InnerHtml.Replace("<image", "<img");
                                    _paragraph.InnerHtml = _paragraph.InnerHtml.Replace("</image", "</img");
                                    _paragraph.InnerHtml = _paragraph.InnerHtml.Replace("xlink:href", "src");
                                }
                            }
                            tempSection.Paragraph.Add(_paragraph.OuterHtml);
                        }
                    }
                    container.FullBook.BookContents.Add(tempSection);
                }
            }
        }
    }
}
