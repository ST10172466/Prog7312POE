using POEPart1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media.Imaging;

namespace POEPart1.ViewModels
{
    public class EventViewModel : INotifyPropertyChanged
    {
        //-----------------------------------------------------------------------------------------------//
        // Stacks to manage event related data structures
        private Stack<Event> undoStack = new Stack<Event>();
        private Stack<Event> redoStack = new Stack<Event>();

        /// <summary>
        /// Sorted dictionary to manage organizing and retrieving event information
        /// </summary>
        private SortedDictionary<DateTime, HashSet<Event>> eventDictionary = new SortedDictionary<DateTime, HashSet<Event>>();

        // Sort/Filter Variables
        public bool titleSortCheck = true;
        public bool categorySortCheck = true;
        public bool dateSortCheck = true;

        /// <summary>
        /// Event that holds the current event
        /// </summary>
        private Event selectedEvent;
        public Event SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }

        /// <summary>
        /// List to hold events
        /// Used for display purposes
        /// </summary>
        private List<Event> eventsList;
        public List<Event> EventsList
        {
            get => eventsList;
            set
            {
                eventsList = value;
                OnPropertyChanged(nameof(EventsList));
            }
        }

        /// <summary>
        /// List to hold recommendations based on selected event
        /// Used for display purposes
        /// </summary>
        private List<Event> recommendationsList;
        public List<Event> RecommendationsList
        {
            get => recommendationsList;
            set
            {
                recommendationsList = value;
                OnPropertyChanged(nameof(RecommendationsList));
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        public EventViewModel()
        {
            PopulateEventDictionary();
        }
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to populate the event dictionary
        /// </summary>
        private void PopulateEventDictionary()
        {
            eventDictionary.Clear();

            var events = new List<Event>
            {
                new Event { Title = "Community Meeting", Category = "Community", Date = DateTime.Now, Description = "Discussion on local development.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/community_meeting.jpg")) },
                new Event { Title = "Music Festival", Category = "Entertainment", Date = DateTime.Now.AddDays(1), Description = "Enjoy live music performances.", Image = new BitmapImage(new Uri("pack://application:,,,/Resources/music_festival.jpg")) },
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

            EventsList = eventDictionary.SelectMany(x => x.Value).ToList();
        }

        //-----------------------------------------------------------------------------------------------//

        // Filter Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to filter events based on search term, category, and date range
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="selectedCategory"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void FilterEvents(string searchTerm, string selectedCategory, DateTime? startDate, DateTime? endDate)
        {
            var allEvents = eventDictionary.Values.SelectMany(e => e).ToList();
            var filteredEvents = allEvents;

            // Filter by search term
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredEvents = filteredEvents.Where(e => e.Title.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            // Filter by category
            if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "-- Filter by Category --")
            {
                filteredEvents = filteredEvents.Where(e => e.Category == selectedCategory).ToList();
            }

            // Filter by date range
            if (startDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(e => e.Date >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(e => e.Date <= endDate.Value).ToList();
            }

            // Error check for no events found
            if (filteredEvents.Count > 0)
            {
                EventsList = filteredEvents;
            }
            else
            {
                EventsList = new List<Event> { new Event { Title = "No events found." } };
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // Title Sorting Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by title
        /// </summary>
        public void SortEventsByTitle()
        {
            if (EventsList != null)
            {
                EventsList = titleSortCheck
                    ? EventsList.OrderBy(x => x.Title).ToList()
                    : EventsList.OrderByDescending(x => x.Title).ToList();

                titleSortCheck = !titleSortCheck;
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // Category Sorting Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by category
        /// </summary>
        public void SortEventsByCategory()
        {
            if (EventsList != null)
            {
                EventsList = categorySortCheck
                    ? EventsList.OrderBy(x => x.Category).ToList()
                    : EventsList.OrderByDescending(x => x.Category).ToList();

                categorySortCheck = !categorySortCheck;
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // Date Sorting Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort events by date
        /// </summary>
        public void SortEventsByDate()
        {
            if (EventsList != null)
            {
                EventsList = dateSortCheck
                    ? EventsList.OrderBy(x => x.Date).ToList()
                    : EventsList.OrderByDescending(x => x.Date).ToList();

                dateSortCheck = !dateSortCheck;
            }
        }

        //-----------------------------------------------------------------------------------------------//

        // View Event Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to view the selected event
        /// </summary>
        /// <param name="selectedEvent"></param>
        public void ViewEvent(Event selectedEvent)
        {
            if (selectedEvent != null)
            {
                if (this.selectedEvent != null)
                {
                    undoStack.Push(this.selectedEvent);
                }

                redoStack.Clear();
                SelectedEvent = selectedEvent;
                PopulateRecommendations(selectedEvent);
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to undo the last action
        /// </summary>
        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push(selectedEvent);
                SelectedEvent = undoStack.Pop();
            }
            else
            {
                Console.WriteLine("No events to undo.");
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to redo the last action
        /// </summary>
        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(selectedEvent);
                SelectedEvent = redoStack.Pop();
            }
            else
            {
                Console.WriteLine("No events to redo.");
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to populate recommendations based on selected event
        /// </summary>
        /// <param name="selectedEvent"></param>
        private void PopulateRecommendations(Event selectedEvent)
        {
            DateTime startDate = selectedEvent.Date.AddDays(-7);
            DateTime endDate = selectedEvent.Date.AddDays(7);

            var recommendedEvents = eventDictionary.SelectMany(entry => entry.Value)
                .Where(evt => (evt.Category == selectedEvent.Category ||
                               (evt.Date >= startDate && evt.Date <= endDate))
                               && evt != selectedEvent)
                .ToList();

            RecommendationsList = recommendedEvents.Count == 0
                ? new List<Event> { new Event { Title = "No similar events found." } }
                : recommendedEvents;
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