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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgentsPractic
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WorkBD wBD = new WorkBD();
        CreateList cL = new CreateList();
        string searchText = "";
        bool order = false;
        bool unorder = false;
        string filter = "Name";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string[] arr = wBD.editForm(b.Tag.ToString());
            AddAgent c = new AddAgent(arr[0], arr[1], arr[2], arr[3], arr[4]);
            if (c.ShowDialog() == true)
            {
                wBD.updateDB(c.TypeT.Text, c.NameT.Text, c.PhoneT.Text, c.PriotityT.Text, c.imageT.Text, b.Tag.ToString());
            }

        }
        public MainWindow()
        {
            InitializeComponent();
            filtCB.SelectedIndex = 0;
            sortCB.SelectedIndex = 0;
            cL.RelSV(sP, Button_Click, searchText, order, unorder, filter);
        }

        private void searchT_KeyDown(object sender, KeyEventArgs e)
        {
			//обработка нажатия на Enter для поиска
            if (e.Key == Key.Enter)
            {
                searchText = searchT.Text;
                cL.RelSV(sP, Button_Click, searchText, order, unorder, filter);
            }
        }

        private void sortCB_Selected(object sender, RoutedEventArgs e)
        {
			//выбор сортировки
            if(sortCB.SelectedIndex == 0)
            {
                order = false;
                unorder = false;
            }
            else if (sortCB.SelectedIndex == 1)
            {
                order = true;
                unorder = false;
            }
            else if (sortCB.SelectedIndex == 2)
            {
                order = false;
                unorder = true;
            }
            cL.RelSV(sP, Button_Click, searchText, order, unorder, filter);
        }

        private void filtCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			//назначение фильтра
            if (filtCB.SelectedIndex == 0)
                filter = "Name";
            else if (filtCB.SelectedIndex == 1)
                filter = "Type";
            else if (filtCB.SelectedIndex == 2)
                filter = "phone";
            cL.RelSV(sP, Button_Click, searchText, order, unorder, filter);
        }

        private void addI_Click(object sender, RoutedEventArgs e)
        {
			//добавление нового агента
            AddAgent c = new AddAgent();
            if (c.ShowDialog() == true)
            {
                wBD.insertDB(c.TypeT.Text, c.NameT.Text, c.PhoneT.Text, c.PriotityT.Text, c.imageT.Text);
            }
        }
    }
}
