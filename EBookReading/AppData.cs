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
       public Dictionary<string, int> BookProgress;
        public Theme CurrentTheme;
        public MyAppData()
        {
            EbookPaths = new List<string>();
            FolderPaths = new List<string>();
            BookProgress = new Dictionary<string, int>();
            CurrentTheme = Theme.DEFAULT;
        }
    }
    public class AppData 
    {        
        private static MyAppData Data = new MyAppData();
        public static IEnumerator GetBookPaths()
        {
            return Data.EbookPaths.GetEnumerator();
        }        
        public static Theme GetTheme()
        {
            return Data.CurrentTheme;
        }
        public static void ChangeTheme(Theme change)
        {
            Data.CurrentTheme = change;
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
        public static void AddBookProgress(string BookName, int ChapterNumber)
        {
            if (Data.BookProgress.Keys.Contains(BookName))
            {
                Data.BookProgress[BookName] = ChapterNumber;
            }
            else
            {
                Data.BookProgress.Add(BookName, ChapterNumber);
            }
        }
        public static int GetChapter(string BookName)
        {
            int chapterNum = 0;
            Data.BookProgress.TryGetValue(BookName, out chapterNum);
            return chapterNum;            
        }
        public static void DeleteBook(string PathToDelete)
        {
            Data.EbookPaths.Remove(PathToDelete);
            string StoragePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\VoBookFiles\\" + System.IO.Path.GetFileNameWithoutExtension(PathToDelete);
            Directory.Delete(StoragePath, true);
        }
        public static void LoadData(string FileName)
        {
            Stream LoadStream = null;
            MyAppData LoadData = null; 
            try
            {
                string StoragePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\VoBookFiles\\" + (FileName);
                IFormatter formatter = new BinaryFormatter();
                LoadStream = new FileStream(StoragePath, FileMode.Open, FileAccess.Read, FileShare.None);                            
                LoadData = (MyAppData)formatter.Deserialize(LoadStream);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to find user data");
            }
            finally
            {
                if (null != LoadStream)
                {
                    Data.EbookPaths = LoadData.EbookPaths;
                    Data.BookProgress = LoadData.BookProgress;
                    Data.CurrentTheme = LoadData.CurrentTheme;
                    //Data.BookProgress = new Dictionary<string, int>();
                    
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
                string StoragePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\VoBookFiles\\" + (FileName);
                IFormatter formatter = new BinaryFormatter();
                SaveStream = new FileStream(StoragePath, FileMode.Create, FileAccess.Write, FileShare.None);                
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
