IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'OfficeReservation')
BEGIN
    CREATE DATABASE OfficeReservation;
END
GO

USE OfficeReservation;
GO

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Password CHAR(64) NOT NULL,
    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);

CREATE TABLE Workstations (
    WorkstationId INT IDENTITY(1,1) PRIMARY KEY,
    Floor INT NOT NULL,
    Zone NVARCHAR(50) NOT NULL,
    HasMonitor BIT NOT NULL,
    HasDockingStation BIT NOT NULL,
    NearWindow BIT NOT NULL,
    NearPrinter BIT NOT NULL
);

CREATE TABLE Reservations (
    ReservationId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    WorkstationId INT NOT NULL,
    ReservationDate DATE NOT NULL,

    CONSTRAINT FK_Reservations_Users FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    CONSTRAINT FK_Reservations_Workstations FOREIGN KEY (WorkstationId) REFERENCES Workstations(WorkstationId) ON DELETE CASCADE
);

CREATE UNIQUE INDEX UX_User_ReservationDate
ON Reservations(UserId, ReservationDate);

CREATE TABLE Favorites (
    FavoriteId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    WorkstationId INT NOT NULL,

    CONSTRAINT FK_Favorites_Users FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    CONSTRAINT FK_Favorites_Workstations FOREIGN KEY (WorkstationId) REFERENCES Workstations(WorkstationId) ON DELETE CASCADE
);

-- Password is 123
INSERT INTO Users (Name, Email, Password) VALUES
('Alice Johnson', 'alice@example.com', 'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3'),
('Bob Smith', 'bob@example.com', 'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3'),
('Charlie Brown', 'charlie@example.com', 'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3'),
('Diana Prince', 'diana@example.com', 'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3'),
('Ethan Hunt', 'ethan@example.com', 'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3');

INSERT INTO Workstations (Floor, Zone, HasMonitor, HasDockingStation, NearWindow, NearPrinter) VALUES
(1, 'A1', 1, 1, 1, 0),
(1, 'A2', 1, 1, 0, 1),
(1, 'A3', 0, 1, 0, 0),
(2, 'B1', 1, 0, 1, 1),
(2, 'B2', 1, 1, 1, 0),
(2, 'B3', 0, 0, 0, 1),
(3, 'C1', 1, 1, 1, 1),
(3, 'C2', 0, 1, 1, 0),
(3, 'C3', 1, 0, 0, 1),
(4, 'D1', 1, 1, 0, 0),
(4, 'D2', 0, 0, 1, 1),
(4, 'D3', 1, 0, 1, 0),
(5, 'E1', 1, 1, 0, 1),
(5, 'E2', 0, 1, 1, 0),
(5, 'E3', 1, 1, 1, 1),
(6, 'F1', 1, 0, 0, 1),
(6, 'F2', 0, 1, 1, 0),
(6, 'F3', 0, 0, 1, 1),
(7, 'G1', 1, 1, 1, 0),
(7, 'G2', 1, 0, 0, 1);
