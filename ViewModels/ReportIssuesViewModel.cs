using Microsoft.Win32;
using POEPart1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POEPart1.ViewModels
{
    public class ReportIssuesViewModel : INotifyPropertyChanged
    {
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Local Report class object to store the report details
        /// </summary>
        private Report localReportClass = new Report();
        public Report LocalReportClass
        {
            get { return localReportClass; }
            set
            {
                localReportClass = value;
                OnPropertyChanged(nameof(LocalReportClass));
            }
        }

        /// <summary>
        /// Attachment file path
        /// </summary>
        private string attachmentFilePath = string.Empty;
        public string AttachmentFilePath
        {
            get { return attachmentFilePath; }
            set
            {
                attachmentFilePath = value;
                OnPropertyChanged(nameof(AttachmentFilePath));
            }
        }

        /// <summary>
        /// List to store reports
        /// </summary>
        private static List<Report> reportsList = new List<Report>();
        public List<Report> ReportsList
        {
            get { return reportsList; }
            set
            {
                reportsList = value;
                OnPropertyChanged(nameof(ReportsList));
            }
        }

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Method to attach file
        /// </summary>
        public void AttachMedia()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                AttachmentFilePath = openFileDialog.FileName;
            }
        }

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Method to save the report
        /// </summary>
        public void SubmitReport(string name, string location, string category, string description)
        {
            try
            {
                if (InputValidation(name, location, category, description))
                {
                    Report report = new Report
                    {
                        Name = name,
                        Location = location,
                        Category = category,
                        Description = description,
                        AttachmentFilePath = AttachmentFilePath
                    };

                    ReportsList.Add(report);

                    MessageBox.Show("Issue reported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetPage();
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
        private bool InputValidation(string name, string location, string category, string description)
        {
            if (!string.IsNullOrEmpty(name)
                && !string.IsNullOrEmpty(location)
                && !string.IsNullOrEmpty(category)
                && !string.IsNullOrEmpty(description)
                && !string.IsNullOrEmpty(AttachmentFilePath))
            {
                return true;
            }
            return false;
        }

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Method to reset the page format
        /// </summary>
        private void ResetPage()
        {
            LocalReportClass = new Report();
            AttachmentFilePath = string.Empty;
        }


        //-----------------------------------------------------------------------------------------------//

        // INotifyPropertyChanged implementation

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// INotifyPropertyChanged implementation
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//