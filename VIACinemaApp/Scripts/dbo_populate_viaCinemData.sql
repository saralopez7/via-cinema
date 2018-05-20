
--- Create view to return a random value on the functions bellow.
CREATE VIEW getRANDValue
AS
SELECT RAND() AS Value
GO

---- Generate movie realease date.
CREATE FUNCTION get_random_release_date ()  
RETURNS Date  
AS  
BEGIN  
   declare @DateStart datetime, @DateEnd datetime
	set @DateStart = getdate()
	set @DateEnd = DATEFROMPARTS(2018, 01, 01)
	return (SELECT DateAdd(Day, (SELECT Value FROM getRANDValue) * DateDiff(Day, @DateStart, @DateEnd), @DateStart));
	END;
GO

--- Generate movie duration
CREATE   FUNCTION get_random_duration ()  
RETURNS decimal
AS  
BEGIN  
DECLARE @Upper int, @Lower decimal
SET @Lower = 1 ---- The lowest random number
SET @Upper = 4 ---- The highest random number
return (SELECT ROUND(((@Upper - @Lower - 1) *  (SELECT Value FROM getRANDValue) + @Lower), 0));
end;
GO

--- Create Movies 
declare @Index int, @TableLength int
	set @Index = 0
	set @TableLength = (SELECT COUNT(*) FROM dbo.movie_title)
	while @Index <= @TableLength
	begin
	set @Index = @Index + 1

	declare @Director NVARCHAR(MAX), @Duration decimal, @Genre int, @MovieTitle NVARCHAR(MAX),
			@Rating int, @Plot NVARCHAR(MAX), @DATE date

		set @Director = (SELECT director from dbo.director where id = @Index)
		set @Duration = (SELECT dbo.get_random_duration())
		set @Genre = (SELECT ROUND(((5 - 1) * (SELECT Value from getRANDValue) + 0), 0))
		set @MovieTitle = (SELECT title from dbo.movie_title where id = @Index)
		set @Plot = (SELECT plot from dbo.plot where id = @Index)
		set @Rating = (SELECT ROUND(((6 - 1) * (SELECT Value from getRANDValue) + 0), 0))
		set @Date = (SELECT dbo.get_random_release_date())

		INSERT INTO dbo.Movies (Director, Duration, Genre, MovieTitle, Plot, Rating, ReleaseDate) VALUES(@Director,
													@Duration,
													@Genre,
													@MovieTitle,
													@Plot,
													@Rating,
													@Date);
	end;
GO
---------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------ Available Movies ---------------------------------------------------------------

---- Generate random date within the current date and the next 7 days. Used to create an available movie. 
CREATE FUNCTION get_random_date ()  
RETURNS Date  
AS  
BEGIN  
   declare @DateStart datetime, @DateEnd datetime
	set @DateStart = getdate()
	set @DateEnd = DateAdd(day, 8, getDate())
	return (SELECT DateAdd(Day, (SELECT Value FROM getRANDValue) * DateDiff(Day, @DateStart, @DateEnd), @DateStart));
	END;
GO

--- Create view to return a random index on the get_random_index function.
CREATE VIEW getRANDIndex
AS
SELECT NEWID() AS Value
GO

--- Get random index from dbo.Movies entity. Used to asssociate a movie to an available movie. 
CREATE OR ALTER FUNCTION get_random_index ()  
RETURNS decimal
AS  
BEGIN  
return (SELECT top 1 percent id from dbo.Movies Order by (SELECT Value FROM getRANDIndex));
end;
GO

--- Create 50 Available Movies associated to a random movie. 
declare @Index int, @TableLength int, @MovieId int
	set @Index = 0
	set @TableLength = 50
	while @Index <= @TableLength
	begin
	set @Index = @Index + 1
	set @MovieId = (SELECT dbo.get_random_index())

	declare @AvailableDate date
		set @AvailableDate = (SELECT dbo.get_random_date())

		INSERT INTO dbo.AvailableMovies (AvailableDate, MovieId) VALUES(@AvailableDate, @MovieId);
	end;
GO