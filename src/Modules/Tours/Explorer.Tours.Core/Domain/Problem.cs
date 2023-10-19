using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper.Configuration.Annotations;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class Problem:Entity
    {
        string Category {  get; init; }
        string Priority { get; init; }
        string Description { get; init; }
       DateTime Time {  get; init; }

        public Problem(string category, string priority, string description, DateTime time)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Invalid Category.");
            if (string.IsNullOrWhiteSpace(priority)) throw new ArgumentException("Invalid Priority.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            Category = category;
            Priority = priority;
            Description = description;
            Time = time;
        }
    }
}
