using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpCompress.Archives.Rar;

namespace EBookReading.CBR
{
    class CBRBook
    {
        private string _localDirectory;
        private IEnumerable<RarArchiveEntry> _entries;
        
        public string LocalDirectory
        {
            get { return _localDirectory; }
            set { _localDirectory = value; }
        }
        public IEnumerable<RarArchiveEntry> Entries
        {
            get { return _entries; }
        }


        public void LoadBook(string FilePath)
        {
            LocalDirectory = FilePath;
            RarArchive r = RarArchive.Open(FilePath);
            _entries = r.Entries.Where(entry => entry.Size > 0).OrderBy(entry => entry.Key);          
        }
        public Image LoadPage(int PageNumber)
        {
            Stream m = _entries.ElementAt(PageNumber).OpenEntryStream();
            Image img = Image.FromStream(m);             
            m.Dispose();
            return img;
        }
     
    }
}
