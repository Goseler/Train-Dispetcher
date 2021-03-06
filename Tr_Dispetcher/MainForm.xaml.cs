using Microsoft.WindowsAPICodePack.Dialogs;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Tr_Dispetcher
{
	/// <summary>
	/// Логика взаимодействия для MainForm.xaml
	/// </summary>
	public partial class MainForm : Window
	{
		private bool _isAdmin;
		private DataTable dt = new DataTable();
		private List<string> cities = new List<string>();
		public MainForm()
		{
			InitializeComponent();
		}

		public MainForm(bool IsAdmin)
		{
			InitializeComponent();
			//Проверка на  сотрудника
			_isAdmin = IsAdmin;
			if (_isAdmin)
			{
				this.Title = "Train Dispetcher - Worker";
			}
			else
			{
				this.Title = "Train Dispetcher - Guest";
				ButtonEdit.Visibility = Visibility.Hidden;
				ButtonAdd.Visibility = Visibility.Hidden;
				ButtonDelete.Visibility = Visibility.Hidden;
			}

			// Инициализация дататейбл
			dt.Columns.Add("Train number", typeof(UInt16));
			dt.Columns.Add("Destination station", typeof(String));
			dt.Columns.Add("Departure time", typeof(TimeSpan));
			dt.Columns.Add("Travel time", typeof(TimeSpan));
			dt.Columns.Add("Number of tickets", typeof(Int32));


			// Обновление данніх
			Update_Data();

			// Инициализация комбо-боксов с часами и минутами
			HoursBoxA.Items.Add("");
			HoursBoxB.Items.Add("");
			for (int i = 0; i < 24; i++)
			{
				if (i > 9)
				{
					HoursBoxA.Items.Add(i);
					HoursBoxB.Items.Add(i);
				}
				else
				{
					HoursBoxA.Items.Add("0" + i.ToString());
					HoursBoxB.Items.Add("0" + i.ToString());
				}
			}
		}

		private void Update_Data()
		{
			// Подгрузка городов из БД
			using (SqlConnection conn = DBUtils.GetDBConnection())
			{
				CityBox.Items.Clear();
				try
				{
					conn.Open();
					DBUtils.QueryCity(conn, ref cities);
					conn.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			foreach (string i in cities)
			{
				CityBox.Items.Add(i);
			}

			// Подгрузка всех данных из БД
			Update_DataTable();
		}

		private void Update_DataTable()
		{
			//Заполнении грида со всеми поездами
			using (SqlConnection conn = DBUtils.GetDBConnection())
			{
				try
				{
					conn.Open();
					DBUtils.QueryTrip(conn, ref dt);
					conn.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			TripsInfoGrid.ItemsSource = dt.DefaultView;
		}

		private void ButtonFind_Click(object sender, RoutedEventArgs e)
		{
			string city = "", hour_a = "", hour_b = "";

			if (CityBox.SelectedIndex > -1)
			{
				city = CityBox.SelectedItem.ToString();
			}

			if (HoursBoxA.SelectedIndex > -1)
			{
				hour_a = HoursBoxA.SelectedItem.ToString();
			}

			if (HoursBoxB.SelectedIndex > -1)
			{
				hour_b = HoursBoxB.SelectedItem.ToString();
			}

			using (SqlConnection conn = DBUtils.GetDBConnection())
			{
				try
				{
					conn.Open();
					DBUtils.QueryTrip_Filters(conn, ref dt, city, hour_a, hour_b);
					conn.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			TripsInfoGrid.ItemsSource = dt.DefaultView;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//Подключение к БД

			using (SqlConnection conn = DBUtils.GetDBConnection())
			{
				try
				{
					conn.Open();
					conn.Close();
				}
				catch (Exception)
				{
					MessageBox.Show("Sorry, data access has been suspended.\nСontact the administrator", "Data access error", MessageBoxButton.OK, MessageBoxImage.Error);
					AuthWindow authWin = new AuthWindow();
					this.Close();
					authWin.Show();
				}
			}
		}

		private void ButtonAdd_Click_1(object sender, RoutedEventArgs e)
		{
			AddWindow addWin = new AddWindow();
			addWin.ShowDialog();
			Update_Data();
		}

		private void ButtonEdit_Click_2(object sender, RoutedEventArgs e)
		{
			EditWindow editWin = new EditWindow();
			editWin.ShowDialog();
			Update_Data();
		}

		private void ButtonDelete_Click_3(object sender, RoutedEventArgs e)
		{
			DeleteWindow dltWin = new DeleteWindow();
			dltWin.ShowDialog();
			Update_Data();
		}


		private void ButtonSave_Click_5(object sender, RoutedEventArgs e)
		{
			try
			{
				CommonSaveFileDialog dialog = new();
				dialog.Filters.Add(new CommonFileDialogFilter("Word Documents", "*.doc"));
				dialog.DefaultFileName = "Tr_Trips_List";
				dialog.DefaultExtension = "doc";
				dialog.InitialDirectory = "";

				if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				{
					string fullPath = dialog.FileName;
					Spire.DataExport.RTF.RTFExport rtfExport = new Spire.DataExport.RTF.RTFExport();
					rtfExport.DataSource = Spire.DataExport.Common.ExportSource.DataTable;
					rtfExport.DataTable = dt;
					rtfExport.ActionAfterExport = Spire.DataExport.Common.ActionType.None;
					rtfExport.FileName = fullPath;
					rtfExport.SaveToFile();

					Document doc = new Document(fullPath);
					//doc.LoadFromFile(fullPath);
					Section section = doc.Sections[0];
					Table table = section.Tables[0] as Table;

					table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);
					doc.SaveToFile(fullPath, FileFormat.Doc);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				MessageBox.Show(ex.StackTrace);
			}
		}
	}
}
