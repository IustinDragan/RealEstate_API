CREATE TABLE Companies
(
    Id          int Primary Key Identity (1,1),
    CompanyName varchar(55),
    CUI         varchar(55), --companyNumber??
);

CREATE TABLE Users
(
    Id          int Primary Key Identity (1,1),
    LastName    varchar(55),
    FirstName   varchar(55),
    Email       varchar(55),
    Password    varchar(55),
    Username    varchar(255),
    PhoneNumber varchar(10),
    Role        int,
    CompanyId   int Null,
    Foreign Key (CompanyId) References Companies (Id)
);

CREATE TABLE Announcements
( --anunt?
    Id        int Primary Key Identity (1,1),
    Title     varchar(255),
    StartDate DateTime,
    EndDate   DateTime,
    UserId int,
);

CREATE TABLE UsersAnnouncements
(
    AnnouncementId int Foreign Key References Announcements (Id),
    UserId         int Foreign Key References Users (Id),
);

CREATE TABLE Properties
(
    Id                int Primary Key Identity (1,1),
    RoomsNumber       int,
    BathroomsNumber   int,
    LandArea          float,
    HouseUsableArea   float,
    HouseTotalArea    float,
    ConstructionYear  int,
    FloorsTotalNumber int,
    ApartamentFloor   int,
    Elevator          BIT,
    Adress            varchar,--tabela separata
    Details           varchar,
    Price             float,
    PropertyType      int,    -- enum {"house":"1", "land":"2"},
    HouseLandDetails  int,    --enum {"planParter":"1", "planParter + 1" : "2", "planParter+2":"3","TerenVilan":"4", "TerenIntravilan":"5"}
    HeatingSource     int,    --enum {"CentralaComuna":"1", "CentralaProprie" : "2", "Radiator":"3","Soba":"4", "PompaCaldura":"5"}
    Utilities         int,    --enum {"Gaz":"1", "CurentElectric" : "2", "Canalizare":"3","Internet":"4"}
    AnnouncementId    int,
    Foreign key (AnnouncementId) References Announcements (Id)
);

CREATE TABLE Addresses
(
    Id                    int Primary Key Identity (1,1),
    Street                varchar(55),
    StreetNumber          int,
    Country               varchar(55), --Judet
    City                  varchar(55), --oras
    Floors                int,         --etaj
    Scale                 varchar(2),  --scara
    ApartmentNumber      int,         --nr apartamentului
    Locality varchar(55),
    District varchar(55),
    GoogleMapsCoordinates varchar,
    PropertyId            int Foreign Key References Properties (Id)
);
