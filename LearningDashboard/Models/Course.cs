using System;
using System.Collections.Generic;

namespace LearningDashboard.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
}
