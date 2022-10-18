using Lab2.Data.Enitites;
using System.Collections.Generic;
using System;

namespace Lab2.Data;

public interface IPlayerRepository
{
    Player Get(string id);
    List<Player> GetAllFilteredByCountry(string countryName);
    void Add(Player player);
    void Update(Player player);
    void Remove(Player player);
}