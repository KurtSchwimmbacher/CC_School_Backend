![GitHub-CC-Banner-backend](https://github.com/user-attachments/assets/47421c40-aaeb-4178-8195-bccae994c212)
---
<h4 align="center"> A cross-platform desktop school management system</h4>

--- 
<h1 align="center">Code & Cloud School</h1>
<h4 align="center">Backend Repo • <a href="https://github.com/Mwape-Kurete/CC-School-Frontend">Frontend Repo</a></h4>
<details>
<summary>📑 <strong>Table of Contents</strong> (Click to expand)</summary>

1. [**About The Project**](#about-the-project)  
   ↳ 1.1 [Project Description](#11-project-description)  
   ↳ 1.2 [Built With](#12-built-with)  
2. [**Getting Started**](#getting-started)  
   ↳ 2.1 [Prerequisites](#21-prerequisites)  
   ↳ 2.2 [How to Install](#22-how-to-install)  
3. [**Features & Usage**](#features--usage)  
4. [**Demonstration**](#demonstration)  
5. [**Architecture / System Design**](#architecture--system-design)  
6. [**Unit Testing & User Testing**](#unit-testing--user-testing)  
7. [**Highlights & Challenges**](#highlights--challenges)  
8. [**Roadmap & Future Implementations**](#roadmap--future-implementations)  
9. [**Contributing & Licenses**](#contributing--licenses)  
10. [**Authors & Contact Info**](#authors--contact-info)  
11. [**Acknowledgements**](#acknowledgements)  
</details>

<hr style="height:2px; border:none; background:#F0F1A5;">

## About The Project   
A Modern, Cross-Platform School Management System


### 1.1 Project Description 
Our system is a .NET Electron-powered desktop application designed to streamline administrative workflows, enhance academic coordination, and improve communication for Cloud & Code Academy. Built with Vue.js for a responsive frontend and Electron for seamless cross-platform compatibility (Windows, macOS, Linux), it centralises critical operations into one intuitive interface:
 
 * Student Management: Simplified enrolment
 * Academic Tools: Assignment creation
 * Dynamic Scheduling: Timetable generation
 * Real-Time Communication: Announcement broadcasting
 * Course Administration: Effortless course creation, updates, and curriculum adjustments.

By unifying these features, the system reduces manual overhead, minimises errors, and empowers educators to focus on teaching, while ensuring students and administrators stay synchronised.


### 1.2 Built With 

**Frontend**  
![Vue.js](https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D) ![Pinia](https://img.shields.io/badge/Pinia-FFD02F?style=for-the-badge&logo=pinia&logoColor=000) ![Composition API](https://img.shields.io/badge/Composition_API-999999?style=for-the-badge)

**Backend**  
![.NET Core](https://img.shields.io/badge/.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![REST API](https://img.shields.io/badge/REST_API-FF5733?style=for-the-badge) ![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

**Desktop**  
![Electron](https://img.shields.io/badge/Electron-47848F?style=for-the-badge&logo=electron&logoColor=white) ![Cross-Platform](https://img.shields.io/badge/Cross_Platform-0178D7?style=for-the-badge&logo=windows11&logoColor=white)

**Database**  
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white) ![Secure](https://img.shields.io/badge/Secure-4EA94B?style=for-the-badge&logo=securityscorecard&logoColor=white)


## Getting Started

### 2.1 Prerequisites

Ensure you have the following installed before running the backend API manually:

- [.NET SDK 7.0+](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Entity Framework CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (`dotnet tool install --global dotnet-ef`)

> ✅ Optional:
> - [pgAdmin](https://www.pgadmin.org/) for database management
> - [Visual Studio](https://visualstudio.microsoft.com/) or [Rider](https://www.jetbrains.com/rider/) for IDE support

---

### 2.2 How to Install (Manual Setup)

1. **Clone the repository**
   ```bash
   git clone https://github.com/KurtSchwimmbacher/CC_School_Backend.git
   cd CC-School-Backend
   ```
2. Configure your database
   * Create a new PostgreSQL database (e.g. schooldb)
   * Update appsettings.json or appsettings.Development.json with your PostgreSQL connection string
3. Restore project dependencies
   ```
   dotnet restore
   ```
4. Apply Entity Framework migrations
   ```
   dotnet ef database update
   ```
5. Run the backend
   ```
   dotnet run
   ```
6. Access the API
   * By default: http://localhost:5000 or https://localhost:5001
## Features & Usage
### **🚀 Core Features by Role**  

#### **👨‍💼 Administrators**  
- **📅 Timetable Management**  
  - Generate conflict-free class timetables and exam schedules.  
  - Automatically resolve room/lecturer clashes.  
- **👩‍🏫 Lecturer Management**  
  - Create and assign lecturers to courses.  
- **🎓 Enrollment Oversight**  
  - Verify/approve student enrollments.  

---

#### **👩‍🏫 Lecturers**  
- **🏫 Class Management**  
  - Upload course materials (PDFs, videos, links).  
- **📝 Assignments & Grading**  
  - Create assignments with deadlines.   
- **📢 Announcements**  
  - Post updates to entire classes or specific students.  

---

#### **🎓 Students**  
- **📚 Enrollment Portal**  
  - Select majors during registration.  
- **🗓️ Schedule Access**  
  - View personalised class timetable.  
- **📤 Assignment Submissions**  
  - Upload files (PDF, DOCX, ZIP).  

---

### **🎨 Usage Highlights**  
- **Timetable**: Avoids overlaps in rooms/lecturers during generation.  
- **Live Edit**: Lecturers can visualise, update, and upload course content with ease  

---

## Demonstration
link to our Demo video -> 

## Architecture / System Design
### System Design Diagram (the layered approach) 
![WhatsApp Image 2025-06-10 at 21 52 58_2b984a57](https://github.com/user-attachments/assets/4105bef9-5f3c-4983-b45a-4f46e27e50b6)


## Unit Testing & User Testing
### ✅ Unit Testing

## Highlights & Challenges
## Roadmap & Future Implementations

### **🌐 Integration Enhancements**  
- **Video Conferencing**: Integrate Google Meet/Zoom for virtual classrooms.  
- **Calendar Sync**: Sync with Google Calendar/iCal for deadline reminders.  

### **👨‍💼 Administrators**  
- **Analytics Dashboard**:  
  - Real-time insights on student performance, course popularity, and resource utilisation.  
- **Automated Teaching Assignments**:  
  - Algorithm-based distribution of teaching loads based on lecturer availability/expertise.  

### **👩‍🏫 Lecturers**  
- **Resource Library**:  
  - Centralised repository for lecture recordings, slides, and reading materials.  
- **Automated Grading**:  
  - AI-assisted grading for quizzes/MCQs (e.g., regex-based answer matching).  
- **Attendance Automation**:  
  - Facial recognition/QR codes for automated attendance marking.  

### **🎓 Students**  
- **Course Recommendations**:  
  - ML-driven suggestions based on past performance/interests.  
- **Smart Planner**:  
  - Unified view of timetables, deadlines, and personal events (with notifications).  
- **Quiz Hosting**:  
  - In-app quiz creation/submission with instant feedback.  

---

## Contributing & Licenses
> This project was developed as part of a university course requirement and is currently private and non-commercial.  
No external contributions are being accepted at this time. 


## Authors & Contact Info
Built with ❤️ by:
- **Brilu Hlongwane**
- **Kurt Schwimmbacher**
- **Mwape Kurete**
- **Ngozi Ojukwu**

  
## Acknowledgements 
Special thanks to:
- **Vue.js** and the open-source community for powerful tools and documentation
- **Microsoft .NET Team** for backend scalability support
- **Code & Cloud Academy** lecturers and students for real-world testing and feedback
- Contributors of [Electron](https://www.electronjs.org/) for enabling seamless cross-platform desktop apps
- [Google Forms](https://forms.google.com) for collecting valuable user insights
