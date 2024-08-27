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
using static dentDoc.MainWindow;

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
            string Nom = txtNom.Text;
            string Prenom = txtPrenom.Text;
            MainWindow mainWindow = new();
          
            DateTime? DateNaissance = txtDateNaissance.SelectedDate;
            string Telephone = txtTelephone.Text;
            // ... autres informations du patient

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
               
                string sql = "INSERT INTO patient (nom, prenom,date_de_naissance, n_telephone) VALUES (@nom, @prenom,  @date_de_naissance, @n_telephone)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nom", Nom);
                    command.Parameters.AddWithValue("@prenom", Prenom);
                    command.Parameters.AddWithValue("@date_de_naissance", DateNaissance);
                    command.Parameters.AddWithValue("@n_telephone", Telephone);
                    
                    // ... autres paramètres

                    int rowsAffected = command.ExecuteNonQuery();
                    
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Patient ajouté avec succès !");
                        Close();
                        
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
