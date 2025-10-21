# MyPortfolio Backend API

## Overview
This repository contains the **ASP.NET Core 8 backend API** for the **MyPortfolioApp** project.  
It provides RESTful APIs to manage **user profiles, about sections, experience, and projects** for multiple users in a shared database.  
The backend is designed to integrate seamlessly with the **Angular frontend** of the portfolio application.

---

## 🚀 Tech Stack
- **Framework:** ASP.NET Core 8 Web API  
- **Language:** C# (.NET 8)  
- **Database:** SQLite (local development)  
- **ORM:** Entity Framework Core  
- **Documentation:** Swagger / OpenAPI  
- **Version Control:** Git + GitHub  
- **Mapping:** AutoMapper  
---

## 🧱 Project Structure

```
Portfolio.Service/
├── Controller/          # API controllers (Users, Projects, Experience, etc.)
├── Contract/            # Interface definitions (IUserService, IProjectService, etc.)
├── Db/                  # Database context and entity models
│   ├── Models/          # Entity models (User, Project, Experience, AboutMe)
│   └── BaseTimeHandling # Base class for timestamps (CreatedAt, UpdatedAt)
├── Entities/            # Request and Response models
├── Migrations/          # EF Core migrations
├── Profiles/            # AutoMapper configuration
├── Service/             # Implementation of business logic
└── Program.cs           # Entry point of the application
```

---

## 🧩 Features Implemented

### 👤 User Management
- Create, read, update, and delete user profiles.
- Validation with DataAnnotations.
- Clean controller-service separation.

### 🧠 About Me Section
- Stores user’s personal description and skill sets.
- Linked to user through UserId.

### 💼 Experience Management
- Add and manage professional experiences.
- Supports multiple experiences per user.

### 📁 Project Management
- Full CRUD for user projects.
- Linked to the authenticated user.
- Includes fields for description, role, duration, and technologies used.

---

## ⚙️ Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/maheshritgithub/portfolio-backend-service.git
cd MyPortfolio-Backend
```

### 2. Configure Database
By default, SQLite is used for local development.  
You can change the connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=portfolio.db"
}
```

### 4. Run the Application
```bash
dotnet run
```
The API will start at `https://localhost:5001` or `http://localhost:5000`.

### 5. Access Swagger UI
Open your browser and visit:  
👉 `https://localhost:5001/swagger`

---

## 📡 API Endpoints (Summary)

| Endpoint | Method | Description |
|-----------|--------|-------------|
| `/api/users` | GET / POST / PUT / DELETE | Manage users |
| `/api/aboutme` | GET / POST / PUT / DELETE | Manage About Me section |
| `/api/experience` | GET / POST / PUT / DELETE | Manage user experiences |
| `/api/projects` | GET / POST / PUT / DELETE | Manage user projects |

---

## 🧑‍💻 Author
**Mahesh** (Backend-Focused .NET Developer | Angular Enthusiast)  
[LinkedIn Profile](https://www.linkedin.com/in/mahesh-kumar-selvaraj-b866591ab/)

---