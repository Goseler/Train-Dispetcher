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
    /// Логика взаимодействия для DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        public DeleteWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int numb = Convert.ToUInt16(Number.Text);
            if (numb <= 0)
            {
                MessageBox.Show("Номер рейсу повинен бути додатнім");
            }
            else
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    try
                    {
                        conn.Open();
                        int k = DBUtils.DeleteDataTrip(conn, numb);
                        conn.Close();
                        if(k == 0)
                        {
                            MessageBox.Show("Рейс з таким номером відсутній");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.StackTrace);
                    }
                }
            }
            this.Close();
        }
    }
}
