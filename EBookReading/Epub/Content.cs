using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EBookReading.Epub
{
    class Content
    {
        private string _title;
        private string _author;
        private string _language;
        private string _id;
        private string _date;
        private string _publisher;
        private string _subject;
        private string _source;
        private string _rights;
        private Manifest _contentManifest;
        private Spine _contentSpine;
        private ToC _contentToC;

        public Content()
        {
            ContentManifest = new Manifest();
            ContentSpine = new Spine();
            ContentToC = new ToC();
        }

        public Content(string title, string author, string language)
        {
            Title = title;
            Author = author;
            Language = language;
            ContentManifest = new Manifest();
            ContentSpine = new Spine();
            ContentToC = new ToC();
        }

        public Content(string title, string author, string language, string id, string date, string publisher, string subject, string source, string rights)
        {
            Title = title;
            Author = author;
            Language = language;
            ID = id;
            Date = date;
            Publisher = publisher;
            Subject = subject;
            Source = source;
            Rights = rights;
            ContentManifest = new Manifest();
            ContentSpine = new Spine();
            ContentToC = new ToC();
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public string Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }

        public Manifest ContentManifest
        {
            get { return _contentManifest; }
            set { _contentManifest = value; }
        }

        public Spine ContentSpine
        {
            get { return _contentSpine; }
            set { _contentSpine = value; }
        }

        public ToC ContentToC
        {
            get { return _contentToC; }
            set { _contentToC = value; }
        }

        public static void GetMetadataFromContent(ref Container container, string path)
        {
            XmlNodeList xmlNodeList;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.XmlResolver = null;
            //settings.ConformanceLevel = ConformanceLevel.Fragment;
            XmlReader xmlReader = XmlReader.Create(Path.Combine(path, container.FullPath), settings);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);

            //METADATA CONSTRUCTION

            //We now need to find the <metadata> tag, but the prefix may vary (in my testing the tag name was actually
            //<opf:metadata>. So what is done here is to get all the tags, into a XmlNodeList, and cycle through until
            //we find one containing "metadata". There's probably a more efficient way, I will check later.

            xmlNodeList = xmlDocument.GetElementsByTagName("*");
            foreach (XmlElement element in xmlNodeList)
            {
                if (element.Name.Contains("metadata"))
                {

                    //If we find the <metadata> tag, we now want to get all the child fields inside it. We can then
                    //break out of the foreach loop since we've found what we're looking for.

                    xmlNodeList = element.GetElementsByTagName("*");
                    break;
                }
            }

            //Okay, so now we have the tags inside the <metadata> tag as an XmlNodeList. We're going to cycle through and assign
            //all of the included properties. Most ePub documents will only have three or four of these. The only three required
            //to be included by the ePub standard are: Title, Author, and Language.

            foreach (XmlElement element in xmlNodeList)
            {
                var con = container.ContentOPF;
                

                    if (element.Name.Contains("title"))
                    {
                        con.Title = element.InnerText;
                        continue;
                    }

                    if (element.Name.Contains("creator"))
                    {
                        con.Author = element.InnerText;
                        continue;
                    }

                    if (element.Name.Contains("language"))
                    {
                        con.Language = element.InnerText;
                        continue;
                    }

                    if (element.Name.Contains("identifier"))
                    {
                        con.ID = element.InnerText;
                        continue;
                    }

                    if (element.Name.Contains("date"))
                    {
                        con.Date = element.InnerText;
                        continue;
                    }

                    if (element.Name.Contains("publisher"))
                    {
                        con.Publisher = element.InnerText;
                        continue;
                    }

                    if (element.Name.Contains("subject"))
                    {
                        con.Subject = element.InnerText;
                        continue;
                    }

                    if (element.Name.Contains("source"))
                    {
                        con.Source = element.InnerText;
                        continue;
                    }

                    if (element.Name.Contains("rights"))
                    {
                        con.Rights = element.InnerText;
                        continue;
                    }
                
            }
        }

    }
}
