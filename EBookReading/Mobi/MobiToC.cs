using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EBookReading.Mobi
{
    class MobiToC
    {
        string _docTitle;
        int _depth;
        private MobiNavigationMap _navMap;

        public MobiToC()
        {
            DocTitle = "";
            Depth = 1;
            MobiNavMap = new MobiNavigationMap();
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

        public MobiNavigationMap MobiNavMap
        {
            get { return _navMap; }
            set { _navMap = value; }
        }

        public static void GetDocTitle(ref MobiContainer container, string ToCFilePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNodeList xmlNodeList;
            xmlDocument.Load(ToCFilePath);
            xmlNodeList = xmlDocument.GetElementsByTagName("*");
            foreach (XmlElement element in xmlNodeList)
            {
                if (element.Name.Contains("docTitle"))
                {
                    container.ContentOPF.ContentToC.DocTitle = element.FirstChild.InnerText;
                    break;
                }
            }
        }

    }
}
