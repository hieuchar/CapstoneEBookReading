using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EBookReading
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void ApplicationExit(object sender, ExitEventArgs e)
        {
            AppData.SaveData("EBookPaths.sav");
            Application.Current.Shutdown();
        }
    }
}
