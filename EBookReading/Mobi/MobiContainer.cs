using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading.Mobi
{
    class MobiContainer
    {
        private MobiContent _contentOPF;
        private string _fullPath;
        private MobiBookContent _fullBook;

        public MobiContent ContentOPF
        {
            get { return _contentOPF; }
            set { _contentOPF = value; }
        }

        public string FullPath
        {
            get { return _fullPath; }
            set { _fullPath = value; }
        }

        public MobiBookContent FullBook
        {
            get { return _fullBook; }
            set { _fullBook = value; }
        }

        public MobiContainer()
        {
            ContentOPF = new MobiContent();
            FullBook = new MobiBookContent();
        }
    }
}
