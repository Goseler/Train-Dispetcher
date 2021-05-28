using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tr_Dispetcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace Tr_Dispetcher.Tests
{
	[TestClass()]
	public class DBUtilsTests
	{
		[TestMethod()]
		public void Test_01_InsertDataTripTest()
		{
			int expected = 1;
			int actual = 0;

			TimeSpan test__dept_time = Convert.ToDateTime("11:12:00").TimeOfDay;
			TimeSpan test_travel_time = Convert.ToDateTime("10:00:00").TimeOfDay;
			ClassTrip test_class = new ClassTrip(111, "TestCity_Insert", test__dept_time, test_travel_time, 52);

			using (SqlConnection conn = DBUtils.GetDBConnection())
			{
					conn.Open();
					actual = DBUtils.InsertDataTrip(conn, test_class);
					conn.Close();
			}

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void Test_02_UpdateDataTripTest()
		{
			int expected = 1;
			int actual = 0;

			TimeSpan test__dept_time = Convert.ToDateTime("11:11:00").TimeOfDay;
			TimeSpan test_travel_time = Convert.ToDateTime("01:56:00").TimeOfDay;
			ClassTrip test_class = new ClassTrip(111, "TestCity_Update", test__dept_time, test_travel_time, 72);

			using (SqlConnection conn = DBUtils.GetDBConnection())
			{
				conn.Open();
				actual = DBUtils.UpdateDataTrip(conn, test_class);
				conn.Close();
			}

			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void Test_03_DeleteDataTripTest()
		{
			int expected = 1;
			int actual = 0;

			using (SqlConnection conn = DBUtils.GetDBConnection())
			{
				conn.Open();
				actual = DBUtils.DeleteDataTrip(conn, 111);
				conn.Close();
			}

			Assert.AreEqual(expected, actual);
		}
	}
}