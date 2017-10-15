using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EBookReading.Epub
{
    class NavigationMap
    {
        List<NavPoint> _navMap;

        public List<NavPoint> NavMap
        {
            get { return _navMap; }
            set { _navMap = value; }
        }

        public NavigationMap()
        {
            NavMap = new List<NavPoint>();
        }

        public void AddNavPoint(NavPoint newNavPoint)
        {
            NavMap.Add(newNavPoint);
        }

        public void ClearNavMap()
        {
            NavMap.Clear();
        }

        public static void BuildNavMap(ref Container container, string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("*");
            
            //The navigation map is subdivided into navigation points (such as chapters, volumes, books)
            //We find <navMap> and get a list of all <navPoint> elements.

            foreach (ManifestItem manifestItem in container.ContentOPF.ContentManifest.ItemManifest)
            {
                if (container.ContentOPF.ContentSpine.toc != null && manifestItem.ID.Contains(container.ContentOPF.ContentSpine.toc))
                {
                    string[] href = Directory.GetFiles(path, manifestItem.href, SearchOption.AllDirectories);
                    string newPath = Path.GetFullPath(href[0]);
                    xmlDocument.Load(newPath);
                    xmlNodeList = xmlDocument.GetElementsByTagName("*");
                    break;
                }                
            }
            
            foreach (XmlElement element in xmlNodeList)
            {                
                if (element.Name == "navMap")
                {
                    xmlNodeList = element.GetElementsByTagName("navPoint");
                    break;
                }
            }

            foreach (XmlElement element in xmlNodeList)
            {
                string navLabel = element.FirstChild.FirstChild.InnerText;
                string navPointID = element.GetAttribute("id");
                int navPointPlayOrder = Convert.ToInt16(element.GetAttribute("playOrder"));
                string navPointSrc = "";

                //Okay, so here the source path is an attribute within the <content> element, like so:
                //<content src="part13.xhtml">

                xmlNodeList = element.GetElementsByTagName("content");
                foreach (XmlElement subElement in xmlNodeList)
                {
                    if (subElement.Name == "content")
                    {
                        navPointSrc = subElement.GetAttribute("src");
                        break;
                    }
                }

                //Now we take what we've pulled from this particular navPoint, and add it as a new NavPoint to the
                //newContainer's NavMap

                container.ContentOPF.ContentToC.ePubNavMap.NavMap.Add(new NavPoint(navPointPlayOrder, navPointID, navLabel, navPointSrc));
            }
        }
    }
}
