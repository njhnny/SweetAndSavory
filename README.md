# Sweet And Savory

### By Nick Hennessy

## Technologies Used

* C#
* .NET 5.0
* ASP.NET Core MVC
* HTML 
* Bootstrap
* MySQL Workbench
* Entity Framework Core

## Description
An application for a bakery. Users can create an account, log in, create treats and flavors, and edit the many to many relationships between them.

## Setup and Installation
* Clone this repository and open in your favorite text editor
* Navigate to SweetAndSavory.Solution/SweetAndSavory
* Create the file Factory/appsettings.json and add the following code:
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=nick_hennessy;uid=root;pwd=[your password];"
  }
}
* Replace [your password] with your mysql password
* Run the command dotnet restore to install all necessary packages
* Run the command dotnet ef database update to create the database from the Migrations folder
* In the terminal, run the commands dotnet build, then dotnet run
## Known Bugs
no known bugs
## License
MIT LICENSE
Copyright (c) 2021 Nick Hennessy

## Contact Information
njhnny@gmail.com