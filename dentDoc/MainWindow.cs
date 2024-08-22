using System.Windows;


namespace dentDoc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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