# PIMTool Project README

This is my completion of PIMTool project as part of ELCA Bootcamp. This README provides an overview of the project requirements and guidelines.

## Introduction

This project aims to create a Project Information Management (PIM). PIMTool is a web application built with ASP.NET Framework and Angular. It provides a platform for managing projects information which includes creating, updating and deleting projects.

## Prerequisites
Before getting started with PIMTool, make sure you have the following software installed:
- [.NET SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks) (version 7.0.40x)
- [Node.js](https://nodejs.org) (version ^18.10.0)
- [Angular CLI](https://angular.io/cli) (version 16.2.x)

## Getting Started
To run PIMTool locally on your machine, follow these steps:

### Client Side
1. Locate the `client` folder in the project directory
2. Install the frontend dependencies:
```
npm install
```
3. Start the frontend application:
```
ng serve
```
This will launch Angular development server and host the frontend application locally.

4. Open your browser and go to [http://localhost:4200](http://localhost:4200)

### Server side
1. Create a database in SQL server.
2. Locate `PIMTool/appsettings.json` and add your database connection strings:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Your connection strings"
}
```
3. Locate API folder in this case `PIMTool` and run:
```
dotnet ef migrations add YourMigrationName
```
or
```
add-migration YourMigrationName
```
4. Start the backend server:
```
dotnet watch
```
This will start the backend server on `localhost:5001`
You don't have to update or add data to your database manually, i have ensured updating and seeding the database everytime the server starts. You can use swagger to test the API controllers of each entity.

