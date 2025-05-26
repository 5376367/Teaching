using LearningDashboard.Interfaces;
using LearningDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningDashboard.Services
{
    public class EntityFrameworkCourseService : ICourseService
    {
        private readonly LearningDashboardContext _context;

        public EntityFrameworkCourseService(LearningDashboardContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.OrderBy(c => c.Name).ToList();
        }

        public Course Get(int id)
        {
            return _context.Courses.Find(id);
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                // Remove related enrolments first
                var enrolments = _context.Enrolments.Where(e => e.CourseId == id);
                _context.Enrolments.RemoveRange(enrolments);

                // Then remove the course
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }
    }
}