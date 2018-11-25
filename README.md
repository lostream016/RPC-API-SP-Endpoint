# RPC-API-SP-Endpoint
This app is an api with RPC style, the app is just a store procedure (SP) executor and the SP do a specific job (Set or Get a values from db in JSON or XML string form).
If you are from an **expert sql background** and want to build API using your SQL background, i think this app might help you figured out the way. 
Fortunately in **SQL Server 2016** or above, **SQL query can read the data inside JSON or XML string form**.

## How it Works
it's pretty simple :
* The client do the request to the API
```
http://localhost:1805/api/SP/Get/GetEmployee?id=6
```
* The API Endpoint in this app is a store procedure (SP) name that to be executed
* So, the syntax when call api is:
```
http://**your domain**/api/**controller**/**action**/**SP Name**?**SP Param 1 = value**&**SP Param 2 = value**&...
```
* The API only throws a value between client and SP, which means the api let the SP do the miracle
* So, if you want to add more specific job to be solved, you only have to create new SP in SQL Server

## Before Run the Project
* Make sure the your DB environment is ready (**this is only just for demo**), run the SQL Query below:
```
CREATE DATABASE Corporation;
GO

USE Corporation;
GO

CREATE TABLE Employee(
[PK_Employee_ID] BIGINT PRIMARY KEY IDENTITY, 
[Name] VARCHAR(100), 
[Age] INT,
[FK_Job_ID] BIGINT, 
[Created_Date] DATETIME, 
[LastUpdate_Date] DATETIME);
GO

CREATE TABLE Job(
[PK_Job_ID] BIGINT PRIMARY KEY IDENTITY,
[Name] VARCHAR(100),
[Desc] VARCHAR(1000),
[Created_Date] DATETIME, 
[LastUpdate_Date] DATETIME);
GO

INSERT INTO Job([Name], [Desc], [Created_Date], [LastUpdate_Date]) 
VALUES
('BackEnd Developer', 'Develop and maintain the backend app using C#, Java, and Python', GETDATE(), GETDATE()), 
('FrontEnd Developer', 'Develop and maintain the frontend app using react.js, angular.js or vue.js', GETDATE(), GETDATE());
GO

INSERT INTO Employee([Name], [Age], [FK_Job_ID], [Created_Date], [LastUpdate_Date]) 
VALUES
('Fadly', 24, 1, GETDATE(), GETDATE()),
('Anna', 27, 2, GETDATE(), GETDATE()),
('John', 35, 1, GETDATE(), GETDATE()),
('Reiner', 21, 1, GETDATE(), GETDATE()),
('Sahsa', 30, 2, GETDATE(), GETDATE());
GO
```
* And for API endpoint example, we add 2 simple SP (Get And Post)
```
USE Corporation;
GO

CREATE PROCEDURE [GetAllEmployee]
AS

BEGIN
	SELECT CAST((
		SELECT * FROM Employee FOR JSON PATH) 
	AS VARCHAR(MAX));
END;

GO


CREATE PROCEDURE [AddEmployee]
@JsonBody AS VARCHAR(MAX)
AS

BEGIN
	DECLARE @compatibility INT
	SELECT @compatibility = compatibility_level from sys.databases WHERE name = 'Corporation';
	IF(@compatibility != 130)
	BEGIN
		ALTER DATABASE ICare SET COMPATIBILITY_LEVEL = 130;
	END

	DECLARE @Name VARCHAR(100),			
			@Age INT,
			@FK_Job_ID BIGINT;
					
	SELECT * 
	INTO #temp_save_employee
	FROM OPENJSON(@JsonBody);
	
	SELECT @Name		  = [value] FROM #temp_save_employee WHERE [key] = 'Name';	
	SELECT @Age			  = [value] FROM #temp_save_employee WHERE [key] = 'Age';
	SELECT @FK_Job_ID	  = [value] FROM #temp_save_employee WHERE [key] = 'FK_Job_ID';

	INSERT INTO Employee([Name], [Age], [FK_Job_ID], [Created_Date], [LastUpdate_Date])
	VALUES (@Name, @Age, @FK_Job_ID, GETDATE(), GETDATE());
END;

GO
```
* Adjust the project settings with your environment <br>
-- Open **Web.config** file <br>
-- Inside **connectionStrings** XML tag <br>
-- Change **data source**=**your server name** , **initial catalog**=**your db name**, And **user id** & **password** to access the database <br>

## It's Time to Try
* Rebuild & Run the project <br>
-- You can use iis express or register your project using iis (local hosting server) to achieve this
* Open Postman desktop app or else and access the SP via API
```
METHOD GET
http://localhost:1805/api/SP/Get/GetAllEmployee
```
```
METHOD POST
http://localhost:1805/api/SP/Get/AddEmployee

BODY JSON
{
	"Name": "Cassandra",
	"Age": 22,
	"FK_Job_ID": "2"
}
```

