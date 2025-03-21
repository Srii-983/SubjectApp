using System;
using System.Collections.Generic;

namespace SubjectApp.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public virtual ICollection<SubTopic> SubTopics { get; set; } = new List<SubTopic>();
}
