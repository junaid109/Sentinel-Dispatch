# SentinelDispatch 🚑

> **Mission-Critical Computer-Aided Dispatch (CAD) Lite System**

SentinelDispatch is a high-availability, real-time emergency response coordinator proof-of-concept. It demonstrates a **Microservices Architecture** suitable for public safety software, aligning with strict reliability and real-time requirements.

![Status](https://img.shields.io/badge/Status-Prototype-blue)
![Build](https://img.shields.io/badge/Build-Passing-green)
![License](https://img.shields.io/badge/License-MIT-purple)

## 🌟 Key Features

*   **Microservices Architecture**: Decoupled Incident and Resource services for independent scaling.
*   **Event-Driven (RabbitMQ)**: Uses the **Transactional Outbox Pattern** to guarantee message delivery between services.
*   **Real-Time Dashboard (SignalR)**: zero-refresh updates for dispatchers when incidents occur.
*   **Live GIS Integration (Leaflet)**: Interactive map visualization of active incidents and unit locations.
*   **Infrastructure as Code (.NET Aspire)**: Seamless orchestration of SQL Server, Redis, RabbitMQ, and .NET services.
*   **Smart Dispatching**: Automated sub-second resource allocation based on availability and proximity.

## 🏗️ Technical Stack

- **Framework**: .NET 10 (Preview)
- **Orchestration**: .NET Aspire
- **Frontend**: Blazor WebAssembly
- **Messaging**: MassTransit over RabbitMQ
- **Database**: SQL Server (EF Core) + Redis (Geo-spatial cache)
- **CI/CD**: GitHub Actions

## 🚀 Getting Started

### Prerequisites

*   [.NET 8.0/9.0/10.0 SDK](https://dotnet.microsoft.com/download)
*   [Docker Desktop](https://www.docker.com/products/docker-desktop)
*   [Aspire Workload](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/install)

### Running the Application

1.  Clone the repository:
    ```bash
    git clone https://github.com/junaid109/Sentinel-Dispatch.git
    cd Sentinel-Dispatch
    ```

2.  Run with Aspire:
    ```powershell
    aspire run
    ```

3.  Access the Dashboard:
    *   The Aspire Dashboard will launch automatically.
    *   Click on the **SentinelDispatch.Web** endpoint to view the CAD Interface.

## 🧪 Testing

The solution includes a comprehensive xUnit test suite for shared contracts and domain logic.

```bash
dotnet test
```

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
