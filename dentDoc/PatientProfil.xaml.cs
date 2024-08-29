using Npgsql;
using System.Windows;
using System.Windows.Input;
using static dentDoc.MainWindow;
using static dentDoc.PatientProfil;

namespace dentDoc
{
    /// <summary>
    /// Logique d'interaction pour AddPatient.xaml
    /// </summary>
    public partial class PatientProfil : Window
    {
        public PatientProfil()
        {
            InitializeComponent(); 


        }
        public class PatientProfile
        {
            public float Id_p { get; set; }
            public string Nom_p { get; set; }
            public string Prenom_p { get; set; }
            public float Age_p { get; set; }
            public string NumeroTelephone_p { get; set; }
        }

        private string connectionString = "Server=localhost;Port=5432;Database=dental_clinic;Username=postgres;Password=123456;";

        public void ShowPatientProfile(float patientId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT id_patient, nom, prenom, n_telephone, age FROM patient WHERE id_patient = @patientId";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        PatientProfile patient = new ();

                        if (reader.Read())
                        {

                            patient.Id_p = reader.GetFloat(0);
                            patient.Nom_p = reader.GetString(1);
                            patient.Prenom_p = reader.GetString(2);
                            patient.NumeroTelephone_p = reader.GetString(3);
                            patient.Age_p = reader.GetFloat(4);
                            
                            
                        }
                    this.DataContext = patient;

                }
                }
            }
        }

        
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
            Close();

        }

        private void AddSienceBtn(object sender, RoutedEventArgs e)
        {
            AddSience addSience = new();


            addSience.Show();


        }

    }
}
