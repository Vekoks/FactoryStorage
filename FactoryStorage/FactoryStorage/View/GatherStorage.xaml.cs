using FactoryStorage.Models.Context;
using FactoryStorage.Service;
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
    /// Interaction logic for GatherStorage.xaml
    /// </summary>
    public partial class GatherStorage : Window
    {
        private List<IModels> listLoadResources;

        public string NameElementText { get; set; }

        public int Number { get; set; }

        private Scheme previous;

        public GatherStorage(String elementText, Scheme previousWindows, List<IModels> resouces)
        {
            InitializeComponent();

            listLoadResources = resouces;

            previous = previousWindows;

            NameElementText = GetNameFromTextChange(elementText);

            Number = GetNumberFromTextChange(elementText);

            labelNameElement.Content = GetNameFromTextChange(elementText);

            //textBoxNumber.Text = number.ToString();
        }

        private void buttonGather_Click(object sender, RoutedEventArgs e)
        {
            var number = int.Parse(textBoxNumber.Text);

            if (NameElementText == "" || number == 0)
            {
                MessageBox.Show("Полето за брой не трябва да е празно или 0");

                return;
            }

            Number += int.Parse(textBoxNumber.Text);

            previous.TextChangeElement = NameElementText + " : " + Number + " бр.";

            foreach (var item in listLoadResources)
            {
                if (string.Equals(item.Name, NameElementText))
                {
                    item.Number += int.Parse(textBoxNumber.Text);

                    var typeTransaction = NameElementText + "%Добавяне на " + number + "бр " + DateTime.Now.ToString();

                    FileProcessing.SaveTransaction(typeTransaction);

                    break;
                }
            }

            textBoxNumber.Text = "0";

            MessageBox.Show("Добавихте успешно");

            previous.InicialisationChangeElement();
        }

        private void buttonExtraction_Click(object sender, RoutedEventArgs e)
        {
            var number = int.Parse(textBoxNumber.Text);

            if (NameElementText == "" || number == 0)
            {
                MessageBox.Show("Полето за брой не трябва да е празно или 0");

                return;
            }

            var result = Number - int.Parse(textBoxNumber.Text);

            if (result < 0)
            {
                MessageBox.Show("Резултата не може да е под нула");

                return;
            }

            Number -= int.Parse(textBoxNumber.Text);

            previous.TextChangeElement = NameElementText + " : " + Number + " бр.";

            foreach (var item in listLoadResources)
            {
                if (string.Equals(item.Name, NameElementText))
                {
                    item.Number -= int.Parse(textBoxNumber.Text);

                    var typeTransaction = NameElementText + "%Вземане на " + number + "бр " + DateTime.Now.ToString();

                    FileProcessing.SaveTransaction(typeTransaction);

                    break;
                }
            }

            textBoxNumber.Text = "0";

            MessageBox.Show("Извадихте успешно");

            previous.InicialisationChangeElement();
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

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private int GetNumberFromTextChange(string textChange)
        {
            var listString = textChange.Split(':')[1].Split(' ');

            return int.Parse(listString[1]);
        }

        private string GetNameFromTextChange(string textChange)
        {
            var listString = textChange.Split(':')[0].TrimEnd(' ');

            return listString;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            previous.DeleteNameElement = NameElementText + " : " + Number + " бр.";

            foreach (var item in listLoadResources)
            {
                if (string.Equals(item.Name, NameElementText))
                {
                    item.Number -= Number;

                    break;
                }
            }

            MessageBox.Show("Изтрихте успешно");

            previous.InicialisationDeleteElement();
        }
    }
}
