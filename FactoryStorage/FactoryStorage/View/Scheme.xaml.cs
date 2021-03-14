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
    /// Interaction logic for Scheme.xaml
    /// </summary>
    public partial class Scheme : Window
    {
        public string NameOFLoadScheme { get; set; }

        public Scheme()
        {
            InitializeComponent();

            InicialisationElement();
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            var list = FileProcessing.GetResources();

            var number = textBoxInputNumber.Text;

            var criticalNumber = textBoxCriticalNumber.Text;
            
            if (int.Parse(number) == 0)
            {
                MessageBox.Show("Полето за брой не може да е: 0");
                return;
            }

            var name = textNameElement.Text;

            var resoult = name + " : " + number + " бр.";

            var flagIsExsist = false;

            foreach (var item in list)
            {
                if (string.Equals(item.Name, name))
                {
                    item.Number += int.Parse(number);

                    TextBox newTextBox = new TextBox();
                    newTextBox.Text = "0";
                    newTextBox.Name = "textBox" + name;

                    Button newBoton = new Button();
                    newBoton.Name = name;
                    newBoton.Content = "Извади";
                    newBoton.Click += button_ChangeNumber;

                    listBoxWithElement.Items.Add(resoult);

                    listBoxWithElement.Items.Add(newBoton);

                    flagIsExsist = true;
                }
              
            }

            if (!flagIsExsist)
            {
                list.Add(new StorageModel
                {
                    Name = name,
                    Number = int.Parse(number),
                    CriticalNmber = int.Parse(criticalNumber),
                });

                TextBox newTextBox = new TextBox();
                newTextBox.Text = "0";
                newTextBox.Name = "textBox" + name;

                Button newBoton = new Button();
                newBoton.Name = name;
                newBoton.Content = "Извади";
                newBoton.Click += button_ChangeNumber;

                listBoxWithElement.Items.Add(resoult);

                listBoxWithElement.Items.Add(newBoton);
            }

            FileProcessing.SaveInfomationInFile(list, "Resources");
        }

        public void InicialisationElement()
        {
            var list = FileProcessing.GetResources();

            if (!String.IsNullOrEmpty(NameOFLoadScheme))
            {
                var loadScheme = FileProcessing.LoadScheme(NameOFLoadScheme);

                textBoxTopic.Text = loadScheme.Topic;

                textBoxDiscribe.Text = loadScheme.Description;

                listBoxWithElement.Items.Clear();

                foreach (var element in loadScheme.Elements)
                {
                    TextBox newTextBox = new TextBox();
                    newTextBox.Text = "0";
                    newTextBox.Name = "textBox" + element.Name;

                    Button newBoton = new Button();
                    newBoton.Click += button_ChangeNumber;
                    newBoton.Name = element.Name;
                    newBoton.Content = "Извади";

                    listBoxWithElement.Items.Add(element.Name + " : " + element.Number + " бр.");

                    listBoxWithElement.Items.Add(newTextBox);

                    listBoxWithElement.Items.Add(newBoton);
                }
            }
        }


        private void buttonSaveScheme_Click(object sender, RoutedEventArgs e)
        {
            var strTopic = textBoxTopic.Text;

            var strdescription = textBoxDiscribe.Text;

            var newSchema = new SchemeModel();

            newSchema.Topic = strTopic;
            newSchema.Description = strdescription;
            newSchema.Elements = new List<StorageModel>();

            var listElement = listBoxWithElement.Items;

            foreach (var element in listElement)
            {

                if (element is string)
                {
                    var stringSplit = element.ToString().Trim().Split(':');

                    var stringNumber = stringSplit[1].Trim().Split(' ');

                    var newElelemt = new StorageModel();

                    newElelemt.Name = stringSplit[0];

                    newElelemt.Number = int.Parse(stringNumber[0]);

                    newSchema.Elements.Add(newElelemt);
                }
            }

            FileProcessing.SaveSchemeInFile(newSchema);

            textBoxTopic.Text = "";

            textBoxDiscribe.Text = "";

            textBoxInputNumber.Text = "0";

            listBoxWithElement.Items.Clear();

            MessageBox.Show("Схемата се записа");
        }

        private void buttonLoadScheme_Click(object sender, RoutedEventArgs e)
        {
            LoadScheme newLoadScheme = new LoadScheme(this);
            newLoadScheme.Show();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void button_ChangeNumber(object sender, RoutedEventArgs e)
        {
            var gatButton = sender as Button;

            var name = "textBox" + gatButton.Name;

            foreach (var items in listBoxWithElement.Items)
            {
                if (items as TextBox != null)
                {
                    var textBox = (TextBox)items;

                    if (string.Equals(textBox.Name, name))
                    {
                        var number = int.Parse(textBox.Text);

                        foreach (var element in listBoxWithElement.Items)
                        {
                            if (element is string)
                            {
                                var stringTrim = element.ToString().Trim(' ').Split(':')[0];

                                if (gatButton.Name.Contains(stringTrim))
                                {
                                    var nsa = "";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonNewScheme_Click(object sender, RoutedEventArgs e)
        {

            textBoxTopic.Text = "";

            textBoxDiscribe.Text = "";

            textBoxInputNumber.Text = "0";

            listBoxWithElement.Items.Clear();
        }
    }
}
