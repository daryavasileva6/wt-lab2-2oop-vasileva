using WtLab2.Models;

namespace WtLab2.Interfaces;

public interface IItemService<T> where T : BaseEntity
{
    void Add(T item);
    T? GetById(int id);
    IEnumerable<T> GetAll();
    bool Remove(int id);
    int Count { get; }
}