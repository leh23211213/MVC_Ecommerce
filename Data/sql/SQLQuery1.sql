CREATE DATABASE Ecommerce;
USE Ecommerce;
CREATE TABLE Products (
    ProductId VARCHAR(50) PRIMARY KEY,
    Name NVARCHAR(100),
    Description NVARCHAR(MAX),
    Price MONEY,
    Quantity INT,
    Category NVARCHAR(50),
	ImageUrl NVARCHAR(255)
);
-- Mẫu dữ liệu cho các sản phẩm iPhone từ iPhone 13 đến iPhone 15 với các biến thể mã sản phẩm đầy đủ
INSERT INTO Products (ProductId, Name, Description, Price, Quantity, Category, ImageUrl) 
VALUES 
('IP13B1MB16', 'iPhone 13 (Black, Mini, 16GB)', 'Description of iPhone 13 (Black, Mini, 16GB)', 999.99, 100, 'SmartPhone', 'image_url_of_iphone13_black_mini'),
('IP13B1MB32', 'iPhone 13 (Black, Mini, 32GB)', 'Description of iPhone 13 (Black, Mini, 32GB)', 1099.99, 150, 'SmartPhone', 'image_url_of_iphone13_black_mini'),
('IP13B1TB16', 'iPhone 13 (Black, Thường, 16GB)', 'Description of iPhone 13 (Black, Thường, 16GB)', 999.99, 100, 'SmartPhone', 'image_url_of_iphone13_black_thường'),
('IP13B1TB32', 'iPhone 13 (Black, Thường, 32GB)', 'Description of iPhone 13 (Black, Thường, 32GB)', 1099.99, 150, 'SmartPhone', 'image_url_of_iphone13_black_thường'),
('IP13B1PMB16', 'iPhone 13 (Black, Pro Max, 16GB)', 'Description of iPhone 13 (Black, Pro Max, 16GB)', 999.99, 100, 'SmartPhone', 'image_url_of_iphone13_black_pro_max'),
('IP13B1PMB32', 'iPhone 13 (Black, Pro Max, 32GB)', 'Description of iPhone 13 (Black, Pro Max, 32GB)', 1099.99, 150, 'SmartPhone', 'image_url_of_iphone13_black_pro_max'),
