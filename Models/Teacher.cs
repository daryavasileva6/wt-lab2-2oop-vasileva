namespace WtLab2.Models;

public class Teacher : Person
{
    public string Department { get; set; }
    public string AcademicDegree { get; set; }

    public Teacher(int id, string fullName, string department, string degree)
        : base(id, fullName)
    {
        Department = department;
        AcademicDegree = degree;
    }

    public override string GetShortInfo() => $"{FullName} — {Department}, {AcademicDegree}";

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"  Кафедра: {Department}, Степень: {AcademicDegree}");
    }
}