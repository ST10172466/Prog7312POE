using Microsoft.Office.Interop.Word;
using POEPart1.Models;
using POEPart1.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace POEPart1
{
    /// <summary>
    /// Interaction logic for ReportViewWindow.xaml
    /// </summary>
    public partial class ReportViewWindow : System.Windows.Window
    {
        /// <summary>
        /// Local ReportViewModel object
        /// </summary>
        private ReportViewModel viewModel;

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reports"></param>
        public ReportViewWindow(List<Report> reports)
        {
            InitializeComponent();
            viewModel = new ReportViewModel(reports);
            DataContext = viewModel;
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to handle issue entries being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstReports.SelectedItem is Report selectedReport)
            {
                viewModel.SelectedReport = selectedReport;

                if (!string.IsNullOrEmpty(selectedReport.AttachmentFilePath))
                {
                    string fileExtension = System.IO.Path.GetExtension(selectedReport.AttachmentFilePath).ToLower();

                    imgAttachment.Visibility = Visibility.Collapsed;
                    pdfViewerCore.Visibility = Visibility.Collapsed;

                    switch (fileExtension)
                    {
                        // PDF format
                        case ".pdf":
                              pdfViewerCore.Visibility = Visibility.Visible;
                              using (FileStream fileStream = new FileStream(selectedReport.AttachmentFilePath, FileMode.Open, FileAccess.Read))
                              {
                                  Xfinium.Pdf.View.PdfVisualDocument visualDocument = new Xfinium.Pdf.View.PdfVisualDocument();
                                  visualDocument.Load(fileStream);  // Load the file into the PdfVisualDocument

                                  pdfViewerCore.Document = visualDocument;
                              }                            
                            break;

                        // Image formats
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                            imgAttachment.Visibility = Visibility.Visible;
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(selectedReport.AttachmentFilePath, UriKind.Absolute);
                            bitmap.EndInit();
                            imgAttachment.Source = bitmap;
                            break;
                        default:
                            MessageBox.Show("Unsupported file format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
                else
                {
                    imgAttachment.Source = null;
                }
            }
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
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//