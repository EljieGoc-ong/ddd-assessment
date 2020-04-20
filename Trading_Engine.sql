DROP DATABASE IF EXISTS Trading_Engine;
CREATE DATABASE Trading_Engine;

USE Trading_Engine;
IF NOT EXISTS
(
    SELECT 1
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Currency]')
          AND type IN ( N'U' )
)
BEGIN
    CREATE TABLE [dbo].[Currency]
    (
        [CurrencyId] [INT] IDENTITY(1, 1) NOT NULL,
        [Name] [NVARCHAR](50) NOT NULL,
        [Ratio] [DECIMAL](18, 2) NOT NULL,
        CONSTRAINT [PK_CurrencyId]
            PRIMARY KEY CLUSTERED ([CurrencyId] ASC) ON [PRIMARY]
    );

END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[User]')
          AND type IN ( N'U' )
)
BEGIN
    CREATE TABLE [dbo].[User]
    (
        [UserId] [INT] IDENTITY(1, 1) NOT NULL,
        [Username] [NVARCHAR](255) NOT NULL,
        CONSTRAINT [PK_UserId]
            PRIMARY KEY CLUSTERED ([UserId] ASC) ON [PRIMARY]
    );

END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Balance]')
          AND type IN ( N'U' )
)
BEGIN
    CREATE TABLE [dbo].[Balance]
    (
        [BalanceId] [INT] IDENTITY(1, 1) NOT NULL,
        [UserId] [INT] NOT NULL,
        [CurrencyId] [INT] NOT NULL,
        [Amount] [DECIMAL](18, 2) NULL,
        CONSTRAINT [PK_Balance]
            PRIMARY KEY CLUSTERED ([BalanceId] ASC) ON [PRIMARY]
    );
END;
GO


INSERT INTO [Currency]
(
    [Name],
    Ratio
)
VALUES
('PhP', 0.02);
INSERT INTO [Currency]
(
    [Name],
    Ratio
)
VALUES
('Euro', 1.5);
INSERT INTO [Currency]
(
    [Name],
    Ratio
)
VALUES
('Usd', 1);

INSERT INTO [User]
(
    Username
)
VALUES
('testUser');
INSERT INTO [Balance]
(
    UserId,
    CurrencyId,
    Amount
)
VALUES
('1', '1', 1000);