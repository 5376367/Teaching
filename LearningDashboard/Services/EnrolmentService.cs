using LearningDashboard.Interfaces;
using LearningDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningDashboard.Services
{
    public class EntityFrameworkEnrolmentService : IEnrolmentService
    {
        private readonly LearningDashboardContext _context;

        public EntityFrameworkEnrolmentService(LearningDashboardContext context)
        {
            _context = context;
        }

        public IEnumerable<Enrolment> GetAll()
        {
            return _context.Enrolments
                .OrderBy(e => e.StudentId)
                .ThenBy(e => e.CourseId)
                .ToList();
        }

        public IEnumerable<Enrolment> GetByStudent(int studentId)
        {
            return _context.Enrolments
                .Where(e => e.StudentId == studentId)
                .OrderBy(e => e.CourseId)
                .ToList();
        }

        public IEnumerable<Enrolment> GetByCourse(int courseId)
        {
            return _context.Enrolments
                .Where(e => e.CourseId == courseId)
                .OrderBy(e => e.StudentId)
                .ToList();
        }

        public void EnrolStudent(int studentId, int courseId)
        {
            // Check if enrolment already exists
            var existingEnrolment = _context.Enrolments
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);

            if (existingEnrolment == null)
            {
                var enrolment = new Enrolment
                {
                    StudentId = studentId,
                    CourseId = courseId
                };

                _context.Enrolments.Add(enrolment);
                _context.SaveChanges();
            }
        }

        public void RemoveEnrolment(int studentId, int courseId)
        {
            var enrolment = _context.Enrolments
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrolment != null)
            {
                _context.Enrolments.Remove(enrolment);
                _context.SaveChanges();
            }
        }

        public List<EnrolmentReportItem> GetEnrolmentReport()
        {
            return _context.Courses
                .GroupJoin(
                    _context.Enrolments,
                    course => course.Id,
                    enrolment => enrolment.CourseId,
                    (course, enrolments) => new EnrolmentReportItem
                    {
                        CourseId = course.Id,
                        CourseName = course.Name,
                        StudentCount = enrolments.Count()
                    })
                .OrderBy(r => r.CourseName)
                .ToList();
        }
    }
}