CREATE TABLE [dbo].[Seat] (
    [Id]     INT          NOT NULL,
    [Status] VARCHAR (10) NOT NULL,
    [Row]    INT          NOT NULL,
    [Column] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);