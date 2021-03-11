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
        private List<IModels> listStorageModel;
        private List<IModels> listCriticaNumbers;

        public Storage()
        {
            InitializeComponent();

            InitializeListBox();
        }

        private void buttonGather_Click(object sender, RoutedEventArgs e)
        {
            var resultWindow = new GatherStorage(this);
            resultWindow.Show();
        }

        private void buttonCriticalNumber_Click(object sender, RoutedEventArgs e)
        {
            var resultCriticalNumberWindow = new CriticalNumber(this);
            resultCriticalNumberWindow.Show();
        }

        public void InitializeListBox()
        {

            listStorageModel = FileProcessing.GetResources();
            listCriticaNumbers = FileProcessing.GetCriticalNumber();

            var criticalNumberElement = 0;

            listBox.Items.Clear();

            foreach (var element in listCriticaNumbers)
            {
                if (string.Equals("Елементи",element.Name))
                {
                    criticalNumberElement = element.Number;
                }
            }

            labelCriticalNumber.Content = "Критична стоност на елементите: " + criticalNumberElement.ToString();

            foreach (var elementStorage in listStorageModel)
            {

                Label newLabel = new Label();

                if (criticalNumberElement >= elementStorage.Number)
                {
                    newLabel.Foreground = new SolidColorBrush(Colors.Red);
                }

                newLabel.Content = elementStorage.Name + " : " + elementStorage.Number.ToString(); ;

                listBox.Items.Add(newLabel);
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
