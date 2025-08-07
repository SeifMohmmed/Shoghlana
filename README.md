# Shoghlana 🎨

Welcome to **Shoghlana**, a modern freelancing platform where **clients** can post jobs and hire skilled **freelancers**.  
It enables seamless profile management, job postings, proposals, messaging, and collaboration.

## 🚀 Features

### 🧑‍💼 Client Features
- **Profile Management**: Edit personal info & profile picture 📝  
- **Freelancer Browsing**: View freelancer portfolios, work history & skills 🕵️‍♂️  
- **Job Management**: Post jobs, browse existing ones & manage proposals 📋  
- **Communication**: Accept/reject proposals, send messages, and chat 💬


### 👩‍💻 Freelancer Features
- **Profile Management**: Update personal info, add/edit portfolio & skills 📝  
- **Job Browsing**: Filter and paginate jobs (budget, category, status) 🔍  
- **Proposal Management**: Submit proposals to open jobs, and view proposal status (accepted, rejected, waiting).
- **Communication**: Participate in group chats with other freelancers using a unique group name as the ID. 🗣️
- **Work History**: Successfully completed jobs are added to the freelancer's work history. 📜
- **Image Validation**: Ensure profile and portfolio images meet the required extensions (jpg, jpeg, png) and size (max 1 MB). 🖼️


### 🔄 Real-Time Communication (SignalR)
- **Individual Chat**: One-to-one chat using `IndividualChatHub`  
- **Group Chat**: Room-based communication with `ChatHub`  
- **Instant Notifications**: Notify users in real time with `NotificationHub`  
- **Online Users Tracking**: Dynamically update online/offline status  
- **Private Chat Groups**: Automatically generate unique private group names for two users 
 
## Technologies Used 🚀
- **Backend:** ASP.NET Core 9 Web API, SignalR (real-time communication), C#, LINQ, EF Core.
- **Database:** Microsoft SQL Server.
- **Authentication & Security:** JWT, Refresh Tokens, External Login (OAuth), Role-based Access Control. 
- **Architecture & Patterns:** Clean Architecture, Repository Pattern, Unit of Work Pattern, Dependency Injection, Services.
- **Data Handling:** DTOs, AutoMapper, Pagination, Fluent API.
- **Real-time Features:** SignalR Hubs for Chat (Individual & Group) and Notifications.
- **Image Uploads** — Handled using `IFormFile`, with conversion to byte arrays for database storage.
- **Validation** — File extension and size checks for safety (max 1MB, image-only uploads).
  
## 📸 Screenshots
### 🗂️ Class Diagram
<p align="center">
  <img src="https://github.com/SeifMohmmed/Shoghlana.API/blob/11b6dff6b0c8b6c0ad23858fbcaa4f4129c4e9d8/Screenshots/Shoglana.png" alt="image alt"/>
</p>

<br>

### 🌐 Endpoints

 <p align="center">
  <img src="https://github.com/SeifMohmmed/Shoghlana.API/blob/11b6dff6b0c8b6c0ad23858fbcaa4f4129c4e9d8/Screenshots/Endpoints.png" alt="image alt"/>
</p>

## 🏃‍♂️ Getting Started

### ✅ Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Microsoft SQL Server  
- Visual Studio / VS Code  

### 🚀 Setup
```bash
# Clone the repository
git clone https://github.com/SeifMohmmed/Shoghlana.API.git

# Navigate to the project folder
cd Shoghlana.API

# Update connection string in appsettings.json

# Run migrations
dotnet ef database update

# Start the API
dotnet run

