using System;
using System.Collections.Generic;
using System.Data;
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
			// Data Source = "tcp:DESKTOP-BQA1TPO\SQLEXPRESS, 49172"; Initial Catalog = dispetcher_db; User ID = remote_user;Password = ru
			// Data Source = "tcp:DESKTOP-BQA1TPO\SQLEXPRESS, 49172";Initial Catalog = dispetcher_db;User ID = sa;Password = sa
			// Data Source = trdispetcher.mssql.somee.com;Initial Catalog = trdispetcher;User ID = goseler_SQLLogin_1;Password = 2yqshnjdhq

			string server = "trdispetcher.mssql.somee.com";
			string catalog = "trdispetcher";
			string user_id = "goseler_SQLLogin_1";
			string password = "2yqshnjdhq";

			return DBSQLServerUtils.GetDBConnection(server, catalog, user_id, password);
		}
		//Функция считывания всех данных с БД
		public static void QueryTrip(SqlConnection conn, ref DataTable tripsTable)
		{
			tripsTable.Rows.Clear();

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

						tripsTable.Rows.Add(number, station, dept_time, travel_time, tickets);
					}
				}
			}

		}

		public static void QueryTrip_Filters(SqlConnection conn, ref DataTable tripsTable, string selected_city, string selected_hour_a, string selected_hour_b)
		{
			string sql;

			tripsTable.Rows.Clear();

			if (selected_hour_a == "")
			{
				selected_hour_a = "00";
			}

			if (selected_city == "")
			{
				
				if (selected_hour_b == "" || selected_hour_b == "00")
				{
					sql = $"select number, station, dept_time, travel_time, tickets from trips_info where dept_time >= '{selected_hour_a}:00' and dept_time <= '23:59'";
				}
				else
				{
					sql = $"select number, station, dept_time, travel_time, tickets from trips_info where dept_time >= '{selected_hour_a}:00' and dept_time <= '{selected_hour_b}:00'";
				}
			}
			else
			{
				//ql = $"select number, station, dept_time, travel_time, tickets from trips_info where station = '{selected_city}' and dept_time >= '{selected_hour_a}:00' and dept_time <= '{selected_hour_b}:00'";
				if (selected_hour_b == "" || selected_hour_b == "00")
				{
					sql = $"select number, station, dept_time, travel_time, tickets from trips_info where station = '{selected_city}' and dept_time >= '{selected_hour_a}:00' and dept_time <= '23:59'";
				}
				else
				{
					sql = $"select number, station, dept_time, travel_time, tickets from trips_info where station = '{selected_city}' and dept_time >= '{selected_hour_a}:00' and dept_time <= '{selected_hour_b}:00'";
				}
			}

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

						tripsTable.Rows.Add(number, station, dept_time, travel_time, tickets);
					}
				}
			}
		}

		public static void QueryCity(SqlConnection conn, ref List<string> cities)
		{
			cities.Clear();

			string sql = "select station from trips_info";

			SqlCommand cmd = new SqlCommand(sql, conn);

			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						byte stationIndex = Convert.ToByte(reader.GetOrdinal("station"));
						string station = reader.GetString(stationIndex);
						cities.Add(station);
					}
				}
			}

		}
	}
}
