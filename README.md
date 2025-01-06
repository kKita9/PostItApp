# PostItApp
PostIt – a social media platform designed for effortless sharing, connecting, and staying updated. 

## Architecture Overview
PostItApp is designed with the following layers:

1. **Frontend**
2. **APIs**: Three independent API layers:
   - **People API**: Manages user-related data.
   - **Identity API**: Handles authentication and authorization.
   - **Post API**: Manages posts.
3. **Database**: A SQL Server database that stores all application data.

---
### Blazor Frontend
Accessible via the following URL:
[http://localhost:5000/login](http://localhost:5000/login)

## Endpoints

### People API
API documentation is available in Swagger at:
[http://localhost:5003/index.html](http://localhost:5003/index.html)

### Identity API
API documentation is available in Swagger at:
[http://localhost:5001/swagger/index.html](http://localhost:5001/swagger/index.html)


### Post API
API documentation is available in Swagger at:
[http://localhost:5002/swagger/index.html](http://localhost:5002/swagger/index.html)

## Database Connection
The application uses a SQL Server database for storing all data. To connect to the database, you can use SQL Server Management Studio (SSMS). Follow these steps:

1. Open SQL Server Management Studio (SSMS).
2. In the "Connect to Server" window:
   - **Server type**: Select `Database Engine`.
   - **Server name**: Enter `127.0.0.1,1433`.
   - **Authentication**: Select `SQL Server Authentication`.
   - **Login**: Use `sa`.
   - **Password**: Use `Password_123#`.
3. Click `Connect` to access the database.

---
## Instalation
To run the project locally, follow these steps:

1. Clone this repository:
```bash
git clone https://github.com/kKita9/PostItApp.git
```
2. Navigate to the project directory
  ```bash
  cd PostItApp
  ```
3. Run the application with Docker Compose:
```bash
docker-compose up --build
```
4. Open a browser and go to [http://localhost:5000/login](http://localhost:5000/login)

## Login Information
You can log in or register to start using the social media app. For testing purposes, you can use the following credentials:

- **Email**: `jan.kowalski@example.com`
- **Password**: `password1`

## Technologies
* Frontend: Blazor
* APIs: ASP.NET Core
* Database: SQL Server Management
* Containerization: Docker and Docker Compose

## Screenshots
![obraz](https://github.com/user-attachments/assets/371f7a24-9ea8-43eb-bf8b-8ab8b5167a88)
![obraz](https://github.com/user-attachments/assets/29f8df46-c8ab-4475-9184-20bfe28c89e1)
![obraz](https://github.com/user-attachments/assets/33324497-b26f-4c9c-bcea-755b03d1e424)
![obraz](https://github.com/user-attachments/assets/418fee1e-6229-4845-8642-0b03fc255411)



