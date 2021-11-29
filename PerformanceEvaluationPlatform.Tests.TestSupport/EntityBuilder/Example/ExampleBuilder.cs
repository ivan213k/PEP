using PerformanceEvaluationPlatform.Domain.Examples;

namespace PerformanceEvaluationPlatform.Tests.TestSupport.EntityBuilder.Example
{
    public class ExampleBuilder
    {
        private string _title = "example";
        private int _id = 1;
        private ExampleState _state = new ExampleStateBuilder().Build();
        private ExampleType _type = new ExampleTypeBuilder().Build();

        public ExampleBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public ExampleBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ExampleBuilder WithState(ExampleState state)
        {
            _state = state;
            return this;
        }

        public ExampleBuilder WithType(ExampleType type)
        {
            _type = type;
            return this;
        }


        public Domain.Examples.Example Build()
        {
            return new Domain.Examples.Example
            {
                Id = _id,
                Title = _title,
                ExampleStateId = _state.Id,
                ExampleState = _state,
                ExampleTypeId = _type.Id,
                ExampleType = _type
            };
        }
    }
}
