using PerformanceEvaluationPlatform.Domain.Examples;

namespace PerformanceEvaluationPlatform.Tests.TestSupport.EntityBuilder.Example
{
    public class ExampleStateBuilder
    {
        private int _id = 1;
        private string _name = "state";

        public ExampleStateBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ExampleStateBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ExampleState Build()
        {
            return new ExampleState
            {
                Id = _id,
                Name = _name
            };
        }
    }
}
