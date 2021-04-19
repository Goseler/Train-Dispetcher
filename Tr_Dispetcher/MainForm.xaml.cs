using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Data.Common;

namespace Tr_Dispetcher
{
	/// <summary>
	/// Логика взаимодействия для MainForm.xaml
	/// </summary>
	public partial class MainForm : Window
	{
		private bool _isAdmin;
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
				this.Title = "Train Dispetcher - Робітник";
			}
			else
			{
				this.Title = "Train Dispetcher - Гість";
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//Переделать и брать города из БД
			CityBox.Items.Add("Lviv");
			CityBox.Items.Add("Kharkiv");
			CityBox.Items.Add("Sumy");

			//Подключение к БД
			SqlConnection conn = DBUtils.GetDBConnection();
			
			/*try
			{
				conn.Open();
				//MessageBox.Show("Connection successful!");
			}
			catch (Exception)
			{
				MessageBox.Show("На жаль доступ до даних призупинено,\n зверніться до адміністратора", "Помилка доступу до даних", MessageBoxButton.OK, MessageBoxImage.Error);
				AuthWindow authWin = new AuthWindow();
				this.Close();
				authWin.Show();
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}*/// расскоментить на случай считывание всех данных при загрузке окна
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			//using вместо метода Dispose
			using (SqlConnection conn = DBUtils.GetDBConnection())
			{
				conn.Open();
				DBUtils.QueryTrip(conn);
				conn.Close();
				//conn.Dispose();
			}
		}
	}
}
