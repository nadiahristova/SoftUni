SELECT 
	CONVERT(varchar(20), GETDATE(), 104) + ' ' + 
	CONVERT(varchar(max), GETDATE(),114) AS [Current Date]