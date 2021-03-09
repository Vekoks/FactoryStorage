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
        private bool flagForPlusButton = false;
        private bool flagForComboBox = false;
        private bool flagForLable = false; 

        public Scheme()
        {
            InitializeComponent();
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            var list = FileProcessing.GetInfomation();

            var element = stackGridBody.Children.OfType<FrameworkElement>().ToList();

            if (element.Count > 10)
            {
                return;
            }

            Button buttonPlusNew = new Button()
            {
                Name = "buttonPlus",
                Height = 35,
                Width = 40,
                Content = "+",
            };

            buttonPlusNew.Click += buttonPlus_Click;

            var resultOldbuttonPlus = element[element.Count - 1];

            Thickness marginOldbuttonPlus = resultOldbuttonPlus.Margin;
            Thickness marginbuttonPlusNew = buttonPlusNew.Margin;

            if (flagForPlusButton)
            {
                marginbuttonPlusNew.Top = marginOldbuttonPlus.Top + 65;
            }
            else
            {
                marginbuttonPlusNew.Top = marginOldbuttonPlus.Top - 80;
                flagForPlusButton = true;
            }

            marginbuttonPlusNew.Left = -205;
            buttonPlusNew.Margin = marginbuttonPlusNew;


            ComboBox comboBox = new ComboBox()
            {
              Name = element[element.Count - 2].Name,
              Height = 25,
              Width = 120
            };

            Thickness marginNewButton = comboBox.Margin;
            var resultcomboBox = element[element.Count - 3];

            if (flagForComboBox)
            {
                marginNewButton.Top = resultcomboBox.Margin.Top + 65;
            }
            else
            {
                marginNewButton.Top = resultcomboBox.Margin.Top - 120;
                flagForComboBox = true;
            }

            marginNewButton.Left = -203;
            comboBox.Margin = marginNewButton;

            foreach (var item in list)
            {

                comboBox.Items.Add(item.Name);
            }

            comboBox.SelectedIndex = 0;

            Label buttonLabel = new Label()
            {
                Name = "labe",
                Height = 25,
                Width = 40,
                Content = "/" + list[0].Number.ToString(),
            };

            Thickness marginNewbuttonLabel = buttonLabel.Margin;
            var resultbuttonLabel = element[element.Count - 2];

            if (flagForLable)
            {
                marginNewbuttonLabel.Top = resultbuttonLabel.Margin.Top + 65;
            }
            else
            {
                marginNewbuttonLabel.Top = resultbuttonLabel.Margin.Top - 120;
                flagForLable = true;
            }

            marginNewbuttonLabel.Left = 65;
            buttonLabel.Margin = marginNewbuttonLabel;


            stackGridBody.Children.RemoveAt(stackGridBody.Children.Count - 1);

            stackGridBody.Children.Add(comboBox);

            stackGridBody.Children.Add(buttonLabel);

            stackGridBody.Children.Add(buttonPlusNew);


        }
    }
}
