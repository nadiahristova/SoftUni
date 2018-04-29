CREATE TABLE WorkHours(
	Id int IDENTITY,
	EmployeeID int NOT NULL,
	[Date] date,
	Task nvarchar(20),
	[Hours] int,
	Comments nvarchar(30),
	CONSTRAINT PK_WorkHours PRIMARY KEY(Id),
	CONSTRAINT FK_Employees_WorkHours FOREIGN KEY(EmployeeId)
		REFERENCES Employees(EmployeeId)
	)