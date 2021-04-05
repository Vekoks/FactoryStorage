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
    /// Interaction logic for ViewTransaction.xaml
    /// </summary>
    public partial class ViewTransaction : Window
    {
        public List<string> transactionElement { get; set; }

        public ViewTransaction(List<string> infoTransactionElement)
        {
            InitializeComponent();

            transactionElement = infoTransactionElement;

            InitializeListBox();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void InitializeListBox()
        {
            foreach (var elementStorage in transactionElement)
            {
                Label newLabel = new Label();

                newLabel.Content = elementStorage;

                newLabel.FontSize = 16;

                listBoxItem.Items.Add(newLabel);
            }
        }
    }
}
