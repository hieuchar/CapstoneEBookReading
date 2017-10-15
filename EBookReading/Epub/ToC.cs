using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EBookReading.Epub
{
    class ToC
    {
        string _docTitle;
        int _depth;
        private NavigationMap _navMap;

        public ToC()
        {
            DocTitle = "";
            Depth = 1;
            ePubNavMap = new NavigationMap();
        }

        public string DocTitle
        {
            get { return _docTitle; }
            set { _docTitle = value; }
        }

        public int Depth
        {
            get { return _depth; }
            set { _depth = value; }
        }

        public NavigationMap ePubNavMap
        {
            get { return _navMap; }
            set { _navMap = value; }
        }

        public static void GetDocTitle(ref Container container, string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNodeList xmlNodeList;
            
            //Open the XML file associated with the Table of Contents ID, whose source is defined
            //in the manifest.

            foreach (ManifestItem manifestItem in container.ContentOPF.ContentManifest.ItemManifest)
            {
                if (container.ContentOPF.ContentSpine.toc != null && manifestItem.ID.Contains(container.ContentOPF.ContentSpine.toc))
                {
                    string[] href = Directory.GetFiles(path, manifestItem.href, SearchOption.AllDirectories);
                    xmlDocument.Load(Path.GetFullPath(href[0]));
                    break;
                }
            }

            //Get the docTitle element.

            xmlNodeList = xmlDocument.GetElementsByTagName("*");
            foreach (XmlElement element in xmlNodeList)
            {
                if (element.Name.Contains("docTitle"))
                {

                    //The text is in the first child of <docTitle>, <text>

                    container.ContentOPF.ContentToC.DocTitle = element.FirstChild.InnerText;
                    break;
                }
            }
        }
    }
}
