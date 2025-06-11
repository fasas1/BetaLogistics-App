# 🚚 BetaLogistics‑App



---

## 🛠️ Technology Stack

- ASP.NET Core Web API (likely .NET 8 or 9)
- Entity Framework Core for database access
- SignalR for real-time communication
- AutoMapper for DTO-to-entity mappings
- SQL Server (or local equivalent)


---

## ⚙️ Core Features

- RESTful API endpoints for logistics entities (shipments, vehicles, routes)
- Real-time updates through SignalR hubs in `Hubs/`
- Clean separation of concerns: Controllers, Models, Repositories, Data Access
- EF Core migrations under `Migrations/`
- Central mapping setup in `MappingConfig.cs`
- Sample `.http` file—suggests Postman-like testing scenarios
- Well-organized code structure for maintainability and testability

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 8+]
- SQL Server / LocalDB
- Optional: VS Code or Visual Studio
- (Optional) Postman or HTTP client to test APIs

### Setup & Run

```bash
git clone https://github.com/fasas1/BetaLogistics-App.git
cd BetaLogistics-App

# Apply EF migrations
dotnet ef database update

# Run the API
dotnet run
