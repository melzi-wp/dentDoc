using Npgsql;
using System.Windows;
using System.Windows.Input;


namespace dentDoc
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=localhost;Port=5432;Database=dental_clinic;Username=postgres;Password=123456;";

        public MainWindow()
        {
            InitializeComponent();
            ChargerLesPatients();

        }
        public class Patient
        {
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public string NumeroTelephone { get; set; }
        }


        public void ChargerLesPatients()
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT nom, prenom, n_telephone FROM patient";
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<Patient> patients = new List<Patient>();
                            
                            while (reader.Read())
                            {
                                patients.Add(new Patient
                                {
                                    Nom = reader.GetString(0),
                                    Prenom = reader.GetString(1),
                                    NumeroTelephone = reader.GetString(2)
                                });
                                 
                            }
                            
                            patientsDataGrid.ItemsSource = patients;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des patients : " + ex.Message);
            }
        }

            private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
            
        }

        private void AddPatientBtn(object sender, RoutedEventArgs e)
        {
            AddPatient addPatient = new();
          

           addPatient.Show();

           
        }
        private void PatientProfilBtn(object sender, RoutedEventArgs e)
        {
            PatientProfil patientProfil = new();


            patientProfil.ShowDialog();
        

        }

    }
}