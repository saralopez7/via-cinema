CREATE TABLE [dbo].[Seat] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [Row]        INT NOT NULL,
    [Column]     INT NOT NULL,
    [SeatNumber] INT NULL,
    [MovieId]    INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Seat_MoveId] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[AvailableMovies] ([Id])
);

