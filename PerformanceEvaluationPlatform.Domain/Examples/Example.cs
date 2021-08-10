using System;
using System.Data;

namespace PerformanceEvaluationPlatform.Domain.Examples
{
    public class Example
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ExampleTypeId { get; set; }
        public int ExampleStateId { get; set; }

        public ExampleType ExampleType { get; set; }
        public ExampleState ExampleState { get; set; }
    }
}
