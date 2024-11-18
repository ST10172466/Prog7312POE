using POEPart1.DataStructures;
using POEPart1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace POEPart1.ViewModels
{
    public class ServiceRequestViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Binary Search Tree of ServiceRequests
        /// </summary>
        private ServiceRequestBST serviceRequestBST;

        /// <summary>
        /// Graph of ServiceRequests
        /// </summary>
        private ServiceRequestGraph serviceRequestGraph;

        /// <summary>
        /// ObservableCollection of ServiceRequests
        /// </summary>
        private ObservableCollection<ServiceRequest> serviceRequests;
        public ObservableCollection<ServiceRequest> ServiceRequests
        {
            get => serviceRequests;
            set { serviceRequests = value; OnPropertyChanged(nameof(ServiceRequests)); }
        }


        /// <summary>
        /// String that holds the ID of the most urgent request
        /// </summary>
        private string mostUrgentRequestID;
        public string MostUrgentRequestID
        {
            get { return mostUrgentRequestID; }
            set
            {
                mostUrgentRequestID = value;
                OnPropertyChanged(nameof(MostUrgentRequestID));
            }
        }

        /// <summary>
        /// String that holds the Title of the most urgent request
        /// </summary>
        private string mostUrgentRequestTitle;
        public string MostUrgentRequestTitle
        {
            get { return mostUrgentRequestTitle; }
            set
            {
                mostUrgentRequestTitle = value;
                OnPropertyChanged(nameof(MostUrgentRequestTitle));
            }
        }

        /// <summary>
        /// ObservableCollection of the most urgent Service Request dependencies
        /// </summary>
        private ObservableCollection<ServiceRequest> mostUrgentRequestDependencies;
        public ObservableCollection<ServiceRequest> MostUrgentRequestDependencies
        {
            get => mostUrgentRequestDependencies;
            set
            {
                mostUrgentRequestDependencies = value;
                OnPropertyChanged(nameof(MostUrgentRequestDependencies));
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceRequestViewModel()
        {
            serviceRequestBST = new ServiceRequestBST();
            serviceRequestGraph = new ServiceRequestGraph();
            serviceRequests = new ObservableCollection<ServiceRequest>
            {
                new ServiceRequest { ServiceRequestID = 101, Title = "Water Leak Repair", DateSubmitted = DateTime.Now.AddDays(-5), Status = "In Progress", Description = "Leaking water pipe at location.", Priority = 1 },
                new ServiceRequest { ServiceRequestID = 102, Title = "Pothole Fix", DateSubmitted = DateTime.Now.AddDays(-10), Status = "Completed", Description = "Repair pothole on main street.", Priority = 3 },
                new ServiceRequest { ServiceRequestID = 103, Title = "Street Light Repair", DateSubmitted = DateTime.Now.AddDays(-2), Status = "Pending", Description = "Street light flickering at park entrance.", Priority = 2 },
                new ServiceRequest { ServiceRequestID = 104, Title = "Park Clean-Up", DateSubmitted = DateTime.Now.AddDays(-8), Status = "In Progress", Description = "Scheduled clean-up for local park.", Priority = 4 },
                new ServiceRequest { ServiceRequestID = 106, Title = "Road Sign Replacement", DateSubmitted = DateTime.Now.AddDays(-7), Status = "Completed", Description = "Replace damaged stop sign at intersection.", Priority = 1 },
                new ServiceRequest { ServiceRequestID = 107, Title = "Graffiti Removal", DateSubmitted = DateTime.Now.AddDays(-1), Status = "Pending", Description = "Graffiti on wall near subway station.", Priority = 5 },
                new ServiceRequest { ServiceRequestID = 108, Title = "Broken Bench Repair", DateSubmitted = DateTime.Now.AddDays(-12), Status = "Completed", Description = "Broken bench at the park needs repair.", Priority = 3 },
                new ServiceRequest { ServiceRequestID = 109, Title = "Sidewalk Crack Fix", DateSubmitted = DateTime.Now.AddDays(-4), Status = "In Progress", Description = "Fix cracks on sidewalk in front of library.", Priority = 4 },
                new ServiceRequest { ServiceRequestID = 110, Title = "Tree Trimming", DateSubmitted = DateTime.Now.AddDays(-6), Status = "Pending", Description = "Overgrown tree branches blocking the view of traffic signals.", Priority = 2 },
                new ServiceRequest { ServiceRequestID = 111, Title = "Speed Bump Installation", DateSubmitted = DateTime.Now.AddDays(-9), Status = "In Progress", Description = "Request to install speed bump on residential street.", Priority = 1 },
                new ServiceRequest { ServiceRequestID = 112, Title = "Playground Equipment Repair", DateSubmitted = DateTime.Now.AddDays(-15), Status = "Completed", Description = "Damaged swing set at local playground.", Priority = 5 },
                new ServiceRequest { ServiceRequestID = 113, Title = "Animal Control Assistance", DateSubmitted = DateTime.Now.AddDays(-2), Status = "In Progress", Description = "Stray dogs frequently seen near the school.", Priority = 3 },
                new ServiceRequest { ServiceRequestID = 114, Title = "Bus Shelter Repair", DateSubmitted = DateTime.Now.AddDays(-5), Status = "Pending", Description = "Damaged roof of bus shelter needs replacement.", Priority = 2 },
                new ServiceRequest { ServiceRequestID = 115, Title = "Street Sweeping", DateSubmitted = DateTime.Now.AddDays(-11), Status = "Completed", Description = "Regular street sweeping requested for downtown area.", Priority = 3 },
                new ServiceRequest { ServiceRequestID = 116, Title = "Fire Hydrant Painting", DateSubmitted = DateTime.Now.AddDays(-13), Status = "Completed", Description = "Paint faded fire hydrant in commercial district.", Priority = 2 },
                new ServiceRequest { ServiceRequestID = 117, Title = "Noise Complaint Resolution", DateSubmitted = DateTime.Now.AddDays(-8), Status = "In Progress", Description = "Resolve frequent loud noise from construction site.", Priority = 4 },
                new ServiceRequest { ServiceRequestID = 118, Title = "Drainage System Cleaning", DateSubmitted = DateTime.Now.AddDays(-3), Status = "Pending", Description = "Clear blocked drainage system on Oak Street.", Priority = 3 },
                new ServiceRequest { ServiceRequestID = 119, Title = "Fence Repair", DateSubmitted = DateTime.Now.AddDays(-14), Status = "Completed", Description = "Repair damaged fence in community garden.", Priority = 3 },
                new ServiceRequest { ServiceRequestID = 120, Title = "Bicycle Lane Painting", DateSubmitted = DateTime.Now.AddDays(-10), Status = "In Progress", Description = "Repaint faded markings on bike lane.", Priority = 3 },
                new ServiceRequest { ServiceRequestID = 121, Title = "Street Name Sign Update", DateSubmitted = DateTime.Now.AddDays(-6), Status = "Pending", Description = "Replace outdated street name signs in the area.", Priority = 2 },
                new ServiceRequest { ServiceRequestID = 122, Title = "Utility Pole Repair", DateSubmitted = DateTime.Now.AddDays(-5), Status = "In Progress", Description = "Leaning utility pole near school zone.", Priority = 3 },
                new ServiceRequest { ServiceRequestID = 123, Title = "Bridge Inspection Request", DateSubmitted = DateTime.Now.AddDays(-7), Status = "Pending", Description = "Inspect bridge after recent heavy rainfall.", Priority = 1 },
                new ServiceRequest { ServiceRequestID = 124, Title = "Mosquito Control Spraying", DateSubmitted = DateTime.Now.AddDays(-1), Status = "In Progress", Description = "Spraying requested due to increase in mosquito population.", Priority = 4 }
            };

            foreach (var request in serviceRequests)
            {
                serviceRequestBST.Insert(request);

                // Add to graph with no dependencies initially
                serviceRequestGraph.AddEdge(request, null, 0);
            }

            // Water Leak Repair depends on Pothole Fix
            AddDependency(101, 102, 5);

            // Testing cyclical dependency prevention
            AddDependency(102, 101, 5);

            // Adding most urgent dependency           
            AddDependency(123, 122, 1); // Utility Pole Repair depends on Bridge Inspection Request
            AddDependency(123, 120, 1); // Bicycle Lane Painting depends on Bridge Inspection Request
            AddDependency(123, 109, 1); // Sidewalk Crack Fix depends on Bridge Inspection Request

            AddDependency(118, 115, 3); // Street Sweeping depends on Drainage System Cleaning

            DetermineMostUrgentRequest();
        }

        //-----------------------------------------------------------------------------------------------//

        // Population Methods

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to add a service request
        /// </summary>
        /// <param name="request"></param>
        public void AddServiceRequest(ServiceRequest request)
        {
            if (serviceRequestBST.Insert(request))
            {
                ServiceRequests.Add(request);

                // Add to graph with no dependencies initially
                serviceRequestGraph.AddEdge(request, null, 0);
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to add a dependency between service requests
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="weight"></param>
        public void AddDependency(int fromId, int toId, int weight)
        {
            // Find the service requests by their IDs
            var fromRequest = serviceRequestBST.Search(fromId);
            var toRequest = serviceRequestBST.Search(toId);

            if (fromRequest != null && toRequest != null)
            {
                // Add the edge to the graph
                serviceRequestGraph.AddEdge(fromRequest, toRequest, weight);

                // Update the graph with the MST to prevent cycles
                UpdateGraphWithMST();
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to update the graph using the minimum spanning tree
        /// </summary>
        public void UpdateGraphWithMST()
        {
            // Get the minimum spanning tree
            var mstEdges = serviceRequestGraph.GetMinimumSpanningTree();

            // Clear the current graph
            serviceRequestGraph = new ServiceRequestGraph();

            // Add the MST edges to the graph
            foreach (var edge in mstEdges)
            {
                serviceRequestGraph.AddEdge(edge.from, edge.to, edge.weight);
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to determine the most urgent request
        /// </summary>
        public void DetermineMostUrgentRequest()
        {
            var dependencyCount = serviceRequestGraph.CountDependencies();
            var heap = new ServiceRequestHeap();

            foreach (var kvp in dependencyCount)
            {
                heap.Insert(kvp.Key, kvp.Value);
            }

            var mostUrgentRequest = dependencyCount.OrderBy(dc => dc.Value).FirstOrDefault().Key;

            if (mostUrgentRequest != null)
            {
                mostUrgentRequest = heap.ExtractMax().request;
                MostUrgentRequestID = mostUrgentRequest.ServiceRequestID.ToString();
                MostUrgentRequestTitle = serviceRequestBST.Search(mostUrgentRequest.ServiceRequestID).Title;

                Console.WriteLine($"Most urgent request determined: '{MostUrgentRequestTitle}' with ID {MostUrgentRequestID}.");

                var dependencies = serviceRequestGraph.GetNeighbors(mostUrgentRequest.ServiceRequestID);
                MostUrgentRequestDependencies = new ObservableCollection<ServiceRequest>(dependencies);
            }
            else
            {
                MostUrgentRequestID = "N/A";
                MostUrgentRequestTitle = "N/A";
            }

            // Notify property change
            OnPropertyChanged(nameof(MostUrgentRequestID));
            OnPropertyChanged(nameof(MostUrgentRequestTitle));
        }

        //-----------------------------------------------------------------------------------------------//

        // Filter Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to filter service requests
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void FilterRequests(string searchTerm, string selectedStatus, DateTime? startDate, DateTime? endDate)
        {
            var allRequests = serviceRequestBST.DisplayAllRequests();

            var filteredRequests = allRequests;

            // Filter by search term
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredRequests = filteredRequests.Where(r => r.Title.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            // Filter by status
            if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "-- Filter by Status --")
            {
                filteredRequests = filteredRequests.Where(r => r.Status == selectedStatus).ToList();
            }

            // Filter by date range
            if (startDate.HasValue)
            {
                filteredRequests = filteredRequests.Where(r => r.DateSubmitted >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                filteredRequests = filteredRequests.Where(r => r.DateSubmitted <= endDate.Value).ToList();
            }

            // Update the ObservableCollection
            ServiceRequests.Clear();
            foreach (var request in filteredRequests)
            {
                ServiceRequests.Add(request);
            }

            OnPropertyChanged(nameof(ServiceRequests));
        }

        //-----------------------------------------------------------------------------------------------//

        // Title Sorting Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort service requests by title
        /// </summary>
        /// <param name="ascending"></param>
        /// <param name="filteredRequests"></param>
        public void SortRequestsByTitle(bool ascending, List<ServiceRequest> filteredRequests)
        {
            var sortedRequests = ascending
         ? filteredRequests.OrderBy(r => r.Title).ToList()
         : filteredRequests.OrderByDescending(r => r.Title).ToList();

            Console.WriteLine("Sorted by Title:");
            foreach (var request in sortedRequests)
            {
                Console.WriteLine($"Title: {request.Title}, Status: {request.Status}, Order: {ascending}");
            }

            ServiceRequests.Clear();
            foreach (var request in sortedRequests)
            {
                ServiceRequests.Add(request);
            }

            OnPropertyChanged(nameof(ServiceRequests));
        }

        //-----------------------------------------------------------------------------------------------//

        // Status Sorting Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort service requests by status
        /// </summary>
        /// <param name="ascending"></param>
        /// <param name="filteredRequests"></param>
        public void SortRequestsByStatus(bool ascending, List<ServiceRequest> filteredRequests)
        {
            var sortedRequests = ascending
         ? filteredRequests.OrderBy(r => r.Status).ToList()
         : filteredRequests.OrderByDescending(r => r.Status).ToList();

            ServiceRequests.Clear();
            foreach (var request in sortedRequests)
            {
                ServiceRequests.Add(request);
            }

            OnPropertyChanged(nameof(ServiceRequests));
        }

        //-----------------------------------------------------------------------------------------------//

        // Date Sorting Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort service requests by date
        /// </summary>
        /// <param name="ascending"></param>
        /// <param name="filteredRequests"></param>
        public void SortRequestsByDate(bool ascending, List<ServiceRequest> filteredRequests)
        {
            var sortedRequests = ascending
       ? filteredRequests.OrderBy(r => r.DateSubmitted).ToList()
       : filteredRequests.OrderByDescending(r => r.DateSubmitted).ToList();

            ServiceRequests.Clear();
            foreach (var request in sortedRequests)
            {
                ServiceRequests.Add(request);
            }
            OnPropertyChanged(nameof(ServiceRequests));
        }

        //-----------------------------------------------------------------------------------------------//

        // Status Sorting Method

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to sort service requests by status
        /// </summary>
        /// <param name="ascending"></param>
        /// <param name="filteredRequests"></param>
        public void SortRequestsByPriority(bool ascending, List<ServiceRequest> filteredRequests)
        {
            var sortedRequests = ascending
         ? filteredRequests.OrderBy(r => r.Priority).ToList()
         : filteredRequests.OrderByDescending(r => r.Priority).ToList();

            ServiceRequests.Clear();
            foreach (var request in sortedRequests)
            {
                ServiceRequests.Add(request);
            }

            OnPropertyChanged(nameof(ServiceRequests));
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