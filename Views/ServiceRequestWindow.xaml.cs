using POEPart1.DataStructures;
using POEPart1.Models;
using POEPart1.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace POEPart1.Views
{
    /// <summary>
    /// Interaction logic for ServiceRequestWindow.xaml
    /// </summary>
    public partial class ServiceRequestWindow : Window
    {
        /// <summary>
        /// ViewModel for the ServiceRequestWindow
        /// </summary>
        private ServiceRequestViewModel viewModel;

        // Sort/Filter Variables
        /// <summary>
        /// Boolean to determine if the events sorted by title are ascending or descending
        /// </summary>
        private bool titleSortCheck = true;

        /// <summary>
        /// Boolean to check if the window is loaded for the first time
        /// </summary>
        private bool isFirstLoad = true;

        /// <summary>
        /// Boolean to determine if the events sorted by category are ascending or descending
        /// </summary>
        private bool statusSortCheck = true;

        /// <summary>
        /// Boolean to determine if the events sorted by date are ascending or descending
        /// </summary>
        private bool dateSortCheck = true;

        /// <summary>
        /// Boolean to determine if the events sorted by priority are ascending or descending
        /// </summary>
        private bool prioritySortCheck = true;

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceRequestWindow()
        {
            InitializeComponent();
            viewModel = new ServiceRequestViewModel();
            DataContext = viewModel;
        }

        //-----------------------------------------------------------------------------------------------//

        // Search Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to search events by title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string selectedStatus = cbStatusFilter.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : null;
            string searchTerm = txtSearch.Text.ToLower();
            DateTime? startDate = dpStart.SelectedDate;
            DateTime? endDate = dpEnd.SelectedDate;

            viewModel.FilterRequests(searchTerm, selectedStatus, startDate, endDate);
        }

        //-----------------------------------------------------------------------------------------------//

        // Title Filtering/Sorting Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by title in ascending or descending order
        /// </summary>
        private void btnTitleSort_Click(object sender, RoutedEventArgs e)
        {
            var filteredRequests = lstRequests.Items.Cast<ServiceRequest>().ToList();
            viewModel.SortRequestsByTitle(titleSortCheck, filteredRequests);
            if (titleSortCheck)
            {
                // Sorts the events by title in ascending order
                HideSortArrows();
                titleAscArrow.Visibility = Visibility.Visible;
                this.titleSortCheck = false;
            }
            else
            {
                // Sorts the events by title in descending order
                HideSortArrows();
                titleDescArrow.Visibility = Visibility.Visible;
                titleSortCheck = true;
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // Status Filtering/Sorting Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to handle category filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isFirstLoad)
            {
                isFirstLoad = false;
            }
            else
            {
                string selectedStatus = cbStatusFilter.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : null;
                string searchTerm = txtSearch.Text.ToLower();
                DateTime? startDate = dpStart.SelectedDate;
                DateTime? endDate = dpEnd.SelectedDate;

                viewModel.FilterRequests(searchTerm, selectedStatus, startDate, endDate);
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by category in ascending or descending order
        /// </summary>
        private void btnStatusSort_Click(object sender, RoutedEventArgs e)
        {
            var filteredRequests = lstRequests.Items.Cast<ServiceRequest>().ToList();
            viewModel.SortRequestsByStatus(statusSortCheck, filteredRequests);
            if (this.statusSortCheck)
            {
                // Sorts the events by title in ascending order
                HideSortArrows();
                statAscArrow.Visibility = Visibility.Visible;
                this.statusSortCheck = false;
            }
            else
            {
                // Sorts the events by title in descending order
                HideSortArrows();
                statDescArrow.Visibility = Visibility.Visible;
                this.statusSortCheck = true;
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // Date Filtering/Sorting Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to handle start date filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStart.SelectedDate.HasValue && dpEnd.SelectedDate.HasValue)
            {
                if (dpEnd.SelectedDate.Value < dpStart.SelectedDate.Value)
                {
                    MessageBox.Show("End date cannot be before start date.", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Warning);
                    dpEnd.SelectedDate = dpStart.SelectedDate;
                    return;
                }
            }

            string selectedStatus = cbStatusFilter.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : null;
            string searchTerm = txtSearch.Text.ToLower();
            DateTime? startDate = dpStart.SelectedDate;
            DateTime? endDate = dpEnd.SelectedDate;

            viewModel.FilterRequests(searchTerm, selectedStatus, startDate, endDate);
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to handle end date filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStart.SelectedDate.HasValue && dpEnd.SelectedDate.HasValue)
            {
                if (dpEnd.SelectedDate.Value < dpStart.SelectedDate.Value)
                {
                    MessageBox.Show("End date cannot be before start date.", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Warning);
                    dpEnd.SelectedDate = dpStart.SelectedDate;
                    return;
                }
            }

            string selectedStatus = cbStatusFilter.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : null;
            string searchTerm = txtSearch.Text.ToLower();
            DateTime? startDate = dpStart.SelectedDate;
            DateTime? endDate = dpEnd.SelectedDate;

            viewModel.FilterRequests(searchTerm, selectedStatus, startDate, endDate);
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by date in ascending or descending order
        /// </summary>
        private void btnDateSort_Click(object sender, RoutedEventArgs e)
        {
            var filteredRequests = lstRequests.Items.Cast<ServiceRequest>().ToList();
            viewModel.SortRequestsByDate(dateSortCheck, filteredRequests);
            if (this.dateSortCheck)
            {
                HideSortArrows();
                dateAscArrow.Visibility = Visibility.Visible;
                this.dateSortCheck = false;
            }
            else
            {
                HideSortArrows();
                dateDescArrow.Visibility = Visibility.Visible;
                this.dateSortCheck = true;
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // Priority Filtering/Sorting Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by category in ascending or descending order
        /// </summary>
        private void btnPrioritySort_Click(object sender, RoutedEventArgs e)
        {
            var filteredRequests = lstRequests.Items.Cast<ServiceRequest>().ToList();
            viewModel.SortRequestsByPriority(prioritySortCheck, filteredRequests);
            if (this.prioritySortCheck)
            {
                // Sorts the events by title in ascending order
                HideSortArrows();
                priorityAscArrow.Visibility = Visibility.Visible;
                this.prioritySortCheck = false;
            }
            else
            {
                // Sorts the events by title in descending order
                HideSortArrows();
                priorityDescArrow.Visibility = Visibility.Visible;
                this.prioritySortCheck = true;
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // Navigation Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Button to navigate back to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            COCTMainWindow mainWindow = new COCTMainWindow();
            mainWindow.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------//

        // Display Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to display the selected request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstRequests.SelectedItem is ServiceRequest selectedRequest)
            {
                if (selectedRequest.Title != "No requests found.")
                {
                    txtRequestID.Text = selectedRequest.ServiceRequestID.ToString();
                    txtRequestTitle.Text = selectedRequest.Title;
                    txtRequestStatus.Text = selectedRequest.Status;
                    txtRequestDate.Text = selectedRequest.DateSubmitted.ToString("MMMM dd, yyyy");
                    txtRequestDescription.Text = selectedRequest.Description;

                    // Displaying priority in a more intuitive way
                    switch (selectedRequest.Priority)
                    {
                        case 1:
                        case 2:
                            txtRequestPriority.Text = "High";
                            break;
                        case 3:
                        case 4:
                            txtRequestPriority.Text = "Medium";
                            break;
                        case 5:
                            txtRequestPriority.Text = "Low";
                            break;
                        default:
                            txtRequestPriority.Text = "Unknown"; // In case of an invalid priority
                            break;
                    }
                }
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to display the selected dependency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDependencies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDependencies.SelectedItem is ServiceRequest selectedRequest)
            {
                txtRequestID.Text = selectedRequest.ServiceRequestID.ToString();
                txtRequestTitle.Text = selectedRequest.Title;
                txtRequestStatus.Text = selectedRequest.Status;
                txtRequestDate.Text = selectedRequest.DateSubmitted.ToString("MMMM dd, yyyy");
                txtRequestDescription.Text = selectedRequest.Description;

                // Displaying priority in a more intuitive way
                switch (selectedRequest.Priority)
                {
                    case 1:
                    case 2:
                        txtRequestPriority.Text = "High";
                        break;
                    case 3:
                    case 4:
                        txtRequestPriority.Text = "Medium";
                        break;
                    case 5:
                        txtRequestPriority.Text = "Low";
                        break;
                    default:
                        txtRequestPriority.Text = "Unknown"; // In case of an invalid priority
                        break;
                }
            }
        }
        //-----------------------------------------------------------------------------------------------//

        // Format Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to format the sorting arrows
        /// </summary>
        private void HideSortArrows()
        {
            titleAscArrow.Visibility = Visibility.Collapsed;
            titleDescArrow.Visibility = Visibility.Collapsed;

            statAscArrow.Visibility = Visibility.Collapsed;
            statDescArrow.Visibility = Visibility.Collapsed;

            dateAscArrow.Visibility = Visibility.Collapsed;
            dateDescArrow.Visibility = Visibility.Collapsed;

            priorityAscArrow.Visibility = Visibility.Collapsed;
            priorityDescArrow.Visibility = Visibility.Collapsed;
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//