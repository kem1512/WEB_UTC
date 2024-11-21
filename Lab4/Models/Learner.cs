using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lab4.Models;

public partial class Learner
{
    public int LearnerId { get; set; }

    [DisplayName("Tên")]
    public string LastName { get; set; } = null!;

    [DisplayName("Họ")]
    public string FirstMidName { get; set; } = null!;

    [DisplayName("Ngày Nhập Học")]
    public DateTime EnrollmentDate { get; set; }

    [DisplayName("Ngành Học")]
    public int MajorId { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Major? Major { get; set; } = null!;
}
