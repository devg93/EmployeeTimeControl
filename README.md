# Employee Time Control System

This project is designed to manage employee work time, including break periods, clock-in/clock-out times, and work schedules. The system tracks when an employee logs in, when they take breaks, and when they log out, helping companies manage work hours efficiently. It integrates with multiple interfaces (API and Web) to collect and display data for employees and management.
 
 
 # System Architecture Overview

The system architecture consists of several key modules:
1. **Break Management**: This module handles break times. It calculates when an employee takes a break and tracks the duration of the break.
2. **Time Management**: This module tracks employee clock-in and clock-out times, calculating the total work time and ensuring that the employee is paid for the correct duration.
3. **Work Schedule Management**: This module manages the work schedules of employees, making sure that each employee has an assigned shift.
4. **Database (DAL)**: Stores the data about employees, their work schedules, breaks, and time logs.

# Architecture Diagram

- **Break, TimeInTimeOut -> Core Infrastructure**: 
  This part of the system manages break and clock-in/clock-out functionalities. Break times and work times are tracked and validated before being sent to the database.
  
- **Database Interaction (DAL)**: 
  Data Access Layer (DAL) is responsible for interacting with the database, storing the time logs, break durations, and schedules.

- **WorkSchedule Module**:
  This module interacts with the work schedule database and manages shift information for each employee. It validates that shifts are completed on time.

# Diagram Overview:
![image](https://github.com/user-attachments/assets/0de3e5f8-0bb7-4fd1-8815-3aeb6e66f073)


# System Architecture Overview - Engineering Approaches
### **System Architecture Overview - Engineering Approaches**
![image](https://github.com/user-attachments/assets/4f7db9bc-c5a0-409c-bf80-2a8265c2e588)

This architecture diagram describes an **event-driven architecture** (EDA) and implements **data replication**, **caching**, **message queues**, and **database interaction**. The system is designed for **scalability, high availability**, and efficient **data processing** with minimal latency.

Here are the main **engineering approaches** used in this system:

---

### **1. Event-Driven Architecture (EDA)**
The system is built around an **event-driven architecture** where data changes or operations are propagated across various components through messages. 

- **Kafka** is used as the **message broker**, allowing components to publish and consume events asynchronously. This ensures that **decoupling** between services is achieved, allowing for easier scalability and management.
  
  - **Kafka Topics**: Events such as user actions or database changes are published to Kafka topics. These messages are consumed by different services (like Redis, PostgreSQL, or MongoDB) to trigger various actions, such as data processing or caching.

---

### **2. Data Replication and Redundancy**
The architecture supports **data replication** using **PostgreSQL** as the primary database and **Redis** for caching. It utilizes both **physical and logical replication** techniques:

- **PostgreSQL Replica-Slave User**: A read-only **replica** of the primary PostgreSQL database is maintained for high availability and load balancing. This ensures the system can handle high traffic by distributing read queries to the replica.
- **Write Redis**: Data that needs to be written to databases (PostgreSQL or MongoDB) is first written to **Redis** for faster processing. Redis caches the writes and serves as a buffer before the data is flushed to the persistent storage (PostgreSQL or MongoDB).

#### **Replication Approach**:
- **Physical Replication**: The replica database uses **physical replication**, where the entire database is mirrored. This is managed through **PostgreSQL's replication slots**.
- **Logical Replication**: For applications requiring finer control over what data to replicate, **logical replication** is used for specific tables or data sets.

---

### **3. Caching with Redis**
**Redis** is a central piece of the architecture, used to provide **caching** and optimize performance:

- **Read Redis**: This is used for fast data retrieval, where frequently accessed data (such as user data) is cached in memory. This reduces the load on the database and speeds up response times.
- **Write Redis**: Data is first written to **Redis** before being written to the main database. This helps with **write-heavy operations** and ensures that changes are processed quickly.

Redis also works in conjunction with **MongoDB** for **caching unstructured data**. It ensures that commonly queried data is retrieved from the cache instead of hitting the database repeatedly.

---

### **4. Database Interaction and Data Storage**
The system uses multiple databases for different types of data storage:

- **PostgreSQL** is the primary relational database that stores structured data, such as user information, work schedules, and time logs.
  
  - **PostgreSQL Replica**: As mentioned, the **replica** is used for load balancing and **read scalability**. This allows read queries to be offloaded from the primary database to avoid overloading the system.
  
- **MongoDB**: Used for storing **unstructured or semi-structured data**. For example, logs, documents, or data that doesn't require a strict relational schema.
  
  - MongoDB is also integrated with **Redis** to cache frequently accessed data for faster retrieval.

---

### **5. Fault Tolerance and Scalability**
- The architecture ensures **high availability** through **replication** (both logical and physical) and **caching** mechanisms (via Redis).
- **Kafka** enables **asynchronous processing**, ensuring that different services can scale independently without affecting the performance of the entire system.
  
  - **Kafka Topics** ensure that data is transmitted between services in an event-driven manner. This means that if one component goes down or is delayed, other components can continue processing asynchronously without being blocked.

---

### **6. User Interactions**
The system allows interaction with both **APIs** and **Web** interfaces:

- **User API**: Allows clients (e.g., mobile applications or other services) to interact with the system. It facilitates user operations such as clocking in/out, taking breaks, etc.
  
  - **User Core**: Handles business logic related to user data. It interacts with PostgreSQL, Redis, and MongoDB as needed.

---

### **7. Microservices and Modularity**
The system is modular, with clear separation between different components. Each module handles specific functionality:

- **Break, TimeInTimeOut Modules**: Handle break times and time in/out tracking. These modules perform calculations and interactions with the database.
- **WorkSchedule Module**: Manages employees' schedules and validates the working hours.
- **MySQL Binlog Replicator**: This service monitors MySQL binlogs and ensures data replication between MySQL and PostgreSQL, as well as syncing with Redis.

This modular approach allows for easy maintenance, scalability, and the addition of new features as required.

---

### **Conclusion**

This architecture follows modern engineering principles to ensure **scalability, performance**, and **reliability** in handling employee time management. By using **event-driven architecture**, **caching**, **replication**, and **message queues**, the system is able to efficiently manage large amounts of user data, ensuring the platform can scale as the number of users increases.

Let me know if you need further elaboration or adjustments on any part of the architecture!
