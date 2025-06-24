# 🌱 Agri-Energy Connect – PROG7311 POE

Agri-Energy Connect is an ASP.NET Core MVC web application that bridges sustainable agriculture with green energy. It enables farmers to manage their product listings and allows employees to register farmers, view products, and apply advanced filtering tools.

---

## 📋 Table of Contents

- [Features](#features)
- [System Architecture](#system-architecture)
- [User Roles](#user-roles)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [Demo Login Accounts](#demo-login-accounts)
- [Usage Guide](#usage-guide)
- [Technologies Used](#technologies-used)

---

## ✅ Features

- 🔐 Secure login with role-based access
- 👨‍🌾 Farmer and 👨‍💼 Employee user roles
- 🌿 Product management (add, view, filter)
- 🧠 Advanced filtering (by category and date)
- 📱 Responsive UI with Bootstrap 5
- 💾 SQL Server data persistence

---

## 🏗️ System Architecture

- **MVC Pattern** (Model-View-Controller)
- **Entity Framework Core** (code-first)
- **SQL Server** for data storage
- Layered logic with clean separation of concerns

---

## 👥 User Roles

### 👨‍🌾 Farmer
- Log in and manage own products
- Add new agricultural or green energy products
- View product dashboard

### 👨‍💼 Employee
- Register new farmers
- View all farmers and their products
- Filter products by category or date

---

## 🛠️ Prerequisites

Before running the project, install:

- [.NET 6.0 SDK or later](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

---

## ⚙️ Setup Instructions

### 1. **Clone the Repository**


git clone https://github.com/ilyaas2004/Prog7311-poe-Agri-energy-connect-web-app.git
cd Prog7311-poe-Agri-energy-connect-web-app

**Update Connection String**
Edit appsettings.json:


"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AgriEnergyConnectDB;Trusted_Connection=True;"
}
**Set Up the Database**
 Run EF Migration
bash:

dotnet ef database update



**🔑 Demo Login Accounts**
-Employee:	employee1	password123
-Farmer:	farmer1	password123

**🚀 Running the App**
Using Visual Studio
Open Prog7311_poe.sln

Press F5 or click the ▶️ "Run" button

**📘 Usage Guide
👨‍💼 Employee**
Add Farmer: Use the navigation to add a new farmer profile

View Farmers: See a list of all registered farmers

Filter Products: Use filter panel by category or date

**👨‍🌾 Farmer**
Add Product: Add new agricultural or green energy products

View Products: See your own listed products

**💻 Technologies Used
Backend:**
ASP.NET Core 6.0 MVC

Entity Framework Core

C#

SQL Server

**Frontend:**
HTML5, CSS3, JavaScript

Bootstrap 5

Animate.css

jQuery

Font Awesome & Bootstrap Icons

**Security:**
Cookie Authentication

Role-Based Authorization

CSRF Protection

Input Validation



