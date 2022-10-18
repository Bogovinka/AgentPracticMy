using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AgentsPractic
{
    class WorkBD
    {
		//строка подключения
        string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Agents.mdf;Integrated Security=True";
		
		//метод добавление данных в БД
        public void insertDB(string type, string name, string phone, string priort, string image)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = $"INSERT INTO agents(Type, Name, phone, Priority, image, INN)" +
                    $" VALUES (N'{type}', N'{name}', N'{phone}', N'{priort}', N'{image}', N'534534')";
                SqlCommand command = new SqlCommand(query, connection);
                // выполняем запрос
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
		//метод обновления данных БД
        public void updateDB(string type, string name, string phone, string priort, string image, string whereS)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = $"UPDATE agents SET Type = N'{type}', Name = N'{name}', phone = N'{phone}', " +
                    $"Priority = N'{priort}', image = N'{image}'" +
                    $"WHERE INN = N'{whereS}'";
                SqlCommand command = new SqlCommand(query, connection);
                // выполняем запрос
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //метод заполнение полей
        public string[] editForm(string where)
        {
            string[] arr = new string[5];
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCom = new SqlCommand($"SELECT Type, Name, phone, Priority, image FROM agents WHERE INN = '{where}'", connection);
                    connection.Open();
                    SqlDataReader read = sqlCom.ExecuteReader();
                    //заполнение объектов данными из запроса
                    while (read.Read())
                    {
                        arr[0] = read.GetString(0).ToString();
                        arr[1] = read.GetString(1).ToString();
                        arr[2] = read.GetString(2).ToString();
                        arr[3] = read.GetString(3).ToString();
                        arr[4] = read.GetString(4).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return arr;
        }
    }
}
