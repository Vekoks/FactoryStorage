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
    /// Interaction logic for Storage.xaml
    /// </summary>
    public partial class Storage : Window
    {
        private List<IModels> listStorageModel;

        public Storage()
        {
            InitializeComponent();

            InitializeListBox();
        }


        private void buttonCriticalNumber_Click(object sender, RoutedEventArgs e)
        {
            var resultCriticalNumberWindow = new CriticalNumber();
            resultCriticalNumberWindow.Show();
        }

        public void InitializeListBox()
        {
            listStorageModel = FileProcessing.GetResources();

            listBoxItem.Items.Clear();

            foreach (var elementStorage in listStorageModel)
            {
                Label newLabel = new Label();

                newLabel.Content = elementStorage.Name + " : " + elementStorage.Number.ToString() + " бр";

                newLabel.FontSize = 16;

                listBoxItem.Items.Add(newLabel);
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
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

            foreach (var element in listBoxItem.Items)
            {
                if (element as Label != null)
                {
                    var label = (Label)element;

                    listInformation.Add(label.Content.ToString());
                }
            }

            FileProcessing.SavePdfFile(fileNameAndPath, "Наличност", listInformation);
        }
    }
}
