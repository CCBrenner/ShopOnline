IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    CREATE TABLE [CartItems] (
        [Id] int NOT NULL IDENTITY,
        [CartId] int NOT NULL,
        [ProductId] int NOT NULL,
        [Qty] int NOT NULL,
        CONSTRAINT [PK_CartItems] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    CREATE TABLE [Carts] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_Carts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    CREATE TABLE [ProductCategories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductCategories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [UserName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [ImageURL] nvarchar(max) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [Qty] int NOT NULL,
        [CategoryId] int NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_ProductCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [ProductCategories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'UserId') AND [object_id] = OBJECT_ID(N'[Carts]'))
        SET IDENTITY_INSERT [Carts] ON;
    EXEC(N'INSERT INTO [Carts] ([Id], [UserId])
    VALUES (1, 1),
    (2, 2)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'UserId') AND [object_id] = OBJECT_ID(N'[Carts]'))
        SET IDENTITY_INSERT [Carts] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ProductCategories]'))
        SET IDENTITY_INSERT [ProductCategories] ON;
    EXEC(N'INSERT INTO [ProductCategories] ([Id], [Name])
    VALUES (1, N''Beauty''),
    (2, N''Furniture''),
    (3, N''Electronics''),
    (4, N''Shoes'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ProductCategories]'))
        SET IDENTITY_INSERT [ProductCategories] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [UserName])
    VALUES (1, N''Bob''),
    (2, N''Sarah'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'Description', N'ImageURL', N'Name', N'Price', N'Qty') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] ON;
    EXEC(N'INSERT INTO [Products] ([Id], [CategoryId], [Description], [ImageURL], [Name], [Price], [Qty])
    VALUES (1, 1, N''A kit provided by Glossier, containing skin care, hair care and makeup products'', N''/Images/Beauty/Beauty1.png'', N''Glossier - Beauty Kit'', 100.0, 100),
    (2, 1, N''A kit provided by Curology, containing skin care products'', N''/Images/Beauty/Beauty2.png'', N''Curology - Skin Care Kit'', 50.0, 45),
    (3, 1, N''A kit provided by Curology, containing skin care products'', N''/Images/Beauty/Beauty3.png'', N''Cocooil - Organic Coconut Oil'', 20.0, 30),
    (4, 1, N''A kit provided by Schwarzkopf, containing skin care and hair care products'', N''/Images/Beauty/Beauty4.png'', N''Schwarzkopf - Hair Care and Skin Care Kit'', 50.0, 60),
    (5, 1, N''Skin Care Kit, containing skin care and hair care products'', N''/Images/Beauty/Beauty5.png'', N''Skin Care Kit'', 30.0, 85),
    (6, 3, N''Air Pods - in-ear wireless headphones'', N''/Images/Electronic/Electronics1.png'', N''Air Pods'', 100.0, 120),
    (7, 3, N''On-ear Golden Headphones - these headphones are not wireless'', N''/Images/Electronic/Electronics2.png'', N''On-ear Golden Headphones'', 40.0, 200),
    (8, 3, N''On-ear Black Headphones - these headphones are not wireless'', N''/Images/Electronic/Electronics3.png'', N''On-ear Black Headphones'', 40.0, 300),
    (9, 3, N''Sennheiser Digital Camera - High quality digital camera provided by Sennheiser - includes tripod'', N''/Images/Electronic/Electronic4.png'', N''Sennheiser Digital Camera with Tripod'', 600.0, 20),
    (10, 3, N''Canon Digital Camera - High quality digital camera provided by Canon'', N''/Images/Electronic/Electronic5.png'', N''Canon Digital Camera'', 500.0, 15),
    (11, 3, N''Gameboy - Provided by Nintendo'', N''/Images/Electronic/technology6.png'', N''Nintendo Gameboy'', 100.0, 60),
    (12, 2, N''Very comfortable black leather office chair'', N''/Images/Furniture/Furniture1.png'', N''Black Leather Office Chair'', 50.0, 212),
    (13, 2, N''Very comfortable pink leather office chair'', N''/Images/Furniture/Furniture2.png'', N''Pink Leather Office Chair'', 50.0, 112),
    (14, 2, N''Very comfortable lounge chair'', N''/Images/Furniture/Furniture3.png'', N''Lounge Chair'', 70.0, 90),
    (15, 2, N''Very comfortable Silver lounge chair'', N''/Images/Furniture/Furniture4.png'', N''Silver Lounge Chair'', 120.0, 95),
    (16, 2, N''White and blue Porcelain Table Lamp'', N''/Images/Furniture/Furniture6.png'', N''Porcelain Table Lamp'', 15.0, 100),
    (17, 2, N''Office Table Lamp'', N''/Images/Furniture/Furniture7.png'', N''Office Table Lamp'', 20.0, 73),
    (18, 4, N''Comfortable Puma Sneakers in most sizes'', N''/Images/Shoes/Shoes1.png'', N''Puma Sneakers'', 100.0, 50),
    (19, 4, N''Colorful trainsers - available in most sizes'', N''/Images/Shoes/Shoes2.png'', N''Colorful Trainers'', 150.0, 60),
    (20, 4, N''Blue Nike Trainers - available in most sizes'', N''/Images/Shoes/Shoes3.png'', N''Blue Nike Trainers'', 200.0, 70),
    (21, 4, N''Colorful Hummel Trainers - available in most sizes'', N''/Images/Shoes/Shoes4.png'', N''Colorful Hummel Trainers'', 120.0, 120),
    (22, 4, N''Red Nike Trainers - available in most sizes'', N''/Images/Shoes/Shoes5.png'', N''Red Nike Trainers'', 200.0, 100),
    (23, 4, N''Birkenstock Sandles - available in most sizes'', N''/Images/Shoes/Shoes6.png'', N''Birkenstock Sandles'', 50.0, 150)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'Description', N'ImageURL', N'Name', N'Price', N'Qty') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221111195356_InitialConfigAndHydration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221111195356_InitialConfigAndHydration', N'6.0.10');
END;
GO

COMMIT;
GO

