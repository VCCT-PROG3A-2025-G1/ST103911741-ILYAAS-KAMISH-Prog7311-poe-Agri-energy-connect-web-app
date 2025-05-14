# Prog7311 poe
Agri-Energy Connect
Agri-Energy Connect is an ASP.NET Core MVC web application that bridges sustainable agriculture with green energy solutions by enabling farmers to manage their products and allowing employees to oversee farmer registrations and product listings.

Table of Contents

Features
System Architecture
User Roles
Prerequisites
Development Environment Setup
Building and Running the Application
Database Configuration
Usage Guide
Screenshots
Technologies Used
Future Enhancements
Contributing
License

Features

User Authentication: Secure login system with role-based authorization
Role-Based Access Control: Different interfaces and capabilities for Farmers and Employees
Farmer Management: Employees can add, view, and manage farmer accounts
Product Management: Farmers can add their agricultural and energy products
Advanced Filtering: Employees can filter products by farmer, category, and date range
Responsive Design: Mobile-friendly interface with Bootstrap and Animate.css

System Architecture
Agri-Energy Connect follows the Model-View-Controller (MVC) architectural pattern:

Models: Define data structures and business logic
Views: Present data to users with responsive, intuitive interfaces
Controllers: Handle user requests and update models and views accordingly

The application uses Entity Framework Core for data access and Microsoft SQL Server for data storage.
User Roles
Employee Role

Add and manage farmer accounts
View all farmers and their products
Filter and search products across all farmers
View system statistics and dashboard

Farmer Role

Manage personal product listings
Add new agricultural and energy products
View own dashboard with product statistics

Prerequisites
Before setting up the development environment, ensure you have the following installed:

.NET 6.0 SDK or later
Visual Studio 2022 or Visual Studio Code
SQL Server (Express or Developer edition)
SQL Server Management Studio (optional, for database management)

Development Environment Setup
1. Clone the Repository
bashgit clone https://github.com/ilyaas2004/Prog7311-poe-Agri-energy-connect-web-app
cd agri-energy-connect
2. Restore NuGet Packages
Open the project in Visual Studio and restore NuGet packages, or use the command line:
bashdotnet restore
3. Update Connection String
Open the appsettings.json file and update the connection string to point to your SQL Server instance:
json"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AgriEnergyConnectDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Building and Running the Application
Using Visual Studio

Open the solution file (Prog7311_poe.sln) in Visual Studio.
Build the solution by pressing F6 or selecting Build > Build Solution.
Press F5 or click the "Play" button to run the application.

Using Command Line
bash# Build the project
dotnet build

# Run the application
dotnet run --project Prog7311_poe
The application will start and be available at https://localhost:7123 (or a similar port).
Database Configuration
The application uses Entity Framework Core Code-First approach. The database will be created automatically when you first run the application.
Manual Database Setup (if needed)
If you prefer to set up the database manually:

Create a new database named AgriEnergyConnectDB in SQL Server.
Run the following command to apply migrations:

bashdotnet ef database update
Demo Accounts
The system comes with pre-configured demo accounts:
Employee Account:

Username: employee1
Password: password123

Farmer Account:

Username: farmer1
Password: password123

Usage Guide
Employee Workflows

Adding a New Farmer:

Log in as an employee
Navigate to "Add Farmer" in the navigation bar
Fill in the farmer details and login credentials
Submit the form


Viewing Farmers:

Navigate to "View Farmers" in the navigation bar
Browse the list of all registered farmers


Filtering Products:

Navigate to "View Products"
Use the filter panel to search by farmer, category, or date range
Apply filters to find specific products



Farmer Workflows

Adding a New Product:

Log in as a farmer
Navigate to "Add Product" in the navigation bar
Fill in the product details (name, category, production date)
Submit the form


Viewing Your Products:

Navigate to "My Dashboard"
View all your products listed in the table





Backend:

ASP.NET Core 6.0 MVC
Entity Framework Core
SQL Server
C# Programming Language


Frontend:

HTML5, CSS3, JavaScript
Bootstrap 5
Animate.css
Font Awesome & Bootstrap Icons
jQuery


Security:

Cookie Authentication
Role-Based Authorization
CSRF Protection
Input Validation
