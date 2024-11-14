# Contacts Management Application - Backend (Web API)
This is the .NET Core Web API for the Contacts Management Application. 
The API provides endpoints to manage contacts with features such as adding, editing, deleting, and retrieving contacts. It supports pagination, searching, and sorting for a seamless experience.

# Restore dependencies:
dotnet restore

# Build the application:
dotnet build

# Run the API:
dotnet run

# Application Features
CRUD Operations: Allows creating, updating, and deleting contacts.
Pagination: Supports paginated results for large datasets.
Error Handling: Built-in error handling for graceful responses to client requests.

# API Endpoints
GET /api/contacts: Get all contacts with pagination.
GET /api/contacts/{id}: Get a contact by ID.
POST /api/contacts: Create a new contact.
PUT /api/contacts/{id}: Update an existing contact.
DELETE /api/contacts/{id}: Delete a contact by ID.

# Folder Structure
Controllers: Contains ContactsController which defines the API endpoints.
Services: Contains ContactService for business logic.
Repositories: Contains ContactRepository to handle data access and file storage.
Models: Defines the Contact model.
Interfaces: Defines interfaces for the services and repository.

# Design Decisions
Service Layer: Encapsulates business logic for ease of testing and separation of concerns.
Repository Pattern: Manages data persistence, enabling flexibility in data storage.
Mock Database: Stores contacts in a JSON file (contacts.json) for simplified data management.
