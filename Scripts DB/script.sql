CREATE DATABASE COELSA
USE COELSA

CREATE TABLE CONTACTS(
id INT PRIMARY KEY IDENTITY NOT NULL,
firstName VARCHAR(30),
lastName VARCHAR(30),
company VARCHAR(30),
email VARCHAR(30),
phoneNumber VARCHAR(30)
)

INSERT INTO CONTACTS(firstName, lastName, company, email, phoneNumber) values ('Alan', 'Ramos', 'ECorp', 'alanramos_18@hotmail.com', '1130302121');