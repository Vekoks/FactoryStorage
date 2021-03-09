using FactoryStorage.Models;
using FactoryStorage.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FactoryStorage.View
{
    /// <summary>
    /// Interaction logic for Storage.xaml
    /// </summary>
    public partial class Storage : Window
    {
        private List<StorageModel> listOrderFromFile;

        public Storage()
        {
            InitializeComponent();

            listOrderFromFile = FileProcessing.GetInfomation();

            foreach (var item in listOrderFromFile)
            {
                string result = item.Name + " : " + item.Number.ToString();

                listBox.Items.Add(result);
            }
        }

        private void buttonGather_Click(object sender, RoutedEventArgs e)
        {
            var resultWindow = new GatherStorage();
            resultWindow.Show();
        }

        private void buttonCriticalNumber_Click(object sender, RoutedEventArgs e)
        {
            var resultCriticalNumberWindow = new CriticalNumber();
            resultCriticalNumberWindow.Show();
        }
    }
}
