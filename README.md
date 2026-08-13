# Maarif.az

Maarif.az is a robust Learning Management System (LMS) built to handle educational workflows, user management, and content delivery. The backend is designed with a strict adherence to domain boundaries and maintainability, ensuring the system can scale and adapt without accumulating technical debt.

## 🏗 Architecture

This project strictly follows **Clean/Onion Architecture**, separating business logic from infrastructure and presentation concerns. 

We utilize the **CQRS (Command Query Responsibility Segregation)** pattern to independently scale and optimize read and write operations, preventing complex domain logic from bleeding into data retrieval paths.

### Project Structure

The solution is divided into the following primary layers:

*   **Domain:** Contains enterprise logic, entities, value objects, and domain exceptions. This layer has zero external dependencies.
*   **Application:** Contains business use cases, MediatR handlers (Commands/Queries), DTOs, and interface definitions. It depends only on the Domain.
*   **Infrastructure:** Implements external concerns such as database access, file storage, and third-party integrations.
*   **Presentation (API):** The entry point of the application (ASP.NET Core Web API). Responsible for HTTP routing, request validation, and API versioning.

## ⚙️ Core Technologies

*   **.NET** / ASP.NET Core Web API
*   **Entity Framework Core (EF Core):** For data access and schema migrations.
*   **MediatR:** For implementing the CQRS pattern and decoupled in-process messaging.
*   **ASP.NET Core Identity:** For secure authentication, authorization, and role management.
*   *(Add database provider here, e.g., PostgreSQL, SQL Server)*

## 🚀 Getting Started

### Prerequisites

*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (or current version)
*   *(Your Database Engine)*
*   *(Any other local dependencies like Redis, Docker, etc.)*

### Local Setup

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/Jafarli-Mahammad/Maarif.az.git](https://github.com/Jafarli-Mahammad/Maarif.az.git)
   cd Maarif.az
