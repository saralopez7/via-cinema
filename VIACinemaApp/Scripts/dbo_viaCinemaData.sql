
CREATE DATABASE [viaCinemaData]
GO

USE [viaCinemaData]
GO

/* Movies database entity */
CREATE TABLE [dbo].[Movies] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Director]    NVARCHAR (MAX) NULL,
    [Duration]    FLOAT (53)     NOT NULL,
    [Genre]       INT            NULL,
    [MovieTitle]  NVARCHAR (MAX) NULL,
    [Plot]        NVARCHAR (MAX) NULL,
    [Rating]      INT            NOT NULL,
    [ReleaseDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO


/* Available Movies database entity */
CREATE TABLE [dbo].[AvailableMovies] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [AvailableDate]  DATETIME2 (7) NOT NULL,
    [AvailableSeats] INT           NULL,
    [MovieId]        INT           NOT NULL,
    CONSTRAINT [PK_AvailableMovies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AvailableMovies_Movie_Id] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movies] ([Id])
);
GO

CREATE NONCLUSTERED INDEX [IX_AvailableMovies_Id]
    ON [dbo].[AvailableMovies]([MovieId] ASC);
GO


/* Seats database entity */
CREATE TABLE [dbo].[Seats] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [SeatNumber] INT NULL,
    [MovieId]    INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Seats_MoveId] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[AvailableMovies] ([Id])
);
GO


/* Transactions database entity */
CREATE TABLE [dbo].[Transactions] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [MovieId]    INT            NOT NULL,
    [SeatNumber] NVARCHAR (450) NOT NULL,
    [UserId]     NVARCHAR (450) NOT NULL,
    [StartTime]  DATETIME       NOT NULL,
    [Status]     INT            NOT NULL,
    [Price]      DECIMAL (18)   NOT NULL,
    [PaymentId]  INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transaction_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[AvailableMovies] ([Id]),
    CONSTRAINT [FK_Transaction_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Transaction_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[Payments] ([Id])
);
GO


/* Payments database entity */
CREATE TABLE [dbo].[Payments] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [CredictCard]  VARCHAR (55) NOT NULL,
    [ExpiryMonth]  VARCHAR(2)          NOT NULL,
    [ExpiryYear]   VARCHAR(4)          NOT NULL,
    [OwnerSurname] VARCHAR (50) NOT NULL,
    [OwnerName]    VARCHAR (50) NOT NULL,
    [SecurityCode] VARCHAR(3)          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO


/* TransactionsInPayment database entity */
CREATE TABLE [dbo].[TransactionsInPayment] (
    [TransactionId] INT NOT NULL,
    [PaymentId]     INT NULL,
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([TransactionId] ASC),
    CONSTRAINT [FK_TransactionsInPayment_TransactionId] FOREIGN KEY ([TransactionId]) REFERENCES [dbo].[Transactions] ([Id]),
    CONSTRAINT [FK_TransactionsInPayment_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[Payments] ([Id])
);
GO

/******************************************************************************************************************************/
/* ASPNET AUTHENTICATION TABLES */

CREATE TABLE [dbo].[AspNetRoles] (
    [Id]               NVARCHAR (450) NOT NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([NormalizedName] ASC);
GO


CREATE TABLE [dbo].[AspNetRoleClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    [RoleId]     NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId]
    ON [dbo].[AspNetRoleClaims]([RoleId] ASC);
GO

CREATE TABLE [dbo].[AspNetUserTokens] (
    [UserId]        NVARCHAR (450) NOT NULL,
    [LoginProvider] NVARCHAR (450) NOT NULL,
    [Name]          NVARCHAR (450) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC)
);
GO

CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (450)     NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[AspNetUsers]([NormalizedEmail] ASC);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([NormalizedUserName] ASC);
GO


CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (450) NOT NULL,
    [RoleId] NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);
GO


CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider]       NVARCHAR (450) NOT NULL,
    [ProviderKey]         NVARCHAR (450) NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);
GO


CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    [UserId]     NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);
