using LearningDashboard.Interfaces;
using LearningDashboard.Models;
using System.Collections.Generic;
using System.Linq;

namespace LearningDashboard.Services
{
    public class MemoryCourseService : ICourseService
    {
        private readonly List<Course> _courses = new();
        private int _nextId = 1;

        public IEnumerable<Course> GetAll() => _courses.OrderBy(x=>x.Name);

        public Course Get(int id) => _courses.FirstOrDefault(c => c.Id == id);

        public void Add(Course course)
        {
            course.Id = _nextId++;
            _courses.Add(course);
        }

        public void Update(Course course)
        {
            var existing = Get(course.Id);
            if (existing != null)
            {
                existing.Name = course.Name;
            }
        }

        public void Delete(int id)
        {
            var course = Get(id);
            if (course != null)
            {
                _courses.Remove(course);
            }
        }
    }
}
