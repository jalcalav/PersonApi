using Moq;
using PersonApi.Domain;
using PersonApi.EventHandlers;
using PersonApi.EventHandlers.Commands;
using PersonApi.Infraestructure.Interfaces;
using PersonApi.Models;
using System;

namespace PersonApiTests.Models;
[TestClass]
public class AppResponseTest
{
    [TestMethod()]
    public void NewInstance()
    {
        var expected = new List<string>();
        var actual = new AppResponse();
        Assert.IsTrue(actual.Ok);
        CollectionAssert.AreEqual(expected, actual.Messages);
    }

    [TestMethod()]
    public void AddError()
    {
        var expected = new List<string> { "Test" };
        var actual = new AppResponse();
        actual.AddError("Test");
        Assert.IsFalse(actual.Ok);
        CollectionAssert.AreEqual(expected, actual.Messages);
    }
}
