using EBookReading.CBR;
using EBookReading.Epub;
using HtmlAgilityPack;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace EBookReading
{
    public class FileManipulator
    {
        //Search a directory for all ebooks within it
        public static List<string> FindFilesFromFolder(string path)
        {
            //Searches the folder to add all files with the following extensions
            var PDFList = System.IO.Directory.GetFiles(path, "*.pdf");
            var EpubList = System.IO.Directory.GetFiles(path, "*.epub");
            var MobiList = System.IO.Directory.GetFiles(path, "*.mobi");
            var CBRList = System.IO.Directory.GetFiles(path, "*.cbr");
            var CBZList = System.IO.Directory.GetFiles(path, "*.cbz");
            List<string> result = new List<string>();
            result.AddRange(PDFList);
            result.AddRange(EpubList);
            result.AddRange(MobiList);
            result.AddRange(CBRList);
            result.AddRange(CBZList);
            return result;
        }
        public static List<BookInfo> CreateBooksFromPaths(List<string> paths)
        {
            List<BookInfo> Books = new List<BookInfo>();
            foreach (string s in paths)
            {
                if (System.IO.File.Exists(s))
                {
                    BookInfo temp = new BookInfo(s);
                    temp.AddInformation(GetBookInformation(s));
                    Books.Add(temp);
                }
            }
            return Books;
        }
        public static BookInfo CreateBookFromPath(string path)
        {

            BookInfo temp = new BookInfo(path);
            temp.AddInformation(GetBookInformation(path));


            return temp;
        }
        public static List<string> GetBookInformation(string FilePath)
        {
            List<string> info = new List<string>();
            switch (GetExtension(FilePath))
            {
                case "pdf":
                    PdfReader InfoReader = new PdfReader(FilePath);
                    try
                    {
                        info.Add(InfoReader.Info["Title"]);
                    }
                    catch
                    {
                        info.Add(Path.GetFileNameWithoutExtension(FilePath));
                    }
                    try
                    {
                        info.Add(InfoReader.Info["Author"]);
                    }
                    catch
                    {
                        info.Add("Unknown Author");
                    }
                    break;
                case "epub":
                    EpubReader eb = new EpubReader();
                    eb.CreatePartialBook(FilePath);
                    info.Add(eb.GetTitle());
                    info.Add(eb.GetAuthor());
                    break;
                default:
                    info.Add(Path.GetFileNameWithoutExtension(FilePath));
                    info.Add("Unknown Author");
                    break;
            }
            return info;
        }
        public static string GetExtension(string FilePath)
        {
            string[] PathSplit = FilePath.Split('.');
            return PathSplit[PathSplit.Length - 1];

        }
        //Open a new window with the content of the book
        public static void LoadBook(BookInfo b)
        {
            //Split the file path by . since some users may have . in their file names and not just extension            
            switch (b.Extension)
            {
                case ".pdf":
                    PDFBrowser PdfWindow = new PDFBrowser();
                    PdfReader Reader = new PdfReader(b.FilePath);
                    //Some pdfs don't have a author or title field
                    try
                    {
                        PdfWindow.Title = Reader.Info["Title"] + " by " + Reader.Info["Author"];
                    }
                    catch
                    {
                        PdfWindow.Title = Path.GetFileNameWithoutExtension(b.FilePath);
                    }
                    PdfWindow.Show();
                    PdfWindow.PDFContent.Source = new System.Uri(b.FilePath);
                    break;
                case ".epub":
                    EpubBrowser EpubWindow = new EpubBrowser();
                    EpubWindow.DisplayBook(b.FilePath);
                    EpubWindow.Show();
                    break;
                case ".mobi":
                    break;
                case ".cbr":
                case ".cbz":
                    ComicBrowser CB = new ComicBrowser();
                    CB.DisplayBook(b.FilePath);
                    CB.Show();
                    break;
            }
        }
    }
}
