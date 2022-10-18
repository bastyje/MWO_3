using Lab2.Data.Enitites;
using System.Linq;
using System;

namespace Lab2.Data;

public class PlayerRepository : IPlayerRepository
{
    private readonly IAppDbContext _appDbContext;

    public PlayerRepository(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Player Get(string id)
    {
        return _appDbContext.Set<Player, string>().FirstOrDefault(p => p.Id == id);
    }

    public List<Player> GetAllFilteredByCountry(string countryName)
    {
        return _appDbContext.Set<Player, string>().Where(p => p.Country == countryName).ToList();
    }

    public void Add(Player player)
    {
        if (player is null) throw new NullReferenceException();
        _appDbContext.Set<Player, string>().Add(player);
        _appDbContext.SaveChanges();
    }

    public void Update(Player player)
    {
        if (player is null) throw new NullReferenceException();
        _appDbContext.Set<Player, string>().Update(player);
        _appDbContext.SaveChanges();
    }

    public void Remove(Player player)
    {
        if (player is null) throw new NullReferenceException();
        _appDbContext.Set<Player, string>().Remove(player);
        _appDbContext.SaveChanges();
    }
}