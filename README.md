# SentinelDispatch 🚑

> **Mission-Critical Computer-Aided Dispatch (CAD) Lite System**

**SentinelDispatch** is a proof-of-concept for a modern, high-availability emergency response system. 

In the real world, calls to 911 are managed by complex Computer-Aided Dispatch (CAD) software. This project simulates that ecosystem, demonstrating how **Event-Driven Architecture** and **Real-Time Data** can scale to save lives by reducing response times from minutes to seconds.

![Status](https://img.shields.io/badge/Status-Prototype-blue)
![Build](https://img.shields.io/badge/Build-Passing-green)
![License](https://img.shields.io/badge/License-Proprietary-red)

## 📖 Project Overview

Imagine a city with dozens of police units and emergency calls flooding in. 
*   **The Problem**: Without a centralized system, coordinating closest units to incidents is slow, manual, and error-prone.
*   **The Solution**: SentinelDispatch automates this coordination.
    1.  **Ingest**: A dispatcher records an incident (e.g., "Fire at Main St").
    2.  **Orchestrate**: The system instantly identifies the nearest available unit using Geo-spatial data.
    3.  **Dispatch**: The unit is assigned, and the directive is pushed to all screens in milliseconds.
    4.  **Resilience**: Every action is guaranteed. Even if a server crashes mid-process, the **Outbox Pattern** ensures the dispatch commands are never lost.

This is not just a CRUD app; it is a **Mission-Critical System Simulator** designed to meet Public Safety reliability standards.

## 🌟 Key Features

*   **Microservices Architecture**: Decoupled *Incident Service* (Caller API) and *Resource Service* (Unit Management) allow independent scaling.
*   **Event-Driven (RabbitMQ)**: Uses the **Transactional Outbox Pattern** to strictly guarantee message delivery. Data consistency is paramount.
*   **Real-Time Dashboard (SignalR)**: Zero-refresh UI. Dispatchers see new calls and unit movements instantly on their screen.
*   **Live GIS Integration (Leaflet)**: Interactive map visualization of active incidents and real-time unit locations.
*   **Secure Access (JWT)**: Role-based authenticated access prevents unauthorized system usage.
*   **Infrastructure as Code (.NET Aspire)**: Seamless orchestration of SQL Server, Redis, RabbitMQ, and .NET services with a single command.

## 🏗️ Technical Stack

- **Framework**: .NET 10 (Preview)
- **Orchestration**: .NET Aspire
- **Frontend**: Blazor WebAssembly + Bootstrap
- **Messaging**: MassTransit over RabbitMQ
- **Caching & Geo**: Redis
- **Database**: SQL Server (EF Core)
- **Auth**: ASP.NET Core Identity + JWT Bearer
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
    *   Click on the **SentinelDispatch.Web** endpoint.
    *   **Login**: Register a new user to access the secure CAD Interface.

## 🧪 Testing

The solution includes a comprehensive xUnit test suite for shared contracts and domain logic.

```bash
dotnet test
```

## 📜 License & Ownership

**Copyright (c) 2026 Junaid Malik. All Rights Reserved.**

This project and its source code are proprietary. Unauthorized copying, modification, distribution, or use for commercial purposes is strictly prohibited without written permission.

For custom licensing, full enterprise implementation, or consulting inquiries, please contact:

*   **Email**: junaidmalik.rm@gmail.com
*   **Website**: [www.junaidmalik.org](http://www.junaidmalik.org)
