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
    /// Logique d'interaction pour AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public AddPatient()
        {
            InitializeComponent();
        }

        
        private void SaveBtn(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=dental_clinic;Username=postgres;Password=123456;";
            string nom = txtNom.Text;
            string prenom = txtPrenom.Text;
            //string dateNaissence = txtDateNaissence.text;
            // ... autres informations du patient

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Patient (nom, prenom, ...) VALUES (@nom, @prenom, ...)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@prenom", prenom);
                    // ... autres paramètres

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Patient ajouté avec succès !");
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de l'ajout du patient.");
                    }
                }
            }
        }


        private void AnnuleAddPatientBtn(object sender, RoutedEventArgs e)
        {
            
            Close();

        }
        
    }
}
