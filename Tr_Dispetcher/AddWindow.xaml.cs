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

namespace Tr_Dispetcher
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void ReadData(string number, string station, string dept_time, string travel_time, string ticket)
        {
            int numb = Convert.ToInt32(number);
            string stat = station;
            string dtime = dept_time;
            string[] arr = dtime.Split(':');

            string ttime = trav_time.Text;
            string[] mas = ttime.Split(':');
            int tick = Convert.ToInt32(tickets.Text);
            try
            {
                if (numb <= 0 || Convert.ToInt32(arr[0]) < 0 || Convert.ToInt32(arr[0]) > 23 || tick < 0 || Convert.ToInt32(mas[0]) <= 0 || Convert.ToInt32(mas[1]) <= 0 || Convert.ToInt32(arr[1]) < 0 || Convert.ToInt32(arr[1]) > 23)
                    MessageBox.Show("Номер повинен бути більшим за нуль\nЧас відправлення та прибуття повинні бути більшими за нуль та менші 24\nКількість квитків повинна бути додатньою\nЧас в дорозі повинен бути додатнім \nВведіть коректні дані", "Некоректні вхідні дані", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    TimeSpan time_of_dept = Convert.ToDateTime(dept_time).TimeOfDay;
                    TimeSpan time_of_travel = Convert.ToDateTime(travel_time).TimeOfDay;
                    ClassTrip trip = new ClassTrip(Convert.ToUInt16(numb), stat, time_of_dept, time_of_travel, tick);
                    using (SqlConnection conn = DBUtils.GetDBConnection())
                    {
                        try
                        {
                            conn.Open();
                            int k = DBUtils.InsertDataTrip(conn, trip);
                            if (k == 0)
                                MessageBox.Show("Такий рейс вже присутній в списку");
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReadData(number.Text, station.Text, dept_time.Text, trav_time.Text, tickets.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReadData(number.Text, station.Text, dept_time.Text, trav_time.Text, tickets.Text);
            this.Show();
        }
    }
}
