using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tr_Dispetcher
{
	class DBUtils
	{
		//Данные нужные для поключения к БД
		public static SqlConnection GetDBConnection()
		{
			//Data Source=DESKTOP-BQA1TPO\SQLEXPRESS;Initial Catalog=dispetcher_db;User ID=remote_user;Password=ru

			string server = "DESKTOP-BQA1TPO\\SQLEXPRESS";
			string catalog = "dispetcher_db";
			string user_id = "remote_user";
			string password = "ru";

			return DBSQLServerUtils.GetDBConnection(server, catalog, user_id, password);
		}
		//Функция считывания              всех данных с БД
		internal static void QueryTrip(SqlConnection conn)
		{
			string sql = "select number, station, dept_time, travel_time, tickets from trips_info";

			SqlCommand cmd = new SqlCommand(sql, conn);

			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				if (reader.HasRows)
				{
					while (reader.Read())
					{

						byte numberIndex = Convert.ToByte(reader.GetOrdinal("number"));
						ushort number = Convert.ToUInt16(reader.GetValue(numberIndex));

						byte stationIndex = Convert.ToByte(reader.GetOrdinal("station"));
						string station = reader.GetString(stationIndex);

						byte deptTimeIndex = Convert.ToByte(reader.GetOrdinal("dept_time"));
						TimeSpan dept_time = reader.GetTimeSpan(deptTimeIndex);

						byte travelTimeIndex = Convert.ToByte(reader.GetOrdinal("travel_time"));
						TimeSpan travel_time = reader.GetTimeSpan(travelTimeIndex);

						byte ticketsIndex = Convert.ToByte(reader.GetOrdinal("tickets"));
						int tickets = Convert.ToInt32(reader.GetValue(ticketsIndex));

						//Проверочный бокс для просмотра всех данных
						MessageBox.Show(
							$"Number: {number}\nStation: {station}\nDept time: {dept_time}\nTravel time: {travel_time}\nTickets: {tickets}");
					}
				}
			}

		}
	}
}
