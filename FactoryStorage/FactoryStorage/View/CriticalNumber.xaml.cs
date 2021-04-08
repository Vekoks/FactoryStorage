using FactoryStorage.Models.Context;
using FactoryStorage.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var listText = selectElement.Content.ToString().Split(':');

            var nameFromSelectElement = listText[0].TrimEnd(' ');

            //var listText = selectElement.Content.ToString().Split(' ');

            //foreach (var elementStorage in listStorageModel)
            //{
            //    if (string.Equals(elementStorage.Name, nameFromSelectElement))
            //    {
            //        elementStorage.CriticalNumber = int.Parse(minNumber);
            //    }
            //}

            //FileProcessing.SaveInfomationInFile(listStorageModel, "Resources");

            DataProcessing.ChangeCriticalNumber(nameFromSelectElement, int.Parse(minNumber));

            this.InitializeElement();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void InitializeElement()
        {
            listStorageModel = DataProcessing.GetResources();

            //listStorageModel = FileProcessing.GetResources();

            listBoxCriticalElement.Items.Clear();

            foreach (var elementStorage in listStorageModel)
            {
                Label newLabel = new Label();

                if (elementStorage.CriticalNumber >= elementStorage.Number)
                {
                    newLabel.Foreground = new SolidColorBrush(Colors.Red);

                    newLabel.Content = elementStorage.Name + " : " + elementStorage.Number.ToString() + " бр";

                    newLabel.FontSize = 16;

                    listBoxCriticalElement.Items.Add(newLabel);
                }
            }
        }

        private void listBoxCriticalElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectElement = (Label)listBoxCriticalElement.SelectedItem;

            if (selectElement == null)
            {
                return;
            }

            var listText = selectElement.Content.ToString().Split(':');

            var nameFromSelectElement = listText[0].TrimEnd(' ');

            foreach (var elementStorage in listStorageModel)
            {
                if (string.Equals(elementStorage.Name, nameFromSelectElement))
                {
                    textBoxMinNumber.Text = elementStorage.CriticalNumber.ToString();
                }
            }
        }

        private void buttonSavePdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            string fileNameAndPath = "";

            if (saveFileDialog.ShowDialog() == true)
            {
                fileNameAndPath = saveFileDialog.FileName + ".pdf";
            }

            if (fileNameAndPath == string.Empty)
            {
                return;
            }

            var listInformation = new List<string>();

            foreach (var element in listBoxCriticalElement.Items)
            {
                if (element as Label != null)
                {
                    var label = (Label)element;

                    listInformation.Add(label.Content.ToString());
                }
            }

            FileProcessing.SavePdfFile(fileNameAndPath, "Критични стойности", listInformation);

            MessageBox.Show("Успешно записване на файла");
        }
    }
}
