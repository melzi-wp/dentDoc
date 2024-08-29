using Npgsql;
using System;
using System.Collections.Generic;
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

namespace dentDoc
{
    /// <summary>
    /// Interaction logic for AddSience.xaml
    /// </summary>
    public partial class AddSience : Window
    {
        public AddSience()
        {
            InitializeComponent();
        }

        private void SaveSienceBtn(object sender, RoutedEventArgs e)
        {

            string connectionString = "Server=localhost;Port=5432;Database=dental_clinic;Username=postgres;Password=123456;";

            string Type = txtType.Text;
            DateTime? Date = txtDate.SelectedDate;
            DateTime? Heure = txtHeure.SelectedDate;
            string Dent = txtDentSoigner.Text;

            // ... autres informations du patient

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO séance (date_seance, type_seance ) VALUES ( @date_seance, @type_seance";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    if (Type == "" )
                    {
                        MessageBox.Show("Entrer les informations de séance");
                    }

                    else
                    {
                        command.Parameters.AddWithValue("@date_seance",Date);
                        command.Parameters.AddWithValue("@type_seance", Type);
                        
                        // ... autres paramètres


                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Séance ajouté avec succès !");
                            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                            if (mainWindow != null)
                            {
                                //mainWindow.ChargerLesSeance();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de l'ajout du Séance.");
                        }

                        Close();
                    }

                }
            }
        }
    }
}
