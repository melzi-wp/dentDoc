using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace dentDoc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
        }

    

   

        private void AddPatientBtn(object sender, RoutedEventArgs e)
        {
            AddPatient addPatient = new();

            addPatient.Show();
           


        }

    }
}