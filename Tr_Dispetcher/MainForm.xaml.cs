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
				ButtonEdit.Visibility = Visibility.Hidden;
				ButtonAdd.Visibility = Visibility.Hidden;
				ButtonDelete.Visibility = Visibility.Hidden;
			}

			// При инициализации окна подгружаются города
			List<string> cities = new List<string>();

			using (SqlConnection conn = DBUtils.GetDBConnection())
			{
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
				//conn.Dispose();
			}
			foreach (string i in cities)
			{
				CityBox.Items.Add(i);
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			

			//CityBox.Items.Add("Lviv");
			//CityBox.Items.Add("Kharkiv");
			//CityBox.Items.Add("Sumy");


			//Подключение к БД
			//SqlConnection conn = DBUtils.GetDBConnection();

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
				try
				{
					conn.Open();
					DBUtils.QueryTrip(conn);
					conn.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				//conn.Dispose();
			}
		}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
			AddWindow addWin = new AddWindow();
			addWin.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
			EditWindow editWin = new EditWindow();
			editWin.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
			DeleteWindow dltWin = new DeleteWindow();
			dltWin.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
			TicketWindow tickWin = new TicketWindow();
			tickWin.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}
