using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace EBookReading.Mobi
{
    class MobiNavigationMap
    {
        List<MobiNavPoint> _navMap;

        public List<MobiNavPoint> NavMap
        {
            get { return _navMap; }
            set { _navMap = value; }
        }

        public MobiNavigationMap()
        {
            NavMap = new List<MobiNavPoint>();
        }

        public void AddNavPoint(MobiNavPoint newNavPoint)
        {
            NavMap.Add(newNavPoint);
        }

        public void ClearNavMap()
        {
            NavMap.Clear();
        }
        public static void BuildNavMapFromHtml(ref MobiContainer container, string BookFilePath)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            IEnumerable<HtmlNode> htmlNodeList;           
            htmlDoc.Load(BookFilePath, Encoding.UTF8);
            htmlNodeList = htmlDoc.DocumentNode.SelectNodes("//a").Distinct();
            Console.WriteLine();
            int counter = 1;
            for (int i = (htmlNodeList.Count() - 1) / 2; i < htmlNodeList.Count();i++ )
            {
                if(htmlNodeList.ElementAt(i).Id != "")
                {
                    container.ContentOPF.ContentToC.MobiNavMap.NavMap.Add(new MobiNavPoint(counter, counter +"", counter++ +"", "#" + htmlNodeList.ElementAt(i).Id));
                }
            }
        }
        public static void BuildNavMap(ref MobiContainer container, string NavMapFilePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("*");
            xmlDocument.Load(NavMapFilePath);
            xmlNodeList = xmlDocument.GetElementsByTagName("*");
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
                xmlNodeList = element.GetElementsByTagName("content");
                foreach (XmlElement subElement in xmlNodeList)
                {
                    if (subElement.Name == "content")
                    {
                        string s = subElement.GetAttribute("src");
                        var y = Regex.Match(s, "#.*");
                        navPointSrc = y.Value;
                        break;
                    }
                }
                container.ContentOPF.ContentToC.MobiNavMap.NavMap.Add(new MobiNavPoint(navPointPlayOrder, navPointID, navLabel, navPointSrc));
            }
        }
    }
}

