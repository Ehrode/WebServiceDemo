CREATE TABLE [dbo].[StudentCourses]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [IdStudent] INT NOT NULL, 
    [IdCourse] INT NOT NULL, 
    [Registered] DATETIME NOT NULL, 
    CONSTRAINT [FK_StudentCourses_ToStudent] FOREIGN KEY ([IdStudent]) REFERENCES [Student]([Id]),
    CONSTRAINT [FK_StudentCourses_ToCourse] FOREIGN KEY ([IdCourse]) REFERENCES [Course]([Id]), 
    CONSTRAINT [CU_StudentCourses_StudentCourse] UNIQUE (IdStudent, IdCourse)
)
