using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EBookReading
{
    [Serializable]
    class MyAppData
    {
        public List<string> EbookPaths;
       public List<string> FolderPaths;

        public MyAppData()
        {
            EbookPaths = new List<string>();
            FolderPaths = new List<string>();
        }
    }
    public class AppData 
    {        
        private static MyAppData Data = new MyAppData();
        public static IEnumerator GetBookPaths()
        {
            return Data.EbookPaths.GetEnumerator();
        }
        public static string GetBookAtIndex(int index)
        {
            return Data.EbookPaths[index];
        }
        public static void AddBookPath(string PathToAdd)
        {
            if (!Data.EbookPaths.Contains(PathToAdd))
            {
                Data.EbookPaths.Add(PathToAdd);
            }
        }
        public static void AddBookPath(List<string> PathsToAdd)
        {
            foreach (string paths in PathsToAdd)
            {
                if (!Data.FolderPaths.Contains(paths))
                {
                    Data.EbookPaths.Add(paths);
                }
            }
        }
        public static void AddFolderPath(string PathToAdd)
        {
            if (!Data.FolderPaths.Contains(PathToAdd))
            {
                Data.FolderPaths.Add(PathToAdd);
            }
        }
        public static void DeleteBook(string PathToDelete)
        {
            Data.EbookPaths.Remove(PathToDelete);
        }
        public static void LoadData(string FileName)
        {
            Stream LoadStream = null;
            MyAppData LoadData = null; 
            try
            {
                IFormatter formatter = new BinaryFormatter();
                LoadStream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.None);                            
                LoadData = (MyAppData)formatter.Deserialize(LoadStream);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {

                if (null != LoadStream)
                {
                    Data.EbookPaths = LoadData.EbookPaths;
                    //Data.FolderPaths = LoadData.FolderPaths;
                    LoadStream.Close();
                }
            }
        }        
        //Save the user's locations of ebooks to the disk
        public static void SaveData(string FileName) 
        {
            Stream SaveStream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                SaveStream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);                
                formatter.Serialize(SaveStream, Data);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (null != SaveStream)   SaveStream.Close();
            }
        }

    }
}
