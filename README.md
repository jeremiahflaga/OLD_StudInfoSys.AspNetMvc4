# Simple Student Information System

This is a practice project for ASP.NET MVC4


**NOTE:** To be able to run the project, you need to download and install SQLServer Express 2012 LocalDb from https://www.sqlshack.com/install-microsoft-sql-server-express-localdb/ (the file named `SqlLocalDB.MSI`, the one with a size of 33.0 MB)


## Things I made or used in this project:

1. Repository and Unit of Work patterns

	I created a repository interface and a repository class for each entity type in the model

	I also created a generic RepositoryBase class which contains concrete implementations of the methods that are common in all repository classes - defined in Irepository interface

2. I used StructureMap as my Dependency Injection Container

3. I used Moq to create mocks in my unit tests

4. I defined ViewModels and used them in creating or editing entity objects to prevent overposting attacks
