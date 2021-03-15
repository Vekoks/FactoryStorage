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

        public string TextChangeElement { get; set; }

        public string DeleteNameElement { get; set; }

        public List<IModels> listLoadResources { get; set; }

        public Scheme()
        {
            InitializeComponent();

            InicialisationElement();

            listLoadResources = FileProcessing.GetResources();
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            var nameElement = textNameElement.Text.Replace(" ","-");

            var numberElement = textBoxInputNumber.Text;

            var criticalNumberElement = textBoxCriticalNumber.Text;

            textNameElement.Text = " ";

            textBoxInputNumber.Text = "0";

            textBoxCriticalNumber.Text = "0";

            if (int.Parse(numberElement) == 0)
            {
                MessageBox.Show("Полето за брой не може да е: 0");
                return;
            }

            var resoult = nameElement + " : " + numberElement + " бр.";

            var flagIsExsist = false;

            foreach (var item in listLoadResources)
            {
                if (string.Equals(item.Name, nameElement))
                {
                    item.Number += int.Parse(numberElement);

                    Button newBoton = new Button();
                    newBoton.Name = nameElement;
                    newBoton.Content = "Промени";
                    newBoton.Click += button_ChangeNumber;


                    Label newLable = new Label();
                    newLable.Name = "lable" + nameElement;
                    newLable.Content = nameElement + " : " + numberElement + " бр.";

                    listBoxWithElement.Items.Add(newLable);

                    listBoxWithElement.Items.Add(newBoton);

                    flagIsExsist = true;
                }
              
            }

            if (!flagIsExsist)
            {
                listLoadResources.Add(new StorageModel
                {
                    Name = nameElement,
                    Number = int.Parse(numberElement),
                    CriticalNmber = int.Parse(criticalNumberElement),
                });

                Button newBoton = new Button();
                newBoton.Name = nameElement;
                newBoton.Content = "Промени";
                newBoton.Click += button_ChangeNumber;

                Label newLable = new Label();
                newLable.Name = "lable" + nameElement;
                newLable.Content = nameElement + " : " + numberElement + " бр.";

                listBoxWithElement.Items.Add(newLable);

                listBoxWithElement.Items.Add(newBoton);
            }
        }

        public void InicialisationElement()
        {
            if (!String.IsNullOrEmpty(NameOFLoadScheme))
            {
                var loadScheme = FileProcessing.LoadScheme(NameOFLoadScheme);

                textBoxTopic.Text = loadScheme.Topic;

                textBoxDiscribe.Text = loadScheme.Description;

                listBoxWithElement.Items.Clear();

                foreach (var element in loadScheme.Elements)
                {
                    Button newBoton = new Button();
                    newBoton.Click += button_ChangeNumber;
                    newBoton.Name = element.Name;
                    newBoton.Content = "Промени";

                    Label newLable = new Label();
                    newLable.Name = "lable" + element.Name;
                    newLable.Content = element.Name + " : " + element.Number + " бр.";

                    listBoxWithElement.Items.Add(newLable);

                    listBoxWithElement.Items.Add(newBoton);
                }
            }

            NameOFLoadScheme = "";
        }

        public void InicialisationChangeElement()
        {
            ListBox newListBox = new ListBox();

            foreach (var element in listBoxWithElement.Items)
            {
                if (element as Label != null)
                {
                    var label = (Label)element;

                    Label newLable = new Label();
                    newLable.Name = label.Name;
                    newLable.Content = label.Content;

                    newListBox.Items.Add(newLable);
                } 
            }

            listBoxWithElement.Items.Clear();

            if (!String.IsNullOrEmpty(TextChangeElement))
            {
                var name = "lable" + TextChangeElement.Split(' ')[0];

                foreach (var element in newListBox.Items)
                {
                    if (element as Label != null)
                    {
                        var label = (Label)element;

                        if (string.Equals(label.Name, name))
                        {
                            Button newBoton = new Button();
                            newBoton.Click += button_ChangeNumber;
                            newBoton.Name = TextChangeElement.Split(' ')[0];
                            newBoton.Content = "Промени";

                            Label newLable = new Label();
                            newLable.Name = name;
                            newLable.Content = TextChangeElement;

                            listBoxWithElement.Items.Add(newLable);

                            listBoxWithElement.Items.Add(newBoton);
                        }
                        else
                        {
                            Button newBoton = new Button();
                            newBoton.Click += button_ChangeNumber;
                            newBoton.Name = label.Content.ToString().Split(' ')[0];
                            newBoton.Content = "Промени";

                            Label newLable = new Label();
                            newLable.Name = label.Name;
                            newLable.Content = label.Content;

                            listBoxWithElement.Items.Add(newLable);

                            listBoxWithElement.Items.Add(newBoton);
                        }
                    }
                }
            }

            TextChangeElement = "";
        }

        public void InicialisationDeleteElement()
        {
            ListBox newListBox = new ListBox();

            foreach (var element in listBoxWithElement.Items)
            {
                if (element as Label != null)
                {
                    var label = (Label)element;

                    Label newLable = new Label();
                    newLable.Name = label.Name;
                    newLable.Content = label.Content;

                    newListBox.Items.Add(newLable);
                }
            }

            listBoxWithElement.Items.Clear();

            if (!String.IsNullOrEmpty(DeleteNameElement))
            {
                var name = "lable" + DeleteNameElement.Split(' ')[0];

                foreach (var element in newListBox.Items)
                {
                    if (element as Label != null)
                    {
                        var label = (Label)element;

                        if (string.Equals(label.Name, name))
                        {
                            continue;
                        }
    
                        Button newBoton = new Button();
                        newBoton.Click += button_ChangeNumber;
                        newBoton.Name = label.Content.ToString().Split(' ')[0];
                        newBoton.Content = "Промени";

                        Label newLable = new Label();
                        newLable.Name = label.Name;
                        newLable.Content = label.Content;

                        listBoxWithElement.Items.Add(newLable);

                        listBoxWithElement.Items.Add(newBoton);
                        
                    }
                }
            }

            TextChangeElement = "";
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
                if (element as Label != null)
                {
                    var label = (Label)element;

                    var stringSplit = label.Content.ToString().Split(':');

                    var stringNumber = stringSplit[1].Split(' ');

                    var newElelemt = new StorageModel();

                    newElelemt.Name = stringSplit[0].Replace(" ", "");

                    newElelemt.Number = int.Parse(stringNumber[1]);

                    newSchema.Elements.Add(newElelemt);
                }
            }

            FileProcessing.SaveSchemeInFile(newSchema);

            textBoxTopic.Text = "";

            textBoxDiscribe.Text = "";

            textBoxInputNumber.Text = "0";

            listBoxWithElement.Items.Clear();

            FileProcessing.SaveInfomationInFile(listLoadResources, "Resources");

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

        private void button_ChangeNumber(object sender, RoutedEventArgs e)
        {
            var gatButton = sender as Button;

            var name = "lable" + gatButton.Name;

            foreach (var items in listBoxWithElement.Items)
            {
                if (items as Label != null)
                {
                    var label = (Label)items;

                    if (string.Equals(label.Name, name))
                    {
                        GatherStorage windowsGatherStorage = new GatherStorage(label.Content.ToString(), this, listLoadResources);
                        windowsGatherStorage.Show();
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

            listLoadResources = FileProcessing.GetResources();
        }
    }
}
