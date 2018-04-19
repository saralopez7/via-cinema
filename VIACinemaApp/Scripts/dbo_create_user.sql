CREATE TABLE [dbo].[Users] (
    [Id]          INT       IDENTITY(1,1) NOT NULL,
    [username]    NCHAR (10) NOT NULL,
    [password]    NCHAR (16) NOT NULL,
    [email]       NCHAR (10) NOT NULL,
    [phoneNumber] VARCHAR(50)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

