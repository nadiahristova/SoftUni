SELECT * INTO ##AGlobalTempTable
FROM EmployeesProjects
 
 DROP TABLE EmployeesProjects
 
 CREATE TABLE EmployeesProjects
  (
   EmployeeID INT FOREIGN KEY REFERENCES Employees(EmployeeID) NOT NULL,
   ProjectID INT FOREIGN KEY REFERENCES Projects(ProjectID) NOT NULL,
  )
 
 INSERT INTO EmployeesProjects
 SELECT * FROM  ##AGlobalTempTable

CREATE TABLE #LocalTempTable(
UserID int,
UserName varchar(50), 
UserAddress varchar(150))

CREATE TABLE ##NewGlobalTempTable(
UserID int,
UserName varchar(50), 
UserAddress varchar(150))