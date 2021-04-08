using FactoryStorage.Models;
using FactoryStorage.Models.Context;
using FactoryStorage.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            listLoadResources = FileProcessing.GetResources();
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            var elementNameWithoutRegex = textNameElement.Text.TrimEnd(' ');

            var nameElement = Regex.Replace(elementNameWithoutRegex, @"[^\w\\s]", "");

            var numberElement = textBoxInputNumber.Text;

            var criticalNumberElement = textBoxCriticalNumber.Text;

            if ((int.Parse(numberElement) == 0) || string.Equals(nameElement, "") || string.Equals(criticalNumberElement, ""))
            {
                MessageBox.Show("Всички полета трябва да са попълнени");
                return;
            }

            var resoultTextForLable = textNameElement.Text + " : " + numberElement + " бр.";

            textNameElement.Text = "";

            textBoxInputNumber.Text = "0";

            textBoxCriticalNumber.Text = "0";

            //var flagIsExsist = false;

            foreach (var element in listBoxWithElement.Items)
            {
                if (element as Label != null)
                {
                    var label = (Label)element;

                    var existElement = label.Content.ToString().Split(':')[0].TrimEnd(' ');

                    if (string.Equals(elementNameWithoutRegex, existElement))
                    {
                        MessageBox.Show("Вече съществува този елемент");

                        return;
                    }
                }
            }

            //foreach (var item in listLoadResources)
            //{
            //    if (string.Equals(item.Name, elementNameWithoutRegex))
            //    {
            //        item.Number += int.Parse(numberElement);

            //        var typeTransaction = elementNameWithoutRegex + "%Добавяне на " + numberElement +"бр " + DateTime.Now.ToString();

            //        FileProcessing.SaveTransaction(typeTransaction);

            //        flagIsExsist = true;

            //        //Button newBoton = new Button();
            //        //newBoton.Name = nameElement;
            //        //newBoton.Content = "Промени";
            //        //newBoton.Click += button_ChangeNumber;

            //        var newBoton = TakeNewButton(nameElement);

            //        //Label newLable = new Label();
            //        //newLable.Name = "lable" + nameElement;
            //        //newLable.Content = resoultTextForLable;

            //        var newLable = TakeNewLable("lable" + nameElement, resoultTextForLable);

            //        listBoxWithElement.Items.Add(newLable);

            //        listBoxWithElement.Items.Add(newBoton);

            //        break;
            //    }
            //}

            //if (!flagIsExsist)
            //{
            //    listLoadResources.Add(new StorageModel
            //    {
            //        Name = elementNameWithoutRegex,
            //        Number = int.Parse(numberElement),
            //        CriticalNumber = int.Parse(criticalNumberElement),
            //    });

            //    var typeTransaction = elementNameWithoutRegex + "%Добавяне на " + numberElement + "бр " + DateTime.Now.ToString();

            //    FileProcessing.SaveTransaction(typeTransaction);

            //    //Button newBoton = new Button();
            //    //newBoton.Name = nameElement;
            //    //newBoton.Content = "Промени";
            //    //newBoton.Click += button_ChangeNumber;

            //    var newBoton = TakeNewButton(nameElement);

            //    //Label newLable = new Label();
            //    //newLable.Name = "lable" + nameElement;
            //    //newLable.Content = resoultTextForLable;

            //    var newLable = TakeNewLable("lable" + nameElement, resoultTextForLable);

            //    listBoxWithElement.Items.Add(newLable);

            //    listBoxWithElement.Items.Add(newBoton);
            //}


            var newBoton = TakeNewButton(nameElement);

            var newLable = TakeNewLable("lable" + nameElement, resoultTextForLable);

            listBoxWithElement.Items.Add(newLable);

            listBoxWithElement.Items.Add(newBoton);


            var elementStoraga = new StorageModel
            {
                Name = elementNameWithoutRegex,
                Number = int.Parse(numberElement),
                CriticalNumber = int.Parse(criticalNumberElement),
            };

            DataProcessing.SaveInfomation(elementStoraga, '+');

            this.SaveScheme();
        }

        public void InicialisationElement()
        {
            if (!String.IsNullOrEmpty(NameOFLoadScheme))
            {
                //var loadScheme = FileProcessing.LoadScheme(NameOFLoadScheme);

                var loadScheme = DataProcessing.LoadScheme(NameOFLoadScheme); 

                textBoxTopic.Text = loadScheme.Topic;

                textBoxDiscribe.Text = loadScheme.Description;

                listBoxWithElement.Items.Clear();

                foreach (var element in loadScheme.Elements)
                {
                    var nameElement = Regex.Replace(element.Name, @"[^\w\\s]", "");

                    //Button newBoton = new Button();
                    //newBoton.Click += button_ChangeNumber;
                    //newBoton.Name = nameElement;
                    //newBoton.Content = "Промени";

                    var newBoton = TakeNewButton(nameElement);

                    //Label newLable = new Label();
                    //newLable.Name = "lable" + nameElement;
                    //newLable.Content = element.Name + " : " + element.Number + " бр.";

                    var newLable = TakeNewLable("lable" + nameElement, element.Name + " : " + element.Number + " бр.");

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

                    //Label newLable = new Label();
                    //newLable.Name = label.Name;
                    //newLable.Content = label.Content;

                    var newLable = TakeNewLable(label.Name, label.Content.ToString());

                    newListBox.Items.Add(newLable);
                }
            }

            listBoxWithElement.Items.Clear();

            if (!String.IsNullOrEmpty(TextChangeElement))
            {
                var text = TextChangeElement.Split(':')[0];

                var nameElement = Regex.Replace(text, @"[^\w\\s]", "");

                var name = "lable" + nameElement;

                foreach (var element in newListBox.Items)
                {
                    if (element as Label != null)
                    {
                        var label = (Label)element;

                        if (string.Equals(label.Name, name))
                        {
                            //Button newBoton = new Button();
                            //newBoton.Click += button_ChangeNumber;
                            //newBoton.Name = nameElement;
                            //newBoton.Content = "Промени";

                            var newBoton = TakeNewButton(nameElement);

                            //Label newLable = new Label();
                            //newLable.Name = name;
                            //newLable.Content = TextChangeElement;

                            var newLable = TakeNewLable(name, TextChangeElement);

                            listBoxWithElement.Items.Add(newLable);

                            listBoxWithElement.Items.Add(newBoton);
                        }
                        else
                        {
                            var textName = label.Content.ToString().Split(':')[0];

                            var nameButton = Regex.Replace(textName, @"[^\w\\s]", "");

                            //Button newBoton = new Button();
                            //newBoton.Click += button_ChangeNumber;
                            //newBoton.Name = nameButton;
                            //newBoton.Content = "Промени";

                            var newBoton = TakeNewButton(nameButton);

                            //Label newLable = new Label();
                            //newLable.Name = label.Name;
                            //newLable.Content = label.Content;

                            var newLable = TakeNewLable(label.Name, label.Content.ToString());

                            listBoxWithElement.Items.Add(newLable);

                            listBoxWithElement.Items.Add(newBoton);
                        }
                    }
                }
            }

            TextChangeElement = "";

            this.SaveScheme();
        }

        public void InicialisationDeleteElement()
        {
            ListBox newListBox = new ListBox();

            foreach (var element in listBoxWithElement.Items)
            {
                if (element as Label != null)
                {
                    var label = (Label)element;

                    //Label newLable = new Label();
                    //newLable.Name = label.Name;
                    //newLable.Content = label.Content;

                    var newLable = TakeNewLable(label.Name, label.Content.ToString());

                    newListBox.Items.Add(newLable);
                }
            }

            listBoxWithElement.Items.Clear();

            if (!String.IsNullOrEmpty(DeleteNameElement))
            {
                var text = DeleteNameElement.Split(':')[0].TrimEnd(' ');

                var nameElement = Regex.Replace(text, @"[^\w\\s]", "");

                var name = "lable" + nameElement;

                foreach (var element in newListBox.Items)
                {
                    if (element as Label != null)
                    {
                        var label = (Label)element;

                        if (string.Equals(label.Name, name))
                        {
                            continue;
                        }

                        var textName = label.Content.ToString().Split(':')[0];

                        var nameButton = Regex.Replace(textName, @"[^\w\\s]", "");

                        //Button newBoton = new Button();
                        //newBoton.Click += button_ChangeNumber;
                        //newBoton.Name = nameButton;
                        //newBoton.Content = "Промени";

                        var newBoton = TakeNewButton(nameButton);

                        //Label newLable = new Label();
                        //newLable.Name = label.Name;
                        //newLable.Content = label.Content;

                        var newLable = TakeNewLable(label.Name, label.Content.ToString());

                        listBoxWithElement.Items.Add(newLable);

                        listBoxWithElement.Items.Add(newBoton);

                    }
                }
            }

            DeleteNameElement = "";

            this.SaveScheme();
        }

        private void buttonSaveScheme_Click(object sender, RoutedEventArgs e)
        {
            if (this.SaveScheme())
            {
                MessageBox.Show("Схемата се записа");
            }

            textBoxTopic.Text = "";

            textBoxDiscribe.Text = "";

            textBoxInputNumber.Text = "0";

            listBoxWithElement.Items.Clear();
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

                        return;
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

            NameOFLoadScheme = "";

            TextChangeElement = "";

            DeleteNameElement = "";
        }

        private Button TakeNewButton(string nameButton)
        {
            Button newBoton = new Button();
            newBoton.Name = nameButton;
            newBoton.Content = "Промени";
            newBoton.Click += button_ChangeNumber;

            return newBoton;
        }

        private Label TakeNewLable(string nameLable, string contextLable)
        {
            Label newLable = new Label();
            newLable.Name = nameLable;
            newLable.Content = contextLable;

            return newLable;
        }

        private bool SaveScheme()
        {
            var strTopic = textBoxTopic.Text;

            var strdescription = textBoxDiscribe.Text;

            if ((string.Equals(strTopic, "") || string.Equals(strdescription, "")))
            {
                MessageBox.Show("Полето за тема и описание трябва да е попълнено");
                return false;
            }

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

                    newElelemt.Name = stringSplit[0].TrimEnd(' ');

                    newElelemt.Number = int.Parse(stringNumber[1]);

                    newSchema.Elements.Add(newElelemt);
                }
            }

            DataProcessing.SaveScheme(newSchema);

            //FileProcessing.SaveSchemeInFile(newSchema);

            //FileProcessing.SaveInfomationInFile(listLoadResources, "Resources");

            return true;
        }
    }
}
