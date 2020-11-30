using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace FirstApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int k = 0;
        List<passanger> Pasnames = new List<passanger>();
        public List<string> towns = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            SQLConnection connection = new SQLConnection();
            SqlConnection conn = new SqlConnection(
            "Data Source=sql.aksenov.in;user=out_east;Initial Catalog=minaev;password=gisiperu;");
            conn.Open();

            string sql = @"SELECT fullname, place,VagonID,TrainNumber, quantitySt*koef as N'цена билета'
                           FROM passangers join stations on(passangers.id=PassangerID)
                           join vagons ON (passangers.VagonID=vagons.id)
                           join trains ON (trains.TrainNum=vagons.TrainNumber)";

            SqlCommand command = new SqlCommand(sql, conn);

            SqlDataReader stringReader = command.ExecuteReader();
            
            while (stringReader.Read())
            {
                Pasnames.Add(new passanger(String.Format("{0}", stringReader[0]), 
                    String.Format("{0}", stringReader[1]), String.Format("{0}", stringReader[2]),
                    String.Format("{0}", stringReader[3]), String.Format("{0}", stringReader[4])));
                k++;
            }

            conn.Close();

            towns.Add("Санкт-Петербург");
            towns.Add("Великий Новгород");
            towns.Add("Москва");
            towns.Add("Ростов-на-Дону");
            towns.Add("Ставрополь");
            towns.Add("Краснодар");
            towns.Add("Уфа");
            towns.Add("Иркутск");
            towns.Add("Хабаровск");

        }
      
        private void PassangerGrid_Loaded(object sender, RoutedEventArgs e)
        {
            PassangerGrid.ItemsSource = Pasnames;
        }

        private void vacation_Loaded(object sender, RoutedEventArgs e)
        {
            combo1.ItemsSource = towns;
            combo2.ItemsSource = towns;
        }

        private void Passangergrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            passanger path = PassangerGrid.SelectedItem as passanger;
            if (path != null)
                MessageBox.Show("fullname :" + path.fullname);
        }
        public class passanger
        {
            public passanger(string fullname, string place, string Vagon_num, string train_num, string ticket_price) { 
                this.fullname = fullname;
                this.place = place;
                this.Vagon_num = Vagon_num;
                this.train_num = train_num; 
                this.ticket_price = ticket_price;
            }
            public string fullname { get; set; }
            public string place { get; set; }
            public string ticket_price { get; set; }
            public string Vagon_num{ get; set; }
            public string train_num { get; set; }
        }
        public class myVacations
        {

            public myVacations(string name_st, int num_st) { this.name_st = name_st; this.num_st = num_st; }
            public string name_st { get; set; }
            public int num_st { get; set; }

        }
    }
}
