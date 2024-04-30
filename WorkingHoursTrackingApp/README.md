# Employee Tracking Web Application
## Description
This web application helps track employee schedules and working hours. It allows users to create, update, and delete schedules, as well as generate reports on employee working hours.

### Prerequisites
1. Visual Studio 2022
2. .NET SDK (version 8.0.204)
3. Microsoft.AspNetCore.App (version 8.0.4)
4. Microsoft.NETCore.App (version 8.0.4)
5. Microsoft.WindowsDesktop.App (version 8.0.4)

### Installation
1. Open Visual Studio 2022 and navigate to the project directory.
2. Restore NuGet packages for the backend by right-clicking on the solution file (*.sln) and selecting Restore NuGet Packages, located in the solution explorer.
3. Start the application by clicking the **Start** button in Visual Studio 2022.

### Usage
- Once the application is running, you can create, update, or delete employees.
- You start and end the tracking for an employee workign time.
- You can also generate reports on employee working hours by navigating to the Reports section.
- And finally you can create, update, or delete employee schedules and list them by employee or by date.

## Additional Information
Use the following format for each date & time query:
>Date only: 2024-04-26

>Date and time: 2024-04-26 08:30:00

If you need to input a start date and end date make sure to add a day or an hour to the end date, for example if you want to list the working hours for employee 1 on April 30th then for the start date put "2024-04-30" and for the end date put "2024-05-01".


To create or update employee schedules use the following format:
```
{
  "employeeId": 123,
  "date": "2024-04-30",
  "startTime": "08:00:00",
  "endTime": "17:00:00"
}
```