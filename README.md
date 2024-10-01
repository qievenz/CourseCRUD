# Course Management Application

This is a course management application built using React for the frontend and ASP.NET Core for the backend, with SQL Server as the database.

## Prerequisites

- [Docker and Docker Compose](https://docs.docker.com/compose/install/)

## Setup Instructions

1. Clone the repository
	```bash
	git clone https://github.com/qievenz/CourseCRUD.git
	cd CourseCRUD
	```
2. Run the application
	```bash
	docker-compose up --build
	```

This will:
- Start SQL Server
- Start the .NET API on http://localhost:5010
- Start the React frontend on http://localhost:3000

3. Access the application
	- [Frontend](http://localhost:3000)
	- [Backend](http://localhost:5010/swagger)