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
			// Данные для подключения
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

			string sql = "SELECT number, station, dept_time, travel_time, tickets FROM trips_info";

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
					sql = $"SELECT number, station, dept_time, travel_time, tickets FROM trips_info WHERE dept_time >= '{selected_hour_a}:00' AND dept_time <= '23:59'";
				}
				else
				{
					sql = $"SELECT number, station, dept_time, travel_time, tickets FROM trips_info WHERE dept_time >= '{selected_hour_a}:00' AND dept_time <= '{selected_hour_b}:00'";
				}
			}
			else
			{
				if (selected_hour_b == "" || selected_hour_b == "00")
				{
					sql = $"SELECT number, station, dept_time, travel_time, tickets FROM trips_info WHERE station = '{selected_city}' AND dept_time >= '{selected_hour_a}:00' AND dept_time <= '23:59'";
				}
				else
				{
					sql = $"SELECT number, station, dept_time, travel_time, tickets FROM trips_info WHERE station = '{selected_city}' AND dept_time >= '{selected_hour_a}:00' AND dept_time <= '{selected_hour_b}:00'";
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
			cities.Add("");

			string sql = "SELECT DISTINCT station FROM trips_info ORDER BY station ASC";

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

		public static int InsertDataTrip(SqlConnection conn, ClassTrip ins_trip)
		{
			string sql = $"INSERT INTO trips_info(number, station, dept_time, travel_time, tickets) VALUES (@number, @station, @dept_time, @travel_time, @tickets)";
			
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = sql;

			cmd.Parameters.Add("@number", SqlDbType.SmallInt).Value = ins_trip.Number;
			cmd.Parameters.Add("@station", SqlDbType.NVarChar).Value = ins_trip.Station;
			cmd.Parameters.Add("@dept_time", SqlDbType.Time).Value = ins_trip.Dept_time;
			cmd.Parameters.Add("@travel_time", SqlDbType.Time).Value = ins_trip.Travel_time;
			cmd.Parameters.Add("@number", SqlDbType.Int).Value = ins_trip.Tickets;

			int rowCount = cmd.ExecuteNonQuery();
			return rowCount;
		}

		public static int UpdateDataTrip(SqlConnection conn, ClassTrip ins_trip)
		{
			string sql = $"UPDATE trips_info SET";
		}


	}
}

