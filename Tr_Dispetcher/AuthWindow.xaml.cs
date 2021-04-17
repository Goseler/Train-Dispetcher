using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tr_Dispetcher
{
	/// <summary>
	/// Interaction logic for AuthWindow.xaml
	/// </summary>
	public partial class AuthWindow : Window
	{
		public AuthWindow()
		{
			InitializeComponent();
			
		}

		private void ButtonGuest_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ButtonAdmin_Click(object sender, RoutedEventArgs e)
		{
			PasswordWindow passWin = new PasswordWindow();
			//passWin.Owner = this;

			passWin.Show();
			this.Close();
		}
	}
}
