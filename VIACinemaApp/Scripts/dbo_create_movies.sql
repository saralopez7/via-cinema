CREATE TABLE [dbo].[Movie] (
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

