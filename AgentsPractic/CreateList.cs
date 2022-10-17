using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AgentsPractic
{
    class CreateList
    {
		//строка подключения
        string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Agents.mdf;Integrated Security=True";
		
		//метод подсчета процента агента
        public string CheckProcent(string Name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                double sum = 0;
                SqlCommand sqlCom = new SqlCommand($"SELECT MinPrice, SizeProduct FROM product, price WHERE Agent = N'{Name}' AND product.Name = price.Name", connection);
                connection.Open();
                SqlDataReader read = sqlCom.ExecuteReader();
                while (read.Read())
                {
                    sum += Convert.ToDouble(read.GetString(0).ToString()) * Convert.ToDouble(read.GetString(1).ToString());
                }
                if (sum >= 10000 && sum <= 50000) return "5%";
                else if (sum <= 150000 && sum >= 50000) return "10%";
                else if (sum >= 150000 && sum <= 500000) return "20%";
                else if (sum >= 500000) return "25%";
                else return "0%";
            }
        }
		
		//метод подсчета количество продаж за всё время
        public int SumP(string Name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int sum = 0;
                SqlCommand sqlCom = new SqlCommand($"SELECT SizeProduct FROM product WHERE Agent = N'{Name}'", connection);
                connection.Open();
                SqlDataReader read = sqlCom.ExecuteReader();
                while (read.Read())
                {
                    sum += Convert.ToInt32(read.GetString(0).ToString());
                }
                return sum;
            }
        }
		//метод создания запроса для списка агентов
        public string CreateSelect(string search, bool order, bool unorder, string filter)
        {
            string select;
			//с сортировкой по убыванию
            if (order)
                select = $"SELECT Type, Name, phone, image, Priority FROM agents WHERE Type LIKE N'%{search}%' OR Name LIKE N'%{search}%' OR phone LIKE N'%{search}%' OR Priority LIKE N'%{search}%' ORDER BY {filter} ASC";
            //с сортировкой по возростанию
			else if (unorder)
                select = $"SELECT Type, Name, phone, image, Priority FROM agents WHERE Type LIKE N'%{search}%' OR Name LIKE N'%{search}%' OR phone LIKE N'%{search}%' OR Priority LIKE N'%{search}%' ORDER BY {filter} DESC";
            //стандартный запрос без сортировки
			else
                select = $"SELECT Type, Name, phone, image, Priority FROM agents WHERE Type LIKE N'%{search}%' OR Name LIKE N'%{search}%' OR phone LIKE N'%{search}%' OR Priority LIKE N'%{search}%'";
            return select;
        }
		//метод создание списка агентов
        public void RelSV(StackPanel sP, RoutedEventHandler Button_Click, string search, bool order, bool unorder, string filter)
        {
			//удаление предыдущего списка
            if (sP.Children.Count > 0)
            {
                sP.Children.RemoveRange(0, sP.Children.Count);
            }
			//перечесление объектов для шаблона
            Image image;
            Label name;
            Label phone;
            Label priorit;
            Label sumP;
            Label procent;
            Border border;
            StackPanel mainRes;
            StackPanel res;
            Button but;
            Grid gr;
            string img;
            string select = CreateSelect(search, order, unorder, filter);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand sqlCom = new SqlCommand(select, connection);
                connection.Open();
                SqlDataReader read = sqlCom.ExecuteReader();
				//заполнение объектов данными из запроса
                while (read.Read())
                {
                    name = new Label();
                    name.Content = $"{read.GetString(0).ToString()} | {read.GetString(1).ToString()}";
                    name.FontSize = 24;
                    phone = new Label();
                    phone.Content = read.GetString(2).ToString();
                    phone.FontSize = 18;
                    img = read.GetString(3).ToString();
                    image = new Image();
                    if (img != "нет")
                    {
                        image.Source = new BitmapImage(new Uri(img, UriKind.Relative));
                    }
                    image.Width = 100;
                    image.Margin = new Thickness(10);
                    priorit = new Label();
                    priorit.Content = "Приоритетность: " + read.GetString(4).ToString();
                    priorit.FontSize = 18;
                    border = new Border();
                    border.BorderBrush = Brushes.DarkGray;
                    border.BorderThickness = new Thickness(1.5, 1.5, 1.5, 1.5);
                    border.Margin = new Thickness(20, 20, 20, 0);
                    but = new Button();
                    but.Content = "Изменить";
                    but.Click += new RoutedEventHandler(Button_Click);
                    but.HorizontalAlignment = HorizontalAlignment.Right;
                    but.Tag = read.GetString(1).ToString();
                    but.Margin = new Thickness(0, 0, 20, 0);
                    mainRes = new StackPanel();
                    mainRes.Orientation = Orientation.Horizontal;
                    mainRes.Children.Add(image);
                    sumP = new Label();
                    sumP.Content = SumP(read.GetString(1).ToString()) + " продаж за год";
                    res = new StackPanel();
                    res.Children.Add(name);
                    res.Children.Add(sumP);
                    res.Children.Add(phone);
                    res.Children.Add(priorit);
                    procent = new Label();
                    procent.Content = CheckProcent(read.GetString(1).ToString());
                    procent.FontSize = 24;
                    procent.Margin = new Thickness(20);
                    mainRes.Children.Add(res);
                    mainRes.Children.Add(procent);
                    gr = new Grid();
                    gr.Children.Add(mainRes);
                    gr.Children.Add(but);
                    border.Child = gr;
                    sP.Children.Add(border);
                }
            }
        }
    }
}
