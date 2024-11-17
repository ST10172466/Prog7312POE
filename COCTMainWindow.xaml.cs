using POEPart1.Views;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace POEPart1
{
    /// <summary>
    /// Interaction logic for COCTMainWindow.xaml
    /// </summary>
    public partial class COCTMainWindow : Window
    {
        //-----------------------------------------------------------------------------------------------//
        public COCTMainWindow()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------//
        private void btnReportIssues_Click(object sender, RoutedEventArgs e)
        {
            ReportIssuesWindow reportIssuesWindow = new ReportIssuesWindow();
            reportIssuesWindow.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------//
        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            EventsWindow eventsWindow = new EventsWindow();
            eventsWindow.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------//
      

//-----------------------------------------------------------------------------------------------//
        private void btnServices_Click(object sender, RoutedEventArgs e)
        {
            ServiceRequestWindow serviceRequestsWindow = new ServiceRequestWindow();
            serviceRequestsWindow.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//