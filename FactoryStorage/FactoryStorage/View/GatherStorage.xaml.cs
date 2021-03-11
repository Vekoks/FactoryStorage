using AutoCompleteTextBox.Editors;
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
    /// Interaction logic for GatherStorage.xaml
    /// </summary>
    public partial class GatherStorage : Window
    {
        private List<IModels> listOrderFromFile;

        private Storage previous;

        public GatherStorage(Storage storageWin)
        {
            InitializeComponent();

            listOrderFromFile = FileProcessing.GetResources();

            InitializeElementAutoCompleteTextBox();

            previous = storageWin;
        }

        private void buttonGather_Click(object sender, RoutedEventArgs e)
        {
            var name = textboxAuto.Text;

            var number = int.Parse(textBoxNumber.Text);

            if (name == "" || number == 0)
            {
                MessageBox.Show("Полето за въвеждане и за брой не трябва да е празно или 0");

                return;
            }

            var existResources = false;

            foreach (var item in listOrderFromFile)
            {
                if (string.Equals(item.Name, name))
                {
                    existResources = true;

                    item.Number += number;
                }
            }

            if (!existResources)
            {
                var newResource = new StorageModel
                {
                    Name = name,
                    Number = number
                };

                listOrderFromFile.Add(newResource);
            }

            FileProcessing.SaveInfomationInFile(listOrderFromFile, "Resources");

            previous.InitializeListBox();
        }

        private void buttonUp_Click(object sender, RoutedEventArgs e)
        {
            var number = int.Parse(textBoxNumber.Text);

            number++;

            textBoxNumber.Text = number.ToString();
        }

        private void buttonDown_Click(object sender, RoutedEventArgs e)
        {
            var number = int.Parse(textBoxNumber.Text);

            number--;

            if (number < 0)
            {
                number = 0;
            }

            textBoxNumber.Text = number.ToString();
        }

        public void InitializeElementAutoCompleteTextBox()
        {
            var provider = new SuggestionProvider(x =>
            {
                var suggestions = new List<string>();

                foreach (var item in listOrderFromFile)
                {
                    suggestions.Add(item.Name);

                }

                return suggestions.Where(y => y.Contains(x));
            });

            textboxAuto.Provider = provider;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            previous.InitializeListBox();
        }
    }
}
