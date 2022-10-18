using Xunit;
using Moq;
using Lab2.Data;
using Lab2.Data.Enitites;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Test;

public class PlayerRepositoryTest
{
    private PlayerRepository _playerRepository;

    public PlayerRepositoryTest()
    {
        _playerRepository = new PlayerRepository(CreateDbContextMock().Object);
    }

    [Fact]
    public void Get_RequestingExistingObject_ReturnsThisObject()
    {
        // act
        var playerFromDb = _playerRepository.Get("1");

        // assert
        Assert.Equal(playerFromDb.Id, "1");
        Assert.Equal(playerFromDb.FirstName, "Robert");
        Assert.Equal(playerFromDb.LastName, "Lewandowski");
        Assert.Equal(playerFromDb.Country, "Poland");
        Assert.Equal(playerFromDb.DateOfBirth, DateTime.Parse("1980-02-02"));
        Assert.Equal(playerFromDb.Height, 180M);
        Assert.Equal(playerFromDb.Weight, 80M);
    }

    [Fact]
    public void Get_RequestingNotExistingObject_ReturnsNull()
    {
        // act
        // assert
        Assert.Null(_playerRepository.Get("3"));
    }

    [Fact]
    public void GetAllFilteredByCountry_RequestingObjectsWithExistingCountry_ReturnsListOfObjects()
    {
        // act
        // assert
        Assert.Equal(_playerRepository.GetAllFilteredByCountry("Poland").Count, 1);
    }

    [Fact]
    public void GetAllFilteredByCountry_RequestingObjectsWithNotExistingCountry_ReturnsEmptyList()
    {
        // act
        // assert
        Assert.Empty(_playerRepository.GetAllFilteredByCountry("NotExistingCountry"));
    }

    [Fact]
    public void Add_ValidCall()
    {
        var dbContextMock = CreateDbContextMock();
        var playerRepository = new PlayerRepository(dbContextMock.Object);
        playerRepository.Add(new Player());
        dbContextMock.Verify(d => d.Set<Player, string>(), Times.Once());
    }

    [Fact]
    public void Add_PassingNull_Throws()
    {
        var dbContextMock = CreateDbContextMock();
        var playerRepository = new PlayerRepository(dbContextMock.Object);
        Assert.Throws<NullReferenceException>(() => playerRepository.Add(null));
    }

    [Fact]
    public void Update_ValidCall()
    {
        var dbContextMock = CreateDbContextMock();
        var playerRepository = new PlayerRepository(dbContextMock.Object);
        playerRepository.Update(new Player());
        dbContextMock.Verify(d => d.Set<Player, string>(), Times.Once());
    }

    [Fact]
    public void Update_PassingNull_Throws()
    {
        var dbContextMock = CreateDbContextMock();
        var playerRepository = new PlayerRepository(dbContextMock.Object);
        Assert.Throws<NullReferenceException>(() => playerRepository.Add(null));
    }

    [Fact]
    public void Remove_ValidCall()
    {
        var dbContextMock = CreateDbContextMock();
        var playerRepository = new PlayerRepository(dbContextMock.Object);
        playerRepository.Remove(new Player());
        dbContextMock.Verify(d => d.Set<Player, string>(), Times.Once());
    }

    [Fact]
    public void Remove_PassingNull_Throws()
    {
        var dbContextMock = CreateDbContextMock();
        var playerRepository = new PlayerRepository(dbContextMock.Object);
        Assert.Throws<NullReferenceException>(() => playerRepository.Add(null));
    }

    private Mock<IAppDbContext> CreateDbContextMock()
    {
        var playersAsQueryable = new List<Player>
        {
            new()
            {
                Id = "1",
                FirstName = "Robert",
                LastName = "Lewandowski",
                Country = "Poland",
                DateOfBirth = DateTime.Parse("1980-02-02"),
                Height = 180M,
                Weight = 80M
            },
            new()
            {
                Id = "2",
                FirstName = "Lionel",
                LastName = "Messi",
                Country = "Argentina",
                DateOfBirth = DateTime.Parse("1990-02-02"),
                Height = 180M,
                Weight = 80M                
            }
        }.AsQueryable<Player>();

        var playerSetMock = new Mock<DbSet<Player>>();
        playerSetMock.As<IQueryable>().Setup(ps => ps.Provider).Returns(playersAsQueryable.Provider);
        playerSetMock.As<IQueryable>().Setup(ps => ps.Expression).Returns(playersAsQueryable.Expression);
        playerSetMock.As<IQueryable>().Setup(ps => ps.ElementType).Returns(playersAsQueryable.ElementType);
        playerSetMock.As<IQueryable>().Setup(ps => ps.GetEnumerator()).Returns(playersAsQueryable.GetEnumerator());

        var mock = new Mock<IAppDbContext>();

        mock.Setup(m => m.SaveChanges()).Returns(1);
        mock.Setup(m => m.Set<Player, string>()).Returns(playerSetMock.Object);
        return mock;
    } 
}