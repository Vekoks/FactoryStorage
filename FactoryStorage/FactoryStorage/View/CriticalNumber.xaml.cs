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
    /// Interaction logic for CriticalNumber.xaml
    /// </summary>
    public partial class CriticalNumber : Window
    {

        private List<IModels> listStorageModel;

        public CriticalNumber()
        {
            InitializeComponent();

            InitializeElement();
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            var minNumber = textBoxMinNumber.Text;

            var selectElement = (Label)listBoxCriticalElement.SelectedItem;

            if (selectElement == null)
            {
                MessageBox.Show("Трябва да бъде избран елемент");

                return;
            }

            var listText = selectElement.Content.ToString().Split(' ');

            foreach (var elementStorage in listStorageModel)
            {
                if (string.Equals(elementStorage.Name, listText[0]))
                {
                    elementStorage.CriticalNmber = int.Parse(minNumber);
                }
            }

            FileProcessing.SaveInfomationInFile(listStorageModel, "Resources");

            this.InitializeElement();
            }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void InitializeElement()
        {
            listStorageModel = FileProcessing.GetResources();

            listBoxCriticalElement.Items.Clear();

            foreach (var elementStorage in listStorageModel)
            {
                Label newLabel = new Label();

                if (elementStorage.CriticalNmber >= elementStorage.Number)
                {
                    newLabel.Foreground = new SolidColorBrush(Colors.Red);

                    newLabel.Content = elementStorage.Name + " : " + elementStorage.Number.ToString() + " бр";

                    listBoxCriticalElement.Items.Add(newLabel);
                }
            }
        }


        private void listBoxCriticalElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectElement = (Label)listBoxCriticalElement.SelectedItem;

            var listText = selectElement.Content.ToString().Split(' ');

            foreach (var elementStorage in listStorageModel)
            {
                if (string.Equals(elementStorage.Name, listText[0]))
                {
                    textBoxMinNumber.Text = elementStorage.CriticalNmber.ToString();
                }
            }
        }
    }
}
