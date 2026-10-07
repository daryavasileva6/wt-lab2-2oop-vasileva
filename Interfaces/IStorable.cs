namespace WtLab2.Interfaces;

public interface IStorable
{
    int Id { get; }
    DateTime CreatedAt { get; }
    string GetShortInfo();
}