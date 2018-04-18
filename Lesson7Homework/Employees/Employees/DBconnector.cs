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
                    list.Add(new Employee(people.Tables[0].Rows[i]["Name"].ToString(),
                        Convert.ToInt32(people.Tables[0].Rows[i]["Age"]),
                        Convert.ToDecimal(people.Tables[0].Rows[i]["Salary"])));
                return list;
            }
        }

        public static void AddEmployee(string name, int age, decimal salary)
        {

            using (var connection = new SqlConnection(conection_string))
            {
                // В конструкторе указываем запрос SELECT и соединение
                var adapter = new SqlDataAdapter(@"SELECT * FROM People", connection); // Соединение, при этом, адаптер откроет сам, когда ему будет надо


                var insert = new SqlCommand(@"INSERT INTO People (Name,Age,Salary) VALUES (@Name,@Age,@Salary); SET @ID=@@IDENTITY", connection);
                // Добавляем нужные параметры
                insert.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
                insert.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
                insert.Parameters.Add("@Salary", SqlDbType.NVarChar, 100, "Salary");
                insert.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").Direction = ParameterDirection.Output; // Указываем, что параметр команды будет выходным - из него можно бдуте прочитать данные в Вашей программе

                adapter.InsertCommand = insert;

                var update = new SqlCommand(@"UPDATE People SET Name=@Name,Age=@Age WHERE ID=@ID", connection);
                update.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
                update.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
                update.Parameters.Add("@ID", SqlDbType.NVarChar, -1, "ID").SourceVersion = DataRowVersion.Original;

                adapter.UpdateCommand = update;

                var row = people.Tables[0].NewRow();
                row["Name"] = name;  // Заполняем её.
                row["Age"] = age;        // Строка пока никак не связана с таблицей. Она просто имеет структуру этой таблицы.
                row["Salary"] = salary;
                people.Tables[0].Rows.Add(row);                   // Добавляем строку в таблицу

                adapter.Update(people);              // Заставляем адаптер обновить данные в БД на основе модифицированного нами локального набора данных

            }
        }

    }
}
