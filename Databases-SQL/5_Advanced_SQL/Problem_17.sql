CREATE TABLE Groups(
	GroupID int identity,
	Name nvarchar(150) UNIQUE,
	CONSTRAINT PK_Groups PRIMARY KEY(GroupID)
	)
GO