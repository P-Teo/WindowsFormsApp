# Library Management System (Windows Forms C#)

## Overview
This project is a **Library Management System** built using **C# Windows Forms** and **SQL Server LocalDB**.  
It provides a simple, user-friendly interface to manage library operations including **products (books)**, **customers**, **categories**, **users**, and **orders**.

The system supports:

- User authentication with login functionality.
- CRUD (Create, Read, Update, Delete) operations for books, customers, categories, users, and orders.
- Dynamic loading of child forms for better navigation.
- Real-time search functionality.
- Password visibility toggle and input clearing in login.
- Order management with stock adjustment on deletion.

---


## Database

The project uses **SQL Server LocalDB**.  

Tables:

tbUser – Stores user login credentials and full names.

tbProduct – Stores product (book) details including quantity and price.

tbCustomer – Stores customer details.

tbCategory – Stores product categories.

tbOrder – Stores order details with product, customer, quantity, and total price.

---
## Features

### Login
- Enter username and password.  
- Toggle password visibility.  
- Clear inputs.  
- Access granted leads to the main dashboard.  
<img src="https://github.com/user-attachments/assets/0fd149c0-6151-44c1-93e9-476d835382b7" width="206" />

### Main Dashboard
- Navigation buttons for Products, Customers, Categories, Users, Orders.  
- Child forms load dynamically within the main panel.  
<img src="https://github.com/user-attachments/assets/4f94a74e-9d94-470c-a8e5-73396da6b2c1" width="400" />

### Product Management
- Add, update, delete products.  
- Search products dynamically.  

| ![Product1](https://github.com/user-attachments/assets/d3fee414-3a2f-4798-b077-c13fa4f86bff) | ![Product2](https://github.com/user-attachments/assets/fedbc95b-6f75-4b68-9080-2c21f5a33a0b) |
|---|---|

### Customer Management
- Add, update, delete customers.  
- Search customers dynamically.  

| ![Customer1](https://github.com/user-attachments/assets/2552df47-0bfb-4708-bb1c-563e262cb6bd) | ![Customer2](https://github.com/user-attachments/assets/47566fb8-5e71-4474-ac6b-6f15f5caff50) |
|---|---|

### Category Management
- Add, update, delete categories.  
- Search categories dynamically.  

| ![Category1](https://github.com/user-attachments/assets/ecbc052e-633d-46c2-9ee1-4be228ac462d) | ![Category2](https://github.com/user-attachments/assets/ab522a6b-6b30-40d6-bc36-1abdf0cb0f67) |
|---|---|

### User Management
- Add, update, delete users.  
- Manage login credentials.  

| ![User1](https://github.com/user-attachments/assets/bf9387db-221e-4d3a-acc2-75af38ddf3d0) | ![User2](https://github.com/user-attachments/assets/505a5065-522a-4d72-a8bf-ff72359b777a) |
|---|---|

### Order Management
- Add orders via `OrderModuleForm`.  
- View all orders in a DataGridView.  
- Search orders dynamically.  
- Delete orders updates product stock automatically.  

| ![Order1](https://github.com/user-attachments/assets/1edddd19-2012-4e02-ada5-a237836ae3bc) | ![Order2](https://github.com/user-attachments/assets/8eaf8fec-08af-4371-8f79-463c516b25fc) |
|---|---|



---

## How to Run

Clone the repository.

Open WindowsFormsApp.sln in Visual Studio 2022 (or later).

Ensure the database file (dbLMS.mdf) exists at the path in connection string or update the connection string in LoginForm.cs.

Build and run the project (F5).

Login using a valid username/password from tbUser (ex. user:teo, password:1234)

---

## Dependencies

.NET Framework 4.7.2 (or compatible Windows Forms version)

SQL Server LocalDB (comes with Visual Studio)

No external NuGet packages required.


---

## 🧾 License
This project is for learning purposes only.

