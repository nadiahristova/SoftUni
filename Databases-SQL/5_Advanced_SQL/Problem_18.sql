USE SoftUni
GO

INSERT INTO Groups(Name) 
	VALUES ('Engineers'),
	('Installation Crew')
GO

ALTER TABLE Users
ADD GroupID int

ALTER TABLE Users
ADD CONSTRAINT FK_Users_Groups 
FOREIGN KEY(GroupID) REFERENCES Groups(GroupID)
GO

