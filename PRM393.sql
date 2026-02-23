-- ==========================================
-- RESET DATABASE
-- ==========================================
DROP DATABASE IF EXISTS BadmintonBooking_PRM393;
GO

CREATE DATABASE BadmintonBooking_PRM393;
GO

USE BadmintonBooking_PRM393;
GO

-- ==========================================
-- USERS
-- ==========================================
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FullName NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(MAX) NOT NULL,
    PhoneNumber VARCHAR(15),

    Role VARCHAR(20) DEFAULT 'Customer',
    IsActive BIT DEFAULT 1,

    IsTwoFactorEnabled BIT DEFAULT 0,
    TwoFactorSecret VARCHAR(100),

    AvatarUrl VARCHAR(MAX), -- thêm ảnh avatar

    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE INDEX IDX_User_Email ON Users(Email);

-- ==========================================
-- SHOPS
-- ==========================================
CREATE TABLE Shops (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ShopName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    Latitude FLOAT,
    Longitude FLOAT
);

-- ẢNH SHOP
CREATE TABLE ShopImages (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ShopId UNIQUEIDENTIFIER NOT NULL,
    ImageUrl VARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (ShopId) REFERENCES Shops(Id) ON DELETE CASCADE
);

CREATE INDEX IDX_ShopImages_Shop ON ShopImages(ShopId);

-- ==========================================
-- COURTS
-- ==========================================
CREATE TABLE Courts (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CourtName NVARCHAR(50) UNIQUE NOT NULL,
    Description NVARCHAR(MAX),
    Status VARCHAR(20) DEFAULT 'Active'
);

-- ẢNH COURT
CREATE TABLE CourtImages (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CourtId UNIQUEIDENTIFIER NOT NULL,
    ImageUrl VARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (CourtId) REFERENCES Courts(Id) ON DELETE CASCADE
);

CREATE INDEX IDX_CourtImages_Court ON CourtImages(CourtId);

-- ==========================================
-- BOOKINGS
-- ==========================================
CREATE TABLE Bookings (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),

    UserId UNIQUEIDENTIFIER NOT NULL,
    CourtId UNIQUEIDENTIFIER NOT NULL,

    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,

    TotalPrice DECIMAL(18,2),

    Status VARCHAR(20) DEFAULT 'Pending',
    IsPaid BIT DEFAULT 0,

    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (CourtId) REFERENCES Courts(Id) ON DELETE CASCADE
);

ALTER TABLE Bookings
ADD CONSTRAINT CK_Booking_Time CHECK (EndTime > StartTime);

CREATE INDEX IDX_Booking_Search
ON Bookings(CourtId, StartTime, EndTime);

-- ==========================================
-- SERVICES
-- ==========================================
CREATE TABLE Services (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ServiceName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Unit NVARCHAR(20),
    StockQuantity INT DEFAULT 0,
    IsActive BIT DEFAULT 1
);

CREATE TABLE BookingServices (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BookingId UNIQUEIDENTIFIER NOT NULL,
    ServiceId UNIQUEIDENTIFIER NOT NULL,
    Quantity INT DEFAULT 1,
    PriceAtBooking DECIMAL(18,2),

    FOREIGN KEY (BookingId) REFERENCES Bookings(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceId) REFERENCES Services(Id)
);

-- ==========================================
-- PRODUCT SYSTEM
-- ==========================================
CREATE TABLE Categories (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CategoryName NVARCHAR(100) NOT NULL
);

CREATE TABLE Products (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CategoryId UNIQUEIDENTIFIER NULL,

    ProductName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    ImageUrl VARCHAR(MAX),

    StockQuantity INT DEFAULT 0,
    IsActive BIT DEFAULT 1,

    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE SET NULL
);

CREATE INDEX IDX_Product_Category
ON Products(CategoryId);

-- ẢNH PRODUCT (multi image)
CREATE TABLE ProductImages (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ProductId UNIQUEIDENTIFIER NOT NULL,
    ImageUrl VARCHAR(MAX) NOT NULL,
    IsThumbnail BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);

CREATE INDEX IDX_ProductImages_Product ON ProductImages(ProductId);

-- ==========================================
-- CART
-- ==========================================
CREATE TABLE Carts (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER UNIQUE,
    UpdatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE CartItems (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CartId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL,
    Quantity INT DEFAULT 1,

    FOREIGN KEY (CartId) REFERENCES Carts(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

CREATE INDEX IDX_CartItems_Cart
ON CartItems(CartId);

-- ==========================================
-- ORDERS
-- ==========================================
CREATE TABLE Orders (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,

    DeliveryAddress NVARCHAR(255),
    DeliveryLatitude FLOAT,
    DeliveryLongitude FLOAT,

    TotalAmount DECIMAL(18,2),

    PaymentMethod VARCHAR(50),
    PaymentStatus VARCHAR(50) DEFAULT 'Unpaid',
    OrderStatus VARCHAR(50) DEFAULT 'Pending',

    OrderDate DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE INDEX IDX_Order_User
ON Orders(UserId);

CREATE TABLE OrderDetails (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    OrderId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,

    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- ==========================================
-- NOTIFICATIONS
-- ==========================================
CREATE TABLE Notifications (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(200),
    Message NVARCHAR(MAX),
    Type VARCHAR(50),
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE INDEX IDX_Notifications_User
ON Notifications(UserId);

-- ==========================================
-- CHAT
-- ==========================================
CREATE TABLE ChatRooms (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    SupportId UNIQUEIDENTIFIER NULL,
    LastMessage NVARCHAR(MAX),
    UpdatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (SupportId) REFERENCES Users(Id)
);

CREATE UNIQUE INDEX UX_Chat_User
ON ChatRooms(UserId);

CREATE TABLE ChatMessages (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ChatRoomId UNIQUEIDENTIFIER NOT NULL,
    SenderId UNIQUEIDENTIFIER NOT NULL,
    MessageText NVARCHAR(MAX),
    ImageUrl VARCHAR(MAX), -- thêm gửi ảnh
    SentAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (ChatRoomId) REFERENCES ChatRooms(Id) ON DELETE CASCADE,
    FOREIGN KEY (SenderId) REFERENCES Users(Id)
);


