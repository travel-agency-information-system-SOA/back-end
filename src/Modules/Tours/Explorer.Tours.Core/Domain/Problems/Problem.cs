using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper.Configuration.Annotations;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Problems;

public class Problem : Entity
{
    public string Category { get; init; }
    public string Priority { get; init; }
    public string Description { get; init; }
    public DateTime Time { get; init; }
    public int IdTourist { get; init; }
    public int IdGuide { get; init; }
    public ICollection<ProblemMessage> Messages { get; } = new List<ProblemMessage>();
    public bool IsSolved { get; init; }
    public DateTime Deadline { get; init; }
    public int IdTour { get; init; }


    public Problem(string category, string priority, string description, DateTime time, int idTourist, int idGuide)
    {
        if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(priority) || string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Field empty.");
        Category = category;
        Priority = priority;
        Description = description;
        Time = time;
        IdTourist = idTourist;
        IdGuide = idGuide;
    }
}
