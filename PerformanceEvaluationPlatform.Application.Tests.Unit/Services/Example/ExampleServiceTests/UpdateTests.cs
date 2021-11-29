using Moq;
using NUnit.Framework;
using PerformanceEvaluationPlatform.Application.Helpers;
using PerformanceEvaluationPlatform.Application.Interfaces.Examples;
using PerformanceEvaluationPlatform.Application.Model.Examples;
using PerformanceEvaluationPlatform.Application.Services.Example;
using PerformanceEvaluationPlatform.Domain.Examples;
using PerformanceEvaluationPlatform.Tests.TestSupport.EntityBuilder.Example;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Tests.Unit.Services.Example.ExampleServiceTests
{
    public class UpdateTests
    {
        private ExamplesService _sut;
        private Mock<IExamplesRepository> _mockRepository;
        private Domain.Examples.Example _exampleModel;

        [SetUp]
        public void Setup()
        {
            _exampleModel = new ExampleBuilder()
                .WithType(
                    new ExampleTypeBuilder()
                        .WithId(2)
                        .Build())
                .WithState(
                    new ExampleStateBuilder()
                        .WithId(2)
                        .Build())
                .Build();


            _mockRepository = new Mock<IExamplesRepository>();

            _mockRepository
                .Setup(t => t.Get(It.IsAny<int>()))
                .ReturnsAsync(_exampleModel);

            _mockRepository
                .Setup(t => t.GetType(It.IsAny<int>()))
                .ReturnsAsync(_exampleModel.ExampleType);

            _mockRepository
                .Setup(t => t.GetState(It.IsAny<int>()))
                .ReturnsAsync(_exampleModel.ExampleState);

            _sut = new ExamplesService(_mockRepository.Object);
        }

        [Test]
        public async Task Should_return_not_found_when_example_does_not_exists()
        {
            // Arrange
            _mockRepository
                .Setup(t => t.Get(It.Is<int>(e => e == 1)))
                .ReturnsAsync((Domain.Examples.Example) null)
                .Verifiable();
            var dto = new UpdateExampleDto();

            // Act
            var result = await _sut.Update(1, dto);

            // Assert
            _mockRepository.Verify();
            Assert.True(result.IsNotFound);
        }

        [Test]
        public async Task Should_return_failure_when_example_type_does_not_exists()
        {
            // Arrange
            _mockRepository
                .Setup(t => t.GetType(It.Is<int>(e => e == 1)))
                .ReturnsAsync((ExampleType) null)
                .Verifiable();

            var dto = new UpdateExampleDto { TypeId = 1};

            // Act
            var result = await _sut.Update(1, dto);

            // Assert
            _mockRepository.Verify();
            Assert.AreEqual(400, result.StatusCode);
            string propertyName = ExpressionHelper.GetPropertyName<UpdateExampleDto>(t => t.TypeId);
            Assert.Contains(propertyName, result.Errors.Keys);
        }

        [Test]
        public async Task Should_return_failure_when_example_state_does_not_exists()
        {
            // Arrange
            _mockRepository
                .Setup(t => t.GetState(It.Is<int>(e => e == 1)))
                .ReturnsAsync((ExampleState) null)
                .Verifiable();

            var dto = new UpdateExampleDto { TypeId = 1, StateId = 1};

            // Act
            var result = await _sut.Update(1, dto);

            // Assert
            _mockRepository.Verify();
            Assert.AreEqual(400, result.StatusCode);
            string propertyName = ExpressionHelper.GetPropertyName<UpdateExampleDto>(t => t.StateId);
            Assert.Contains(propertyName, result.Errors.Keys);
            
        }

        [Test]
        public async Task Should_update_example_model()
        {
            // Arrange
            _mockRepository
                .Setup(t => t.SaveChanges());
            var dto = new UpdateExampleDto { TypeId = 1, StateId = 1, Title = "new example"};

            // Act
            await _sut.Update(1, dto);

            // Assert
            Assert.AreEqual("new example", _exampleModel.Title);
            Assert.AreEqual(1, _exampleModel.ExampleStateId);
            Assert.AreEqual(1, _exampleModel.ExampleTypeId);
        }

        [Test]
        public async Task Should_save_changes_and_return_result()
        {
            // Arrange
            _mockRepository
                .Setup(t => t.SaveChanges())
                .Verifiable();
            var dto = new UpdateExampleDto { TypeId = 1, StateId = 1, Title = "new example" };

            // Act
            var result = await _sut.Update(1, dto);

            // Assert
            Assert.True(result.IsValid);
            Assert.AreEqual(200, result.StatusCode);

            _mockRepository.Verify(t => t.SaveChanges(), Times.Once);
        }

    }
}