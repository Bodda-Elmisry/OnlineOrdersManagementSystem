Online orders management system is an api system to track customers orders

Technology :-
 - Asp.Net core API 8.0

Architecture :-
 - Clean Code Architecture

Design :-
  - Repository Design Pattern.
  - Unit Of Work Design Pattern.

Dependencis :-
  - Domain layer :-
    * Mapster : to map between entities and dtos.
    * Microsoft.EntityFrameworkCore : to add enitities configration files.
    * Microsoft.EntityFrameworkCore.SqlServer : to map entity to table and schema in configration file.
  - Inferastructure layer :-
    * Microsoft.EntityFrameworkCore : 
    * Microsoft.EntityFrameworkCore.SqlServer :
    * Microsoft.EntityFrameworkCore.Tools : to migrate entites to databas.
  - API layer :-
    * Microsoft.EntityFrameworkCore.Design : to maigration file
   
Instructions to run the project :-
  1- Update connection string in appsetting.json.
  2- Open "Package Manager Consol" from "Tools >> NuGet Package Manager"
  3- From "Default project" dropdown select "OnlineOrderManagementSystem.Inferastructure"
  4- Write command "Update-Database" and click enter

Database Schema :-


 ![image](https://github.com/user-attachments/assets/cc8e8473-314a-4074-bd57-f5309e77bfb6)

 

