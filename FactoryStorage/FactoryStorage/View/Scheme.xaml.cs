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
    /// Interaction logic for Scheme.xaml
    /// </summary>
    public partial class Scheme : Window
    {
        public Scheme()
        {
            InitializeComponent();

            InicialisationElement();   

        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            var list = FileProcessing.GetResources();

            var name = comboBoxStorage.SelectedValue.ToString();

            var number = textBoxInputNumber.Text;

            if (int.Parse(number) == 0)
            {
                MessageBox.Show("Полето за брой не може да е: 0");
                return;
            }

            var resoult = name + " : " + number + " бр.";

            foreach (var item in list)
            {
                if (string.Equals(item.Name, name))
                {
                    if (int.Parse(number) > item.Number)
                    {
                        MessageBox.Show("Няма достатъчно наличност");

                        textBoxInputNumber.Text = "0";

                        return;
                    }
                    else
                    {
                        listBoxWithElement.Items.Add(resoult);

                        var residue = item.Number - int.Parse(number);

                        labelNumber.Content = "/" + residue.ToString();

                        if (residue <= 5)
                        {
                            SolidColorBrush myBrush = new SolidColorBrush(Colors.Red);
                            labelNumber.Foreground = myBrush;
                        }
                        else
                        {
                            SolidColorBrush myBrush = new SolidColorBrush(Colors.Black);
                            labelNumber.Foreground = myBrush;
                        }

                        item.Number = residue;

                        FileProcessing.SaveInfomationInFile(list, "Resources");
                    }

                }
            }


        }


        public void InicialisationElement()
        {

            var list = FileProcessing.GetResources();

            foreach (var item in list)
            {
                comboBoxStorage.Items.Add(item.Name);
            }

            comboBoxStorage.SelectedIndex = 0;

            labelNumber.Content = "/" + list[0].Number.ToString();
        }

        private void comboBoxStorage_CelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = FileProcessing.GetResources();

            var name = comboBoxStorage.SelectedValue.ToString();

            foreach (var item in list)
            {
                if (string.Equals(item.Name, name))
                {
                    //textBoxNumber.Text = item.Number.ToString();

                    labelNumber.Content = "/" + item.Number.ToString();


                    if (item.Number <= 5)
                    {
                        SolidColorBrush myBrush = new SolidColorBrush(Colors.Red);
                        labelNumber.Foreground = myBrush;
                    }
                    else
                    {
                        SolidColorBrush myBrush = new SolidColorBrush(Colors.Black);
                        labelNumber.Foreground = myBrush;
                    }

                    break;
                }
            }
        }
    }
}
