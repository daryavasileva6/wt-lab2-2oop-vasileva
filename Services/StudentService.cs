using WtLab2.Interfaces;
using WtLab2.Models;

namespace WtLab2.Services;

public class StudentService : IItemService<Student>
{
    private readonly List<Student> _students = new();

    public int Count => _students.Count;

    public void Add(Student item) => _students.Add(item);

    public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public IEnumerable<Student> GetAll() => _students;

    public bool Remove(int id)
    {
        var s = GetById(id);
        return s != null && _students.Remove(s);
    }

    // ---- Перегрузка 1: по группе ----
    public IEnumerable<Student> Find(string group) =>
        _students.Where(s => s.Group.Equals(group, StringComparison.OrdinalIgnoreCase));

    // ---- Перегрузка 2: по группе + минимальному баллу ----
    public IEnumerable<Student> Find(string group, double minAverage) =>
        _students.Where(s => s.Group.Equals(group, StringComparison.OrdinalIgnoreCase)
                          && s.AverageGrade >= minAverage);

    // ---- Перегрузка 3: топ-N по баллу ----
    public IEnumerable<Student> GetTop(int n) =>
        _students.OrderByDescending(s => s.AverageGrade).Take(n);
}