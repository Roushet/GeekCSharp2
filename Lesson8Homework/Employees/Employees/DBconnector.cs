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
    public sealed class DBconnector
    {

        private static DBconnector _connector;


        public static DBconnector Connector => GetConnector();

        public static DBconnector GetConnector()
        {
            if (_connector != null) return _connector;
            _connector = new DBconnector();
            return _connector;
        }

        private static string conection_string = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Lesson7;Pooling=False";
        private static DataSet people = new DataSet();
        private static SqlDataAdapter adapter;

        public void Refresh()
        {
            SqlConnection connection = new SqlConnection(conection_string);
            adapter = new SqlDataAdapter(@"SELECT * FROM People", connection);
        }


        public static List<Employee> GetPeople()
        {
            List<Employee> list = new List<Employee>();

            adapter.Fill(people);

            for (int i = 0; i < people.Tables[0].Rows.Count; i++)
                list.Add(new Employee(people.Tables[0].Rows[i]["Name"].ToString(),
                    Convert.ToInt32(people.Tables[0].Rows[i]["Age"]),
                    Convert.ToDecimal(people.Tables[0].Rows[i]["Salary"])));
            return list;
        }

        public static void AddEmployee(string name, int age, decimal salary)
        {


            SqlCommand insert = new SqlCommand(@"INSERT INTO People (Name,Age,Salary) VALUES (@Name,@Age,@Salary); SET @ID=@@IDENTITY");
            // Добавляем нужные параметры
            insert.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            insert.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
            insert.Parameters.Add("@Salary", SqlDbType.NVarChar, 100, "Salary");
            insert.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").Direction = ParameterDirection.Output; // Указываем, что параметр команды будет выходным - из него можно бдуте прочитать данные в Вашей программе

            adapter.InsertCommand = insert;

            var update = new SqlCommand(@"UPDATE People SET Name=@Name,Age=@Age WHERE ID=@ID");
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

        public static void RemoveEmployeeByName(string name)
        {
            SqlCommand delete = new SqlCommand(@"DELETE FROM People WHERE @Name = name");
            // Добавляем нужные параметры
            delete.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            delete.Parameters["@Name"].Value = name;
            //delete.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").Direction = ParameterDirection.Output; // Указываем, что параметр команды будет выходным - из него можно бдуте прочитать данные в Вашей программе

            adapter.DeleteCommand = delete;

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
        
            adapter.Update(people);              // Заставляем адаптер обновить данные в БД на основе модифицированного нами локального набора данных

        }

    }

}
