using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                        navPointSrc = subElement.GetAttribute("src");
                        break;
                    }
                }
                container.ContentOPF.ContentToC.ePubNavMap.NavMap.Add(new MobiNavPoint(navPointPlayOrder, navPointID, navLabel, navPointSrc));
            }
        }
    }
}

