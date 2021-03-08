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
        private List<StorageModel> listOrderFromFile;

        public GatherStorage()
        {
            InitializeComponent();

            listOrderFromFile = FileProcessing.GetInfomation();

            foreach (var item in listOrderFromFile)
            {

                comboBoxChoice.Items.Add(item.Name);
            }
        }

        private void buttonGather_Click(object sender, RoutedEventArgs e)
        {
            var name = comboBoxChoice.SelectedValue.ToString();

            var number = int.Parse(textBoxNumber.Text);

            foreach (var item in listOrderFromFile)
            {
                if (string.Equals(item.Name, name))
                {
                    item.Number += number;

                    labelNumberStorage.Content = item.Number.ToString() + " налични";
                }
            }



            FileProcessing.SaveInfomation(listOrderFromFile);
        }

        private void comboBoxChoice_CelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var name = comboBoxChoice.SelectedValue.ToString();

            foreach (var item in listOrderFromFile)
            {
                if (string.Equals(item.Name,name))
                {
                    //textBoxNumber.Text = item.Number.ToString();

                    labelNumberStorage.Content = item.Number.ToString() + " налични" ;

                    break;
                }
            } 
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

        private void GatherStorage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var resultWindow = new Storage();
            resultWindow.Show();
        }
    }
}
