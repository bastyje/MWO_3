using Xunit;
using Moq;
using Lab2.API.Controllers;
using Lab2.Data;
using Lab2.Data.Enitites;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Test;

public class PlayerControllerTest
{
    [Fact]
    public void GetAll_ExistingCountry_ReturnsNotEmptyList()
    {
        var playerController = new PlayerController(CreatePlayerRepositoryMock().Object);
        var result = playerController.GetAll("Poland");
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<Player>>(((OkObjectResult) result.Result).Value);
        Assert.Equal(((List<Player>) ((OkObjectResult) result.Result).Value).Count, 2);
    }

    [Fact]
    public void GetAll_NotExistingCountry_ReturnsNull()
    {
        var playerController = new PlayerController(CreatePlayerRepositoryMock().Object);
        var result = playerController.GetAll("NotExistingCountry");
        Assert.IsType<NotFoundResult>(result.Result);
    }


    [Fact]
    public void Get_ExistingPlayer_ReturnsPlayer()
    {
        var playerController = new PlayerController(CreatePlayerRepositoryMock().Object);
        var result = playerController.Get("1");
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<Player>(((OkObjectResult) result.Result).Value);
    }

    [Fact]
    public void Get_NotExistingPlayer_ReturnsNotFound()
    {
        var playerController = new PlayerController(CreatePlayerRepositoryMock().Object);
        var result = playerController.Get("2");
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void Post_ValidCall()
    {
        var playerController = new PlayerController(CreatePlayerRepositoryMock().Object);
        var result = playerController.Post(new Player());
        Assert.IsType<OkResult>(result);
    }


    [Fact]
    public void Put_ValidCall()
    {
        var playerController = new PlayerController(CreatePlayerRepositoryMock().Object);
        var result = playerController.Put(new Player());
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void Delete_ValidCall()
    {
        var playerController = new PlayerController(CreatePlayerRepositoryMock().Object);
        var result = playerController.Delete("1");
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void Delete_NotExisting()
    {
        var playerController = new PlayerController(CreatePlayerRepositoryMock().Object);
        var result = playerController.Delete("2");
        Assert.IsType<NotFoundResult>(result);
    }

    private Mock<IPlayerRepository> CreatePlayerRepositoryMock()
    {
        var mock = new Mock<IPlayerRepository>();
        mock.Setup(m => m.Add(new Player()));
        mock.Setup(m => m.Update(new Player()));
        mock.Setup(m => m.Remove(new Player()));
        mock.Setup(m => m.Get("1")).Returns(new Player());
        mock.Setup(m => m.Get("2")).Returns((Player) null);
        mock.Setup(m => m.GetAllFilteredByCountry("Poland")).Returns(new List<Player>
        {
            new Player {Country = "Poland"},
            new Player {Country = "Poland"}
        });
        mock.Setup(m => m.GetAllFilteredByCountry("NotExtistingCountry")).Returns(new List<Player>());
        return mock;
    }
}