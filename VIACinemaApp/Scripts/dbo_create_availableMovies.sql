CREATE TABLE [dbo].[AvailableMovies] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [AvailableDate]  DATETIME2 (7) NOT NULL,
    [AvailableSeats] INT           NOT NULL,
    [MovieId]        INT           NOT NULL,
    CONSTRAINT [PK_AvailableMovies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AvailableMovies_Movie_Id] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movie] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AvailableMovies_Id]
    ON [dbo].[AvailableMovies]([MovieId] ASC);

