using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EBookReading.Epub
{
    class Manifest
    {
        private List<ManifestItem> _itemManifest;

        public List<ManifestItem> ItemManifest
        {
            get { return _itemManifest; }
            set { _itemManifest = value; }
        }

        public Manifest()
        {
            ItemManifest = new List<ManifestItem>();
        }

        public static void BuildManifest(ref Container container, string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNodeList xmlNodeList;
            XmlAttributeCollection xmlAttributeCollection;

            xmlDocument.Load(Path.Combine(path, container.FullPath));            
            
            //Same way that we searched for the <metadata> tag, we're going to look for the <manifest> tag. The <manifest>
            //tag contains the list of all the documents the book includes; from the Table of Contents to the Chapters
            //and Volumes. It contains three fields: id (an identifier), href (the path/name of the file), media-type (specifies
            //the type of document).

            xmlNodeList = xmlDocument.GetElementsByTagName("*");
            foreach (XmlElement element in xmlNodeList)
            {
                if (element.Name.Contains("manifest"))
                {
                    xmlNodeList = element.GetElementsByTagName("*");
                    break;
                }
            }

            //Now, we're going to cycle through the tags within the <manifest> tag, and pull out the attributes of each item.
            //Again, there's probably a better way of doing this, but for now, this works. It will cycle through the attributes,
            //assign their values to temporary variables, which will then be used to construct a new ManifestItem, which will
            //be added to the Manifest.

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

                    //So this is really starting to get unwieldy...surely there's a shorter way of notating this depth?

                    container.ContentOPF.ContentManifest.ItemManifest.Add(new ManifestItem(id, href, media));
                }
            }
        }
    }
}
