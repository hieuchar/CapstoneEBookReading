using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EBookReading.Epub
{
    class EpubBook
    {
        public Container _newContainer;
        private string _localDirectory;

        public EpubBook()
        {
            NewContainer = new Container();
        }

        public string LocalDirectory
        {
            get { return _localDirectory; }
            set { _localDirectory = value; }
        }
        
        public Container NewContainer
        {
            get { return _newContainer; }
            set { _newContainer = value; }
        }

        public static bool isValid(string path)
        {
            //Check to ensure that this is a standard, valid ePub document. By definition, the mimetype file 
            //must be included, and it must contain only "application/epub+zip"
            
            //string localPath = Directory.GetCurrentDirectory();
            using (StreamReader sr = new StreamReader(Path.Combine(path, "mimetype")))
            {
                return (sr.ReadLine() == "application/epub+zip");
            }
        }
        
        public void LoadBook(string path)
        {
            LocalDirectory = path;
            //if (isValid(path))
            {
                Container.GetContentFromContainer(ref _newContainer, LocalDirectory);
                Content.GetMetadataFromContent(ref _newContainer, LocalDirectory);
                Manifest.BuildManifest(ref _newContainer, LocalDirectory);
                Spine.BuildSpine(ref _newContainer, LocalDirectory);
                ToC.GetDocTitle(ref _newContainer, LocalDirectory);
                NavigationMap.BuildNavMap(ref _newContainer, LocalDirectory);
                Book.BuildBook(ref _newContainer, LocalDirectory);                
            }
        }
    }
}
