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


