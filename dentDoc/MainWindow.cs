using Npgsql;
using System.Windows;
using System.Windows.Input;


namespace dentDoc
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
           
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