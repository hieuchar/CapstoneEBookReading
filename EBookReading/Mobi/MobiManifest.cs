using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EBookReading.Mobi
{
    class MobiManifest
    {
        private List<MobiManifestItem> _itemManifest;
        public List<MobiManifestItem> ItemManifest
        {
            get { return _itemManifest; }
            set { _itemManifest = value; }
        }
        public MobiManifest()
        {
            ItemManifest = new List<MobiManifestItem>();
        }
        public static void BuildManifest(ref MobiContainer container, string ManafestFilePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNodeList xmlNodeList;
            XmlAttributeCollection xmlAttributeCollection;
            xmlDocument.Load(ManafestFilePath);
            xmlNodeList = xmlDocument.GetElementsByTagName("*");
            foreach (XmlElement element in xmlNodeList)
            {
                if (element.Name.Contains("manifest"))
                {
                    xmlNodeList = element.GetElementsByTagName("*");
                    break;
                }
            }
            string id, href, media;
            foreach (XmlElement element in xmlNodeList)
            {
                id = href = media = "";

                if (element.Name.Contains("item"))
                {
                    xmlAttributeCollection = element.Attributes;
                    foreach (XmlAttribute attribute in xmlAttributeCollection)
                    {
                        if (attribute.Name == "id")
                        {
                            id = attribute.InnerText;
                            continue;
                        }
                        if (attribute.Name == "href")
                        {
                            href = attribute.InnerText;
                            continue;
                        }
                        if (attribute.Name == "media-type")
                        {
                            media = attribute.InnerText;
                            continue;
                        }
                    }
                    container.ContentOPF.ContentManifest.ItemManifest.Add(new MobiManifestItem(id, href, media));
                }
            }
        }
    }
}

