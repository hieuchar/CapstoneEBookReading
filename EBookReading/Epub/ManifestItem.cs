using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBookReading.Epub
{
    class ManifestItem
    {
        private string _id;
        private string _href;
        private string _mediaType;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string href
        {
            get { return _href; }
            set { _href = value; }
        }

        public string MediaType
        {
            get { return _mediaType; }
            set { _mediaType = value; }
        }

        public ManifestItem()
        {
        }

        public ManifestItem(string id, string thehref, string mediaType)
        {
            ID = id;
            href = thehref;
            MediaType = mediaType;
        }
    }
}
