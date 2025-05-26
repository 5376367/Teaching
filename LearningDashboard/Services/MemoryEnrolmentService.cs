using LearningDashboard.Interfaces;
using LearningDashboard.Models;
using System.Collections.Generic;
using System.Linq;

namespace LearningDashboard.Services
{
    public class MemoryEnrolmentService : IEnrolmentService
    {
        private readonly List<Enrolment> _enrolments = new();
        private readonly ICourseService _courseService;

        public MemoryEnrolmentService(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IEnumerable<Enrolment> GetAll() => _enrolments;

        public IEnumerable<Enrolment> GetByStudent(int studentId) =>
            _enrolments.Where(e => e.StudentId == studentId);

        public IEnumerable<Enrolment> GetByCourse(int courseId) =>
            _enrolments.Where(e => e.CourseId == courseId);

        public void EnrolStudent(int studentId, int courseId)
        {
            if (!_enrolments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
            {
                _enrolments.Add(new Enrolment
                {
                    StudentId = studentId,
                    CourseId = courseId
                });
            }
        }

        public void RemoveEnrolment(int studentId, int courseId)
        {
            var enrolment = _enrolments
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrolment != null)
            {
                _enrolments.Remove(enrolment);
            }
        }

        public List<EnrolmentReportItem> GetEnrolmentReport()
        {
            var courseLookup = _courseService.GetAll().ToDictionary(c => c.Id, c => c.Name);

            return _enrolments
                .GroupBy(e => e.CourseId)
                .Select(g => new EnrolmentReportItem
                {
                    CourseId = g.Key,
                    CourseName = courseLookup.TryGetValue(g.Key, out var name) ? name : "Unknown",
                    StudentCount = g.Count()
                })
                .ToList();
        }

    }
}
