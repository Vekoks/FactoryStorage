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
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {

            Button buttonPlusNew = new Button()
            {
                Name = "buttonPlus",
                Height = 35,
                Width = 40,
                Content = "+",
            };

            buttonPlusNew.Click += buttonPlus_Click;

            var element = stackGridBody.Children.OfType<FrameworkElement>().ToList();

            var resultOldbuttonPlus = element[element.Count - 1];

            Thickness marginOldbuttonPlus = resultOldbuttonPlus.Margin;
            Thickness marginbuttonPlusNew = buttonPlusNew.Margin;

            marginbuttonPlusNew.Top = marginOldbuttonPlus.Top;
            marginbuttonPlusNew.Left = marginOldbuttonPlus.Left;
            marginbuttonPlusNew.Right = marginOldbuttonPlus.Right;
            marginbuttonPlusNew.Bottom = marginOldbuttonPlus.Bottom;
            buttonPlusNew.Margin = marginbuttonPlusNew;


            ComboBox comboBox = new ComboBox()
            {
              Name = element[element.Count - 2].Name,
              Height = 25,
              Width = 120
            };

            var resultcomboBox = element[element.Count - 3];

            var topmarginGather= - 15;

            var current = 0.0;

            if ((resultcomboBox.Margin.Top - 15) < 0)
            {
                topmarginGather = 15;

                current = resultcomboBox.Margin.Top - 15;

            }
            else
            {
                current = resultcomboBox.Margin.Top;
            }

            Thickness marginNewButton = comboBox.Margin;
            marginNewButton.Top = current + topmarginGather;
            marginNewButton.Left = -173;
            comboBox.Margin = marginNewButton;

            stackGridBody.Children.RemoveAt(stackGridBody.Children.Count - 1);

            stackGridBody.Children.Add(comboBox);
            stackGridBody.Children.Add(buttonPlusNew);


        }
    }
}
