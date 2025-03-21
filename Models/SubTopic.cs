using System;
using System.Collections.Generic;

namespace SubjectApp.Models;

public partial class SubTopic
{
    public int SubTopicId { get; set; }

    public int? SubjectId { get; set; }

    public string SubTopicName { get; set; } = null!;

    public virtual Subject? Subject { get; set; }
}
