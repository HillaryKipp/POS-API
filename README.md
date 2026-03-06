# Astrol POS API 🚀

Astrol POS API is a robust, enterprise-grade backend for the Astrol Point of Sale system. Built with C# and ASP.NET Core 8, it follows Clean Architecture principles to ensure scalability, maintainability, and clear separation of concerns.

It is designed to handle multi-tenant retail environments, complete with complex financial accounting logic, inventory management, and external payment integrations (e.g., M-Pesa).

## 🏗️ Architecture

The project is structured using **Clean Architecture**:

*   **`AstrolPOSAPI.Domain`**: Core enterprise logic. Contains entities (e.g., `Item`, `SalesOrder`, `GLAccount`), enums, and base classes (`BaseEntity`, `BaseAuditableEntity`).
*   **`AstrolPOSAPI.Application`**: Business logic and use cases. Implements CQRS using **MediatR**. Contains command/query handlers, interfaces (`IGenericRepository`), **FluentValidation** validators, and **AutoMapper** profiles.
*   **`AstrolPOSAPI.Infrastructure`**: Implementation of external concerns. Includes payment gateway integrations (e.g., M-Pesa API), file upload services, and internal infrastructure like `NoSeriesService`.
*   **`AstrolPOSAPI.Persistence`**: Data access layer. Contains the **Entity Framework Core** `AppDbContext`, database migrations, and the Unit of Work / Generic Repository implementations.
*   **`Astrol_POS_API` (Web API)**: The presentation layer. Exposes RESTful endpoints via Controllers, handles JWT Authentication, global exception handling, rate limiting, and configures Swagger.

## 🛠️ Tech Stack & Patterns

*   **Framework:** .NET 8 / C#
*   **ORM:** Entity Framework Core (SQL Server)
*   **Architecture Patterns:** Clean Architecture, CQRS (MediatR), Repository + Unit of Work
*   **Authentication:** ASP.NET Core Identity with JWT (JSON Web Tokens)
*   **Validation:** FluentValidation
*   **Object Mapping:** AutoMapper
*   **Logging:** Serilog (Console & File sinks)

## 📦 Key Modules

1.  **POS (Point of Sale):** Sales orders, line items, cash drawers, touch screen layouts, and receipt generation.
2.  **Accounting:** General Ledger (GL), ledger entries, charting, and ERP-style grouping (GenPostingSetup).
3.  **Purchasing:** Vendor management, purchase orders, and purchase invoicing.
4.  **Identity:** Users, roles, permissions, OTP management, and company/store hierarchy.
5.  **Core settings:** Number Series generation for sequential document IDs (e.g., receipts, invoices).

## 🚀 Getting Started

### Prerequisites
*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer edition)

### Setup Instructions

1.  **Clone the repository**
2.  **Configure Database Connection:**
    Open `Astrol_POS_API/appsettings.json` (or `appsettings.Development.json`) and update the `DefaultConnection` string to point to your SQL Server instance.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER;Database=AstrolPOSDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
    }
    ```
3.  **Apply Migrations:**
    Open your terminal in the `Astrol_POS_API` directory and run:
    ```bash
    dotnet ef database update --project ../AstrolPOSAPI.Persistence/AstrolPOSAPI.Persistence.csproj
    ```
4.  **Run the API:**
    ```bash
    dotnet run
    ```
    The API will launch on `https://localhost:7082` (or similar).

5.  **View Swagger Docs:**
    Navigate to `https://localhost:<port>` to view the interactive Swagger UI and test endpoints. Note: For secured endpoints, you must first register/login via the `AuthController` and provide the JWT token.

## 🧪 Testing

The solution includes skeleton testing projects:
*   `AstrolPOSAPI.UnitTests`: For testing domain logic and MediatR handlers without database dependencies.
*   `AstrolPOSAPI.IntegrationTests`: Uses `WebApplicationFactory` for testing full HTTP request/response lifecycles against an in-memory or throwaway database.
