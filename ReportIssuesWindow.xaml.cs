using Microsoft.Win32;
using POEPart1.Models;
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

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        public ReportIssuesWindow()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to attach file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAttachMedia_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                attachmentFilePath = openFileDialog.FileName;
                chkbxAttached.IsChecked = true;
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to save the report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InputValidation())
                {
                    Report report = new Report
                    {
                        Name = txtName.Text,
                        Location = txtLocation.Text,
                        Category = cmbCategory.Text,
                        Description = new TextRange(rtbDescription.Document.ContentStart, rtbDescription.Document.ContentEnd).Text,
                        AttachmentFilePath = attachmentFilePath
                    };

                    reportsList.Add(report);

                    MessageBox.Show("Issue reported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.ResetPage();
                }
                else
                {
                    MessageBox.Show("Please fill in all the fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while reporting the issue!\n" + ex.ToString());
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // Input Validation

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to ensure inputs are valid
        /// </summary>
        /// <returns></returns>
        private bool InputValidation()
        {
            if (!string.IsNullOrEmpty(txtName.Text)
                && !string.IsNullOrEmpty(txtLocation.Text)
                && cmbCategory.SelectedIndex != 0
                && !string.IsNullOrEmpty(new TextRange(rtbDescription.Document.ContentStart, rtbDescription.Document.ContentEnd).Text)
                && !string.IsNullOrEmpty(attachmentFilePath))
            {
                return true;
            }
            return false;
        }

        //-----------------------------------------------------------------------------------------------//

        // Format

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to reset the page format
        /// </summary>
        private void ResetPage()
        {
            txtName.Clear();
            txtLocation.Clear();
            cmbCategory.SelectedIndex = 0;
            rtbDescription.Document.Blocks.Clear();
            attachmentFilePath = string.Empty;
            chkbxAttached.IsChecked = false;
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
            if (reportsList.Count != 0)
            {
                ReportViewWindow reportViewWindow = new ReportViewWindow(reportsList);
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