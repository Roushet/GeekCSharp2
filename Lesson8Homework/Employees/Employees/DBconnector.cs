using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Employees
{
    static class DBconnector
    {
        private static string conection_string = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Lesson7;Pooling=False";

        static DataSet people = new DataSet();

        public static List<Employee> GetPeople()
        {


            List<Employee> list = new List<Employee>();

            using (var connection = new SqlConnection(conection_string))
            {
                // В конструкторе указываем запрос SELECT и соединение
                var adapter = new SqlDataAdapter(@"SELECT * FROM People", connection); // Соединение, при этом, адаптер откроет сам, когда ему будет надо

                adapter.Fill(people);

                for (int i = 0; i < people.Tables[0].Rows.Count; i++)
                    //TODO переделать базу, чтобы не было затычек (или переделать класс)
                        list.Add(new Employee (people.Tables[0].Rows[i]["Name"].ToString(), 0, 20));
                return list;
            }
        }

        public static void AddEmployee(string name)
        {

            using (var connection = new SqlConnection(conection_string))
            {
                // В конструкторе указываем запрос SELECT и соединение
                var adapter = new SqlDataAdapter(@"SELECT * FROM People", connection); // Соединение, при этом, адаптер откроет сам, когда ему будет надо


                var insert = new SqlCommand(@"INSERT INTO People (Name,Birthday,Email,Phone) VALUES (@Name,@Birthday,@Email,@Phone); SET @ID=@@IDENTITY", connection);
                // Добавляем нужные параметры
                insert.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
                insert.Parameters.Add("@Birthday", SqlDbType.NVarChar, -1, "Birthday");
                insert.Parameters.Add("@Email", SqlDbType.NVarChar, 100, "Email");
                insert.Parameters.Add("@Phone", SqlDbType.NVarChar, -1, "Phone");
                insert.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").Direction = ParameterDirection.Output; // Указываем, что параметр команды будет выходным - из него можно бдуте прочитать данные в Вашей программе

                adapter.InsertCommand = insert;

                var update = new SqlCommand(@"UPDATE People SET Name=@Name,Birthday=@Birthday WHERE ID=@ID", connection);
                update.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
                update.Parameters.Add("@Birthday", SqlDbType.NVarChar, -1, "Birthday");
                update.Parameters.Add("@ID", SqlDbType.NVarChar, -1, "ID").SourceVersion = DataRowVersion.Original;

                adapter.UpdateCommand = update;

                var row = people.Tables[0].NewRow();
                row["Name"] = name;  // Заполняем её.
                row["Birthday"] = "21.04.2012";        // Строка пока никак не связана с таблицей. Она просто имеет структуру этой таблицы.
                people.Tables[0].Rows.Add(row);                   // Добавляем строку в таблицу

                adapter.Update(people);              // Заставляем адаптер обновить данные в БД на основе модифицированного нами локального набора данных

            }
        }

    }
}
