using Microsoft.Win32;
using POEPart1.Models;
using POEPart1.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace POEPart1
{
    public partial class ReportIssuesWindow : Window
    {
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Local Report class object to store the report details
        /// </summary>
        private Report LocalReportClass = new Report();

        /// <summary>
        /// Attachment file path
        /// </summary>
        private string attachmentFilePath = string.Empty;

        /// <summary>
        /// List to store reports
        /// </summary>
        private static List<Report> reportsList = new List<Report>();

        /// <summary>
        /// Local ReportIssuesViewModel object
        /// </summary>
        private ReportIssuesViewModel viewModel;

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        public ReportIssuesWindow()
        {
            InitializeComponent();
            viewModel = new ReportIssuesViewModel();
            DataContext = viewModel;
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to attach file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAttachMedia_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AttachMedia();
            chkbxAttached.IsChecked = !string.IsNullOrEmpty(viewModel.AttachmentFilePath);
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to save the report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string location = txtLocation.Text;
            string category = cmbCategory.Text;
            string description = new TextRange(rtbDescription.Document.ContentStart, rtbDescription.Document.ContentEnd).Text;

            viewModel.SubmitReport(name, location, category, description);

        }

        //-----------------------------------------------------------------------------------------------//

        // Navigation

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to navigate back to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            COCTMainWindow mainWindow = new COCTMainWindow();
            mainWindow.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to navigate to the report view window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.ReportsList.Count != 0)
            {
                ReportViewWindow reportViewWindow = new ReportViewWindow(viewModel.ReportsList);
                reportViewWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No reports available to display.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//