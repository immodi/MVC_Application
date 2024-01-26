
# Company Resource Management MVC Web Application

This MVC (Model-View-Controller) web application is designed to efficiently manage a company's resources, including employees, departments, and users. Built using the .NET framework, it provides a robust and scalable solution for organizations to streamline resource management processes.
## Features
1. User Management
- Role-based Access Control (RBAC): Define user roles such as admin, manager, and employee with corresponding permissions.
- User Authentication: Secure login system to protect sensitive data and ensure access control.

2. Employee Management
- Search and Filter: Effortlessly find books using search functionality and apply filters.
- Responsive Design: The website is designed to work seamlessly across various devices.

3. Department Management
- Create and Edit Departments: Easily create new departments and modify existing ones.
- Assign Employees: Assign employees to specific departments and manage departmental structures.

4. Resource Allocation
- Assign Resources: Efficiently allocate resources to employees and departments.
- View Resource Utilization: Monitor and analyze resource utilization across the organization.
## Prerequisites

Before running the project, ensure you have the following installed:

- .NET Framework: The application is built using the .NET framework, ensuring high performance and reliability.
- ASP.NET MVC: Utilizing the Model-View-Controller architecture for a modular and maintainable codebase.
- Entity Framework: Streamlining database interactions and providing an object-relational mapping (ORM) approach.
- SQL Server: Storing and managing data in a secure and scalable relational database.
## Getting Started

To run this application locally, follow these steps
 
```bash
git clone https://github.com/immodi/MVC_Application.git
```

```bash
cd MVC_Application
```

```bash
dotnet ef database update
```

```bash
dotnet run
```






## Configuration

Adjust the application settings in the appsettings.json file to match your environment, including database connection strings and any other relevant configurations.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your_database_connection_string"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  // Add other configurations as needed
}
```
## Contributing

Contributions are always welcome!

If you'd like to contribute to this project, please follow the standard Git workflow:

- Fork the repository.
- Create a new branch for your feature or bug fix.
- Make your changes and commit them.
- Push your branch to your fork.
- Submit a pull request.

## License

This project is licensed under the [MIT](https://choosealicense.com/licenses/mit/) License

