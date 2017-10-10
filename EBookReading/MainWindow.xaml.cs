
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



namespace EBookReading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileManipulator FileManip;
        public MainWindow()
        {
            InitializeComponent();
            FileManip = new FileManipulator();
            AppData.LoadData("EBookPaths.sav");
            RefreshList();            
        }
        #region Menu/File Loading
       
        private void AddFolderCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void AddFolderCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Open up a file explorer to find a folder with ebooks within it and add all of them
            //Only searches that directory, will not search farther
            var AddFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = AddFolderDialog.ShowDialog();                
            if(result == System.Windows.Forms.DialogResult.OK)
            {              
                AppData.AddFolderPath(AddFolderDialog.SelectedPath);
                AppData.AddBookPath(FileManip.FindFilesFromFolder(AddFolderDialog.SelectedPath));
                Console.WriteLine("Added a folder");
                RefreshList();
            }                       
        }
        private void AddBookCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddBookCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Open up a file explorer to add a specific ebook
            var AddBookDialog = new System.Windows.Forms.OpenFileDialog();
            AddBookDialog.Filter = "Ebook Files | *.mobi; *.pdf; *.epub";
            AddBookDialog.InitialDirectory = @"C:\";
            System.Windows.Forms.DialogResult result = AddBookDialog.ShowDialog();            
            if (result == System.Windows.Forms.DialogResult.OK)
            {                
                AppData.AddBookPath(AddBookDialog.FileName);
                Console.WriteLine("Added a book");
                RefreshList();
            }            
        }
        #endregion
        #region List Display
        //Gets list of books from App Data and refreshes the ListBox on the main window
        public void RefreshList()
        {
            var Books = AppData.GetBookPaths();
            int BookIndex = 0;
            while(Books.MoveNext())
            {
                
                //Create a custom panel to hold book name and a button to open a new window with
                //the content of the book
                StackPanel BookPanel = new StackPanel();
                BookPanel.Orientation = Orientation.Horizontal;
                
                //Get the name of the book from the file name. 
                //TODO: Change from file name to title metadata of ebook
                TextBlock BookName = new TextBlock();
                BookName.Text = Path.GetFileName(Books.Current.ToString());
                BookPanel.Children.Add(BookName);

                Button OpenBook = new Button();
                OpenBook.Content = "Open Book";
                OpenBook.Name = "Button_" + BookIndex++;
                OpenBook.Click += Open_Book;
                BookPanel.Children.Add(OpenBook);

                //Add the panel to the list
                BookList.Items.Add(BookPanel);
            }
        }
        //Opens a new window with the content of the book
        private void Open_Book(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            int index = int.Parse(temp.Name.Split('_')[1]);
            FileManip.LoadBook(AppData.GetBookAtIndex(index));
        }
        #endregion 
    }
}
