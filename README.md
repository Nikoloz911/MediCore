# MediCore - Healthcare Medical System API

**MediCore** is a comprehensive healthcare backend system developed using **ASP.NET Core Web API** and **Entity Framework Core**, designed to streamline and manage medical operations. It incorporates advanced backend concepts including role-based access, complex relational data, real-time communication, secure authentication, analytics, and background services.

---

## 🚀 Technologies Used

- ASP.NET Core Web API
- .NET Framework
- Entity Framework Core
- AutoMapper
- FluentValidation
- JWT (JSON Web Tokens) 
- Bcrypt 
- WebSocket
- Hangfire
- SMTP (Email)
- Twilio (SMS)
- LINQ for analytics
- Postman
- Logging: TXT, PDF, Excel

---

## 🏥 System Overview

MediCore is designed to manage a healthcare environment where multiple user roles interact with each other through a well-structured RESTful API.

### User Roles

- Admin
- Doctor
- Nurse
- Patient

Each user has role-based permissions and secure authentication using JWT tokens.

---

## 🧩 Core Features

### Authentication & Authorization

- Role-based access control
- JWT-based secure login
- Password hashing with Bcrypt

### User Management

- Create and manage users with roles: Admin, Doctor, Nurse, Patient
- Each user is linked with respective data like departments, appointments, and records

### Appointment System

- Patients can schedule appointments with doctors
- Doctors are assigned to specific departments

### Medical Records

- Each patient has a Medical Record
- Each medical record may include:
  - Diagnoses
  - Prescriptions
    - Prescription Items
    - Medications

### Lab Results

- Patients can receive lab results
- Each result is linked to a Lab Test

### Messaging (Chat)

- Real-time chat between patients and doctors using WebSocket

---

## 📁 Project Structure

- Properties – Launch settings and project metadata
- Configurations – JWT, Services, Hangfire, and custom application settings
- Controllers – API endpoints organized by feature (Users, Appointments, Records, etc.)
- Core – Shared utilities, constants, and core business logic
- Data – Entity Framework DbContext and data seeders
- DTOs – Data Transfer Objects used in API requests/responses
- Enums – Enum definitions (e.g., UserRoles, AppointmentStatus)
- JWT – Token generation, validation, and JWT helper logic
- Migrations – EF Core migrations for database versioning
- Models – Domain models representing database entities
- Profiles – AutoMapper profiles for mapping between entities and DTOs
- Requests – Specialized request models (e.g., User)
- Service
  - Interfaces – Contracts for service logic (e.g., IUser, IAppointment)
  - Implementations – Business logic implementations for each service
- SMTP – Email service integration for sending notifications and reports
- Twillio – SMS functionality integration using Twilio
- Validators – FluentValidation rules for validating request data and DTOs

---

## 🔐 Security

- JWT Authentication
- Bcrypt password hashing
- Role-based access control
- Sensitive operations are logged and validated

---

## 📡 Communication & Background Services

- WebSocket – Real-time doctor-patient chat
- Hangfire – Background jobs like appointment reminder emails
- SMTP – Sends HTML-styled branded emails with PDFs
- Twilio – Sends appointment SMS reminders to patients

---

## ✅ Validation & Documentation

- FluentValidation for DTO and request validation
- Postman documentation (with folders per controller and testable requests)
- JWT authorization and examples included in Postman

---

## 📊 Logging & Monitoring

- Security logs
- User activity logs
- System exception logs
- Export options:
  - .txt plain text
  - .pdf formatted
  - .xlsx for analysis

