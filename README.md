# RPG Character API

### This project is a RESTful API for managing RPG characters. It allows you to perform various CRUD operations on RPG characters, including adding, updating, deleting, and retrieving character information

## Prerequisites

Before running the application, ensure you have the following prerequisites installed:

- .NET SDK (at least version 7.0)
- Docker (for database containerization)
- Docker Compose (for managing multi-container applications)
- Dotnet-ef (Tool to manage migrations on the database) command: `dotnet tool install --global dotnet-ef`

## Getting Started

Clone this repository to your local machine:

```asgl
    git clone git@github.com:vagnerpelais/.net-rpg.git
    cd .net-rpg
```

Build the Docker container for the PostgreSQL database:

```asgl
    docker-compose up -d
```

Run database migrations to set up the database schema:

```asgl
    dotnet ef database update --project net-rpg.Data
```

Start the API application:

```asgl
    dotnet watch run
```

## The API will be accessible at `http://localhost:5271/swagger/index.html`

## API Endpoints

- `GET` /api/characters/get

Get a list of all characters.

- `GET` /api/characters/get/{id}

Get details of a single character by ID.

- `POST` /api/characters/create

Add a new character.

- `PUT` /api/characters/update

Update an existing character.

- `DELETE` /api/characters/delete/{id}

Delete a character by ID.

- `POST` /api/auth/register

Register a new user

- `POST` /api/auth/login

Verify the username and password and returns a token

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- AutoMapper
- PostgreSQL
- Docker
- Swagger (OpenAPI)


  Adding token auth soon....
