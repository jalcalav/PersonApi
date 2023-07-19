using Moq;
using PersonApi.Domain;
using PersonApi.EventHandlers;
using PersonApi.EventHandlers.Commands;
using PersonApi.Infraestructure.Interfaces;

namespace PersonApiTests.EventHandlers;
[TestClass]
public class DeletePersonHandlerTest
{
    [TestMethod()]
    public async Task Handle_NotValid_ReturnsError()
    {
        var cancellationToken = new CancellationToken();
        var request = new DeletePersonCommand
        {
            Id = Guid.NewGuid(),
        };
        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(m => m.FindByIdAsync(request.Id)).Returns(Task.FromResult<Person?>(null));
        var handler = new DeletePersonHandler(personRepository.Object);
        var actual = await handler.Handle(request, cancellationToken);
        var expected = new List<string> {
                string.Format("Id {0} doesn't exists", request.Id),
            };
        CollectionAssert.AreEqual(expected, actual.Messages);
    }

    [TestMethod()]
    public async Task Handle_Valid_ReturnsOk()
    {
        var cancellationToken = new CancellationToken();
        Person person = new();
        var request = new DeletePersonCommand
        {
            Id = person.Id,
        };
        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(m => m.FindByIdAsync(request.Id)).Returns(Task.FromResult<Person?>(person));
        var handler = new DeletePersonHandler(personRepository.Object);
        var actual = await handler.Handle(request, cancellationToken);
        var expected = new List<string>();
        CollectionAssert.AreEqual(expected, actual.Messages);
    }
}
