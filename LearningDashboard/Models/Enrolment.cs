﻿using System;
using System.Collections.Generic;

namespace LearningDashboard.Models;

public partial class Enrolment
{
    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public DateTime EnrolmentDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
