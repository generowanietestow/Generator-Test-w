using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;


namespace test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string tresc, kat;
        string categoryValue;

        public MainWindow()
        {
            InitializeComponent();

            //POBRANIE UNIKALNYCH NAZW KATEGORII Z BAZY DANYCH I UTWORZENIE ELEMENTÓW DLA COMBOBOXA
            //LINK DO FILMU: https://www.youtube.com/watch?v=au-5buEKT_k
            string connectionString = "SERVER=10.100.5.142; DATABASE=tomber_testy; user id=tomber; PASSWORD=tomber;";
            string result = "SELECT DISTINCT Kategoria FROM tomber_testy.Pytania";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                using (MySqlCommand komenda = new MySqlCommand(result, connection))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(result, connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        comboBox.Items.Add(dt.Rows[i]["Kategoria"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void pyt_TextChanged(object sender, TextChangedEventArgs e)
        {
            tresc = pyt.Text;
            

        }

        private void zapisz_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=10.100.5.142; DATABASE=tomber_testy; user id=tomber; PASSWORD=tomber;";
            if(pyt.Text != string.Empty)
            {
                string query = "insert into tomber_testy.Pytania (id, treść, kategoria) values('null','" + tresc + "','" + categoryValue + "')";
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand komenda = new MySqlCommand(query, connection);
                MySqlDataReader myreader;
                try
                {
                    connection.Open();
                    myreader = komenda.ExecuteReader();
                    MessageBox.Show("Zapisano");
                    while (myreader.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Pusty");
            }
           
        }


        TextBox t;


        private void kategoria_Click(object sender, RoutedEventArgs e)
        {
            Window kategoria = new Window
            {
                SizeToContent = SizeToContent.WidthAndHeight
            };
            kategoria.Title = "Dodaj kategorię";
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(20) };
            stackPanel.Children.Add(new Label { Content = "Dodaj kategorię" });
            TextBox tex = new TextBox
            {
                Name = "tex45",
                Width = 150,
                Height = 20
            };
            tex.TextChanged += tex_Changed;
            stackPanel.Children.Add(tex);
           /* stackPanel.Children.Add(new Label { Content = "Dodaj kategorię" });
            stackPanel.Children.Add(new TextBox { Name = "tex1", Height = 30, Width = 200, TextWrapping =TextWrapping.Wrap });
            stackPanel.Children.Add(new Button { Name = "but", Content = "Dodaj" });*/
            Button but1 = new Button
            {
                Name = "but",
                Content = "Dodaj"
            };
            but1.Click += but_Click;
            stackPanel.Children.Add(but1);
            kategoria.Content = stackPanel;
            kategoria.Show();
            t = tex;
           

        }
        private void tex_Changed(object sender, TextChangedEventArgs e)
        {
            kat = t.Text;
        }


        private void but_Click(object sender, RoutedEventArgs e)
        {
            if(t.Text != string.Empty)
            {
                comboBox.Items.Add(kat);
                MessageBox.Show("Dodane");
            }
            else
            {
                MessageBox.Show("Wypełnij pole!");
            }
            
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryValue = this.comboBox.SelectedItem.ToString();
        }
    }
}
