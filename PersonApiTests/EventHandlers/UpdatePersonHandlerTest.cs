using Moq;
using PersonApi.Domain;
using PersonApi.EventHandlers;
using PersonApi.EventHandlers.Commands;
using PersonApi.Infraestructure.Interfaces;

namespace PersonApiTests.EventHandlers;
[TestClass]
public class UpdatePersonHandlerTest
{
    [TestMethod()]
    public async Task Handle_NotValid_ReturnsError()
    {
        var cancellationToken = new CancellationToken();
        var request = new UpdatePersonCommand
        {
            Id = Guid.NewGuid(),
            Identification = "123",
            FullName = "FullName",
            BirthDate = DateTime.Today.AddDays(1),
        };
        var personRepository = new Mock<IPersonRepository>();
        Person person = new();
        personRepository.Setup(m => m.FindByIdentification(request.Identification, cancellationToken)).Returns(Task.FromResult<Person?>(person));
        var handler = new UpdatePersonHandler(personRepository.Object);
        var actual = await handler.Handle(request, cancellationToken);
        var expected = new List<string> {
                "Invalid BirthDate",
                "Identification 123 already exists",
            };
        CollectionAssert.AreEqual(expected, actual.Messages);
    }

    [TestMethod()]
    public async Task Handle_NotExists_ReturnsError()
    {
        var cancellationToken = new CancellationToken();
        var request = new UpdatePersonCommand
        {
            Identification = "123",
            FullName = "FullName",
            BirthDate = DateTime.Today,
        };
        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(m => m.FindByIdentification(request.Identification, cancellationToken)).Returns(Task.FromResult<Person?>(null));
        personRepository.Setup(m => m.FindByIdAsync(request.Id)).Returns(Task.FromResult<Person?>(null));
        var handler = new UpdatePersonHandler(personRepository.Object);
        var actual = await handler.Handle(request, cancellationToken);
        var expected = new List<string> {
                String.Format("Id {0} doesn't exists", request.Id),
            };
        CollectionAssert.AreEqual(expected, actual.Messages);
    }

    [TestMethod()]
    public async Task Handle_Valid_ReturnsOk()
    {
        var cancellationToken = new CancellationToken();
        var request = new UpdatePersonCommand
        {
            Identification = "123",
            FullName = "FullName",
            BirthDate = DateTime.Today,
        };
        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(m => m.FindByIdentification(request.Identification, cancellationToken)).Returns(Task.FromResult<Person?>(null));
        var person = new Person();
        personRepository.Setup(m => m.FindByIdAsync(request.Id)).Returns(Task.FromResult<Person?>(person));
        var handler = new UpdatePersonHandler(personRepository.Object);
        var actual = await handler.Handle(request, cancellationToken);
        var expected = new List<string>();
        CollectionAssert.AreEqual(expected, actual.Messages);
    }
}
