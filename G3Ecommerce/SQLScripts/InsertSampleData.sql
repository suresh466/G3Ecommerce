-- Insert data into Admins table
INSERT INTO Admins (admin_id, username, password, email)
VALUES 
    (1, 'admin', 'admin123', 'admin@example.com');

-- Insert data into Categories table
INSERT INTO Categories (category_id, category_name)
VALUES 
    (1, 'Desserts'),
    (2, 'Starter'),
    (3, 'Drinks');

-- Insert data into Items table
INSERT INTO Items (item_id, item_name, item_price, category_id)
VALUES 
    (1, 'Teramesu', 19.99, 1),
    (2, 'Cake', 499.99, 2),
    (3, 'Coke', 9.99, 3);

-- Insert data into Customers table
INSERT INTO Customers (customer_id, customer_name, customer_email, customer_address, customer_phone)
VALUES 
    (1, 'John Doe', 'john@example.com', '123 Main St', '555-1234'),
    (2, 'Jane Smith', 'jane@example.com', '456 Elm St', '555-5678');

-- Insert data into Orders table
INSERT INTO Orders (order_id, customer_id, total_amount, order_status, payment_method, delivery_address)
VALUES 
    (1, 1, 39.98, 'IN_CART', 'COD', '123 Main St'),
    (2, 2, 509.98, 'SHIPPED', 'PayPal', '456 Elm St');

-- Insert data into OrderItems table
INSERT INTO OrderItems (order_item_id, order_id, item_id, quantity, total_price)
VALUES 
    (1, 1, 1, 2, 39.98),
    (2, 2, 2, 1, 499.99);
