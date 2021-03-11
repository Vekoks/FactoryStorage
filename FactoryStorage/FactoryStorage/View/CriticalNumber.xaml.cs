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
    /// Interaction logic for CriticalNumber.xaml
    /// </summary>
    public partial class CriticalNumber : Window
    {
        public CriticalNumber()
        {
            InitializeComponent();
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            var minNumber = textBoxMinNumber.Text;

            var listCriticalElelment = FileProcessing.GetCriticalNumber();

            foreach (var element in listCriticalElelment)
            {
                if (string.Equals("Елементи", element.Name))
                {
                    element.Number = int.Parse(minNumber);
                }
            }

            FileProcessing.SaveInfomationInFile(listCriticalElelment, "CriticalNumber");
        }
    }
}
