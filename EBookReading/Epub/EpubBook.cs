using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Compression;

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
        //Creates a partial ebook with only metadata for the app to display to the user. Does not load images/text.
        public void LoadInfo(string FilePath)
        {
            LocalDirectory = FilePath;
            ZipArchive z = ZipFile.OpenRead(FilePath);
            ZipArchiveEntry ContainerXml = z.GetEntry("META-INF/container.xml");
            Container.GetContentFromContainer(ref _newContainer, ref ContainerXml);
            ZipArchiveEntry ContentFile = z.GetEntry(_newContainer.FullPath);
            Stream MetaStream = ContentFile.Open();
            Content.GetMetadataFromContent(ref _newContainer, ref MetaStream);
        }
        public void LoadBook(string FilePath)
        {
            LocalDirectory = FilePath;
            //Extract the files for each part of the book
            ZipArchive z = ZipFile.OpenRead(FilePath);
            Style.LoadStyle(ref _newContainer, ref z);
            //Get the container.xml file from the epub archive
            ZipArchiveEntry ContainerXml = z.GetEntry("META-INF/container.xml");            
            Container.GetContentFromContainer(ref _newContainer, ref ContainerXml);

            //Pass in the opf file from the container.xml    
            //Build the spine, toc, and manifest
            ZipArchiveEntry ContentFile = z.GetEntry(_newContainer.FullPath);
            Stream MetaStream = ContentFile.Open();
            Stream ManifestStream = ContentFile.Open();
            Stream SpineStream = ContentFile.Open();
            Content.GetMetadataFromContent(ref _newContainer, ref MetaStream);
            Manifest.BuildManifest(ref _newContainer, ref ManifestStream);
            Spine.BuildSpine(ref _newContainer, ref SpineStream);
            ZipArchiveEntry ToCFile = null; //= z.GetEntry("OEBPS/toc.ncx");
            Stream ToCStream = null;
            foreach (ZipArchiveEntry ToCSearch in z.Entries)
            {
                if (ToCSearch.Name.Contains("toc.ncx"))
                {
                    ToCFile = ToCSearch;
                    ToCStream = ToCFile.Open();
                    break;
                }
            }
            ToC.GetDocTitle(ref _newContainer, ref ToCStream);
            Stream NavMapStream = ToCFile.Open();
            NavigationMap.BuildNavMap(ref _newContainer, ref NavMapStream);
            Book.BuildBook(ref _newContainer, ref z);
            MetaStream.Dispose();
            ManifestStream.Dispose();
            SpineStream.Dispose();
            ToCStream.Dispose();
            NavMapStream.Dispose();
        }
    }
}
