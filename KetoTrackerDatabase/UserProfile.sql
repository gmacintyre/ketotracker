CREATE TABLE [dbo].[UserProfile]
(
	[UserProfileId] INT IDENTITY (1, 1) NOT NULL,
	[AspNetUserId] NVARCHAR(128) NOT NULL,
	[CaloricIntakePerDay] INT NOT NULL,
	[FatPerDay] INT NOT NULL,
	[ProteinPerDay] INT NOT NULL,
	[NetCarbsPerDay] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserProfileId] ASC)
)
