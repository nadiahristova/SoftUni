INSERT INTO WorkHours(
	EmpLoyeeID, [Date], Task, [Hours])
VALUES (55, GETDATE(), 'Work', 8),
(55, DATEADD(day, -16, GETDATE()), 'Project-556', 256),
(55, CAST('2014-11-30' AS DATE), 'Work', 88)

UPDATE WorkHours 
SET [Hours] = 118
WHERE Id = 1

DELETE FROM WorkHours
WHERE Id = 3

