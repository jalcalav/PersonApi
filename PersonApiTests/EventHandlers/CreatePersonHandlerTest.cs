using Moq;
using PersonApi.EventHandlers;
using PersonApi.EventHandlers.Commands;
using PersonApi.Infraestructure.Interfaces;

namespace PersonApiTests.EventHandlers;
[TestClass]
public class CreatePersonHandlerTest
{
    [TestMethod()]
    public async Task Handle_NotValid_ReturnsError()
    {
        var cancellationToken = new CancellationToken();
        var request = new CreatePersonCommand
        {
            Identification = "123",
            FullName = "FullName",
            BirthDate = DateTime.Today.AddDays(1),
        };
        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(m => m.ExistsByIdentification(request.Identification, cancellationToken)).Returns(Task.FromResult(true));
        var handler = new CreatePersonHandler(personRepository.Object);
        var actual = await handler.Handle(request, cancellationToken);
        var expected = new List<string> {
                "Invalid BirthDate",
                "Identification 123 already exists",
            };
        CollectionAssert.AreEqual(expected, actual.Messages);
    }

    [TestMethod()]
    public async Task Handle_Valid_ReturnsOk()
    {
        var cancellationToken = new CancellationToken();
        var request = new CreatePersonCommand
        {
            Identification = "123",
            FullName = "FullName",
            BirthDate = DateTime.Today,
        };
        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(m => m.ExistsByIdentification(request.Identification, cancellationToken)).Returns(Task.FromResult(false));
        var handler = new CreatePersonHandler(personRepository.Object);
        var actual = await handler.Handle(request, cancellationToken);
        var expected = new List<string>();
        CollectionAssert.AreEqual(expected, actual.Messages);
    }
}
