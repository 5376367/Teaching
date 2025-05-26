using System.Data;
using LearningDashboard.Interfaces;
using LearningDashboard.Models;
using Microsoft.Data.SqlClient;

namespace LearningDashboard.Services
{
    public class DatabaseEnrolmentService : IEnrolmentService
    {
        private readonly string _connectionString;
        private readonly ICourseService _courseService;

        public DatabaseEnrolmentService(IConfiguration configuration, ICourseService courseService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string not found");
            _courseService = courseService;
        }

        public IEnumerable<Enrolment> GetAll()
        {
            var enrolments = new List<Enrolment>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("SELECT StudentId, CourseId FROM Enrolments ORDER BY StudentId, CourseId", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                enrolments.Add(new Enrolment
                {
                    StudentId = reader.GetInt32("StudentId"),
                    CourseId = reader.GetInt32("CourseId")
                });
            }

            return enrolments;
        }

        public IEnumerable<Enrolment> GetByStudent(int studentId)
        {
            var enrolments = new List<Enrolment>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(
                "SELECT StudentId, CourseId FROM Enrolments WHERE StudentId = @StudentId ORDER BY CourseId",
                connection);
            command.Parameters.AddWithValue("@StudentId", studentId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                enrolments.Add(new Enrolment
                {
                    StudentId = reader.GetInt32("StudentId"),
                    CourseId = reader.GetInt32("CourseId")
                });
            }

            return enrolments;
        }

        public IEnumerable<Enrolment> GetByCourse(int courseId)
        {
            var enrolments = new List<Enrolment>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(
                "SELECT StudentId, CourseId FROM Enrolments WHERE CourseId = @CourseId ORDER BY StudentId",
                connection);
            command.Parameters.AddWithValue("@CourseId", courseId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                enrolments.Add(new Enrolment
                {
                    StudentId = reader.GetInt32("StudentId"),
                    CourseId = reader.GetInt32("CourseId")
                });
            }

            return enrolments;
        }

        public void EnrolStudent(int studentId, int courseId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // Check if enrolment already exists
            var checkCommand = new SqlCommand(
                "SELECT COUNT(*) FROM Enrolments WHERE StudentId = @StudentId AND CourseId = @CourseId",
                connection);
            checkCommand.Parameters.AddWithValue("@StudentId", studentId);
            checkCommand.Parameters.AddWithValue("@CourseId", courseId);

            var existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount == 0)
            {
                var insertCommand = new SqlCommand(
                    "INSERT INTO Enrolments (StudentId, CourseId) VALUES (@StudentId, @CourseId)",
                    connection);
                insertCommand.Parameters.AddWithValue("@StudentId", studentId);
                insertCommand.Parameters.AddWithValue("@CourseId", courseId);

                insertCommand.ExecuteNonQuery();
            }
        }

        public void RemoveEnrolment(int studentId, int courseId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(
                "DELETE FROM Enrolments WHERE StudentId = @StudentId AND CourseId = @CourseId",
                connection);
            command.Parameters.AddWithValue("@StudentId", studentId);
            command.Parameters.AddWithValue("@CourseId", courseId);

            command.ExecuteNonQuery();
        }

        public List<EnrolmentReportItem> GetEnrolmentReport()
        {
            var reportItems = new List<EnrolmentReportItem>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(@"
                SELECT 
                    c.Id as CourseId,
                    c.Name as CourseName,
                    COUNT(e.StudentId) as StudentCount
                FROM Courses c
                LEFT JOIN Enrolments e ON c.Id = e.CourseId
                GROUP BY c.Id, c.Name
                ORDER BY c.Name", connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                reportItems.Add(new EnrolmentReportItem
                {
                    CourseId = reader.GetInt32("CourseId"),
                    CourseName = reader.GetString("CourseName"),
                    StudentCount = reader.GetInt32("StudentCount")
                });
            }

            return reportItems;
        }
    }
}