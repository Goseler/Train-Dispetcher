using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tr_Dispetcher
{
	class DBSQLServerUtils
	{
		//Создание подключения по данным
		public static SqlConnection GetDBConnection(string server, string catalog, string username, string password)
		{
			//Data Source=DESKTOP-BQA1TPO\SQLEXPRESS;Initial Catalog=dispetcher_db;User ID=remote_user;Password=ru

			string connString = $"Data Source={server};Initial Catalog={catalog};User ID={username};Password={password}";

			SqlConnection conn = new SqlConnection(connString);

			return conn;
		}
    }
}
