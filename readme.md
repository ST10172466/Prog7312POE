# Welcome to the Municipal App V3
## Getting Started
## **1. Prerequisites**

Ensure you have the following installed:
- [Visual Studio](https://visualstudio.microsoft.com/) (2022 or later recommended).
- The **.NET SDK** version required by the project.
- Git for version control ([Download Git](https://git-scm.com)).

---

## **2. Clone the Repository**

1. **Copy the Repository Link**:
   - Navigate to the repository page (e.g., on GitHub).
   - Click the **Code** button and copy the HTTPS or SSH link.

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

1. **Locate the Solution File**:
   - Look for a `.sln` file in the root directory of the cloned repository. This is the solution file for the application.

2. **Open the Solution in Visual Studio**:
   - If the solution is not automatically opened after cloning, go to `File > Open > Project/Solution`, navigate to the `.sln` file, and open it.

---

## **4. Build the Application**

1. **Restore Dependencies**:
   - Visual Studio should automatically restore NuGet packages when the solution is opened.
   - If not, go to `Tools > NuGet Package Manager > Manage NuGet Packages for Solution` and restore packages manually.

2. **Build the Solution**:
   - Press `Ctrl + Shift + B` or go to `Build > Build Solution` to compile the project and check for errors.

---

## **5. Run the Application**

1. **Set the Startup Project**:
   - If there are multiple projects in the solution, right-click the desired startup project in the **Solution Explorer** and select **Set as Startup Project**.

2. **Run the Application**:
   - Press `F5` or click the **Start** button (green play icon) to run the application.
   - The application should launch in the appropriate environment (console, browser, or GUI).

---

## **6. Configure Environment Variables (if applicable)**

1. **Locate the `.env` File**:
   - If the application uses a `.env` file or appsettings.json, configure it by adding any required values.

2. **Modify Configuration**:
   - Open `appsettings.json` or use the **Settings** in Visual Studio to add necessary configurations.

---

## **7. Troubleshooting**

1. **Build Errors**:
   - Ensure all dependencies are installed by restoring NuGet packages.
   - Verify that the .NET SDK version matches the required version for the project.

2. **Missing Configurations**:
   - Double-check environment variables or configuration files (e.g., `appsettings.json`).

3. **Git Issues**:
   - If the repository fails to clone, confirm that Git is installed and configured on your system.

---

## **8. Additional Tips**

- **Debugging**:
  Use breakpoints and the Debugger in Visual Studio to inspect and test your code.

- **Update Dependencies**:
  Periodically update NuGet packages to ensure compatibility:
  ```bash
  dotnet add package <PackageName>



## Running the Application
Upon launching the application you will be greeted with 3 options.

### Report Issues
This button takes you to a new page where you can input details about the issue you'd like to report.
There are 5 fields you must fill in:
-   Issue Name
-   Address
-   Category
-   Description
-   Media Attachment

Once all the fields have been filled in, the report can be submitted by clicking the "Submit" button, and you can either submit another report or view all the reports you've submitted.
If you choose to view the reports you've submitted, you can click the "View Reports" button which will take you to the appropriate page.

### Report View
You can select an entry using it's name and all the details about it will be displayed.

### Local Events and Announcements
This button takes you to a new page where you can select and view various events.
You can also:
- Filter Events: Filter events based on category and date range.
- Sort Events: Sort events by title, category, or date.
- View Event: View detailed information about a selected event.

### Services
This button takes you to a new page where you can view all the service requests in your area.
You can also:
- Search Requests: Search requests based on the title.
  - 
- Filter Requests: Filter requests based on status and date range.
- Sort Requests: Sort requests by title, status, date, or priority.
- View Requests: View detailed information about a selected request.
