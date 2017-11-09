using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading.Mobi
{
    class MobiBook
    {
        public MobiContainer _newContainer;
        private string _localDirectory;
        public MobiBook()
        {
            NewContainer = new MobiContainer();
        }

        public string LocalDirectory
        {
            get { return _localDirectory; }
            set { _localDirectory = value; }
        }
        public void LoadInfo(string FilePath)
        {
            LocalDirectory = FilePath;
            string[] OPFFile = Directory.GetFiles(FilePath, "*.opf");
            //Build the meta data from the opf file
            MobiContent.GetMetadataFromContent(ref _newContainer, OPFFile[0]);
        }
        public MobiContainer NewContainer
        {
            get { return _newContainer; }
            set { _newContainer = value; }
        }
        public void LoadBook(string FilePath)
        {
            LocalDirectory = FilePath;
            //Search the extracted directory for the opf file
            string[] OPFFile = Directory.GetFiles(FilePath, "*.opf");           
            //Build the meta data from the opf file
            MobiContent.GetMetadataFromContent(ref _newContainer, OPFFile[0]);
            MobiManifest.BuildManifest(ref _newContainer, OPFFile[0]);
            MobiSpine.BuildSpine(ref _newContainer, OPFFile[0]);
            string[] ToCFile = Directory.GetFiles(FilePath, "*.ncx");
            MobiToC.GetDocTitle(ref _newContainer, ToCFile[0]);
            MobiNavigationMap.BuildNavMap(ref _newContainer, ToCFile[0]);
            string[] BookFile = Directory.GetFiles(FilePath, "*.html");
            MobiBookContent.BuildBook(ref _newContainer, BookFile[0]);
        }
    }
}
