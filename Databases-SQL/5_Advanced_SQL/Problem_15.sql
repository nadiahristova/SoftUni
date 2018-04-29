CREATE TABLE Users(
	UsernameID int identity,
	Username nvarchar(50) UNIQUE NOT NULL,
	[Password] nvarchar(20),
	FullName nvarchar(50) NOT NULL,
	LastLogin datetime,
	CONSTRAINT PK_Users PRIMARY KEY(UsernameID),
	CONSTRAINT PasswordMINLength CHECK (LEN([Password]) > 4 )
	)

INSERT INTO 
	Users(Username, [Password], FullName, LastLogin)
VALUES 
	('pencho007', 'fluffy1517', 'Pencho Ivanov', GETDATE()),
	('ilianka', 'ILIATHEGREAT', 'Iliankata', GETDATE() - 2),
	('svetlina_n', '1323,n13b1jkb4', 'Svetlina Nikolova', GETDATE())
	
GO