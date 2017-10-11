using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

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
        public List<Book> CreateBooksFromPaths(List<string> paths)
        {
            List<Book> Books = new List<Book>();
            foreach (string s in paths)
            {
                Book temp = new Book(s);
                temp.AddInformation(GetBookInformation(s));
                Books.Add(temp);
            }
            return Books;
        }
        public Book CreateBookFromPath(string path)
        {


            Book temp = new Book(path);
            temp.AddInformation(GetBookInformation(path));


            return temp;
        }
        public List<string> GetBookInformation(string FilePath)
        {
            List<string> info = new List<string>();
            switch (GetExtension(FilePath))
            {
                case "pdf":
                    PdfReader InfoReader = new PdfReader(FilePath);
                    info.Add(InfoReader.Info["Title"]);
                    info.Add(InfoReader.Info["Author"]);
                    break;
                default:
                    info.Add(Path.GetFileNameWithoutExtension(FilePath));
                    info.Add("Unknown Author");
                    break;
            }
            return info;
        }
        public string GetExtension(string FilePath)
        {
            string[] PathSplit = FilePath.Split('.');
            return PathSplit[PathSplit.Length - 1];

        }
        //Open a new window with the content of the book
        public void LoadBook(Book b)
        {
            //Split the file path by . since some users may have . in their file names and not just extension            
            switch (b.Extension)
            {
                case ".pdf":
                    PDFBrowser PdfWindow = new PDFBrowser();
                    PdfReader Reader = new PdfReader(b.FilePath);
                    PdfWindow.Title = Reader.Info["Title"] + " by " + Reader.Info["Author"];
                    PdfWindow.Show();
                    PdfWindow.Content.Source = new System.Uri(b.FilePath);
                    break;
                case ".epub":
                    break;
                case ".mobi":
                    break;
            }
        }
    }
}
