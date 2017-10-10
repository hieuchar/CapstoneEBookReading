using System.Collections.Generic;

namespace EBookReading
{
    public class FileManipulator
    {
        //Search a directory for all ebooks within it
        public List<string> FindFilesFromFolder(string path)
        {
            //Searches the folder to add all files with the following extensions
            var PDFList = System.IO.Directory.GetFiles(path, "*.pdf");
            var EpubList = System.IO.Directory.GetFiles(path, "*.epub");
            var MobiList = System.IO.Directory.GetFiles(path, "*.mobi");
            
            List<string> result = new List<string>();
            result.AddRange(PDFList);
            result.AddRange(EpubList);
            result.AddRange(MobiList);
            
            return result;
        }
        
        //Open a new window with the content of the book
        public void LoadBook(string FilePath)
        {
            //Split the file path by . since some users may have . in their file names and not just extension               
            string[] PathSplit = FilePath.Split('.');
            string Extension = PathSplit[PathSplit.Length - 1];
            switch (Extension)
            {
                case "pdf":
                  
                    
                    
                                      
                    
                    
                    
                    break;
                case "epub":
                    break;
                case "mobi":
                    break;
            }
        }
    }
}
