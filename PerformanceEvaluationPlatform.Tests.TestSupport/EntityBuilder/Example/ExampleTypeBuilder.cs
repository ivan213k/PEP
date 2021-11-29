using PerformanceEvaluationPlatform.Domain.Examples;

namespace PerformanceEvaluationPlatform.Tests.TestSupport.EntityBuilder.Example
{
    public class ExampleTypeBuilder
    {
        private int _id = 1;
        private string _name = "state";

        public ExampleTypeBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ExampleTypeBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ExampleType Build()
        {
            return new ExampleType
            {
                Id = _id,
                Name = _name
            };
        }
    }
}
