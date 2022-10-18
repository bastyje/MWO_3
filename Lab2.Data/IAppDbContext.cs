using Microsoft.EntityFrameworkCore;
using Lab2.Data.Enitites;
using System.Linq;

namespace Lab2.Data;

public interface IAppDbContext
{
    DbSet<TEntity> Set<TEntity, TKey>() where TEntity : class, IEntityBase<TKey> where TKey : class;
    int SaveChanges();
}