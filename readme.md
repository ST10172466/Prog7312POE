# Welcome to the Municipal App V3
# Getting Started
## **1. Prerequisites**

Ensure you have the following installed:
- [Visual Studio](https://visualstudio.microsoft.com/) (2022 or later recommended).
- The **.NET SDK** version required by the project.
- Git for version control ([Download Git](https://git-scm.com)).

---

## **2. Clone the Repository**

1. **Copy the Repository Link**:
   - Navigate to the repository page.
   - Click the **Code** button and copy the HTTPS or SSH link.
   - Or use the link provided: https://github.com/ST10172466/Prog7312POE.git

2. **Open Visual Studio**:
   - Launch Visual Studio.

3. **Open the Git Clone Dialog**:
   - Go to the menu and select `File > Clone Repository...`.

4. **Paste the Repository Link**:
   - In the "Repository location" field, paste the copied repository link.
   - Choose a local folder to store the repository and click **Clone**.

5. **Verify the Clone**:
   - Once cloning is complete, the repository should appear in Visual Studio as an open solution or project.

---

## **3. Open the Project**

1. **Open the Solution in Visual Studio**:
   - If the solution is not automatically opened after cloning, go to `File > Open > Project/Solution`, navigate to the `.sln` file, located in the root directory of the cloned repository, and open it.

---

## **4. Build the Application**

1. **Restore Dependencies**:
   - Visual Studio should automatically restore NuGet packages when the solution is opened.
   - If not, go to `Tools > NuGet Package Manager > Manage NuGet Packages for Solution` and restore packages manually.

2. **Build the Solution**:
   - Press `Ctrl + Shift + B` or go to `Build > Build Solution` to compile the project and check for errors.

---

## **5. Run the Application**

1. **Run the Application**:
   - Press `F5` or click the **Start** button (green play icon) to run the application.
   - The application should launch in the appropriate environment.

---

## **6. Troubleshooting**

1. **Build Errors**:
   - Ensure all dependencies are installed by restoring NuGet packages.
   - Verify that the .NET SDK version matches the required version for the project.

2. **Missing Configurations**:
   - Double-check environment variables or configuration files (e.g., `appsettings.json`).

3. **Git Issues**:
   - If the repository fails to clone, confirm that Git is installed and configured on your system.

---

# Using the Application
Upon launching the application you will be greeted with 3 options.

## **1. Report Issues**

This button takes you to a new page where you can input details about the issue you'd like to report.
There are 5 fields you must fill in:
-   Issue Name
-   Address
-   Category
-   Description
-   Media Attachment

Once all the fields have been filled in, the report can be submitted by clicking the "Submit" button, and you can either submit another report or view all the reports you've submitted.
If you choose to view the reports you've submitted, you can click the "View Reports" button which will take you to the appropriate page.

---

## **2. Report View**

You can select an entry using it's name and all the details about it will be displayed.

---

## **3. Local Events and Announcements**

This button takes you to a new page where you can select and view various events.
You can also:
- Search Events: Search events based on the title.
- Filter Events: Filter events based on category and date range.
- Sort Events: Sort events by title, category, or date.
- View Event: View detailed information about a selected event.

---

## **4. Services**

This button takes you to a new page where you can view all the service requests in your area.
You can also:
- Search Requests: Search requests based on the title.
- Filter Requests: Filter requests based on status and date range.
- Sort Requests: Sort requests by title, status, date, or priority.
- View Requests: View detailed information about a selected request.

### **4.1. Data Structures Used**

**4.1.1. Binary Tree**
- Used to store all **Service Request data** in a structured format.
- A **binary tree** is a tree that has at most two children for any of its nodes.
   - Reference: [GeeksforGeeks - Applications, Advantages and Disadvantages of Binary Tree](https://www.geeksforgeeks.org/applications-advantages-and-disadvantages-of-binary-tree)
- A binary tree is used to store all Service Request data.

- **Advantages**:
  - Represents the Service Requests logically, making it easy to navigate through different layers of data.
  - Supports multiple traversal techniques for various use cases like displaying all requests in a specific order.
  - Data can be quickly added without disrupting the overall structure.

---

**4.1.2. Binary Search Tree (BST)**
- Used to search, retrieve, and filter all **Service Requests**.
- A BST arranges the elements in a specific order:
  - The value of the left node must be smaller than the parent node.
  - The value of the right node must be greater than the parent node.  
  - This rule is applied recursively to both subtrees of the root.
  - Reference: [JavaTPoint - Binary Search Tree](https://www.javatpoint.com/binary-search-tree)

- **Advantages**:
   - Efficient searching for Service Requests.  
   - Enables in-order traversal for retrieving data in sorted order.  
   - Handles dynamic insertion and deletion of nodes while maintaining order.  
   - Facilitates efficient range queries, such as retrieving all service requests between two dates.


---

**4.1.3. Weighted Graph**
- Used manage complex relationships between the Service Requests, specifically the dependencies of each request.
- Represents nodes connected by edges, where weights indicate the cost or priority of the connection.
- It uses an adjacency list or matrix for representation, and algorithms for processing the weights to find optimal paths or prevent cycles.
  - Reference: [Metcalf, L., & Casey, W. (2016). *Weighted Graph. In Cybersecurity and Applied Mathematics.*](https://www.sciencedirect.com/book/9780128044520/cybersecurity-and-applied-mathematics)

- **Advantages:**
   - Efficiently represents Service Requests with dependencies like prerequisite tasks.  
   - Can handle directed, undirected, and weighted relationships.  
   - Compatible with graph algorithms.  
   - Graphs provide an intuitive way to understand and visualize relationships.

---

**4.1.4. Minimum Spanning Tree (MST)**
- Used to prevent cyclical recursion errors in the graph when adding dependencies.
- It uses Kruskal’s Minimum Spanning Tree (MST) Algorithm:
  - Sorts all edges in the given graph in increasing order by weight.
  - Adds edges to the MST if the newly added edge does not form a cycle.  
  - Reference: [GeeksforGeeks - Kruskal’s MST Algorithm](https://www.geeksforgeeks.org/kruskals-minimum-spanning-tree-algorithm-greedy-algo-2/)

- **Advantages**:
   - Ensures the graph remains acyclic, which is critical for dependency management.  
   - Guarantees the minimum total weight for the dependency network.  
   - Suitable for large scale graphs.  

---

**4.1.5. Heap**
- Used to determine the most urgent event by checking which events has the most dependencies.
- A heap is a form of binary tree where:
  - In a min-heap, every node's value is less than or equal to its children.
  - In a max-heap, every node's value is greater than or equal to its children.  
- It is created by using a "heapify" operation to organize elements.
  - Reference: [Stanford Lecture on Heaps](https://web.stanford.edu/class/archive/cs/cs161/cs161.1168/lecture4.pdf)

- **Advantages**:
   - Quickly identifies and retrieves the most urgent Service Request.  
   - Requires minimal space compared to other priority queue implementations.  
   - Can be used as a min-heap for least urgent tasks or a max-heap for most urgent tasks, based on the application's requirements.

---

# **Recommended Technologies**

## Database Technologies
- SQLite: A lightweight, file-based database that could be used to store Report, Event and Service Request data.
- Entity Framework Core: Simplifies database interaction with LINQ and supports multiple database providers, including the aforementioned SQLite.

## Data Caching
- SQLite with Local Caching: Since SQLite is already incorporated for the database, we can make use of the included functionality to store frequently accessed data locally to improve performance and support offline functionality.
- In-Memory Caching: Cache temporary data during runtime to reduce database queries.

## Performance Optimization
- NLog or Serilog: For advanced logging to monitor app performance and troubleshoot issues.
- Async/Await Patterns: Use asynchronous programming to prevent UI freezing during heavy operations like database queries or network calls.

## Location and Geographic Information Systems (GIS) Integration
- Google Maps API: To provide precise location-based services or tracking for more accurate locations for reports and events.

## Integration Tools
- RestSharp: Simplifies API calls with previously mentioned Google Maps API.

## Notifications
- SignalR for WPF: Enables real-time communication for live updates and alerts which could be used to update users on new events or issues in their area.
