using LearningDashboard.Models;
using System.Collections.Generic;

namespace LearningDashboard.Interfaces
{
    public interface IEnrolmentService
    {
        IEnumerable<Enrolment> GetAll();
        IEnumerable<Enrolment> GetByStudent(int studentId);
        IEnumerable<Enrolment> GetByCourse(int courseId);
        void EnrolStudent(int studentId, int courseId);
        void RemoveEnrolment(int studentId, int courseId);
        List<EnrolmentReportItem> GetEnrolmentReport();

    }
}
