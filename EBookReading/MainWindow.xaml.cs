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
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();                
            if(result == System.Windows.Forms.DialogResult.OK)
            {              
                AppData.AddFolderPath(dialog.SelectedPath);
                AppData.AddBookPath(FileManip.FindFiles(dialog.SelectedPath));
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
            Console.WriteLine("Add Book pressed");
        }
        #endregion
        #region List Display
        //Gets list of books from App Data and refreshes the ListBox on the main window
        public void RefreshList()
        {
            var Books = AppData.GetBookPaths();
            while (Books.MoveNext())
            {
                //Create a custom panel to hold book name and a button to open a new window with
                //the content of the book
                StackPanel BookPanel = new StackPanel();
                BookPanel.Orientation = Orientation.Horizontal;
                
                //Get the name of the book from the file name. 
                //TODO: Change from file name to title field of ebook
                TextBlock BookName = new TextBlock();
                BookName.Text = Path.GetFileName(Books.Current.ToString());
                BookPanel.Children.Add(BookName);

                Button OpenBook = new Button();
                OpenBook.Content = "Open Book";
                OpenBook.Click += Open_Book;
                BookPanel.Children.Add(OpenBook);

                //Add the panel to the list
                BookList.Items.Add(BookPanel);
            }
        }
        private void Open_Book(object sender, EventArgs e)
        {

        }
        #endregion 
    }
}
