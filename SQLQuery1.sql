CREATE DATABASE SaleDB
GO

USE SaleDB
GO

CREATE TABLE Products 
(
	ProductID INT PRIMARY KEY ,
	ProductName VARCHAR(50) NOT NULL,
	UnitPrice decimal NOT NULL,
	Quantity INT NOT NULL
)
GO