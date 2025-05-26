-- Create the database (run this separately if needed)
-- CREATE DATABASE LearningDashboard;
-- GO

-- USE LearningDashboard;
-- GO

-- Create Courses table
CREATE TABLE Courses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

-- Create Enrolments table
CREATE TABLE Enrolments (
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    PRIMARY KEY (StudentId, CourseId),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id) ON DELETE CASCADE
);

-- Create indexes for better performance
CREATE INDEX IX_Enrolments_StudentId ON Enrolments(StudentId);
CREATE INDEX IX_Enrolments_CourseId ON Enrolments(CourseId);

-- Insert some sample data
INSERT INTO Courses (Name) VALUES 
    ('Introduction to Programming'),
    ('Database Design'),
    ('Web Development'),
    ('Data Structures'),
    ('Software Engineering');

-- Insert some sample enrolments
INSERT INTO Enrolments (StudentId, CourseId) VALUES 
    (1, 1),
    (1, 2),
    (2, 1),
    (2, 3),
    (3, 1),
    (3, 2),
    (3, 3),
    (4, 2),
    (5, 1),
    (5, 4);