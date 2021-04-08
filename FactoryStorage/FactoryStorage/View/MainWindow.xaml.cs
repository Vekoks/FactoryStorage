using FactoryStorage.Data.Data;
using FactoryStorage.Migrations;
using FactoryStorage.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FactoryStorage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataFactoryStorageContext, Configuration>());
            DataFactoryStorageContext.Create().Database.Initialize(true);

            InitializeComponent();
        }

        private void buttonStorage_Click(object sender, RoutedEventArgs e)
        {
            var resultWindow = new Storage();
            resultWindow.Show();
        }

        private void buttonScheme_Click(object sender, RoutedEventArgs e)
        {
            var resultWindow = new Scheme();
            resultWindow.Show();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
