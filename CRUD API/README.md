## CRUD REST API

## About the application

Application represents simple CRUD operations thet uses SOLID principles 

## NuGet Packages

* Microsoft.EntityFrameworkCore (5.0.17)
* Microsoft.EntityFrameworkCore.InMemory (5.0.17)

## Testing endpoints with Postman

After runing the aplication go to http://localhost:5000. Thsi endpoints are open to everyone:

* POST		/api/user		- Used to create user
* GET		/api/user		- Used to retrieve all users
* GET		/api/user/id	- Used to retrieve user by id
* DELETE	/api/user/id	- Used to delete user by id

## Testing endpoints with Swagger

## Tech decisions

* Web API written in .NET Core 5.0
* Entity Framework
* AutoMapper
* NUnit

## Project Srtucture

* Authorization - contains JwtUtils for handling JWT authorization and authentication
* AutoMapper - contains Profiles for mapping Models to corresponding DTOs and vise versa
* Controllers - contains Controllers
* DbConnection - contains DBContext and Seed data in memory
* Data Transfer Objects - contains objects needed for CRUD actions
* Helpers - contains helpers
* Middlewares - contains ExceptionHandler and Jwt middleware
* Models - contains core entites
* Repositories - contains actions executed against database
* Services - contains services for communication with repositories
* Shared - contains data which is of use to multiple layers
* Tests - contains unit tests splitted into folders representing layers
