using Moq;
using PersonApi.Domain;
using PersonApi.EventHandlers;
using PersonApi.EventHandlers.Commands;
using PersonApi.Infraestructure.Interfaces;
using PersonApi.Models;
using System;

namespace PersonApiTests.EventHandlers;
[TestClass]
public class AppResponseTest
{
    [TestMethod()]
    public async Task Handle_Valid_ReturnsList()
    {
        var cancellationToken = new CancellationToken();
        var request = new ListPersonCommand();
        var personRepository = new Mock<IPersonRepository>();
        var expected = new List<PersonDTO> { new PersonDTO() };
        personRepository.Setup(m => m.ListAllAsync(cancellationToken)).Returns(Task.FromResult(expected));
        var handler = new ListPersonHandler(personRepository.Object);
        var actual = await handler.Handle(request, cancellationToken);
        CollectionAssert.AreEqual(expected, actual);
    }
}
