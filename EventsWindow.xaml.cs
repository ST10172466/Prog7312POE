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
            string searchTerm = txtSearch.Text.ToLower();
            var selectedCategory = cbCategoryFilter.SelectedItem is ComboBoxItem selectedItem
                ? selectedItem.Content.ToString()
                : string.Empty;

            DateTime? startDate = dpStart.SelectedDate;
            DateTime? endDate = dpEnd.SelectedDate;

            viewModel.FilterEvents(searchTerm, selectedCategory, startDate, endDate);

            /*
            var filteredEvents = eventDictionary.SelectMany(entry => entry.Value)
                .Where(evt => evt.Title.ToLower().Contains(searchTerm)
                              && (selectedCategory.Equals("-- Filter by Category --") || evt.Category == selectedCategory)
                              && (!startDate.HasValue || evt.Date >= startDate.Value)
                              && (!endDate.HasValue || evt.Date <= endDate.Value))
                .ToList();

            // Display filtered events in the ListBox
            if (filteredEvents.Count > 0)
            {
                lstEvents.ItemsSource = filteredEvents;
            }
            else
            {
                lstEvents.ItemsSource = new List<Event> { new Event { Title = "No events found." } };
            }*/
        }

        //-----------------------------------------------------------------------------------------------//

        // Title Filtering/Sorting Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by title in ascending or descending order
        /// </summary>
        private void btnTitleSort_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Use the current items in the listbox to sort
            var currentEvents = lstEvents.ItemsSource as List<Event>;

            if (currentEvents != null)
            {
                List<Event> sortedEvents;

                if (this.titleSortCheck)
                {
                    // Sorts the events by title in ascending order
                    sortedEvents = currentEvents.OrderBy(x => x.Title).ToList();
                    HideSortArrows();
                    titleAscArrow.Visibility = Visibility.Visible;
                    this.titleSortCheck = false;
                }
                else
                {
                    // Sorts the events by title in descending order
                    sortedEvents = currentEvents.OrderByDescending(x => x.Title).ToList();
                    HideSortArrows();
                    titleDescArrow.Visibility = Visibility.Visible;
                    this.titleSortCheck = true;
                }

                // Update listbox
                lstEvents.ItemsSource = sortedEvents;
            }*/

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
                    var selectedCategory = selectedItem.Content.ToString();
                    string searchTerm = txtSearch.Text.ToLower();

                    var filteredEvents = string.IsNullOrEmpty(searchTerm)
                        ? eventDictionary.SelectMany(x => x.Value).ToList() // Get all events
                        : eventDictionary.SelectMany(entry => entry.Value)
                                         .Where(evt => evt.Title.ToLower().Contains(searchTerm))
                                         .ToList(); // Filter by search term

                    // Filter events by selected date range
                    DateTime? startDate = dpStart.SelectedDate;
                    DateTime? endDate = dpEnd.SelectedDate;

                    /*
                    if (startDate.HasValue)
                    {
                        filteredEvents = filteredEvents.Where(evt => evt.Date >= startDate.Value).ToList();
                    }

                    if (endDate.HasValue)
                    {
                        filteredEvents = filteredEvents.Where(evt => evt.Date <= endDate.Value).ToList();
                    }

                    // Apply category filter on top of the search results
                    CategoryFilterEvents(selectedCategory, filteredEvents);
                    */
                    viewModel.FilterEvents(searchTerm, selectedCategory, startDate, endDate);

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
            /*
            // Use the current items in the listbox to sort
            var currentEvents = lstEvents.ItemsSource as List<Event>;

            if (currentEvents != null)
            {
                List<Event> sortedEvents;

                if (this.categorySortCheck)
                {
                    // Sorts the events by title in ascending order
                    sortedEvents = currentEvents.OrderBy(x => x.Category).ToList();
                    HideSortArrows();
                    catAscArrow.Visibility = Visibility.Visible;
                    this.categorySortCheck = false;
                }
                else
                {
                    // Sorts the events by title in descending order
                    sortedEvents = currentEvents.OrderByDescending(x => x.Category).ToList();
                    HideSortArrows();
                    catDescArrow.Visibility = Visibility.Visible;
                    this.categorySortCheck = true;
                }

                // Update listbox
                lstEvents.ItemsSource = sortedEvents;
            }*/

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
            DateFilterEvents();
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
            DateFilterEvents();
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to filter events between selected dates
        /// </summary>
        private void DateFilterEvents()
        {
            string searchTerm = txtSearch.Text.ToLower();
            string selectedCategory = cbCategoryFilter.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : null;

            DateTime? startDate = dpStart.SelectedDate;
            DateTime? endDate = dpEnd.SelectedDate;

            viewModel.FilterEvents(searchTerm, selectedCategory, startDate, endDate);
/*
            // Start with all events
            var filteredEvents = eventDictionary.SelectMany(entry => entry.Value)
                                                .Where(evt =>
                                                    (string.IsNullOrEmpty(searchTerm) || evt.Title.ToLower().Contains(searchTerm)) &&
                                                    (selectedCategory.Equals("-- Filter by Category --") || evt.Category == selectedCategory));

            // Filter based on the selected dates
            if (startDate.HasValue && endDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(evt => evt.Date.Date >= startDate.Value.Date && evt.Date.Date <= endDate.Value.Date);
            }
            else if (startDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(evt => evt.Date.Date >= startDate.Value.Date);
            }
            else if (endDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(evt => evt.Date.Date <= endDate.Value.Date);
            }

            // Update ListBox with filtered events
            lstEvents.ItemsSource = filteredEvents.ToList();

            if (!filteredEvents.Any())
            {
                lstEvents.ItemsSource = new List<Event> { new Event { Title = "No events found." } };
            }*/
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by date in ascending or descending order
        /// </summary>
        private void btnDateSort_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Use the current items in the listbox to sort
            var currentEvents = lstEvents.ItemsSource as List<Event>;

            if (currentEvents != null)
            {
                List<Event> sortedEvents;

                if (this.dateSortCheck)
                {
                    sortedEvents = currentEvents.OrderBy(x => x.Date).ToList();
                    HideSortArrows();
                    dateAscArrow.Visibility = Visibility.Visible;
                    this.dateSortCheck = false;
                }
                else
                {
                    sortedEvents = currentEvents.OrderByDescending(x => x.Date).ToList();
                    HideSortArrows();
                    dateDescArrow.Visibility = Visibility.Visible;
                    this.dateSortCheck = true;
                }

                // Update listbox
                lstEvents.ItemsSource = sortedEvents;
            }*/
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
                    //ViewEvent(selectedEvent);
                    viewModel.ViewEvent(selectedEvent);

                }
            }
        }

        /// <summary>
        /// Method to handle recommendations selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstRecommendations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstRecommendations.SelectedItem is Event selectedEvent)
            {
                //ViewEvent(selectedEvent);
                viewModel.ViewEvent(selectedEvent);
            }
        }

        /*
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to handle displaying the selected event and managing the undo and redo stacks
        /// </summary>
        /// <param name="selectedEvent"></param>
        private void ViewEvent(Event selectedEvent)
        {
            if (selectedEvent != null)
            {
                // Push the current event to the undo stack
                if (currentEvent != null)
                {
                    undoStack.Push(currentEvent);
                }

                // Clear the redo stack since a new event is being viewed
                redoStack.Clear();

                currentEvent = selectedEvent;
                DisplayEvent(currentEvent);
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to display the selected event
        /// </summary>
        /// <param name="eventToDisplay"></param>
        private void DisplayEvent(Event eventToDisplay)
        {
            if (eventToDisplay != null)
            {
                txtEventTitle.Text = eventToDisplay.Title;
                txtEventDate.Text = eventToDisplay.Date.ToString("MMMM dd, yyyy");
                txtEventCategory.Text = eventToDisplay.Category;
                txtEventDescription.Text = eventToDisplay.Description;

                if (eventToDisplay.Image != null)
                {
                    imgAttachment.Visibility = Visibility.Visible;
                    imgAttachment.Source = eventToDisplay.Image;  // Display the stored image
                }
                else
                {
                    imgAttachment.Visibility = Visibility.Collapsed;  // Hide if no image
                }

                // Populate recommendations
                PopulateRecommendations(eventToDisplay);
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to populate the recommendations ListBox
        /// </summary>
        /// <param name="selectedEvent"></param>
        private void PopulateRecommendations(Event selectedEvent)
        {
            // Define the date range for recommendations (one week before and after)
            DateTime startDate = selectedEvent.Date.AddDays(-7);
            DateTime endDate = selectedEvent.Date.AddDays(7);

            var recommendedEvents = eventDictionary.SelectMany(entry => entry.Value)
                .Where(evt => (evt.Category == selectedEvent.Category ||
                               (evt.Date >= startDate && evt.Date <= endDate))
                               && evt != selectedEvent)
                .ToList();

            if (recommendedEvents.Count == 0)
            {
                lstRecommendations.ItemsSource = new List<string> { "No similar events found." };
            }
            else
            {
                lstRecommendations.ItemsSource = recommendedEvents;
            }
        }
        */
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
            /*
            if (undoStack.Count > 0)
            {
                // Move the current event to the redo stack
                redoStack.Push(currentEvent);

                // Retrieve the last viewed event
                currentEvent = undoStack.Pop();

                // Display the current event
                DisplayEvent(currentEvent);
            }
            else
            {
                MessageBox.Show("No events to undo.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }*/
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

            /*
            if (redoStack.Count > 0)
            {
                // Move the current event to the undo stack
                undoStack.Push(currentEvent);

                // Retrieve the next event to redo
                currentEvent = redoStack.Pop();

                // Display the current event
                DisplayEvent(currentEvent);
            }
            else
            {
                MessageBox.Show("No events to redo.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }*/
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