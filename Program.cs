using WtLab2.Interfaces;
using WtLab2.Models;
using WtLab2.Services;

namespace WtLab2;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== ЛР №2. Вариант 6: Студенческий портал ===");
        Console.WriteLine("Доступные операции:");
        Console.WriteLine("  1. Классы и объекты (Блок 1)");
        Console.WriteLine("  2. Сервис и перегрузка (Блок 1)");
        Console.WriteLine("  3. Коллекции и LINQ (Блок 2)");
        Console.WriteLine("  4. Асинхронная загрузка (Блок 3)");
        Console.WriteLine();

        // ================= БЛОК 1, УРОВЕНЬ 2 =================
        Console.WriteLine("--- БЛОК 1, Уровень 2 ---\n");

        var s1 = new Student(1, "Иванов Иван Иванович", "ПИ-21", 2021);
        var s2 = new Student(2, "Петрова Анна Сергеевна", "ПИ-21", 2021);
        s2.Email = "petrova@univ.ru";

        s1.UpdateAverageGrade(4.5);
        s2.UpdateAverageGrade(4.8);

        var teacher = new Teacher(10, "Сидоров Пётр Алексеевич", "Кафедра ИТ", "к.т.н.");
        teacher.Email = "sidorov@univ.ru";

        var people = new List<Person> { s1, s2, teacher };
        foreach (var p in people)
            p.PrintInfo();

        Console.WriteLine("\n--- Проверка валидации ---");
        try
        {
            s1.UpdateAverageGrade(7);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Поймано исключение: {ex.Message}");
        }

        // ================= БЛОК 1, УРОВЕНЬ 3 =================
        Console.WriteLine("\n--- БЛОК 1, Уровень 3 ---\n");

        var students = new List<Student>
        {
            new Student(1, "Иванов Иван Иванович",        "ПИ-21", 2021),
            new Student(2, "Петрова Анна Сергеевна",      "ПИ-21", 2021),
            new Student(3, "Смирнов Дмитрий Олегович",    "ПИ-22", 2022),
            new Student(4, "Кузнецова Мария Игоревна",    "ПИ-22", 2022),
            new Student(5, "Попов Артём Владимирович",    "ПИ-23", 2023),
            new Student(6, "Соколова Екатерина Павловна", "ПИ-23", 2023),
            new Student(7, "Новиков Сергей Андреевич",    "ПИ-21", 2021)
        };

        double[] avgGrades = { 4.5, 4.8, 4.2, 4.9, 3.8, 4.6, 3.9 };
        for (int i = 0; i < students.Count; i++)
            students[i].UpdateAverageGrade(avgGrades[i]);

        IItemService<Student> service = new StudentService();
        foreach (var s in students)
            service.Add(s);

        Console.WriteLine($"Всего в сервисе: {service.Count}");

        var concrete = (StudentService)service;

        Console.WriteLine("\nСтуденты группы ПИ-22:");
        foreach (var s in concrete.Find("ПИ-22"))
            Console.WriteLine($"  {s.GetShortInfo()}");

        Console.WriteLine("\nСтуденты ПИ-22 со средним баллом >= 4.5:");
        foreach (var s in concrete.Find("ПИ-22", 4.5))
            Console.WriteLine($"  {s.GetShortInfo()}");

        Console.WriteLine("\nТоп-3 по среднему баллу:");
        foreach (var s in concrete.GetTop(3))
            Console.WriteLine($"  {s.GetShortInfo()}");

        Console.WriteLine("\nПоиск Id=3:");
        var found3 = service.GetById(3);
        Console.WriteLine(found3 != null ? $"  {found3.GetShortInfo()}" : "  Не найден");

        Console.WriteLine($"\nУдаление Id=5: {(service.Remove(5) ? "успешно" : "не найдено")}");
        Console.WriteLine($"Осталось: {service.Count}");

        // ================= БЛОК 2, УРОВЕНЬ 1 =================
        Console.WriteLine("\n--- БЛОК 2, Уровень 1: List<Student> + foreach ---\n");
        foreach (var s in students)
            s.PrintInfo();

        // ================= БЛОК 2, УРОВЕНЬ 2 =================
        Console.WriteLine("\n--- БЛОК 2, Уровень 2: Dictionary + TryGetValue ---\n");
        var studentDict = students.ToDictionary(s => s.Id, s => s);

        if (studentDict.TryGetValue(3, out var foundStudent))
            Console.WriteLine($"Найден: {foundStudent.GetShortInfo()}");
        else
            Console.WriteLine("Студент с Id=3 не найден");

        if (studentDict.TryGetValue(99, out _))
            Console.WriteLine("Id=99 найден");
        else
            Console.WriteLine("Id=99 НЕ найден (обработано корректно)");

        if (studentDict.TryGetValue(6, out var s6))
            Console.WriteLine($"Id=6: {s6.GetShortInfo()}");

        // ================= БЛОК 2, УРОВЕНЬ 3 =================
        Console.WriteLine("\n--- БЛОК 2, Уровень 3: LINQ ---\n");

        Console.WriteLine("Студенты со средним баллом > 4.5:");
        students.Where(s => s.AverageGrade > 4.5)
                .ToList()
                .ForEach(s => Console.WriteLine($"  {s.GetShortInfo()}"));

        Console.WriteLine("\nСортировка по среднему баллу (убыв.):");
        students.OrderByDescending(s => s.AverageGrade)
                .ToList()
                .ForEach(s => Console.WriteLine($"  {s.GetShortInfo()}"));

        Console.WriteLine("\nПроекция (ФИО, группа, балл):");
        students.Select(s => new { s.FullName, s.Group, s.AverageGrade })
                .ToList()
                .ForEach(x => Console.WriteLine($"  {x.FullName} | {x.Group} | {x.AverageGrade:F2}"));

        Console.WriteLine("\nАгрегаты:");
        Console.WriteLine($"  Count   = {students.Count}");
        Console.WriteLine($"  Sum     = {students.Sum(s => s.AverageGrade):F2}");
        Console.WriteLine($"  Average = {students.Average(s => s.AverageGrade):F2}");
        Console.WriteLine($"  Min     = {students.Min(s => s.AverageGrade):F2}");
        Console.WriteLine($"  Max     = {students.Max(s => s.AverageGrade):F2}");

        Console.WriteLine("\nГруппировка по группам:");
        foreach (var g in students.GroupBy(s => s.Group).OrderBy(g => g.Key))
        {
            Console.WriteLine($"  Группа {g.Key}: {g.Count()} чел., средний балл {g.Average(s => s.AverageGrade):F2}");
            foreach (var s in g)
                Console.WriteLine($"      - {s.FullName} ({s.AverageGrade:F2})");
        }

        // ================= БЛОК 3, УРОВЕНЬ 1 =================
        Console.WriteLine("\n--- БЛОК 3, Уровень 1: async Task ---\n");
        await SimulateProcessingAsync();

        // ================= БЛОК 3, УРОВЕНЬ 2 =================
        Console.WriteLine("\n--- БЛОК 3, Уровень 2: async Task<List<T>> ---\n");
        var loaded = await LoadStudentsFromDbAsync();
        Console.WriteLine($"Загружено студентов: {loaded.Count}");
        foreach (var s in loaded)
            s.PrintInfo();

        // ================= БЛОК 3, УРОВЕНЬ 3 =================
        Console.WriteLine("\n--- БЛОК 3, Уровень 3: Task.WhenAll + CancellationToken ---\n");
        await DemoParallelAsync();

        Console.WriteLine("\n=== Работа завершена ===");
    }

    // -------------- БЛОК 3, УРОВЕНЬ 1 --------------
    private static async Task SimulateProcessingAsync()
    {
        Console.WriteLine("Начало обработки данных портала...");
        await Task.Delay(1000);
        Console.WriteLine("Обработка завершена.");
    }

    // -------------- БЛОК 3, УРОВЕНЬ 2 --------------
    private static async Task<List<Student>> LoadStudentsFromDbAsync()
    {
        var result = new List<Student>();
        try
        {
            Console.WriteLine("Загрузка студентов из БД...");
            await Task.Delay(1500);

            // Раскомментируй для проверки catch:
            // throw new InvalidOperationException("Ошибка подключения к БД");

            result.Add(new Student(101, "Алексеев Алексей Алексеевич", "ПИ-21", 2021));
            result.Add(new Student(102, "Борисова Ольга Дмитриевна", "ПИ-22", 2022));
            result.Add(new Student(103, "Волков Никита Сергеевич", "ПИ-23", 2023));

            foreach (var s in result)
                s.UpdateAverageGrade(4.0);

            Console.WriteLine($"Успешно загружено {result.Count} студентов.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция загрузки завершена (finally).");
        }
        return result;
    }

    // -------------- БЛОК 3, УРОВЕНЬ 3 --------------
    private static async Task DemoParallelAsync()
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var token = cts.Token;

        try
        {
            var tasks = new[]
            {
                LoadGroupAsync("ПИ-21", token),
                LoadGroupAsync("ПИ-22", token),
                LoadGroupAsync("ПИ-23", token)
            };

            var results = await Task.WhenAll(tasks);
            Console.WriteLine($"Параллельно загружено: {results.Sum(r => r.Count)} студентов");

            foreach (var list in results)
                foreach (var s in list)
                    Console.WriteLine($"  {s.GetShortInfo()}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Операция отменена по таймауту.");
        }
    }

    private static async Task<List<Student>> LoadGroupAsync(string group, CancellationToken token)
    {
        Console.WriteLine($"Загрузка группы '{group}'...");
        await Task.Delay(1200, token);

        var s = new Student(Random.Shared.Next(1000, 9999),
                            $"Студент из {group}",
                            group,
                            DateTime.Now.Year);
        s.UpdateAverageGrade(4.0);
        return new List<Student> { s };
    }
}