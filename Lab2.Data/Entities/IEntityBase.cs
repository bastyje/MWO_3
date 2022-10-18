namespace Lab2.Data.Enitites;

public interface IEntityBase<TKey> where TKey : class
{
    public TKey Id { get; set; }
}