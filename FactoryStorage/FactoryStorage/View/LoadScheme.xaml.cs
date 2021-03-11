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
    /// Interaction logic for LoadScheme.xaml
    /// </summary>
    public partial class LoadScheme : Window
    {
        private Scheme previous;

        public LoadScheme(Scheme windowScheme)
        {
            InitializeComponent();

            InitializeCollectionScheme();

            previous = windowScheme;
        }

        public void InitializeCollectionScheme()
        {
            var listScheme = FileProcessing.LoadSchemeNames();

            foreach (var scheme in listScheme)
            {
                comboBoxScheme.Items.Add(scheme);
            }

            comboBoxScheme.SelectedIndex = 0;
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            previous.NameOFLoadScheme = comboBoxScheme.SelectedValue.ToString();

            previous.InicialisationElement();

            this.Close();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
