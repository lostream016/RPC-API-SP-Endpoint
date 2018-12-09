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
