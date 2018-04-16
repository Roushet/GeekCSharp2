using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employes.DataBase
{
	class Program
	{
		#region Строковые константы запросов SQL

		/// <summary>Создание новой таблицы</summary>
		private const string sql_CreateTable = @"CREATE TABLE[dbo].[People] 
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Name] NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[Birthday] NVARCHAR(MAX) NULL,
	[Email]    NVARCHAR(100) NULL,
	[Phone]    NVARCHAR(MAX) NULL,
	CONSTRAINT[PK_dbo.People] PRIMARY KEY CLUSTERED([Id] ASC)
);";

		/// <summary>Создание хранимой процедуры</summary>
		private const string sql_CreateStoredProcedure = @"CREATE PROCEDURE [dbo].[sp_GetPeople] AS SELECT * FROM People;";

		/// <summary>Добавление данных в таблицу People</summary>
		private const string sql_InsertToPeopleTemplate = @"INSERT INTO People (Name,Birthday,Email,Phone) VALUES (N'{0}','{1}','{2}','{3}');";

		/// <summary>Добавление данных в таблицу People с возвращением идентификатора добавленной записи</summary>
		private const string sql_InsertToPeopleWithIDTemplate = @"INSERT INTO People (Name,Birthday,Email,Phone) OUTPUT INSERTED.ID VALUES (N'{0}','{1}','{2}','{3}');";

		/// <summary>Подсчёт количества строк в таблице People</summary>
		private const string sql_SelectCountOfPeople = @"SELECT COUNT(*) FROM People";

		/// <summary>Подсчёт количества строк в таблице People, удовлетворяющих критерию (шаблон строки с одним параметром)</summary>
		private const string sql_SelectCountWithFilter = @"SELECT COUNT(*) FROM People WHERE {0}";

		/// <summary>Запрос на извлечение строк со всеми записями из таблицы People</summary>
		private const string sql_SelectFromPeople = @"SELECT * FROM People";

		/// <summary>Запуск хранимой процедуры</summary>
		private const string sql_ExecuteStoredProc = @"[dbo].[sp_GetPeople]";

		#endregion

		static void Main(string[] args)
		{
			// СТроку подключения можо скомпоновать самостоятельно вручную
			var conection_string = @"data source=(LocalDB)\MSSQLLocalDB;initial catalog=EmployeesDB;integrated sequrity=True";
			// Можно скопировать из окна "Обозреватель серверов"
			conection_string = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EmployeesDB;Integrated Security=True;Pooling=False";

			// Можно получить из файла конфигурации (наиболее предпочтительный способ) (Надо подключить библиотеку System.Configuration)
			conection_string = ConfigurationManager.ConnectionStrings["EmployeesDB"].ConnectionString;

            // Можно воспользоваться построителем строк подключения	SqlConnectionStringBuilder
            //var connetion_string_builder = new SqlConnectionStringBuilder(conection_string);

            // Для подключения к серверу БД используем класс SqlConnection
            //using (var connection = new SqlConnection(conection_string))
            //{
            // connection.Open(); // Не забываем его октрыть!


            //}

            //ExecuteNonQuery(conection_string);	// Запросы к серверу могут быть без возвращаемого значения
            //ExecuteScalar(conection_string);		//	со скалярным ответом
            //ExecuteReader(conection_string);		//	с веторным ответом
            //ParametricQuery(conection_string);	//  параметризованные

            DataAdapterTest(conection_string);                  //	с использованием DataAdapter

            Console.ReadLine();

		}

		/// <summary>Запросы T-SQL</summary>
		/// <param name="ConnectionString">Строка подключения</param>
		private static void ExecuteNonQuery(string ConnectionString)
		{
			int result; // Результат выполнения команды - либо (-1), либо число обработанных строк
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open(); // Не забываем открыть соединение

				// Не забываем указать команде соединение, с которым она будет работать.
				var create_table_command = new SqlCommand(sql_CreateTable, connection); // Создаём таблицу
																						//result = create_table_command.ExecuteNonQuery(); // Исполнение команды (повторно создать таблицу в БД, где она уже есть, не получится!)

				var create_stored_proc = new SqlCommand(sql_CreateStoredProcedure, connection); // Создаём хранимую процедуру
																								//result = create_stored_proc.ExecuteNonQuery();

				var insert_into_people_command = new SqlCommand(  // Добавляем строку данных
					string.Format(sql_InsertToPeopleTemplate,     //	используя шаблон запроса
						"Иванов Иван Иванович",                   //	формируем строку запроса с жёстко-закодированными данными
						"18.10.2001",                             //	Команда без параметров!
						"Hello@World.ru",
						"+7(987)654-32-10"
						), connection);
				//result = insert_into_people_command.ExecuteNonQuery();
			}
		}

		/// <summary>Команды со скалярным ответом (одно значение)</summary>
		/// <param name="ConnectionString">Строка подключения</param>
		private static void ExecuteScalar(string ConnectionString)
		{
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();  // Открыть соединение!

				var select_count_command = new SqlCommand(
					sql_SelectCountOfPeople, // Строка запроса подсчёта количества строк 
					connection               // Незабыть указать соединение!!!
					);
				if (!(select_count_command.ExecuteScalar() is int count)) // Если результатом выполнения команды будет не целое число, то...
					throw new InvalidOperationException();                //	мы что-то делаем не так. Надо сразу об этом узнать! (Называется "быстрый отказ", или "быстрое падение")

				Console.WriteLine("Кол-во строк в таблице {0}", count);

				var insert_command_with_return = new SqlCommand(
					string.Format(sql_InsertToPeopleWithIDTemplate,
						$"Сидоров Пётр Иванович {count}",
						$"16.10.20{count:00}",
						"qwe@asd.com",
						"+7(123)456-78-90"), connection);

				if (!(insert_command_with_return.ExecuteScalar() is int id)) throw new InvalidOperationException();
				Console.WriteLine("id={0}", id);
			}
		}

		/// <summary>Векторный запрос (результатом будет набор данных, Который надо читать построчно)</summary>
		/// <param name="ConnectionString">Строка подключения</param>
		private static void ExecuteReader(string ConnectionString)
		{
			// Локальная процедура, читающая даныне из объекта DbDataReader
			void ReadData(DbDataReader reader)
			{
				if (!reader.HasRows) return; // Проверяем, если в объекте чтения данных нет данных, то дальше работать бессмысленно. Выходим.
				while (reader.Read()) // Читаем до тех пор, пока читается...
				{
					var id = (int)reader.GetValue(0);   // Можем запросить данные по номеру столбца. Они вернуться в "обезличенном" виде "object"
														//id = reader.GetInt32(0);			// Можем прочитать сразу в том виде, в котором нам они нужны.
					var name = reader.GetString(1);
					var email = reader["Email"] as string;  // Можем использовать индексатор
					var phone = reader.GetString(reader.GetOrdinal("Phone")); // Можем определить индекс интересующего нас столбца по его имени
					Console.WriteLine("id:{0}\tname:{1}\temail:{2}\tphone:{3}", id, name, email, phone);
				}
			}

			// Обычный запрос SELECT * FROM ...
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				var select_command = new SqlCommand(sql_SelectFromPeople, connection);
				using (var reader = select_command.ExecuteReader(CommandBehavior.CloseConnection))
				{
					ReadData(reader);
					//reader.Close();
				}
			}

			// Вызов хранимой процедуры, возвращающей набор данных
			Console.WriteLine("\r\nЗапуск хранимой процедуры");
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				var execute_stored_proc = new SqlCommand(sql_ExecuteStoredProc, connection)
				{
					CommandType = CommandType.StoredProcedure
				};
				using (var reader = execute_stored_proc.ExecuteReader())
					ReadData(reader);
			}
		}

		/// <summary>Параметризованные запросы</summary>
		/// <param name="ConnectionString">СТрока подключения</param>
		private static void ParametricQuery(string ConnectionString)
		{
			// Ниже создаём запрос с одним параметром и добавляем в команду этот параметр в явном виде
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				// SELECT COUNT(*) FROM People WHERE Birthday=@Birthday
				var select_command = new SqlCommand(
					string.Format(sql_SelectCountWithFilter, "Birthday=@Birthday"), // Указываем критерий WHERE запроса через строку форматирования
					connection);
				var birthday = new SqlParameter( // Создаём параметр команды 
					"@Birthday",                 //		с указанием его имени
					SqlDbType.NVarChar,          //		типа данных в базе данных
					-1);                         //		и размера (-1 - размер не указан).
				select_command.Parameters.Add(birthday); // Добавляем параметр в команду вручную

				birthday.Value = "16.10.2001";           // Устанавливаем значение параметра команды перез её выполнением
				if (!(select_command.ExecuteScalar() is int count)) throw new InvalidOperationException();
				Console.WriteLine("count = {0}", count);
			}

			// Упрощённый способ задания парамеров команды
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				// SELECT COUNT(*) FROM People WHERE Birthday=@Birthday
				var select_command = new SqlCommand(
					string.Format(sql_SelectCountWithFilter, "Birthday=@Birthday"),
					connection);
				select_command.Parameters.AddWithValue("@Birthday", "16.10.2001"); // Сразу добавляем паарметр указывая имя и значение. Тип параметра будет определён автоматически

				if (!(select_command.ExecuteScalar() is int count)) throw new InvalidOperationException();
				Console.WriteLine("count = {0}", count);
			}
		}

		/// <summary>Запросы к БД через DataAdapter</summary>
		/// <param name="ConnectionString">Строка подключения</param>
		private static void DataAdapterTest(string ConnectionString)
		{
			using (var connection = new SqlConnection(ConnectionString))
			{
				var adapter = new SqlDataAdapter();
				// Для адаптера данных надо указать как минимум команду SELECT
				adapter.SelectCommand = new SqlCommand(@"SELECT * FROM People", connection);

				// Адаптер может выдавать данные в объекты
				var table = new DataTable();    // Талбицы
												//var data_set = new DataSet();	// Наборы данных, содержащие связанные между собой таблицы

				adapter.Fill(table); // Метод, заставляющий адаптер обратиться к БД и извлечь данные, положив их в указанный контейнер

			}

			// Упрощённый способ создания адаптера
			using (var connection = new SqlConnection(ConnectionString))
			{
				// В конструкторе указываем запрос SELECT и соединение
				var adapter = new SqlDataAdapter(@"SELECT * FROM People", connection); // Соединение, при этом, адаптер откроет сам, когда ему будет надо

				var table = new DataTable();
				adapter.Fill(table);

				var set = new DataSet();
				adapter.Fill(set);
			}

			// Полномасштабное наполнение адаптера командами для того, что бы он мог не только запрашивать данные из БД, но и модифицировать их там
			using (var connection = new SqlConnection(ConnectionString))
			{
				// Нужны команды SELECT, INSERT, UPDATE, DELETE - так называемые CRUD- операции: (C)reate, (R)ead, (U)pdate, (D)elete
				var select = new SqlCommand(@"SELECT * FROM People", connection);

				var insert = new SqlCommand(@"INSERT INTO People (Name,Birthday,Email,Phone) VALUES (@Name,@Birthday,@Email,@Phone); SET @ID=@@IDENTITY", connection);
				// Добавляем нужные параметры
				insert.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
				insert.Parameters.Add("@Birthday", SqlDbType.NVarChar, -1, "Birthday");
				insert.Parameters.Add("@Email", SqlDbType.NVarChar, 100, "Email");
				insert.Parameters.Add("@Phone", SqlDbType.NVarChar, -1, "Phone");
				insert.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").Direction = ParameterDirection.Output; // Указываем, что параметр команды будет выходным - из него можно бдуте прочитать данные в Вашей программе

				var update = new SqlCommand(@"UPDATE People SET Name=@Name,Birthday=@Birthday WHERE ID=@ID", connection);
				update.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
				update.Parameters.Add("@Birthday", SqlDbType.NVarChar, -1, "Birthday");
				update.Parameters.Add("@ID", SqlDbType.NVarChar, -1, "ID").SourceVersion = DataRowVersion.Original;

				var delete = new SqlCommand(@"DELETE FROM People WHERE ID=@ID", connection);
				delete.Parameters.Add("@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;

				// Создаём адаптер, указывая ему все нужные ему команды
				var adapter = new SqlDataAdapter
				{
					SelectCommand = select,
					InsertCommand = insert,
					UpdateCommand = update,
					DeleteCommand = delete
				};

				var data_set = new DataSet(); // Создаём
				adapter.Fill(data_set);       //	и заполняем набор данных

				var table = data_set.Tables[0]; // Извлекаем первую (и единственную) таблицу

				var row = table.NewRow();              // Используя таблицу, создаём новую строку данных
				row["Name"] = "Петров Пётр Петрович";  // Заполняем её.
				row["Birthday"] = "21.04.2012";        // Строка пока никак не связана с таблицей. Она просто имеет структуру этой таблицы.
				table.Rows.Add(row);                   // Добавляем строку в таблицу

				adapter.Update(data_set);              // Заставляем адаптер обновить данные в БД на основе модифицированного нами локального набора данных

				// Команды для адаптера можно создать автоматически на основе тлько одной команды SELECT
				var adapter2 = new SqlDataAdapter(select);
				var builder = new SqlCommandBuilder(adapter2);		// Для этого используем построитель команд
				adapter2.InsertCommand = builder.GetInsertCommand();
				adapter2.UpdateCommand = builder.GetUpdateCommand();
				adapter2.DeleteCommand = builder.GetDeleteCommand();
			}
		}
	}
}
