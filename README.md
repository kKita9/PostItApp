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
docker-compose up
```
4. Open a browser and go to [http://localhost:5000/login](http://localhost:5000/login)

Now can log in or register to start using the social media app.

## Technologies
* Frontend: Blazor
* APIs: ASP.NET Core
* Database: SQL Server Management
* Containerization: Docker and Docker Compose
