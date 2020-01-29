CREATE TABLE [dbo].[Recipe]
(
	[RecipeId] INT IDENTITY (1, 1) NOT NULL,
	[Name] VARCHAR(100) NOT NULL,
	[Url] NVARCHAR(MAX) NULL,
	[Servings] INT NOT NULL,
	[CaloriesTotal] DECIMAL NOT NULL,
	[CaloriesPerServing] DECIMAL NOT NULL,
	[FatTotal] DECIMAL NOT NULL, 
	[FatPerServing] DECIMAL NOT NULL, 
	[ProteinTotal] DECIMAL NOT NULL, 
	[ProteinPerServing] DECIMAL NOT NULL,
	[NetCarbsTotal] DECIMAL NOT NULL,
	[NetCarbsPerServing] DECIMAL NOT NULL,
    [CreatedBy] NVARCHAR(128) NULL, 
    PRIMARY KEY CLUSTERED ([RecipeId] ASC)

)
