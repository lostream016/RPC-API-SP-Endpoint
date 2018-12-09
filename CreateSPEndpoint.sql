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
	SELECT @Age		  = [value] FROM #temp_save_employee WHERE [key] = 'Age';
	SELECT @FK_Job_ID	  = [value] FROM #temp_save_employee WHERE [key] = 'FK_Job_ID';

	INSERT INTO Employee([Name], [Age], [FK_Job_ID], [Created_Date], [LastUpdate_Date])
	VALUES (@Name, @Age, @FK_Job_ID, GETDATE(), GETDATE());
END;

GO
