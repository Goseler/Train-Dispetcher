using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace Tr_Dispetcher
{
	/// <summary>
	/// Логика взаимодействия для PasswordWindow.xaml
	/// </summary>
	public partial class PasswordWindow : Window
	{
		public PasswordWindow()
		{
			InitializeComponent();
		}

		//Проверка пароля и переход в главное окно
		private void ButtonAcceptPass_Click(object sender, RoutedEventArgs e)
		{
			string password = "123456"; // пароль
			string enteredPass = PasswordBox.Password.ToString();
			if (password != enteredPass)
			{
				MessageBox.Show("Невірний пароль! Спробуйте ще раз.", "Помилка авторизації", MessageBoxButton.OK,MessageBoxImage.Error);
			}
			else
			{
				//Переход в главное окно с параметром сотрудника
				MainForm mainWindow = new MainForm(true);
				this.Close();
				mainWindow.Show();
			}
		}

		//Функционал кнопки НАЗАД
		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			AuthWindow authWin = new AuthWindow();
			this.Close();
			authWin.Show();
		}
	}
}
