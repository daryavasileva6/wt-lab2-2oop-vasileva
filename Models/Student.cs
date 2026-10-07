namespace WtLab2.Models;

public class Student : Person
{
    private double _averageGrade;

    public string Group { get; set; }
    public int EnrollmentYear { get; set; }

    public double AverageGrade
    {
        get => _averageGrade;
        private set => _averageGrade = value;
    }

    public Student(int id, string fullName, string group, int year)
        : base(id, fullName)
    {
        Group = group;
        EnrollmentYear = year;
        _averageGrade = 0;
    }

    public void UpdateAverageGrade(double newGrade)
    {
        if (newGrade < 0 || newGrade > 5)
            throw new ArgumentException("Средний балл должен быть в диапазоне 0..5");

        LogChange($"Средний балл {FullName}: {_averageGrade:F2} → {newGrade:F2}");
        _averageGrade = newGrade;
    }

    public override string GetShortInfo() => $"{FullName} ({Group}) — ср. балл {AverageGrade:F2}";

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"  Группа: {Group}, Год поступления: {EnrollmentYear}, Средний балл: {AverageGrade:F2}");
    }
}