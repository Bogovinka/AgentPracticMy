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

namespace AgentsPractic
{
    /// <summary>
    /// Логика взаимодействия для AddAgent.xaml
    /// </summary>
    public partial class AddAgent : Window
    {
        public AddAgent()
        {
            InitializeComponent();
        }

        private void createB_Click(object sender, RoutedEventArgs e)
        {
            if (TypeT.Text.Length > 0 && NameT.Text.Length > 0 && PhoneT.Text.Length > 0 && PriotityT.Text.Length > 0 && imageT.Text.Length > 0)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("error", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
