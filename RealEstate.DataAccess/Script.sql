Drop table Users;
Drop table Company;
Select *
from Users;
Select *
from Users
where Id = 2;

--1
CREATE TABLE Users
(
    Id          int Primary Key Identity (1,1),
    LastName    varchar(55),
    FirstName   varchar(55),
    Email       varchar(55),
    Password    varchar(55),
    PhoneNumber varchar(10),
    Role        int,
    CompanyId   int Null,
    Foreign Key (CompanyId) References Company (Id)
);
--2
CREATE TABLE Company
(
    Id          int Primary Key Identity (1,1),
    CompanyName varchar(55),
    CUI         varchar(55), --companyNumber??
);
--3
CREATE TABLE Announcement
(
    Id        int Primary Key,
    StartDate DateTime,
    EndDate   DateTime,
);
--4
CREATE TABLE Users_Announcement
(
    AnnouncementId int Foreign Key References Announcement (Id),
    UserId         int Foreign Key References Users (Id),
);
