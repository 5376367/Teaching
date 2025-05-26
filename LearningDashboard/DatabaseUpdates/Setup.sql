-- Create the database (run this separately if needed)
-- CREATE DATABASE LearningDashboard;
-- GO
-- USE LearningDashboard;
-- GO

-- Create Students table FIRST (since it will be referenced)
CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

-- Create Courses table
CREATE TABLE Courses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

-- Create Enrolments table with foreign keys to both Students and Courses
CREATE TABLE Enrolments (
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    PRIMARY KEY (StudentId, CourseId),
    FOREIGN KEY (StudentId) REFERENCES Students(Id) ON DELETE CASCADE,
    FOREIGN KEY (CourseId) REFERENCES Courses(Id) ON DELETE CASCADE
);

-- Create indexes for better performance
CREATE INDEX IX_Enrolments_StudentId ON Enrolments(StudentId);
CREATE INDEX IX_Enrolments_CourseId ON Enrolments(CourseId);

-- Insert sample students
INSERT INTO Students (Name) VALUES 
    ('Bob'),
    ('John'),
    ('Adam'),
    ('Sarah'),
    ('Mike');

-- Insert sample courses
INSERT INTO Courses (Name) VALUES 
    ('Introduction to Programming'),
    ('Database Design'),
    ('Web Development'),
    ('Data Structures'),
    ('Software Engineering');

-- Insert sample enrolments (now with valid StudentIds that exist in Students table)
INSERT INTO Enrolments (StudentId, CourseId) VALUES 
    (1, 1),  -- Bob enrolled in Introduction to Programming
    (1, 2),  -- Bob enrolled in Database Design
    (2, 1),  -- John enrolled in Introduction to Programming
    (2, 3),  -- John enrolled in Web Development
    (3, 1),  -- Adam enrolled in Introduction to Programming
    (3, 2),  -- Adam enrolled in Database Design
    (3, 3),  -- Adam enrolled in Web Development
    (4, 2),  -- Sarah enrolled in Database Design
    (5, 1),  -- Mike enrolled in Introduction to Programming
    (5, 4);  -- Mike enrolled in Data Structures