
# **Employee Time Control System – Simplified Architecture Overview**

The **Employee Time Control System** is a modular platform for managing work hours, breaks, and schedules. It uses **event-driven architecture**, **Redis caching**, and **multi-database integration** to ensure real-time tracking and high scalability.

---

## 🧩 Core Infrastructure (Break, TimeInTimeOut)

![Core Infrastructure] ![image](https://github.com/user-attachments/assets/2416af28-139d-42c5-b4fd-131bb1e94c75)


This internal module handles break time and time-in/time-out functionality. The workflow includes:

1. **Events** like `BreakStarted` or `WorkTimeHit` trigger the flow.
2. **OrchestrationService** manages the event lifecycle.
3. **BrakeTimeHandler** and **TimeValidator** ensure logic accuracy.
4. **PersistenceService** sends validated data to the **DAL (Data Access Layer)**.
5. DAL stores records in **disk storage** (PostgreSQL, MongoDB).

---

## 🌐 Full System Architecture

![image](https://github.com/user-attachments/assets/4e47a8e1-2d21-4ddb-b262-f97519cedc9a)


This diagram represents the **overall distributed system**, including:

- **Kafka** as the event bus (decouples modules and enables async communication).
- **Redis** for both caching (Read Redis) and buffering writes (Write Redis).
- **PostgreSQL & Replica** for structured data (users, logs, shifts).
- **MongoDB** for unstructured or flexible schemas.
- **MySQL Binlog Replicator** keeps legacy MySQL data in sync with modern databases.
- **UserCore** and **WorkScheduleCore** expose business logic via APIs to clients.

---

## ✅ Summary

By combining **EDA, caching, replication**, and **modular design**, the system ensures:
- Fast and consistent data handling  
- Easy scaling and fault isolation  
- Seamless real-time tracking of employee work patterns  

> Ideal for enterprises needing a reliable and modern time-tracking solution.

