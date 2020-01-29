CREATE TABLE [dbo].[Meal]
(
	[MealId] INT IDENTITY (1, 1) NOT NULL,
	[RecipeId] INT NOT NULL,
	[UserId] NVARCHAR(128) NOT NULL,
	[Servings] INT NOT NULL DEFAULT 1, 
    [DateEaten] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_dbo.Meal_dbo.Reciple_RecipeId] FOREIGN KEY ([RecipeId]) 
        REFERENCES [dbo].[Recipe] ([RecipeId]) ON DELETE CASCADE,
    PRIMARY KEY CLUSTERED ([MealId] ASC)
)
