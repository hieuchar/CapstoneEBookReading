using SharpCompress.Archives.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading.CBZ
{
    class CBZBook
    {
        private string _localDirectory;
        private IEnumerable<ZipArchiveEntry> _entries;
        
        public string LocalDirectory
        {
            get { return _localDirectory; }
            set { _localDirectory = value; }
        }

        public IEnumerable<ZipArchiveEntry> Entries
        {
            get { return _entries; }            
        }
        public CBZBook()
        {
            
        }
        public void LoadBook(string FilePath)
        {
            LocalDirectory = FilePath;
            ZipArchive z = ZipArchive.Open(FilePath);
            _entries = z.Entries.OrderBy(ze => ze.Key);           
        }
        public Image LoadPage(int PageNumber)
        {
            Stream m = _entries.ElementAt(PageNumber).OpenEntryStream();
            Image img = Image.FromStream(m);
            
            m.Dispose();
            m.Close();
            return img;
        }
     
    }
}
