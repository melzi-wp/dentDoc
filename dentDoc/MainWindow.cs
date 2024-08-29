using Npgsql;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static dentDoc.PatientProfil;


namespace dentDoc
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ChargerLesPatients();

        }

        public class Patient
        {
            public float Id { get; set; }
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public float Age { get; set; }
            public string NumeroTelephone { get; set; }
        }

        private string connectionString = "Server=localhost;Port=5432;Database=dental_clinic;Username=postgres;Password=123456;";

        public void ChargerLesPatients()
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT id_patient, nom, prenom, n_telephone, age FROM patient";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<Patient> patients = new List<Patient>();

                            while (reader.Read())
                            {
                                patients.Add(new Patient
                                {
                                    Id = reader.GetFloat(0),
                                    Nom = reader.GetString(1),
                                    Prenom = reader.GetString(2),
                                    NumeroTelephone = reader.GetString(3),
                                    Age = reader.GetFloat(4)
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

        // delete patient from patients list
        private void DeletePatient(float patientId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM patient WHERE id_patient = @patientId";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@patientId", patientId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeletePatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce patient ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var selectedPatient = patientsDataGrid.SelectedItem as Patient;

                if (selectedPatient != null)
                {
                    float patientId = selectedPatient.Id;
                    DeletePatient(patientId);
                    ChargerLesPatients();
                }
            }
        }

        // show profile patient from patients list

        public void ShowPatientProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientProfil patientProfile = new();

            var selectedPatient = patientsDataGrid.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                float patientId = selectedPatient.Id;
                //string nom_patient = selectedPatient.Nom;
                //string prenom_patient = selectedPatient.Prenom;
                //float age_patient = selectedPatient.Age;

                
                patientProfile.ShowPatientProfile(patientId);

                patientProfile.ShowDialog();

            }

        }


        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();

        }


        private void Min_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

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
