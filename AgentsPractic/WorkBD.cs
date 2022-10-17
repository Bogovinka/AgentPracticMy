using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                string query = $"INSERT INTO agents(Type, Name, phone, Priority, image)" +
                    $" VALUES (N'{type}', N'{name}', N'{phone}', N'{priort}', N'{image}')";
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
        public void updateDB(string name, string ip, string mac, string type, string state, string adres, string note, string name_item, string login, string pass, string SNMP, string vlan, string ser, string mark, string model, string whereS)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = $"UPDATE Equipment SET Name = N'{name}', IP = N'{ip}', MAC = N'{mac}', " +
                    $"Device_Type = N'{type}', Conditions = N'{state}', Address = N'{adres}', Note = N'{note}', Name_Item = N'{name_item}', Login = N'{login}', Password = N'{pass}', SNMP = N'{SNMP}', Num_vlan = N'{vlan}', Serial_num = N'{ser}', Brand = N'{mark}', Model = N'{model}' " +
                    $"WHERE Name_Item = N'{whereS}'";
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
    }
}
