using LearningDashboard.Models;
using System.Collections.Generic;

namespace LearningDashboard.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAll();
        Course Get(int id);
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
    }
}
