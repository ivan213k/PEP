using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Domain.Examples
{
    public class ExampleState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Example> Examples { get; set; }

    }
}
