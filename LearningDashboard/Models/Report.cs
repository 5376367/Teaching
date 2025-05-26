namespace LearningDashboard.Models
{
    public class EnrolmentReportItem
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
    }
}
