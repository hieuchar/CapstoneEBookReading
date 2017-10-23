using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace EBookReading.Epub
{
    class Style
    {
        private List<string> _stylesheets;
        public Style()
        {
            _stylesheets = new List<string>();
        }
        public List<string> StyleSheets
        {
            get { return _stylesheets; }
            set { _stylesheets = value; }
        }
        public static void LoadStyle(ref Container container, ref ZipArchive z)
        {
            List<ZipArchiveEntry> temp = new List<ZipArchiveEntry>();
            foreach(ZipArchiveEntry entry in z.Entries)
            {
                if (entry.Name.Contains("css"))
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.Load(entry.Open());
                    container.ContentOPF.StyleSheet._stylesheets.Add(doc.DocumentNode.OuterHtml);
                    
                }
            }
            
        }
        
        
    }
}
