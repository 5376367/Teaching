using System.Data;
using LearningDashboard.Interfaces;
using LearningDashboard.Models;
using Microsoft.Data.SqlClient;

namespace LearningDashboard.Services
{
    public class DatabaseCourseService : ICourseService
    {
        private readonly string _connectionString;

        public DatabaseCourseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string not found");
        }

        public IEnumerable<Course> GetAll()
        {
            var courses = new List<Course>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("SELECT Id, Name FROM Courses ORDER BY Name", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                courses.Add(new Course
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name")
                });
            }

            return courses;
        }

        public Course Get(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("SELECT Id, Name FROM Courses WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Course
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name")
                };
            }

            return null;
        }

        public void Add(Course course)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(
                "INSERT INTO Courses (Name) OUTPUT INSERTED.Id VALUES (@Name)",
                connection);
            command.Parameters.AddWithValue("@Name", course.Name ?? string.Empty);

            var insertedId = command.ExecuteScalar();
            if (insertedId != null)
            {
                course.Id = Convert.ToInt32(insertedId);
            }
        }

        public void Update(Course course)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(
                "UPDATE Courses SET Name = @Name WHERE Id = @Id",
                connection);
            command.Parameters.AddWithValue("@Name", course.Name ?? string.Empty);
            command.Parameters.AddWithValue("@Id", course.Id);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // First delete related enrolments to avoid foreign key constraint issues
            var deleteEnrolmentsCommand = new SqlCommand(
                "DELETE FROM Enrolments WHERE CourseId = @CourseId",
                connection);
            deleteEnrolmentsCommand.Parameters.AddWithValue("@CourseId", id);
            deleteEnrolmentsCommand.ExecuteNonQuery();

            // Then delete the course
            var deleteCourseCommand = new SqlCommand(
                "DELETE FROM Courses WHERE Id = @Id",
                connection);
            deleteCourseCommand.Parameters.AddWithValue("@Id", id);
            deleteCourseCommand.ExecuteNonQuery();
        }
    }
}