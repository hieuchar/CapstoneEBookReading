using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace EBookReading.Epub
{
    public class EpubReader
    {
        /// <summary>
        ///Creates a new epub book and loads all the content in
        ///
        /// </summary>
        /// <param name="FilePath">Direct file path to the epub file</param>
        public static void CreateBook(string FilePath)
        {
            EpubBook book = new EpubBook();            
            book.loadBook(FilePath);            //{
            //    ZipArchiveEntry container = zip.GetEntry("META-INF/container.xml");
            //    container.Open();
            //    zip.ExtractToDirectory(Path.GetDirectoryName(FilePath));                
            //    book.LoadBook(Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(FilePath)));
            //}            
        }
    }
}
