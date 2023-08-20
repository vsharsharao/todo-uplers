# To-Do Web API

## Overview
	The application is a simple REST API built using .Net 6 ans sqlite/sql server as the backend database.
	Following are few resources necessary to run this application.
	 - .Net 6 sdk or Docker

## How to run locally
	Clone the project from the git repository
	1. Using .Net sdk
		- Go to todo.server folder
		- Set environment variable to Localhost  $env:ASPNETCORE_ENVIRONMENT = "Localhost" in powershell
		- Open a command prompt or powershell window and execute `dotnet build` to build the binaries
		- Run `dotnet run` to run the project. The server run in port 7020.

	2. Docker
		- Install docker daemon
		- Run docker
		- Go to todo.server folder
		- Build docker image -> docker build -t tdo-server .
		- Run docker container -> docker run 7020:7020 tdo-server

	Open https:\\localhost:7020\swagger or use the postman collection in the project location to test the APIs.


## Database
	The application is configure to run with sqlite locally. However the configuration can be changed to use SQL server in development or production environemt. Use the connection string with Azure Defaults and Managed Identity enabled in the app service.
	EF core is used with code first approach. There are 2 contexts created, one for localhost and dev/stage/prod. 

## Design patters
	As the task is predominantly a CRUD operation, I've used Repository & Unit of work patterns. Built-in Depenedncy Injection pattern is used  for writing a loosely coupled code.

## Deployment
	The code can be deployed to Azure environment assuming the resources are created beforhand. "deploy.yml" files consists of steps to build a docker image, push to container registry and publish to an app service in an app service plan.

## How to make it better
	With limited time, I could't complete the following items.
	- Write unit tests to increase code coverage
	- Write unit tests for negative scenarios
	- Comment each method for more readability
	- Move the static variables to constants file