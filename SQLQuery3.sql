/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [ID]
      ,[FastName]
      ,[LastName]
      ,[Email]
      ,[Phone]
      ,[Description]
      ,[Password]
  FROM [CRUD].[dbo].[Student_Information]