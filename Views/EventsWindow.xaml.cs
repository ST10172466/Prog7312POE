using POEPart1.Models;
using POEPart1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace POEPart1
{
    /// <summary>
    /// Interaction logic for EventsWindow.xaml
    /// </summary>
    public partial class EventsWindow : Window
    {
        //-----------------------------------------------------------------------------------------------//
        // Stacks to manage event related data structures
        /// <summary>
        /// Stack to manage undo events
        /// </summary>
        private Stack<Event> undoStack = new Stack<Event>();

        /// <summary>
        /// Stack to manage redo events
        /// </summary>
        private Stack<Event> redoStack = new Stack<Event>();

        // Sorted dictionary to manage organising and retrieving event information
        /// <summary>
        /// Dictionary to store sorted events
        /// </summary>
        private SortedDictionary<DateTime, HashSet<Event>> eventDictionary = new SortedDictionary<DateTime, HashSet<Event>>();

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
        private bool categorySortCheck = true;

        /// <summary>
        /// Boolean to determine if the events sorted by date are ascending or descending
        /// </summary>
        private bool dateSortCheck = true;

        /// <summary>
        /// Current event being viewed
        /// </summary>
        private Event currentEvent;

        private EventViewModel viewModel;

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        public EventsWindow()
        {
            InitializeComponent();
            viewModel = new EventViewModel();
            DataContext = viewModel;
            //PopulateEventDictionary();
        }

        //-----------------------------------------------------------------------------------------------//
        private void PopulateEventDictionary()
        {
            eventDictionary.Clear();

            // Create a list of events for various dates
            var events = new List<Event>
            {
                new Event { Title = "Community Meeting", Category = "Community", Date = DateTime.Now, Description = "Discussion on local development.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/community_meeting.jpg")) },
                new Event { Title = "Music Festival", Category = "Entertainment", Date = DateTime.Now.AddDays(1), Description = "Enjoy live music performances.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/music_festival.jpg")) },

                // Duplicate prevention testing
                new Event { Title = "Music Festival", Category = "Entertainment", Date = DateTime.Now.AddDays(1), Description = "Enjoy live music performances." },

                new Event { Title = "Food Festival", Category = "Culture", Date = DateTime.Now.AddDays(3), Description = "Savor local delicacies.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/food_festival.jpg"))},
                new Event { Title = "Art in the Park", Category = "Art", Date = DateTime.Now.AddDays(5), Description = "Local artists showcase their work.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/art_in_the_park.jpg")) },
                new Event { Title = "Charity Marathon", Category = "Community", Date = DateTime.Now.AddDays(7), Description = "Run to support local charities.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/charity_marathon.jpg")) },
                new Event { Title = "Tech Talk", Category = "Technology", Date = DateTime.Now.AddDays(10), Description = "Discussion on the latest in tech.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/tech_talk.jpg")) },
                new Event { Title = "Health Awareness Day", Category = "Health", Date = DateTime.Now.AddDays(12), Description = "Free health screenings and information.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/health_awareness_day.jpg")) },
                new Event { Title = "Cultural Festival", Category = "Culture", Date = DateTime.Now.AddDays(15), Description = "Celebration of local cultures.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/cultural_festival.jpg")) },
                new Event { Title = "Job Fair", Category = "Career", Date = DateTime.Now.AddDays(18), Description = "Connect with local employers.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/job_fair.jpg")) },
                new Event { Title = "Summer Concert Series", Category = "Entertainment", Date = DateTime.Now.AddDays(20), Description = "Outdoor concerts every Saturday.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/summer_concert.jpg")) },
                new Event { Title = "Photography Workshop", Category = "Education", Date = DateTime.Now.AddDays(22), Description = "Learn photography basics.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/photography_workshop.jpg")) },
                new Event { Title = "Gardening Club Meeting", Category = "Community", Date = DateTime.Now.AddDays(25), Description = "Share gardening tips.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/gardening_club.jpg")) },
                new Event { Title = "Book Reading", Category = "Literature", Date = DateTime.Now.AddDays(28), Description = "Local author reads from their new book.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/book_reading.jpg")) },
                new Event { Title = "Summer Sports Day", Category = "Sports", Date = new DateTime(DateTime.Now.Year, 12, 1), Description = "Fun summer activities for all ages.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/summer_sports_day.jpg")) },
                new Event { Title = "Holiday Craft Fair", Category = "Culture", Date = new DateTime(DateTime.Now.Year, 12, 5), Description = "Handmade gifts for the holidays.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/craft_fair.jpg")) },
                new Event { Title = "New Year's Eve Celebration", Category = "Entertainment", Date = new DateTime(DateTime.Now.Year, 12, 31), Description = "Join us for a countdown to the new year!", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/new_years_eve.jpg")) }
            };

            // Add events to the sorted dictionary by their date, using a HashSet to prevent duplicates
            foreach (var eventItem in events)
            {
                if (!eventDictionary.ContainsKey(eventItem.Date.Date))
                {
                    eventDictionary[eventItem.Date.Date] = new HashSet<Event>();
                }

                if (!eventDictionary[eventItem.Date.Date].Add(eventItem))
                {
                    Console.WriteLine($"Duplicate event not added: {eventItem.Title} on {eventItem.Date.Date}");
                }
            }

            lstEvents.ItemsSource = eventDictionary.SelectMany(x => x.Value).ToList();
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
            FilterEvents();
        }

        //-----------------------------------------------------------------------------------------------//

        // Title Filtering/Sorting Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by title in ascending or descending order
        /// </summary>
        private void btnTitleSort_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SortEventsByTitle();
            HideSortArrows();
            titleAscArrow.Visibility = viewModel.titleSortCheck ? Visibility.Visible : Visibility.Collapsed;
            titleDescArrow.Visibility = !viewModel.titleSortCheck ? Visibility.Visible : Visibility.Collapsed;

        }

        //-----------------------------------------------------------------------------------------------//

        // Category Filtering/Sorting Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to handle category filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isFirstLoad)
            {
                isFirstLoad = false;
            }
            else
            {
                if (cbCategoryFilter.SelectedItem is ComboBoxItem selectedItem)
                {
                    FilterEvents();
                }
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to filter events by category
        /// </summary>
        /// <param name="selectedCategory"></param>
        /// <param name="filteredEvents"></param>
        private void CategoryFilterEvents(string selectedCategory, List<Event> filteredEvents)
        {
            Console.WriteLine("Category: " + selectedCategory);

            if (selectedCategory.Equals("-- Filter by Category --"))
            {
                // Reset to show all filtered events
                lstEvents.ItemsSource = filteredEvents;
            }
            else
            {
                var categoryFilteredEvents = filteredEvents.Where(evt => evt.Category == selectedCategory).ToList();

                // Update ListBox based on category filter results
                if (categoryFilteredEvents.Count > 0)
                {
                    lstEvents.ItemsSource = categoryFilteredEvents;
                }
                else
                {
                    lstEvents.ItemsSource = new List<Event> { new Event { Title = "No events found." } };
                }

                if (categoryFilteredEvents.Count == 0)
                {
                    Console.WriteLine("No events found for the selected category.");
                }
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by category in ascending or descending order
        /// </summary>
        private void btnCategorySort_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SortEventsByCategory();
            HideSortArrows();
            catAscArrow.Visibility = viewModel.categorySortCheck ? Visibility.Visible : Visibility.Collapsed;
            catDescArrow.Visibility = !viewModel.categorySortCheck ? Visibility.Visible : Visibility.Collapsed;
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
            FilterEvents();
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
            FilterEvents();
        }

        //-----------------------------------------------------------------------------------------------//

        // Filter Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to filter events between selected dates
        /// </summary>
        private void FilterEvents()
        {
            string searchTerm = txtSearch.Text.ToLower();
            string selectedCategory = cbCategoryFilter.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : null;

            DateTime? startDate = dpStart.SelectedDate;
            DateTime? endDate = dpEnd.SelectedDate;

            viewModel.FilterEvents(searchTerm, selectedCategory, startDate, endDate);
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by date in ascending or descending order
        /// </summary>
        private void btnDateSort_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SortEventsByDate();
            HideSortArrows();
            dateAscArrow.Visibility = viewModel.dateSortCheck ? Visibility.Visible : Visibility.Collapsed;
            dateDescArrow.Visibility = !viewModel.dateSortCheck ? Visibility.Visible : Visibility.Collapsed;
        }

        //-----------------------------------------------------------------------------------------------//

        // View Event Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to handle event selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstEvents.SelectedItem is Event selectedEvent)
            {
                if (selectedEvent.Title != "No events found.")
                {
                    viewModel.ViewEvent(selectedEvent);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to handle recommendations selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstRecommendations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstRecommendations.SelectedItem is Event selectedEvent)
            {
                viewModel.ViewEvent(selectedEvent);
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // Undo and Redo Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to undo the last viewed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Undo();
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to redo the last undone event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Redo();
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

        // Format Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to format the sorting arrows
        /// </summary>
        private void HideSortArrows()
        {
            titleAscArrow.Visibility = Visibility.Collapsed;
            titleDescArrow.Visibility = Visibility.Collapsed;

            catAscArrow.Visibility = Visibility.Collapsed;
            catDescArrow.Visibility = Visibility.Collapsed;

            dateAscArrow.Visibility = Visibility.Collapsed;
            dateDescArrow.Visibility = Visibility.Collapsed;
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//