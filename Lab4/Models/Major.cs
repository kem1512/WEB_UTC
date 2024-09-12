﻿using System;
using System.Collections.Generic;

namespace Lab4.Models;

public partial class Major
{
    public int MajorId { get; set; }

    public string MajorName { get; set; } = null!;

    public virtual ICollection<Learner> Learners { get; set; } = new List<Learner>();
}
