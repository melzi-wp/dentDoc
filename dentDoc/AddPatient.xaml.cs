using Npgsql;
using System.Windows;

namespace dentDoc
{
    /// <summary>
    /// Logique d'interaction pour AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {



        private MainWindow mainWindow;

        public AddPatient()
        {
            InitializeComponent();
           
        }

        
        private void SaveBtn(object sender, RoutedEventArgs e)
        {
            
            string connectionString = "Server=localhost;Port=5432;Database=dental_clinic;Username=postgres;Password=123456;";

            string Nom = txtNom.Text;
            string Prenom = txtPrenom.Text;
            int Age = int.Parse(txtAge.Text);
            DateTime? DateNaissance = txtDateNaissance.SelectedDate;
            string Telephone = txtTelephone.Text;
            
            // ... autres informations du patient

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
               
                string sql = "INSERT INTO patient (nom, prenom, date_de_naissance, n_telephone,age) VALUES (@nom, @prenom,  @date_de_naissance, @n_telephone, @age)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    if (Nom == "" || Prenom == "" )
                    {
                        MessageBox.Show("Entrer les informations de patient");
                    }

                    else
                    {
                        command.Parameters.AddWithValue("@nom", Nom);
                        command.Parameters.AddWithValue("@prenom", Prenom);
                        command.Parameters.AddWithValue("@age", Age);
                        command.Parameters.AddWithValue("@nom", Nom);
                        command.Parameters.AddWithValue("@date_de_naissance", DateNaissance);
                        command.Parameters.AddWithValue("@n_telephone", Telephone);

                        // ... autres paramètres
                        

                        int rowsAffected = command.ExecuteNonQuery();
                    
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Patient ajouté avec succès !");
                            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                            if (mainWindow != null)
                            {
                                mainWindow.ChargerLesPatients();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de l'ajout du patient.");
                        }
                        
                        Close();
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
