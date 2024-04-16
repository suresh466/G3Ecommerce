ALTER TABLE [dbo].[Categories] 
ADD [details] VARCHAR(MAX),
    [picture_url] VARCHAR(MAX);

    -- Update details and imageURL for category with ID 1 (Desserts)
UPDATE Categories
SET details = 'Delicious desserts to satisfy your sweet tooth!',
    picture_url = '/images/desserts.jpg'
WHERE category_id = 1;

-- Update details and imageURL for category with ID 2 (Starter)
UPDATE Categories
SET details = 'Start your meal with our appetizing starters!',
    picture_url = '/images/starter.jpg'
WHERE category_id = 2;

-- Update details and imageURL for category with ID 3 (Drinks)
UPDATE Categories
SET details = 'Quench your thirst with our refreshing drinks selection!',
    picture_url = '/images/drinks.jpg'
WHERE category_id = 3;

ALTER TABLE [dbo].[Items]
ADD [item_description] VARCHAR(MAX) NULL,
    [item_image] VARCHAR(255) NULL;

    UPDATE Items
SET item_description = 'Delicious Japanese dessert',
    item_image = '/images/Items/teramesu.jpg'
WHERE item_id = 1;

-- Update the item_description and item_image for item ID 2
UPDATE Items
SET item_description = 'Rich and creamy cake for any occasion',
    item_image = '/images/Items/cake.jpg'
WHERE item_id = 2;

-- Update the item_description and item_image for item ID 3
UPDATE Items
SET item_description = 'Refreshing soda drink',
    item_image = '/images/Items/coke.jpg'
WHERE item_id = 3;

-- Update the item_description and item_image for item ID 4
UPDATE Items
SET item_description = 'Classic dessert with a tangy twist',
    item_image = '/images/Items/key_lime_pie.jpg'
WHERE item_id = 4;