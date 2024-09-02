-- Connect to BonvoyageUsersDb database
USE BonvoyageUsersDb;
GO

-- Drop existing tables if they exist
IF OBJECT_ID('Wishlist', 'U') IS NOT NULL DROP TABLE Wishlist;
IF OBJECT_ID('Users', 'U') IS NOT NULL DROP TABLE Users;

-- Create Users Table
CREATE TABLE Users (
    userID VARCHAR(50) PRIMARY KEY,
    userName VARCHAR(100) NOT NULL UNIQUE,
    userEmail VARCHAR(100) NOT NULL UNIQUE,
    userPhone BIGINT NOT NULL UNIQUE,
    userPassword VARCHAR(255) NOT NULL,
    userAddress TEXT NOT NULL,
    userRole VARCHAR(50) NOT NULL
);

-- Insert sample Users data
INSERT INTO Users (userID, userName, userEmail, userPhone, userPassword, userAddress, userRole) VALUES
('U001', 'John Doe', 'john.doe@example.com', 1234567890, 'password123', '123 Main St', 'customer'),
('U002', 'Jane Smith', 'jane.smith@example.com', 9876543210, 'password456', '456 Elm St', 'admin');

-- Create Wishlist Table

CREATE TABLE Wishlist (
    id INT PRIMARY KEY,
    userId INT NOT NULL,
    packageId INT NOT NULL
);
INSERT INTO Wishlist  (id, userId, packageId)
VALUES
    (1, 'U001', 'P101'),
     (2, 'U001', 'P102'),
     (3, 'U002', 'P101'),
     (4, 'U002', 'P102');

-- Connect to BonvoyagePackageDb database
USE BonvoyagePackageDb;
GO

-- Drop existing tables if they exist
IF OBJECT_ID('Packages', 'U') IS NOT NULL DROP TABLE Packages;

-- Create Packages Table
CREATE TABLE Packages (
    packageID VARCHAR(50) PRIMARY KEY,
    packageCountry VARCHAR(100) NOT NULL,
    packageCity VARCHAR(100) NOT NULL,
    packageName VARCHAR(100) NOT NULL UNIQUE,
    packageDesc TEXT NOT NULL,
    packageRating DECIMAL(2, 1) NOT NULL,
    packageReviews INT NOT NULL,
    packagePrice VARCHAR(50) NOT NULL,
    packageDuration VARCHAR(50) NOT NULL,
    minAge VARCHAR(10) NOT NULL,
    maxPeople INT NOT NULL,
    packagePickup VARCHAR(100) NOT NULL,
    availableDate DATE NOT NULL,
    packageLanguage VARCHAR(50) NOT NULL,
    packageIternary NVARCHAR(MAX) NOT NULL,
    packageImage VARCHAR(255) NOT NULL
);

-- Insert sample Packages data
INSERT INTO Packages (packageID, packageCountry, packageCity, packageName, packageDesc, packageRating, packageReviews, packagePrice, packageDuration, minAge, maxPeople, packagePickup, availableDate, packageLanguage, packageIternary, packageImage) VALUES
('P101', 'USA', 'New York', 'NYC Tour', 'A wonderful tour of NYC', 4.5, 150, '$200', '3 Days', '10', 20, 'Hotel Pickup', '2024-07-15', 'English', N'{"Day 1": "Visit Central Park", "Day 2": "Tour of the Statue of Liberty", "Day 3": "Times Square"}', 'nyc.jpg'),
('P102', 'France', 'Paris', 'Paris Lights', 'Explore the beauty of Paris', 4.8, 200, '€300', '5 Days', '12', 15, 'Airport Pickup', '2024-08-01', 'French', N'{"Day 1": "Eiffel Tower", "Day 2": "Louvre Museum", "Day 3": "Seine River Cruise"}', 'paris.jpg');

-- Connect to BonvoyageBookingDb database
USE BonvoyageBookingDb;
GO

-- Drop existing tables if they exist
IF OBJECT_ID('Booking', 'U') IS NOT NULL DROP TABLE Booking;

-- Create Booking Table
CREATE TABLE Booking (
    bookingID VARCHAR(50) PRIMARY KEY,
    userID VARCHAR(50) NOT NULL,
    packageID VARCHAR(50) NOT NULL,
    packageName VARCHAR(100) NOT NULL,
    packageImage VARCHAR(255) NOT NULL,
    bookingPerson INT NOT NULL,
    bookingRooms INT NOT NULL,
    TotalCost INT NOT NULL,
    FOREIGN KEY (userID) REFERENCES BonvoyageUsersDb.Users(userID),
    FOREIGN KEY (packageID) REFERENCES BonvoyagePackageDb.Packages(packageID)
);

-- Insert sample Booking data
INSERT INTO Booking (bookingID, userID, packageID, packageName, packageImage, bookingPerson, bookingRooms, TotalCost) VALUES
('B001', 'U001', 'P101', 'NYC Tour', 'nyc.jpg', 2, 1, 4000),
('B002', 'U002', 'P102', 'Paris Lights', 'paris.jpg', 1, 1, 3000);
